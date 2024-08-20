using System.Collections.Generic;
using UnityEngine;

public class Cup : Pickable
{
    [SerializeField] private float capacity = 150f;

    private LiquidHolder liquidHolder = new LiquidHolder();

    MultiColorMeter meter;

    private new void Start()
    {
        base.Start();
        liquidHolder.Capacity = capacity;
        meter = GetComponentInChildren<MultiColorMeter>();
        meter.SetVisible(false);
    }


    public void AddLiquid(Liquid liquid)
    {
        liquidHolder.AddLiquid(liquid);
        UpdateMeter();
    }

    private void UpdateMeter()
    {
        foreach (Liquid l in liquidHolder.liquids)
        {
            meter.SetContent(l.name, l.amount / capacity, l.color);
        }
    }

    public void ToggleMeterVisibility(bool visible)
    {
        meter.SetVisible(visible);
    }

    public override void HoverEnter(Hand hand)
    {
        Highlight();
        // machines that require knowing the amount of liquids will trigger the bar themselves
        if (snapZone is null) meter.SetVisible(true);
    }

    public override void ExitHover(Hand hand)
    {
        EndHighlight();
        // if the cup is snapped to a machine dont hide the bar
        if (snapZone is null) meter.SetVisible(false);
    }
}
