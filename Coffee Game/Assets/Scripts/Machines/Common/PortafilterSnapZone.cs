using UnityEngine;
using UnityEngine.Events;

public class PortafilterSnapZone : SnapZone
{
    public UnityEvent<Portafilter> onPortafilterSnapped;

    public override bool Interact(Hand hand)
    {
        Debug.Log("PortafilterSnapZones");
        Pickable pick = hand.GetPickableInHand();
        if (pick is Portafilter p)
        {
            hand.GetPickableInHand().SetSnapZone(this);
            pickable = pick;
            hand.SnapPickable(snapPosition);
            onPortafilterSnapped.Invoke(p);
            return true;
        }

        return false;
    }
}