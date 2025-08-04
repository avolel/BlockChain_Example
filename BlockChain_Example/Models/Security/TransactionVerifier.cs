namespace BlockChain_Example.Models.Security
{
    public class TransactionVerifier
    {
        public static bool IsValidTransaction(SignedTransaction transaction)
        {
            string data = $"{transaction.SenderPublicKey}{transaction.Receiver}{transaction.Amount}";
            return  CryptoWallet.VerifySignature(transaction.SenderPublicKey, data, transaction.Signature);
        }
    }
}