using BlockChain_Example.Models;
using BlockChain_Example.Models.SmartContracts;

// Create a new blockchain
BlockChain chain = new BlockChain();
chain.Difficulty = 2; // Adjust difficulty as needed

// Add some initial transactions
chain.AddTransaction(new Transaction("Buju", "Bounty", 10));
chain.AddTransaction(new Transaction("Lyn", "Andy", 5));
chain.AddTransaction(new Transaction("Shabba", "Sean", 2));

// Mine the first block (initial mining)
Console.WriteLine("Mining initial block...");
chain.MinePendingTransactions("Miner1");

// Add more transactions
chain.AddTransaction(new Transaction("Bob", "Grace", 3));
chain.AddTransaction(new Transaction("Machel", "Ivy", 7));

// Mine the second block
Console.WriteLine("\nMining second block...");
chain.MinePendingTransactions("Miner2");

// Execute a simple smart contract (airdrop)
AirDropContract airDrop = new AirDropContract("Doug", 15);
Console.WriteLine("\nExecuting Airdrop Contract...");
airDrop.Execute(chain);

// Mine the third block (including the airdrop transaction)
Console.WriteLine("\nMining third block...");
chain.MinePendingTransactions("Miner3");

// Validate the chain
Console.WriteLine("\nValidating Blockchain...");
if (chain.IsChainValid())
{
    Console.WriteLine("Blockchain is valid.");
}
else
{
    Console.WriteLine("Blockchain is NOT valid!");
}

// Print the blockchain contents for verification
Console.WriteLine("\nBlockchain Contents:");
foreach (var block in chain.Chain)
{
    Console.WriteLine($"Block {block.Index}:");
    Console.WriteLine($"  Previous Hash: {block.PreviousHash}");
    Console.WriteLine($"  Timestamp: {block.TimeStamp}");
    Console.WriteLine($"  Merkle Root: {block.MerkleRoot}");
    Console.WriteLine("  Transactions:");
    foreach (var transaction in block.Transactions)
    {
        Console.WriteLine($"    {transaction}");
    }
    Console.WriteLine($"  Hash: {block.Hash}");
    Console.WriteLine($"  Nonce: {block.Nonce}");
    Console.WriteLine();
}

Console.ReadKey(); // Keep the console window open until a key is pressed