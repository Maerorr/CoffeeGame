using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EspressoMachine : MonoBehaviour
{
    //[SerializeField] private List<SnapZone> portafilterSnaps = new List<SnapZone>();
    //[SerializeField] private List<SnapZone> cups = new List<SnapZone>();

    private List<Portafilter> portafilters = new List<Portafilter>();
    private List<Cup> cups = new List<Cup>();

    [SerializeField] private List<MachineButton> buttons = new List<MachineButton>();
    private List<Coroutine> pouringCoroutines = new List<Coroutine>();
    [SerializeField] float pourSpeed = 10f;

    private float pourTimeFrame = 0.033f;

    private Liquid coffee;

    private void Start()
    {
        for (int i = 0; i < 8; i++)
        {
            pouringCoroutines.Add(null);
            portafilters.Add(null);
            cups.Add(null);
        }

        coffee = new Liquid(LiquidType.Espresso, pourSpeed * pourTimeFrame);
    }

    private void StartPouring(int id, Portafilter p, Cup cup)
    {
        pouringCoroutines[id] = StartCoroutine(Pour(id, p, cup));
    }

    private void StopPouring(int id)
    {
        StopCoroutine(pouringCoroutines[id]);
        portafilters[id].ToggleLiquid(false);
    }

    private IEnumerator Pour(int id, Portafilter p, Cup cup)
    {
        p.ToggleLiquid(true);
        p.UseCoffee();
        while (true)
        {
            yield return new WaitForSeconds(pourTimeFrame);
            cup.AddLiquid(coffee);
        }
    }
    
    public void SetPortafilter(Portafilter p, int id) {
        portafilters[id] = p;
    }

    public void SetCup(Pickable c, int id) {
        if (c is Cup cup) {
            cup.ToggleMeterVisibility(true);
            cups[id] = cup;
        }
    }

    public void UnsetPickable(Pickable p, int id) {
        if (p is Portafilter portafilter) {
            portafilter.ToggleMeterVisibility(false);
            portafilters[id] = null;
            return;
        }

        if (p is Cup c) {
            c.ToggleMeterVisibility(false);
            cups[id] = null;
        }
    }

    public void OnButtonPress(int id)
    {
        if (portafilters[id] is not null &&
            cups[id] is not null)
        {
            StartPouring(id, portafilters[id], cups[id]);
        }
    }
    
    public void OnButtonUp(int id)
    {
        if (portafilters[id] is not null &&
            cups[id] is not null)
        {
            StopPouring(id);
        }
    }
}
