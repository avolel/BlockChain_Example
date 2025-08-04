using System.Security.Cryptography;
using System.Text;

namespace BlockChain_Example.Models
{
    public static class MerkleTree
    {
        public static string ComputeMerkleRoot(List<SignedTransaction> transactions)
        {
            List<string> hashes = transactions.Select(tx => Hash(tx.ToString())).ToList();
            while (hashes.Count > 1) {
                List<string> new_hashes = new();

                for (int i = 0; i < hashes.Count; i += 2) {
                    if (i + 1 < hashes.Count) new_hashes.Add(Hash(hashes[i] + hashes[i + 1]));
                    else new_hashes.Add(Hash(hashes[i] + hashes[i])); // duplicate last if odd
                }

                hashes = new_hashes;
            }

            return hashes.FirstOrDefault();
        }

        private static string Hash(string input)
        {
            using var sha = SHA256.Create();
            return BitConverter.ToString(sha.ComputeHash(Encoding.UTF8.GetBytes(input))).Replace("-","").ToLower();
        }
    }
}