using UnityEngine;

public class MilkPitcher : Pickable
{
    // ideally we want to store only milk and foam
    [HideInInspector]
    public LiquidHolder liquidHolder = new LiquidHolder();
    [SerializeField] private float capacity = 150f;
    private MultiColorMeter meter;

    private new void Start()
    {
        base.Start();
        meter = GetComponentInChildren<MultiColorMeter>();
        meter.SetRuler(0.5f, 2);
        liquidHolder.Capacity = capacity;
        liquidHolder.AddLiquid(new Milk(100f));
        Debug.Log(liquidHolder.ToString());
        UpdateMeter();
        meter.SetVisible(false);
        liquidHolder.onLiquidsChange.AddListener(UpdateMeter);
    }

    public override void HoverEnter(Hand hand)
    {
        Highlight();
        if (snapZone is null) meter.SetVisible(true);
    }

    public override void ExitHover(Hand hand)
    {
        EndHighlight();
        if (snapZone is null) meter.SetVisible(false);
    }

    public void ToggleMeterVisibility(bool visible)
    {
        meter.SetVisible(visible);
    }

    public float GetMilkAmount()
    {
        var milk = liquidHolder.GetByName(LiquidType.Milk.ToString());
        return milk is null ? 0f : milk.amount;
    }

    private void UpdateMeter()
    {
        foreach (Liquid l in liquidHolder.liquids)
        {
            //Debug.Log($"{l.name}, {l.amount / capacity}, {l.color}");
            meter.SetContent(l.name, l.amount / capacity, l.color);
        }
    }
}
