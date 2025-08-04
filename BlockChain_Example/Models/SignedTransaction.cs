namespace BlockChain_Example.Models
{
    public class SignedTransaction
    {
        public string SenderPublicKey { get; set; }
        public string Receiver { get; set; }
        public decimal Amount { get; set; }
        public string Signature { get; set; }

        public SignedTransaction(string senderkey, string receiver, decimal amount, string signature)
        {
            SenderPublicKey = senderkey;
            Receiver = receiver;
            Amount = amount;
            Signature = signature;
        }

        public override string ToString() =>
            $"{SenderPublicKey.Substring(0, 10)}... -> {Receiver}: {Amount} (Signed)";
    }
}