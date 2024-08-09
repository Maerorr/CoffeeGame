using UnityEngine;

public class Cup : Pickable
{
    [SerializeField] private float capacity = 150f;
    private float liquidAmount = 0;

    MultiColorMeter meter;

    private new void Start() {
        base.Start();

        meter = GetComponentInChildren<MultiColorMeter>();
        meter.SetVisible(false);
    }

    
    public void AddLiquid(float amount)
    {
        liquidAmount += amount;
        meter.SetContent("espresso", liquidAmount / capacity, Colors.Get("espresso"));
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
