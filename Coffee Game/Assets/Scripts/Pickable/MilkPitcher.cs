using System.Collections;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class MilkPitcher : Pickable, ILiquidTransferable
{
    // ideally we want to store only milk and foam
    [HideInInspector]
    public LiquidHolder liquidHolder = new LiquidHolder();
    [SerializeField] private float capacity = 150f;
    private MultiColorMeter meter;


    # region Liquid Transfer
    private LiquidHolder targetLiquidHolder;
    private int rotateTweenID;
    private Coroutine transferCoroutine;
    #endregion

    private new void Start()
    {
        base.Start();
        meter = GetComponentInChildren<MultiColorMeter>();
        meter.SetRuler(0.5f, 2);
        liquidHolder.Capacity = capacity;
        liquidHolder.AddLiquid(new Liquid(LiquidType.Milk, 100f));
        Debug.Log(liquidHolder.ToString());
        UpdateMeter();
        meter.SetVisible(false);
        liquidHolder.onLiquidsChange.AddListener(UpdateMeter);
        onPickedUp.AddListener(ShowMeter);
        onDropped.AddListener(HideMeter);
    }

    private void ShowMeter()
    {
        if (snapZone is null) meter.SetVisible(true);
    }

    private void HideMeter()
    {
        if (snapZone is null) meter.SetVisible(false);
    }

    public override void HoverEnter(Hand hand)
    {
        Highlight();
        ShowMeter();
    }

    public new bool Interact(Hand hand)
    {
        return base.Interact(hand);
    }

    public override void ExitHover(Hand hand)
    {
        EndHighlight();
        HideMeter();
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

    private IEnumerator TransferLiquid()
    {
        float liqAmountToTransfer;
        while (true)
        {
            yield return new WaitForNextFrameUnit();
            liqAmountToTransfer = Time.deltaTime * 33f;
            Liquid l = liquidHolder.GetTopMostLiquid();
            if (l == null) break;
            if (l.amount < liqAmountToTransfer)
            {
                liqAmountToTransfer = l.amount;
            }
            targetLiquidHolder.AddLiquid(new Liquid(l.liquidType, liqAmountToTransfer));
            liquidHolder.SubtractLiquid(new Liquid(l.liquidType, liqAmountToTransfer));
        }
    }

    public void SetLiquidHolder(ref LiquidHolder lq)
    {
        targetLiquidHolder = lq;
    }

    public void StartLiquidTransfer()
    {
        DOTween.Kill(rotateTweenID);
        var tween = transform.DORotate(new Vector3(0f, 0f, 75f), 0.4f).SetId(this).OnComplete(
            () =>
            {
                transferCoroutine = StartCoroutine(TransferLiquid());
            }
        );
        rotateTweenID = tween.intId;
    }

    public void StopLiquidTransfer()
    {
        DOTween.Kill(rotateTweenID);
        var tween = transform.DORotate(new Vector3(0f, 0f, 0f), 0.4f).SetId(this);
        rotateTweenID = tween.intId;
        if (transferCoroutine != null)
        {
            StopCoroutine(transferCoroutine);
        }
        targetLiquidHolder = null;
    }
}
