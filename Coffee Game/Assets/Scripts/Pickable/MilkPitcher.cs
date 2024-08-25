using System.Collections;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class MilkPitcher : PickableLiquidHolder, ILiquidTransferable
{

    # region Liquid Transfer
    private LiquidHolder targetLiquidHolder;
    private int rotateTweenID;
    private Coroutine transferCoroutine;
    #endregion

    private new void Start()
    {
        base.Start();
        liquidHolder.AddLiquid(new Liquid(LiquidType.Milk, 100f));
        liquidHolder.AddLiquid(new Liquid(LiquidType.MilkFoam, 40f));
    }


    public float GetMilkAmount()
    {
        var milk = liquidHolder.GetByName(LiquidType.Milk.ToString());
        return milk is null ? 0f : milk.amount;
    }

    private IEnumerator TransferLiquid()
    {
        float liqAmountToTransfer;
        while (true)
        {
            yield return new WaitForNextFrameUnit();
            liqAmountToTransfer = Time.deltaTime * 33f;
            Liquid l = liquidHolder.GetTopMostLiquid();
            
            if (l == null)
            {
                Debug.Log("Topmost Liquid is NULL");
                break;
            };
            if (l.amount < liqAmountToTransfer)
            {
                liqAmountToTransfer = l.amount;
            }
            Debug.Log($"{l.liquidType}, {liqAmountToTransfer}");
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
