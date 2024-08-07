using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EspressoMachine : MonoBehaviour
{
    [SerializeField] private List<SnapZone> portafilterSnaps = new List<SnapZone>();
    [SerializeField] private List<SnapZone> cups = new List<SnapZone>();
    [SerializeField] private List<MachineButton> buttons = new List<MachineButton>();

    private void StartPouring(int id)
    {
        ((Portafilter)portafilterSnaps[id].GetPickable()).ToggleLiquid(true);
    }

    private void StopPouring(int id)
    {
        if (portafilterSnaps[id].GetPickable() is Portafilter p)
        {
            p.ToggleLiquid(false);
        }
    }
    
    public void OnButtonPress(int id)
    {
        Debug.Log($"portafilter {portafilterSnaps[id].GetPickable() is Portafilter}, cup { cups[id].GetPickable() is Cup}");
        if (portafilterSnaps[id].GetPickable() is Portafilter &&
            cups[id].GetPickable() is Cup)
        {
            StartPouring(id);
        }
    }
    
    public void OnButtonUp(int id)
    {
        StopPouring(id);
    }
}
