using UnityEngine;

public class Knockbox : MonoBehaviour, IInteractable
{

    public bool Interact(Hand hand)
    {
        return true;
    }

    public void EndInteraction(Hand hand)
    {
    }

    public void HoverEnter(Hand hand)
    {
        if (hand.GetPickableInHand() is Portafilter p)
        {
            p.StartThrowingOut();
        }
    }

    public void ExitHover(Hand hand)
    {
        if (hand.GetPickableInHand() is Portafilter p)
        {
            p.StopThrowingOut();
        }
    }
}
