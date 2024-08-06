using System;
using UnityEngine;

public class Grinder : MonoBehaviour
{
    private Portafilter _portafilter;
    private PortafilterSnapZone pSnap;
    
    private void Start()
    {
        pSnap = GetComponentInChildren<PortafilterSnapZone>();
        pSnap.onPortafilterSnapped.AddListener(SetPortafilter);
    }

    private void SetPortafilter(Portafilter p)
    {
        _portafilter = p;
    }

    public void Grind()
    {
        if (_portafilter is null) return;
        _portafilter.AddCoffee(5.0f);
    }
}
