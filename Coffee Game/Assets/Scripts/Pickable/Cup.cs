using System.Collections.Generic;
using UnityEngine;

public class Cup : Pickable
{
    [SerializeField] private float capacity = 150f;
    private float liquidAmount = 0;

    private List<Liquid> liquids = new List<Liquid>();

    MultiColorMeter meter;

    private new void Start() {
        base.Start();

        meter = GetComponentInChildren<MultiColorMeter>();
        meter.SetVisible(false);
    }

    
    public void AddLiquid(Liquid liquid)
    {
        // liquidAmount += amount;
        // meter.SetContent("espresso", liquidAmount / capacity, Colors.Get("espresso"));
        int idx = liquids.FindIndex(el => el.name == liquid.name);
        Debug.Log($"Adding {liquid.amount} of liquid");
        if (idx == -1) {
            liquids.Add(new Liquid(liquid.liquidType));
            UpdateMeter();
            return;
        } 
        liquids[idx].Add(liquid.amount);
        UpdateMeter();
    }

    private void UpdateMeter() {
        foreach (Liquid l in liquids) {
            meter.SetContent(l.name, l.amount / capacity, l.color);
        }
    }

    public void ToggleMeterVisibility(bool visible) {
        meter.SetVisible(visible);
    }

    public override void HoverEnter(Hand hand)
    {
        Highlight();
        // machines that require knowing the amount of liquids will trigger the bar themselves
        if (snapZone is null) meter.SetVisible(true);
    }

    public override void ExitHover()
    {
        EndHighlight();
        // if the cup is snapped to a machine dont hide the bar
        if (snapZone is null) meter.SetVisible(false);
    }
}
