using UnityEngine;
using UnityEngine.InputSystem;

public class Hand : MonoBehaviour
{
    private Pickable _handPickable = null;
    private GameObject currentRayCastedObject = null;
    private IInteractable currentInteractable = null;

    [SerializeField] LayerMask handRaycastLayerMask;

    public void OnLeftClick(InputAction.CallbackContext ctx)
    {
        if (ctx.phase == InputActionPhase.Canceled)
        {
            if (currentInteractable is null) return;
            currentInteractable.EndInteraction();
            return;
        }

        if (ctx.phase != InputActionPhase.Performed) return;
        if (_handPickable is not null)
        {
            ClickHeldItem();
        }
        else
        {
            ClickEmptyHand();
        }
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        Vector2 v = ctx.ReadValue<Vector2>();
        Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(v.x, v.y, 0.5f));
        transform.position = new Vector3(pos.x, pos.y, transform.position.z);
        
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.zero, Mathf.Infinity, handRaycastLayerMask);
        if (hit.collider is null)
        {
            if (currentInteractable is not null)
            {
                currentInteractable.EndInteraction();
                currentInteractable.ExitHover();
            }
            currentInteractable = null;
            currentRayCastedObject = null;
            return;
        }
        //Debug.Log($"RAYCAST HIT: {hit.transform.name}");
        
        // the interactable can be a child or a parent of the current collider
        IInteractable interactable;
        interactable = hit.transform.GetComponentInChildren<IInteractable>();
        interactable ??= hit.transform.GetComponentInParent<IInteractable>();

        if (interactable is not null)
        {
            currentInteractable = interactable;
            // '==' is a REFERENCE comparison, which is what we want here
            if (hit.transform.gameObject != currentRayCastedObject)
            {
                interactable.HoverEnter(this);
            }
        } else {
            currentInteractable.EndInteraction();
            currentInteractable.ExitHover();
            currentInteractable = null;
        }

        currentRayCastedObject = hit.transform.gameObject;
    }
    
    private void ClickEmptyHand()
    {
        if (currentInteractable is not null)
        {
            currentInteractable.ExitHover();
            currentInteractable.Interact(this);
        }
    }

    private void ClickHeldItem()
    {
        if (currentInteractable is null)
        {
            // if we're pointing into nothing, then click can drop the thing
            _handPickable.Drop();
            _handPickable = null;
        }
        else // if we're pointing at something that might want to interact with whatever we're carrying, let it interact
        {
            currentInteractable.Interact(this);
        }
        
    }

    /// <summary>
    /// Compares given Pickable item to the Pickable that's currently being held in hand.
    /// </summary>
    /// <param name="p">Pickable to compare agains the Hand's Pickable</param>
    /// <returns>True if the pickables are the same or if hand is empty (is null). False if the pickables are not the same.</returns>
    public bool ComparePickable(Pickable p)
    {
        if (_handPickable is null) return true;
        return p == _handPickable;
    }

    public void SetPickable(Pickable p)
    {
        _handPickable = p;
    }

    public Pickable GetPickableInHand()
    {
        return _handPickable;
    }

    public void SnapPickable(Transform t)
    {
        if (_handPickable is null) return;
        _handPickable.SnapTo(t);
        _handPickable = null;
    }
}
