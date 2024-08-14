using UnityEngine;
using UnityEngine.EventSystems;

public class BeanDropZone : MonoBehaviour, IInteractable
{
    Grinder grinder;


    public bool Interact(Hand hand)
    {

        return true;
    }

    public bool IsOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject() &&
            EventSystem.current.currentSelectedGameObject.gameObject is not null &&
            EventSystem.current.currentSelectedGameObject.gameObject.CompareTag("UI");
    }

    public void EndInteraction(Hand hand)
    {

    }

    public void ExitHover(Hand hand)
    {
        if (hand.GetPickableInHand() is CoffeeBag bag)
        {
            bag.StopPouringBeans();
        }
    }

    public void HoverEnter(Hand hand)
    {
        if (hand.GetPickableInHand() is CoffeeBag bag)
        {
            bag.StartPouringBeans();
        }
    }
}
