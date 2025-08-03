namespace BlockChain_Example.Models
{
    public class Wallet
    {
        public string Address { get; }

        public Wallet(string address)
        {
            Address = address;
        }

        public Transaction CreateTransaction(string receiver, decimal amount)
        {
            return new Transaction(Address, receiver, amount);
        }

        public override string ToString() => Address;
    }
}