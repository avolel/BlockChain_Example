using BlockChain_Example.Models;
using BlockChain_Example.Networking;
using BlockChain_Example.Models.Security;

Console.Write("Enter port for this node: ");
int port = int.Parse(Console.ReadLine());

BlockChain chain = new BlockChain();
P2PNode node = new P2PNode(port, chain);
node.Start();

Console.Write("Connect to peer? (y/n): ");
if (Console.ReadLine()?.ToLower() == "y")
{
    Console.Write("Enter peer IP: ");
    string ip = Console.ReadLine();
    Console.Write("Enter peer port: ");
    int peerPort = int.Parse(Console.ReadLine());

    node.ConnectToPeer(ip, peerPort);
}

CryptoWallet wallet = new CryptoWallet();
Console.WriteLine($"Your public key: {wallet.PublicKey.Substring(0, 20)}...");

while (true)
{
    Console.WriteLine("\n1. Send Transaction");
    Console.WriteLine("2. Mine Block");
    Console.WriteLine("3. View Blockchain");
    Console.WriteLine("4. Exit");
    var choice = Console.ReadLine();

    switch (choice)
    {
        case "1": // Send a transaction
            Console.Write("Receiver: ");
            string receiver = Console.ReadLine();
            Console.Write("Amount: ");
            decimal amt = decimal.Parse(Console.ReadLine());

            string raw = $"{wallet.PublicKey}{receiver}{amt}";
            string sig = wallet.SignData(raw);
            var tx = new SignedTransaction(wallet.PublicKey, receiver, amt, sig);

            if (TransactionVerifier.IsValidTransaction(tx))
            {
                chain.AddTransaction(tx);
                node.Broadcast(tx);
                Console.WriteLine("Transaction sent and broadcasted.");
            }
            else
            {
                Console.WriteLine("Invalid transaction.");
            }
            break;

        case "2": // Mine a block
            chain.MinePendingTransactions(wallet.PublicKey);
            node.Broadcast(chain.GetLatestBlock());
            Console.WriteLine("Block mined and broadcasted.");
            break;

        case "3": // View the blockchain
            foreach (var block in chain.Chain)
            {
                Console.WriteLine($"\nBlock {block.Index}: {block.Hash}");
                foreach (var t in block.Transactions)
                    Console.WriteLine($"  {t}");
            }
            break;

        case "4": // Exit the application
            Environment.Exit(0);
            break;
    }
}