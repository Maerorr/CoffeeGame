using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public abstract class Pickable : MonoBehaviour, IInteractable// IPointerDownHandler, IPointerEnterHandler, IPointerUpHandler, IPointerExitHandler
{
    protected bool isPicked = false;
    protected SnapZone snapZone;

    [SerializeField] private SpriteRenderer _mainSprite;
    private Rigidbody2D rb;
    public Collider2D collider;
    private Color initialColor;
    private float initialZ;

    private bool locked = false;

    private Guid colorTweenId = Guid.NewGuid();

    private int interactableLayer;
    private int pickedLayer;

    public UnityEvent onPickedUp;
    public UnityEvent onDropped;

    protected virtual void Start()
    {
        interactableLayer = LayerMask.NameToLayer("Interactable");
        pickedLayer = LayerMask.NameToLayer("HandPicked");
        initialColor = _mainSprite.color;
        initialZ = transform.position.z;
        rb = GetComponentInChildren<Rigidbody2D>();
        collider ??= GetComponentInChildren<Collider2D>();
    }

    public void Drop()
    {
        transform.SetParent(null);
        Vector3 pos = transform.position;
        transform.position = new Vector3(pos.x, pos.y, initialZ);
        if (rb is not null) rb.bodyType = RigidbodyType2D.Dynamic;
        gameObject.layer = interactableLayer;
        isPicked = false;
        onDropped.Invoke();
    }

    public bool Interact(Hand hand)
    {
        if (!hand.ComparePickable(this)) return false;
        if (isPicked)
        {
            hand.SetPickable(null);
        }
        else
        {
            transform.SetParent(hand.transform, true);
            hand.SetPickable(this);
            transform.localPosition = new Vector3(0f, 0f, 0f);
            if (rb is not null) rb.bodyType = RigidbodyType2D.Kinematic;
            gameObject.layer = pickedLayer;
            //collider.enabled = false;

            if (snapZone is not null) snapZone.Unsnap();
            snapZone = null;

            isPicked = true;
            onPickedUp.Invoke();
        }
        return true;
    }

    public abstract void HoverEnter(Hand hand);

    public abstract void ExitHover(Hand hand);

    protected void Highlight()
    {
        var colTween = _mainSprite.DOColor(initialColor * 1.05f, 0.1f);
        colTween.id = colorTweenId;
    }

    protected void EndHighlight()
    {
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
        gameObject.layer = interactableLayer;

        Vector3 position = t.position;
        position.z = transform.position.z;
        transform.position = position;
    }
    public bool IsSnapped()
    {
        return snapZone != null;
    }

    public void SetSnapZone(SnapZone sz)
    {
        snapZone = sz;
    }

    public void Lock()
    {
        locked = true;
    }

    public void Unlock()
    {
        locked = false;
    }
}
