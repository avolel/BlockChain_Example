using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using BlockChain_Example.Models;
using BlockChain_Example.Models.Security;

namespace BlockChain_Example.Networking
{
    /// <summary>
    /// This class represents a peer-to-peer (P2P) node in the blockchain network.
    /// It is responsible for handling network communication, including sending and receiving messages.
    /// </summary>
    public class P2PNode
    {
        private TcpListener _listener;
        public List<TcpClient> _peers = new();
        public BlockChain _blockchain;

        public int Port { get; private set; }

        public P2PNode(int port, BlockChain chain)
        {
            Port = port;
            _blockchain = chain;
            _listener = new TcpListener(IPAddress.Loopback, port);
        }

        public void Start() {
            _listener.Start();
            Console.WriteLine($"P2P Node started on port {Port}");
            Task.Run(async () => {
                while (true)
                {
                    var client = await _listener.AcceptTcpClientAsync();
                    _peers.Add(client);
                    Console.WriteLine($"New peer connected: {client.Client.RemoteEndPoint}");
                    HandleClient(client);
                }
            });
        }

        public void ConnectToPeer(string ip, int port) {
            var client = new TcpClient();
            client.Connect(IPAddress.Parse(ip), port);
            _peers.Add(client);
            HandleClient(client);
        }

        public void Broadcast(object data)
        {
            string json = JsonSerializer.Serialize(data);
            byte[] bytes = Encoding.UTF8.GetBytes(json + "\n");
            foreach (var peer in _peers)
            {
                try
                {
                    NetworkStream stream = peer.GetStream();
                    stream.Write(bytes, 0, bytes.Length);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error sending data to peer {peer.Client.RemoteEndPoint}: {ex.Message}");
                }
            }
        }

        private void HandleClient(TcpClient client)
        {
            Task.Run(async () => {
                using var stream = client.GetStream();
                using var reader = new StreamReader(stream);

                while (!reader.EndOfStream)
                {
                    string? line = await reader.ReadLineAsync();
                    if (string.IsNullOrWhiteSpace(line)) continue;

                    try
                    {
                        //Trying to parse as a transaction
                        var transaction = JsonSerializer.Deserialize<SignedTransaction>(line);
                        if (TransactionVerifier.IsValidTransaction(transaction))
                        { 
                            _blockchain.AddTransaction(transaction);
                            Console.WriteLine($"Transaction received.");
                        }
                    }
                    catch
                    {
                      
                    }
                }
            });
        }
    }
}