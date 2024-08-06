using UnityEngine;

public interface IInteractable
{
    public bool Interact(Hand hand);

    public void EndInteraction();
    public void HoverEnter(Hand hand);
    public void ExitHover();
}
