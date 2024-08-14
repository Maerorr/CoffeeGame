public interface IInteractable
{
    /// <summary>
    /// Function triggered by a Hand starting an interaction by pressing left mouse button.
    /// </summary>
    /// <param name="hand"></param>
    /// <returns></returns>
    public bool Interact(Hand hand);

    /// <summary>
    /// Function called on de-press of a left mouse button, useful mostly for hold buttons.
    /// </summary>
    public void EndInteraction(Hand hand);
    
    /// <summary>
    /// Function called when a hand hovers over an IInteractable object.
    /// </summary>
    /// <param name="hand"></param>
    public void HoverEnter(Hand hand);
    
    /// <summary>
    /// Function called when a hand exits hover area of an IInteractable object.
    /// </summary>
    public void ExitHover(Hand hand);
}
