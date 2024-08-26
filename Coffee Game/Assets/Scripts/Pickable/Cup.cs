public class Cup : PickableLiquidHolder
{
    private new void Start()
    {
        base.Start();
        GetComponentInChildren<LiquidTransferZone>().SetLiquidHolder(ref liquidHolder);
    }

    public void AddLiquid(Liquid liquid)
    {
        liquidHolder.AddLiquid(liquid);
        UpdateMeter();
    }

    public void SetLiquid(Liquid liquid)
    {
        liquidHolder.SetLiquid(liquid);
    }
}
