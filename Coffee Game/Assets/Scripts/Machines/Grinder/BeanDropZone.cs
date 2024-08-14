using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class BeanDropZone : MonoBehaviour, IInteractable
{
    Grinder grinder;

    [SerializeField] private float beansPerSecond = 2f;

    private Coroutine beanPour;

    private void Start()
    {
        grinder = GetComponentInParent<Grinder>();
    }

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

    private IEnumerator PourBeansToGrinder()
    {
        while (true)
        {
            grinder.AddBeans(beansPerSecond * Time.deltaTime);
            yield return new WaitForNextFrameUnit();
        }
    }

    public void ExitHover(Hand hand)
    {
        if (hand.GetPickableInHand() is CoffeeBag bag)
        {
            bag.StopPouringBeans();
            if (beanPour is not null)
            {
                StopCoroutine(beanPour);
            }
        }
    }

    public void HoverEnter(Hand hand)
    {
        if (hand.GetPickableInHand() is CoffeeBag bag)
        {
            bag.StartPouringBeans();
            if (beanPour is not null)
            {
                StopCoroutine(beanPour);
            }
            beanPour = StartCoroutine(PourBeansToGrinder());
        }
    }
}
