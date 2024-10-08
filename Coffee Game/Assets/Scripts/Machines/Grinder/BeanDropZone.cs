using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class BeanDropZone : MonoBehaviour, IInteractable
{
    Grinder grinder;

    [SerializeField] private float beansPerSecond = 2f;

    CoffeeBag bag;

    private Coroutine beanPour;
    private float bagVelocity;
    public float BagVelocity
    {
        get => bagVelocity;
        set
        {
            bagVelocity = Mathf.Clamp(value, 0f, float.MaxValue);
        }
    }

    private void Start()
    {
        grinder = GetComponentInParent<Grinder>();
    }

    public bool Interact(Hand hand)
    {
        ExitHover(hand);
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
            grinder.AddBeans(beansPerSecond * Time.deltaTime * (1f + bagVelocity * 1.5f));

            yield return null;
        }
    }

    // void OnTriggerEnter2D(Collider2D col)
    // {
    //     if (col.gameObject.TryGetComponent(out CoffeeBag bag))
    //     {
    //         bag.SetDropZone(this);
    //         bag.StartPouringBeans();
    //         this.bag = bag;
    //         if (beanPour is not null)
    //         {
    //             StopCoroutine(beanPour);
    //         }
    //         beanPour = StartCoroutine(PourBeansToGrinder());
    //     }
    // }

    // void OnTriggerExit2D(Collider2D col)
    // {
    //     if (col.gameObject.TryGetComponent(out CoffeeBag bag))
    //     {
    //         bag.StopPouringBeans();
    //         if (beanPour is not null)
    //         {
    //             StopCoroutine(beanPour);
    //         }
    //         this.bag = null;
    //     }
    // }

    public void HoverEnter(Hand hand)
    {
        if (hand.GetPickableInHand() is CoffeeBag bag)
        {
            bag.SetDropZone(this);
            bag.StartPouringBeans();
            this.bag = bag;
            if (beanPour is not null)
            {
                StopCoroutine(beanPour);
            }
            beanPour = StartCoroutine(PourBeansToGrinder());
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
            this.bag = null;
        }
    }

    // public void ExitHover(Hand hand)
    // {
    //     if (hand.GetPickableInHand() is CoffeeBag bag)
    //     {
    //         bag.StopPouringBeans();
    //         if (beanPour is not null)
    //         {
    //             StopCoroutine(beanPour);
    //         }
    //     }
    // }

    // public void HoverEnter(Hand hand)
    // {
    //     Debug.Log("Pickabke over bean drop zone");
    //     if (hand.GetPickableInHand() is CoffeeBag bag)
    //     {
    //         Debug.Log("COFFEE BAG over bean drop zone");
    //         bag.StartPouringBeans();
    //         if (beanPour is not null)
    //         {
    //             StopCoroutine(beanPour);
    //         }
    //         beanPour = StartCoroutine(PourBeansToGrinder());
    //     }
    // }
}
