public interface ILiquidTransferable
{
    public void SetLiquidHolder(ref LiquidHolder lq);
    public void StartLiquidTransfer();
    public void StopLiquidTransfer();
    
    // TODO: Possiby add a function to 'ask' the reciever if it accepts the liquid type
}
