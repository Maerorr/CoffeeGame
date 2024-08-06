using System;
using UnityEngine;
using UnityEngine.Events;

public class MachineButton : MonoBehaviour, IInteractable
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField, Range(0.8f, 2f)] private float hoverHighlightAmount = 1.1f;
    [SerializeField, Range(0.8f, 2f)] private float pressHighlightAmount = 0.9f;
    private Color initialColor;
    
    public UnityEvent onPress;
    public UnityEvent onRelease;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        initialColor = spriteRenderer.color;
    }

    public bool Interact(Hand hand)
    {
        spriteRenderer.color = initialColor * pressHighlightAmount;
        Debug.Log("ON PRESS INVOKED");
        onPress.Invoke();
        return true;
    }

    public void EndInteraction()
    {
        spriteRenderer.color = initialColor;
        Debug.Log("ON RELEASE INVOKED");
        onRelease.Invoke();
    }

    public void HoverEnter(Hand hand)
    {
        spriteRenderer.color = initialColor * hoverHighlightAmount;
    }

    public void ExitHover()
    {
        spriteRenderer.color = initialColor;
    }
}
