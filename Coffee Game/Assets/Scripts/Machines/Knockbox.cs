using UnityEngine;

public class Knockbox : MonoBehaviour, IInteractable
{

    public bool Interact(Hand hand)
    {
        if (hand.GetPickableInHand() is Portafilter p)
        {
            p.Clear();
            return true;
        }

        return false;
    }

    public void EndInteraction()
    {
    }

    public void HoverEnter(Hand hand)
    {
    }

    public void ExitHover()
    {
    }
}
