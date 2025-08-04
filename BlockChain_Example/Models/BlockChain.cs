using BlockChain_Example.Models.Security;

namespace BlockChain_Example.Models
{
    public class BlockChain
    {
        public List<Block> Chain { get; set; } = new();
        public int Difficulty { get; set; } = 2;
        public List<SignedTransaction> pending_transactions { get; set; } = new();
        public decimal TransactionFee { get; set; } = 0.01m;

        public BlockChain()
        {
           Chain.Add(CreateGenesisBlock());
        }

        private Block CreateGenesisBlock() {
            return new Block(0, DateTime.UtcNow, new List<SignedTransaction>(), "0");
        }

        public Block GetLatestBlock() =>
            Chain.Last();

        public void AddBlock(Block new_block)
        {
            if (new_block.PreviousHash != GetLatestBlock().Hash)
                throw new InvalidOperationException("Invalid previous hash.");
            if (new_block.Hash != new_block.CalculateHash())
                throw new InvalidOperationException("Invalid block hash.");
            Chain.Add(new_block);
        }

        public void AddTransaction(SignedTransaction transaction)
        {
            if(!TransactionVerifier.IsValidTransaction(transaction))
                throw new InvalidOperationException("Invalid transaction signature.");
            pending_transactions.Add(transaction);
        }

        public void MinePendingTransactions(string minerPublicKey)
        {
            decimal totalFees = pending_transactions.Count * TransactionFee;
            var rewardTransaction = new SignedTransaction("System", minerPublicKey, 1 + totalFees, ""); //Transactions from the system do not require a signature
            pending_transactions.Add(rewardTransaction);
            Block block = new Block(Chain.Count, DateTime.UtcNow, new List<SignedTransaction>(pending_transactions), GetLatestBlock().Hash);
            block.MineBlock(Difficulty);
            Chain.Add(block);
            pending_transactions.Clear();
        }

        public bool IsChainValid()
        {
            for (int i = 1; i < Chain.Count; i++) {
                var current_block = Chain[i];
                var previous_block = Chain[i - 1];
                if(current_block.Hash != current_block.CalculateHash())
                    return false;
                if(current_block.PreviousHash != previous_block.Hash)
                    return false;

                foreach (var transaction in current_block.Transactions) { 
                    if(transaction.SenderPublicKey != "System" && !TransactionVerifier.IsValidTransaction(transaction))
                        return false;
                }
            }
            return true;
        }

        public decimal GetBalance(string address) { 
            decimal balance = 0;
            foreach (var block in Chain)
            {
                foreach (var transaction in block.Transactions)
                {
                    if (transaction.SenderPublicKey == address) balance -= transaction.Amount;
                    if (transaction.Receiver == address) balance += transaction.Amount;
                }
            }
            return balance;
        }
    }
}