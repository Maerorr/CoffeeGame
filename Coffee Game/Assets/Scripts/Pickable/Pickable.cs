using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Pickable : MonoBehaviour, IInteractable// IPointerDownHandler, IPointerEnterHandler, IPointerUpHandler, IPointerExitHandler
{
    protected bool isPicked = false;

    [SerializeField] private SpriteRenderer _mainSprite;
    private Rigidbody2D rb;
    public Collider2D collider;
    private Color initialColor;

    private Guid colorTweenId = System.Guid.NewGuid();

    private void Start()
    {
        initialColor = _mainSprite.color;
        rb = GetComponentInChildren<Rigidbody2D>();
        collider = GetComponentInChildren<Collider2D>();
    }

    public void PickUp(Transform parent)
    {
        transform.SetParent(parent, true);
        transform.localPosition = new Vector3(0f, 0f, transform.localPosition.z);
        if (rb is null) return;
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    public void Drop()
    {
        transform.SetParent(null);
        if (rb is not null) rb.bodyType = RigidbodyType2D.Dynamic;
        collider.enabled = true;
        isPicked = false;
    }

    public bool Interact(Hand hand)
    {
        if (!hand.ComparePickable(this)) return false;
        if (isPicked)
        {
            transform.SetParent(null);
            if (rb is not null) rb.bodyType = RigidbodyType2D.Dynamic;
            collider.enabled = true;
            isPicked = false;
            hand.SetPickable(null);
        }
        else
        {
            transform.SetParent(hand.transform, true);
            hand.SetPickable(this);
            transform.localPosition = new Vector3(0f, 0f, transform.localPosition.z);
            if (rb is not null) rb.bodyType = RigidbodyType2D.Kinematic;
            collider.enabled = false;
            isPicked = true;
        }
        return true;
    }

    public void HoverEnter(Hand hand)
    {
        //Debug.Log("HOVER ENTER");
        var colTween = _mainSprite.DOColor(initialColor * 1.05f, 0.1f);
        colTween.id = colorTweenId;
    }

    public void ExitHover()
    {
        //Debug.Log("EXIT HOVER");
        DOTween.Kill(colorTweenId);
        var colTween = _mainSprite.DOColor(initialColor, 0.1f);
        colTween.id = colorTweenId;
    }
    
    public void EndInteraction()
    {
        
    }

    public void SnapTo(Transform t)
    {
        transform.SetParent(null);
        collider.enabled = true;
        isPicked = false;
        
        Vector3 position = t.position;
        position.z = transform.position.z;
        transform.position = position;
    }
}
