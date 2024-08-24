
using UnityEngine;

public class LiquidTransferZone : MonoBehaviour
{
    LiquidHolder lqh;

    public void SetLiquidHolder(ref LiquidHolder holder)
    {
        lqh = holder;
    }

    void OnTriggerEnter2D(Collider2D col)
    {   
        Debug.Log(col.gameObject.name);
        //ILiquidTransferable lqt = col.gameObject.GetComponent<ILiquidTransferable>();
        if (col.gameObject.TryGetComponent(out ILiquidTransferable lqt))
        {
            lqt.SetLiquidHolder(ref lqh);
            lqt.StartLiquidTransfer();
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.TryGetComponent(out ILiquidTransferable lqt))
        {
            lqt.StopLiquidTransfer();
        }
    }
}
