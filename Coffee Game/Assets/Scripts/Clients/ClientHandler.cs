using UnityEngine;
using UnityEngine.UIElements;

public class ClientHandler : MonoBehaviour, IInteractable
{

    [SerializeField]
    private DayQueueSO dayQueue;
    private int currentClient = 0;
    private SpriteRenderer spriteRenderer;
    public UIDocument clientDialogue;
    public DialogueEvents dialogueEvents;

    public void EndInteraction(Hand hand)
    {
        Debug.Log("interaction finished");
    }

    public void ExitHover(Hand hand)
    {
        Debug.Log("stopped hovering client");
    }

    public void HoverEnter(Hand hand)
    {
        Debug.Log("hovering client");
    }

    public bool Interact(Hand hand)
    {
        if (clientDialogue == null)
        {
            Debug.LogError("UIDocument is not assigned.");
            return false;
        }

        clientDialogue.gameObject.SetActive(!clientDialogue.gameObject.activeSelf);
        dialogueEvents.setClientOrder(dayQueue.queue[currentClient].order);
        dialogueEvents.setClientExplanation(dayQueue.queue[currentClient].explanation);
        return true;
    }

    public void nextClient()
    {
        if (currentClient < dayQueue.queue.Count - 1)
        {
            currentClient += 1;
            spriteRenderer.sprite = dayQueue.queue[currentClient].sprite;
            Interact(null);
        }
        else
        {
            Debug.Log("No more clients in the queue");
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = dayQueue.queue[currentClient].sprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
