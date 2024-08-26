using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class EspressoMachine : MonoBehaviour
{
    //[SerializeField] private List<SnapZone> portafilterSnaps = new List<SnapZone>();
    //[SerializeField] private List<SnapZone> cups = new List<SnapZone>();

    private List<Portafilter> portafilters = new List<Portafilter>();
    private List<Cup> cups = new List<Cup>();

    [SerializeField] private Gauge minigameGauge;

    [SerializeField] private List<MachineButton> buttons = new List<MachineButton>();
    private List<Coroutine> pouringCoroutines = new List<Coroutine>();
    private List<Coroutine> usedCoffeeCoroutines = new List<Coroutine>();
    [SerializeField] float pourSpeed = 10f;

    private float pourTimeFrame = 0.033f;
    private int minigameTweenID;

    private void Start()
    {
        for (int i = 0; i < 8; i++)
        {
            pouringCoroutines.Add(null);
            usedCoffeeCoroutines.Add(null);
            portafilters.Add(null);
            cups.Add(null);
        }
    }

    private void StartPouring(int id, Portafilter p, Cup cup, float amountToPour)
    {
        pouringCoroutines[id] = StartCoroutine(Pour(id, p, cup, amountToPour));
        usedCoffeeCoroutines[id] = StartCoroutine(SetCoffeeUsed(id, p));
    }

    private void StopPouring(int id)
    {
        if (pouringCoroutines[id] != null) StopCoroutine(pouringCoroutines[id]);
        if (usedCoffeeCoroutines[id] != null) StopCoroutine(usedCoffeeCoroutines[id]);
        portafilters[id].ToggleLiquid(false);
    }

    private IEnumerator Minigame()
    {
        GaugeUp();
        while (true)
        {
            yield return null;
        }
    }

    private void GaugeUp()
    {
        var tween = DOTween.To(() => minigameGauge.Val, x => minigameGauge.Val = x, 1f, 1f)
        .OnComplete(() => GaugeDown())
        .SetEase(Ease.Linear)
        .SetId(this);

        minigameTweenID = tween.intId;
    }

    private void GaugeDown()
    {
        var tween = DOTween.To(() => minigameGauge.Val, x => minigameGauge.Val = x, 0f, 1f)
        .OnComplete(() => GaugeUp())
        .SetEase(Ease.Linear)
        .SetId(this);

        minigameTweenID = tween.intId;
    }

    private IEnumerator Pour(int id, Portafilter p, Cup cup, float amountToPour)
    {
        p.ToggleLiquid(true);
        float pouredVal = 0f;
        //var espresso = ;
        Debug.Log($"Amount to pour: {amountToPour}");
        while (pouredVal < amountToPour)
        {
            cup.AddLiquid(new Liquid(LiquidType.Espresso, pourSpeed * Time.deltaTime));
            pouredVal += pourSpeed * Time.deltaTime;
            Debug.Log($"poured val: {pouredVal}");
            yield return null;
        }
        cup.SetLiquid(new Liquid(LiquidType.Espresso, amountToPour));
        Debug.Log($"setting liquid val to {amountToPour}");
        p.ToggleLiquid(false);
    }

    private IEnumerator SetCoffeeUsed(int id, Portafilter p)
    {
        yield return new WaitForSeconds(0.25f);
        p.SetUsed();
    }

    public void SetPortafilter(Portafilter p, int id)
    {
        portafilters[id] = p;
    }

    public void SetCup(Pickable c, int id)
    {
        if (c is Cup cup)
        {
            cup.ToggleMeterVisibility(true);
            cups[id] = cup;
        }
    }

    public void UnsetPickable(Pickable p, int id)
    {
        if (p is Portafilter portafilter)
        {
            portafilter.ToggleMeterVisibility(false);
            portafilters[id] = null;
            return;
        }

        if (p is Cup c)
        {
            c.ToggleMeterVisibility(false);
            cups[id] = null;
        }
    }

    public void OnButtonPress(int id)
    {
        if (portafilters[id] is not null &&
            cups[id] is not null)
        {
            StartCoroutine(Minigame());
            minigameGauge.ToggleTarget(true);
            //StartPouring(id, portafilters[id], cups[id]);
        }
    }

    public void OnButtonUp(int id)
    {
        if (portafilters[id] is not null &&
            cups[id] is not null)
        {
            minigameGauge.ToggleTarget(false);
            DOTween.Kill(minigameTweenID);
            float amountToPour = minigameGauge.Val * 40f;
            StartPouring(id, portafilters[id], cups[id], amountToPour);
            //StopPouring(id);
        }
    }
}
