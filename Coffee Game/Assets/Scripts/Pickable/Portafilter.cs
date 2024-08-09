using UnityEngine;

public class Portafilter : Pickable
{
    [SerializeField] private float capacity = 30f;
    [SerializeField] private float coffeeAmount = 0f;
    private bool used = false;
    public GameObject coffeeGrounds;
    public GameObject liquid;

    MultiColorMeter meter;

    private new void Start()
    {
        base.Start();
        
        coffeeGrounds.SetActive(false);
        liquid.SetActive(false);
        meter = GetComponentInChildren<MultiColorMeter>();
        meter.SetRuler(0.5f, 2);
        meter.SetVisible(false);
        
    }
    
    public void AddCoffee(float c)
    {
        if (coffeeAmount + c > capacity) return;
        coffeeGrounds.SetActive(true);
        coffeeAmount += c;
        meter.SetContent("grounds", coffeeAmount / capacity, Colors.Get("espresso"));
    }

    public void ToggleLiquid(bool show)
    {
        liquid.SetActive(show);
    }
    
    public void UseCoffee()
    {
        used = true;
    }

    public void Clear()
    {
        coffeeAmount = 0f;
        coffeeGrounds.SetActive(false);
        used = false;
    }

    public void ToggleMeterVisibility(bool visible) {
        meter.SetVisible(visible);
    }

    public override void HoverEnter(Hand hand)
    {
        Highlight();
        // machines that require knowing the amount of grounds will trigger the bar themselves
        if (snapZone is null) meter.SetVisible(true);
    }

    public override void ExitHover()
    {
        EndHighlight();
        // if the filter is snapped to a machine dont hide the bar
        if (snapZone is null) meter.SetVisible(false);
    }
}
