using UnityEngine;
using UnityEngine.Events;

public class PortafilterSnapZone : MonoBehaviour, IInteractable
{
    private IPortafilterSnap machine;

    [SerializeField] private Transform snapPosition;

    public UnityEvent<Portafilter> onPortafilterSnapped;
    
    private void Start()
    {
        snapPosition ??= transform;
    }

    public bool Interact(Hand hand)
    {
        if (hand.GetPickableInHand() is Portafilter)
        {
            Portafilter p = hand.GetPickableInHand() as Portafilter;
            hand.SnapPickable(transform);
            onPortafilterSnapped.Invoke(p);
            return true;
        }

        return false;
    }

    public void HoverEnter(Hand hand)
    {
        
    }

    public void ExitHover()
    {
        
    }

    public void EndInteraction()
    {
        
    }
}