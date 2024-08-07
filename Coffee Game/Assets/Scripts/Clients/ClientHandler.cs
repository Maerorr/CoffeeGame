using UnityEngine;
using UnityEngine.UIElements;

public class ClientHandler : MonoBehaviour, IInteractable
{

    public UIDocument clientDialogue;

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
        return true;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
