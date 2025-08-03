using System.Collections.Generic;

namespace BlockChain_Example.Models
{
    public class BlockChain
    {
        public List<Block> Chain { get; set; } = new();
        public int Difficulty { get; set; } = 2;
        public List<Transaction> pending_transactions { get; set; } = new();

        public Block GetLatestBlock() =>
            Chain[Chain.Count - 1];

        public void AddBlock(Block new_block)
        {
            new_block.PreviousHash = GetLatestBlock().Hash;
            new_block.Hash = new_block.CalculateHash();
            Chain.Add(new_block);
        }

        public void AddTransaction(Transaction transaction) =>
            pending_transactions.Add(transaction);

        public void MinePendingTransactions(string minerAddress)
        {
            var reward = new Transaction("System", minerAddress, 1);
            pending_transactions.Add(reward);
            var block = new Block(Chain.Count, DateTime.UtcNow, new List<Transaction>(pending_transactions), (Chain.Count != 0) ? GetLatestBlock().Hash : "");
            block.MineBlock(Difficulty);
            Chain.Add(block);
            pending_transactions.Clear();
        }

        public bool IsChainValid()
        {
            for (int i = 1; i < Chain.Count; i++)
            {
                Block current = Chain[i];
                Block previous = Chain[i - 1];
                if(current.Hash != current.CalculateHash()) return false;
                if(current.PreviousHash != previous.Hash) return false;
            }
            return true;
        }

        public decimal GetBalance(string address) { 
            decimal balance = 0;
            foreach (var block in Chain)
            {
                foreach (var transaction in block.Transactions)
                {
                    if (transaction.Sender == address) balance -= transaction.Amount;
                    if (transaction.Receiver == address) balance += transaction.Amount;
                }
            }
            return balance;
        }
    }
}