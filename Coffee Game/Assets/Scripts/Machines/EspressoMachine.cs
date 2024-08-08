using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EspressoMachine : MonoBehaviour
{
    [SerializeField] private List<SnapZone> portafilterSnaps = new List<SnapZone>();
    [SerializeField] private List<SnapZone> cups = new List<SnapZone>();
    [SerializeField] private List<MachineButton> buttons = new List<MachineButton>();
    private List<Coroutine> pouringCoroutines = new List<Coroutine>();

    private void Start()
    {
        for (int i = 0; i < portafilterSnaps.Count; i++)
        {
            pouringCoroutines.Add(null);
        }
    }

    private void StartPouring(int id, Portafilter p, Cup cup)
    {
        pouringCoroutines[id] = StartCoroutine(Pour(id, p, cup));
    }

    private void StopPouring(int id)
    {
        StopCoroutine(pouringCoroutines[id]);
        ((Portafilter)portafilterSnaps[id].GetPickable()).ToggleLiquid(false);
    }

    private IEnumerator Pour(int id, Portafilter p, Cup cup)
    {
        p.ToggleLiquid(true);
        p.UseCoffee();
        while (true)
        {
            yield return new WaitForSeconds(0.033f);
            cup.AddLiquid(1f * 0.033f);
        }
    }
    
    public void OnButtonPress(int id)
    {
        if (portafilterSnaps[id].GetPickable() is Portafilter p &&
            cups[id].GetPickable() is Cup c)
        {
            StartPouring(id, p, c);
        }
    }
    
    public void OnButtonUp(int id)
    {
        if (portafilterSnaps[id].GetPickable() is Portafilter p &&
            cups[id].GetPickable() is Cup c)
        {
            StopPouring(id);
        }
    }
}
