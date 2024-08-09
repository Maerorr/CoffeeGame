using UnityEngine;
using UnityEngine.UIElements;

public class ClientHandler : MonoBehaviour, IInteractable
{

    // serialize field do scriptable object
    // dodac sprite renderer z obiektu Client

    [SerializeField]
    private ClientSO client;
    private SpriteRenderer spriteRenderer;
    public UIDocument clientDialogue;
    public DialogueEvents dialogueEvents;

    public void EndInteraction()
    {
        Debug.Log("interaction finished");
    }

    public void ExitHover()
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
        dialogueEvents.setClientOrder(client.order);
        dialogueEvents.setClientExplanation(client.explanation);
        return true;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = client.sprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
