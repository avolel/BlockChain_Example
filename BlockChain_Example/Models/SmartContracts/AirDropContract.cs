namespace BlockChain_Example.Models.SmartContracts
{
    public class AirDropContract : ISmartContract
    {
        public string Receiver { get; set; }
        public decimal Amount { get; set; }

        public AirDropContract(string receiver, decimal amount)
        {
            Receiver = receiver;
            Amount = amount;
        }

        public void Execute(BlockChain chain)
        {
            chain.AddTransaction(new Transaction(
                "System",
                Receiver,
                Amount
            ));
        }
    }
}