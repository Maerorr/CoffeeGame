using System;
using DG.Tweening;
using UnityEngine;

public abstract class Pickable : MonoBehaviour, IInteractable// IPointerDownHandler, IPointerEnterHandler, IPointerUpHandler, IPointerExitHandler
{
    protected bool isPicked = false;
    protected SnapZone snapZone;

    [SerializeField] private SpriteRenderer _mainSprite;
    private Rigidbody2D rb;
    public Collider2D collider;
    private Color initialColor;
    private float initialZ;
    
    private Guid colorTweenId = System.Guid.NewGuid();

    protected void Start()
    {
        initialColor = _mainSprite.color;
        initialZ = transform.position.z;
        rb = GetComponentInChildren<Rigidbody2D>();
        collider = GetComponentInChildren<Collider2D>();
    }
    
    public void Drop()
    {
        transform.SetParent(null);
        Vector3 pos = transform.position;
        transform.position = new Vector3(pos.x, pos.y, initialZ);
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
            Vector3 pos = transform.position;
            transform.position = new Vector3(pos.x, pos.y, initialZ);
            if (rb is not null) rb.bodyType = RigidbodyType2D.Dynamic;
            collider.enabled = true;
            isPicked = false;
            hand.SetPickable(null);
        }
        else
        {
            transform.SetParent(hand.transform, true);
            hand.SetPickable(this);
            transform.localPosition = new Vector3(0f, 0f, 0f);
            if (rb is not null) rb.bodyType = RigidbodyType2D.Kinematic;
            collider.enabled = false;
            
            if (snapZone is not null) snapZone.Unsnap();
            snapZone = null;
            
            isPicked = true;
        }
        return true;
    }
    
    public abstract void HoverEnter(Hand hand);

    public abstract void ExitHover(Hand hand);

    
    protected void Highlight() {
        var colTween = _mainSprite.DOColor(initialColor * 1.05f, 0.1f);
        colTween.id = colorTweenId;
    }

    protected void EndHighlight() {
        DOTween.Kill(colorTweenId);
        var colTween = _mainSprite.DOColor(initialColor, 0.1f);
        colTween.id = colorTweenId;
    }
    
    public void EndInteraction(Hand hand)
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

    public void SetSnapZone(SnapZone sz)
    {
        snapZone = sz;
    }
}
