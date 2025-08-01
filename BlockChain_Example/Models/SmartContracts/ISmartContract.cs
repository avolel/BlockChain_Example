namespace BlockChain_Example.Models.SmartContracts
{
    public interface ISmartContract
    {
        void Execute(BlockChain chain);
    }
}