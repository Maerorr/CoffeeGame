using System.Collections;
using UnityEngine;

public class MilkFoamer : MonoBehaviour
{
    private MilkPitcher pitcher;
    [SerializeField] float foamingSpeed = 5f;
    [SerializeField] float foamTimeScale = 0.033f;

    private Coroutine foamCoroutine;


    public void SetPitcher(Pickable p, int _)
    {
        if (p is MilkPitcher mp)
        {
            pitcher = mp;
            pitcher.ToggleMeterVisibility(true);
        }
    }

    public void UnsetPitcher(Pickable p, int _)
    {
        if (p is MilkPitcher mp)
        {
            mp.ToggleMeterVisibility(false);
            pitcher = null;
        }
    }

    private IEnumerator Foam()
    {
        float milk = 0f;
        while (true)
        {
            if (pitcher is null) break;
            milk = pitcher.GetMilkAmount();
            //Debug.Log(milk);
            float takenMilk = 0f;
            if (milk <= 0f) break;
            if (milk < foamingSpeed * foamTimeScale)
            {
                takenMilk = milk;
            }
            else
            {
                takenMilk = foamingSpeed * foamTimeScale;
            }

            pitcher.liquidHolder.SubtractLiquid(new Milk(takenMilk));
            pitcher.liquidHolder.AddLiquid(new MilkFoam(55f, takenMilk));
            yield return new WaitForSeconds(foamTimeScale);
        }
    }

    public void GrindButton()
    {
        if (foamCoroutine is not null)
        {
            StopFoaming();
        }
        else
        {
            StartFoaming();
        }
    }

    private void StartFoaming()
    {
        if (foamCoroutine is not null)
        {
            StopCoroutine(foamCoroutine);
        }
        foamCoroutine = StartCoroutine(Foam());
    }

    private void StopFoaming()
    {
        if (foamCoroutine is null) return;
        StopCoroutine(foamCoroutine);
    }
}
