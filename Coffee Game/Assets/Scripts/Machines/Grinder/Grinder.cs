using System;
using System.Collections;
using UnityEngine;

public class Grinder : MonoBehaviour
{
    private Portafilter _portafilter;
    private PortafilterSnapZone pSnap;

    [SerializeField] float grindSpeed = 3f;
    private Coroutine grindCoroutine;

    private void Start()
    {
        pSnap = GetComponentInChildren<PortafilterSnapZone>();
        pSnap.onPortafilterSnapped.AddListener(SetPortafilter);
        pSnap.onPickableUnSnapped.AddListener(PickableUnsnapped);
    }

    private void SetPortafilter(Portafilter p, int _)
    {
        _portafilter = p;
        _portafilter.ToggleMeterVisibility(true);
    }

    private void PickableUnsnapped(Pickable pick, int _)
    {
        if (pick is Portafilter p)
        {
            p.ToggleMeterVisibility(false);
        }
    }

    public IEnumerator Grind()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.033f);
            _portafilter.AddCoffee(grindSpeed * 0.033f);
        }
    }

    public void StartGrinding()
    {
        if (_portafilter is null) return;
        grindCoroutine = StartCoroutine(Grind());
    }

    public void StopGrinding()
    {
        if (grindCoroutine is not null) StopCoroutine(grindCoroutine);
    }
}
