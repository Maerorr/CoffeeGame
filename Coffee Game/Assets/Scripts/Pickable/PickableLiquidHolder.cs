using System.Collections;
using UnityEngine;

public class PickableLiquidHolder : Pickable
{

    [SerializeField] protected float capacity = 150f;
    public LiquidHolder liquidHolder = new LiquidHolder();
    protected MultiColorMeter meter;

    [SerializeField] protected float meterShowTime = 0.5f;

    protected Coroutine timedMeterShowCoroutine;

    protected override void Start()
    {
        base.Start();
        liquidHolder.Capacity = capacity;
        meter = GetComponentInChildren<MultiColorMeter>();
        meter.SetVisible(false);
        liquidHolder.onLiquidsChange.AddListener(UpdateMeter);
        onPickedUp.AddListener(OnPickedUp);
        onDropped.AddListener(OnDropped);
    }

    protected void UpdateMeter()
    {
        TriggerMeterShowup();
        foreach (Liquid l in liquidHolder.liquids)
        {
            meter.SetContent(l.name, l.amount / capacity, l.color);
        }
    }

    public void TriggerMeterShowup()
    {
        meter.SetVisible(true);
        if (timedMeterShowCoroutine != null)
        {
            StopCoroutine(timedMeterShowCoroutine);
        }
        timedMeterShowCoroutine = StartCoroutine(TimedShowMeter());
    }

    public void ToggleMeterVisibility(bool visible)
    {
        meter.SetVisible(visible);
    }

    protected IEnumerator TimedShowMeter()
    {
        yield return new WaitForSeconds(meterShowTime);
        meter.SetVisible(false);

    }

    private void OnPickedUp()
    {
        StopCoroutine(timedMeterShowCoroutine);
    }

    private void OnDropped()
    {
        TriggerMeterShowup();
    }

    public override void ExitHover(Hand hand)
    {
        // if object is snapped we (for now) want it to display the meter at all times
        if (snapZone != null) return;
        meter.SetVisible(true);
        Highlight();
    }

    public override void HoverEnter(Hand hand)
    {
        if (snapZone != null) return;
        TriggerMeterShowup();
        EndHighlight();
    }
}
