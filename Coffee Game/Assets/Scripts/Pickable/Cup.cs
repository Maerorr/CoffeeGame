public class Cup : Pickable
{
    private float liquidAmount = 0;

    public void AddLiquid(float amount)
    {
        liquidAmount += amount;
    }
}
