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
        onPress.Invoke();
        return true;
    }

    public void EndInteraction(Hand hand)
    {
        spriteRenderer.color = initialColor;
        onRelease.Invoke();
    }

    public void HoverEnter(Hand hand)
    {
        spriteRenderer.color = initialColor * hoverHighlightAmount;
    }

    public void ExitHover(Hand hand)
    {
        spriteRenderer.color = initialColor;
    }
}
