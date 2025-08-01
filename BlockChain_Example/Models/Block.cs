using System.Security.Cryptography;
using System.Text;

namespace BlockChain_Example.Models
{
    public class Block
    {
        public List<Transaction> Transactions { get; set; }
        public string MerkleRoot { get; set; }
        public int Index { get; set; }
        public DateTime TimeStamp { get; set; }
        public string PreviousHash { get; set; }
        public string Hash { get; set; }
        public int Nonce { get; set; }

        public Block(int index, DateTime timestamp, List<Transaction> transactions, 
            string previousHash = "")
        {
            Index = index;
            TimeStamp = timestamp;
            Transactions = transactions;
            PreviousHash = previousHash;
            MerkleRoot = MerkleTree.ComputeMerkleRoot(transactions);
            Nonce = 0;
            Hash = CalculateHash();            
        }

        public string CalculateHash()
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                string rawData = $"{Index}{TimeStamp.ToString()}{MerkleRoot}{PreviousHash}{Nonce}";
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                return BitConverter.ToString(bytes, 0, bytes.Length).Replace("-", "").ToLower();
            }
        }

        public void MineBlock(int difficulty)
        {
            string target = new string('0', difficulty);
            while(Hash.Substring(0,difficulty) != target)
            {
                Nonce++;
                Hash = CalculateHash();
            }
            Console.WriteLine($"Block mined: {Hash}");
        }
    }
}