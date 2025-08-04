using System.Security.Cryptography;
using System.Text;

namespace BlockChain_Example.Models.Security
{
    public class CryptoWallet
    {
        public string PublicKey { get; private set; }
        private ECDsa _ecdsa;

        public CryptoWallet()
        {
            _ecdsa = ECDsa.Create(ECCurve.NamedCurves.nistP256);
            PublicKey = Convert.ToBase64String(_ecdsa.ExportSubjectPublicKeyInfo());
        }

        public string SignData(string data)
        {
            var hash = SHA256.HashData(Encoding.UTF8.GetBytes(data));
            var signature = _ecdsa.SignHash(hash);
            return Convert.ToBase64String(signature);
        }

        public static bool VerifySignature(string publicKey, string data, string signature)
        {
            var ecdsa = ECDsa.Create();
            ecdsa.ImportSubjectPublicKeyInfo(Convert.FromBase64String(publicKey), out _);
            var hash = SHA256.HashData(Encoding.UTF8.GetBytes(data));
            return ecdsa.VerifyHash(hash, Convert.FromBase64String(signature));
        }
    }
}