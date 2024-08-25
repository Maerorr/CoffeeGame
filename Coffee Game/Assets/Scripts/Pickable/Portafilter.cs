using DG.Tweening;
using UnityEngine;

public class Portafilter : Pickable
{
    [SerializeField] private float capacity = 30f;
    [SerializeField] private float coffeeAmount;

    private bool used = false;
    public GameObject coffeeGrounds;
    public GameObject liquid;

    MultiColorMeter meter;

    int rotTweenID;

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
        coffeeAmount += c;
        coffeeAmount = Mathf.Clamp(coffeeAmount, 0f, capacity);

        coffeeGrounds.SetActive(coffeeAmount > 0f);
        meter.SetContent("grounds", coffeeAmount / capacity, Colors.Get("espresso"));
    }

    public void ToggleLiquid(bool show)
    {
        liquid.SetActive(show);
    }

    public void SetUsed()
    {
        used = true;
    }

    public void Clear()
    {
        coffeeAmount = 0f;
        coffeeGrounds.SetActive(false);
        used = false;
        meter.SetContent("grounds", 0f, Colors.Get("espresso"));
    }

    public void ToggleMeterVisibility(bool visible)
    {
        meter.SetVisible(visible);
    }

    public override void HoverEnter(Hand hand)
    {
        Highlight();
        // machines that require knowing the amount of grounds will trigger the bar themselves
        if (snapZone is null) meter.SetVisible(true);
    }

    public override void ExitHover(Hand hand)
    {
        EndHighlight();
        // if the filter is snapped to a machine dont hide the bar
        if (snapZone is null) meter.SetVisible(false);
    }

    public float groundsAmountInFilter()
    {
        return coffeeAmount;
    }

    public void StartThrowingOut()
    {
        DOTween.Kill(rotTweenID);
        var tween = transform.DORotate(new Vector3(0f, 0f, -179f), 0.5f).SetId(this).OnComplete(
            () =>
            {
                Clear();
                StopThrowingOut();
            }
        );
        rotTweenID = tween.intId;
    }

    public void StopThrowingOut()
    {
        DOTween.Kill(rotTweenID);
        var tween = transform.DORotate(new Vector3(0f, 0f, 0f), 0.5f).SetId(this);
        rotTweenID = tween.intId;
    }
}
