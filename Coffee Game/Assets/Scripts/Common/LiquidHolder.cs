using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class LiquidHolder
{
    public List<Liquid> liquids = new List<Liquid>();
    private float capacity;

    public float Capacity
    {
        get => capacity;
        set
        {
            if (value < 0f)
            {
                capacity = 0;
            }
            else
            {
                capacity = value;
            }
        }
    }

    public UnityEvent onLiquidsChange = new UnityEvent();

    /// <summary>
    /// Add liquid to the list. Note that this does not clone the object so they list holds given references.
    /// If adding new liquid would overflow capacity. Only the fitting part is added and the rest is discarded.
    /// </summary>
    /// <param name="liquid"></param>
    public void AddLiquid(Liquid liquid)
    {
        float currentAndNew = GetTotalLiquidAmount() + liquid.amount;
        if (currentAndNew > capacity)
        {
            liquid.amount = Mathf.Clamp(currentAndNew - capacity, 0f, float.MaxValue);
        }

        int idx = liquids.FindIndex(el => el.name == liquid.name);
        if (idx == -1)
        {
            liquids.Add(liquid);
            onLiquidsChange.Invoke();
            return;
        }
        liquids[idx].Add(liquid.amount);
        onLiquidsChange.Invoke();
    }

    public void SubtractLiquid(Liquid liquid)
    {
        int idx = liquids.FindIndex(el => el.name == liquid.name);
        if (idx == -1)
        {
            return;
        }
        liquids[idx].amount -= liquid.amount;
        if (liquids[idx].amount < 0.0001f)
        {
            liquids.Remove(liquids[idx]);
        }
        onLiquidsChange.Invoke();
    }

    public Liquid GetByName(string name)
    {
        int idx = liquids.FindIndex(el => el.name == name);
        if (idx == -1)
        {
            Debug.LogWarning($"liquid {name} not found returning NULL!");
            return null;
        };
        return liquids[idx];
    }

    public float GetTotalLiquidAmount()
    {
        return liquids.Sum(l => l.amount);
    }

    public bool IsFull()
    {
        return GetTotalLiquidAmount() == capacity;
    }

    public override string ToString()
    {
        string output = "";
        foreach (Liquid l in liquids)
        {
            output += $"[{l.name}: {l.amount}ml.]";
        }

        return output;
    }

    public Liquid GetTopMostLiquid()
    {
        if (liquids.Count == 0) return null;
        return liquids.Last();
    }
}
