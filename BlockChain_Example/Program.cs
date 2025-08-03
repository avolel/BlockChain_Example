using BlockChain_Example.Models;
using BlockChain_Example.Models.SmartContracts;

Dictionary<string, Wallet> wallets = new();
BlockChain chain = new();
chain.Difficulty = 2;

bool running = true;

while(running)
{
    Console.WriteLine("\n=== Blockchain CLI Menu ===");
    Console.WriteLine("1. Create Wallet");
    Console.WriteLine("2. View Wallet Balance");
    Console.WriteLine("3. Create Transaction");
    Console.WriteLine("4. Mine Block");
    Console.WriteLine("5. View Chain");
    Console.WriteLine("6. Air Drop");
    Console.WriteLine("7. Exit");
    Console.Write("Select an option: ");
    string choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            Console.Write("Enter wallet name: ");
            string walletName = Console.ReadLine();
            if (!wallets.ContainsKey(walletName))
            {
                wallets[walletName] = new Wallet(walletName);
                Console.WriteLine($"Wallet '{walletName}' created.");
            }
            else
                Console.WriteLine($"Wallet '{walletName}' already exists.");
            break;
        case "2":
            Console.Write("Enter wallet name: ");
            var wallet = Console.ReadLine();
            if (wallets.ContainsKey(wallet))
            {
                var balance = chain.GetBalance(wallet);
                Console.WriteLine($"Balance of {wallet}: {balance}");
            }
            else Console.WriteLine("Wallet not found.");
            break;
        case "3":
            Console.Write("Sender: ");
            var sender = Console.ReadLine();
            Console.Write("Receiver: ");
            var receiver = Console.ReadLine();
            Console.Write("Amount: ");
            if (!decimal.TryParse(Console.ReadLine(), out var amount)) { Console.WriteLine("Invalid amount."); break; }

            if (!wallets.ContainsKey(sender)) { Console.WriteLine("Sender wallet not found."); break; }
            if (chain.GetBalance(sender) < amount) { Console.WriteLine("Insufficient balance."); break; }

            var tx = wallets[sender].CreateTransaction(receiver, amount);
            chain.AddTransaction(tx);
            Console.WriteLine("Transaction added.");
            break;
        case "4":
            Console.Write("Enter miner address: ");
            var miner = Console.ReadLine();
            chain.MinePendingTransactions(miner);
            Console.WriteLine("Block mined!");
            break;
        case "5":
            Console.WriteLine("\n=== Blockchain Contents ===");
            foreach (var block in chain.Chain)
            {
                Console.WriteLine($"\nBlock {block.Index}");
                Console.WriteLine($"  Hash: {block.Hash}");
                Console.WriteLine($"  Prev: {block.PreviousHash}");
                Console.WriteLine($"  Timestamp: {block.TimeStamp}");
                foreach (var tx2 in block.Transactions)
                    Console.WriteLine($"    {tx2}");
            }
            break;
        case "6":
            Console.Write("Airdrop receiver: ");
            var dropTarget = Console.ReadLine();
            Console.Write("Airdrop amount: ");
            if (!decimal.TryParse(Console.ReadLine(), out var dropAmount)) { Console.WriteLine("Invalid amount."); break; }

            var contract = new AirDropContract(dropTarget, dropAmount);
            contract.Execute(chain);
            Console.WriteLine("Airdrop added. Mine the block to finalize.");
            break;
        case "7":
            running = false;
            break;
        default:
            Console.WriteLine("Invalid choice, please try again.");
            break;
    }
}