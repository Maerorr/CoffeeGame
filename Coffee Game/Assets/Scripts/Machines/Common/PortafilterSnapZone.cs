using UnityEngine;
using UnityEngine.Events;

public class PortafilterSnapZone : SnapZone
{
    public UnityEvent<Portafilter, int> onPortafilterSnapped;

    public override bool Interact(Hand hand)
    {
        Pickable pick = hand.GetPickableInHand();
        if (pick is Portafilter p)
        {
            hand.GetPickableInHand().SetSnapZone(this);
            pickable = pick;
            hand.SnapPickable(snapPosition);
            onPortafilterSnapped.Invoke(p, id);
            return true;
        }

        return false;
    }
}