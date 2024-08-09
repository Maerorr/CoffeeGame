using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

// This class is abstract only for the sake of serializing for the Unity Editor.
public abstract class SnapZone: MonoBehaviour, IInteractable
{
    [SerializeField] protected Transform snapPosition;
    protected Pickable pickable;
    
    [SerializeField] private int _id;

    public int id
    {
        get => _id;
        set
        {
            if (value >= 0)
            {
                _id = value;
            }
        }
    }
    
    public UnityEvent<Pickable, int> onPickableSnapped;
    public UnityEvent<Pickable, int> onPickableUnSnapped;
    
    private void Start()
    {
        snapPosition ??= transform;
    }
    
    public virtual bool Interact(Hand hand)
    {
        Debug.Log("PickableSnapZone");
        pickable = hand.GetPickableInHand();
        pickable.SetSnapZone(this);
        hand.SnapPickable(snapPosition);
        onPickableSnapped.Invoke(pickable, id);
        return true;
    }
    
    public void HoverEnter(Hand hand)
    {
    }

    public void ExitHover()
    {
    }

    public void EndInteraction()
    {
    }
    
    public Pickable GetPickable()
    {
        return pickable;
    }
    
    public void Unsnap()
    {
        onPickableUnSnapped.Invoke(pickable, id);
        pickable = null;
    }
}
