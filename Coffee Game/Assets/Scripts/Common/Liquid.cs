using System;
using UnityEngine;

[Serializable]
public class Liquid
{
    [SerializeField] protected LiquidType _liquidType;

    public LiquidType liquidType
    {
        get => _liquidType;
    }

    public string name
    {
        get => _liquidType.ToString();
    }

    [SerializeField] protected float _amount;

    public float amount
    {
        get => _amount;
        set
        {
            if (value <= 0f) _amount = 0f;
            _amount = value;
        }
    }

    public Liquid(LiquidType lType, float val = 0f)
    {
        _liquidType = lType;
        amount = val;
    }

    public Color color
    {
        get => _liquidType.ToColor();
    }

    public void Add(float v)
    {
        _amount += v;
        _amount = Mathf.Clamp(_amount, 0f, float.MaxValue);
    }

    /// <summary>
    /// Calculates the liquid's quality. e.g. Espresso can have varying quality scores depending on amount of coffee/water used to create a single shot.
    /// </summary>
    /// <returns>float value representing a liquid's score from 0 (bad) to 1 (great). Defaults to 1f for liquids that don't have quality (e.g. water)</returns>
    public virtual float GetQuality()
    {
        return 1f;
    }

    public static Liquid Espresso(float val = 0f)
    {
        return new Liquid(LiquidType.Espresso, val);
    }

    public static Liquid Milk(float val = 0f)
    {
        return new Liquid(LiquidType.Milk, val);
    }

    public static Liquid MilkFoam(float val = 0f)
    {
        return new Liquid(LiquidType.MilkFoam, val);
    }
}

[Serializable]
public enum LiquidType
{
    Espresso,
    Milk,
    MilkFoam,
    Water
}

public static class LiquidTypeExtensions
{
    public static string ToString(this LiquidType liquidType) =>
        liquidType switch
        {
            LiquidType.Espresso => "Espresso",
            LiquidType.Milk => "Milk",
            LiquidType.MilkFoam => "Milk Foam",
            LiquidType.Water => "Water",
            _ => throw new ArgumentOutOfRangeException(nameof(liquidType), liquidType, null)
        };

    public static string ToStringNoWhitespaces(this LiquidType liquidType) =>
        liquidType switch
        {
            LiquidType.Espresso => "Espresso",
            LiquidType.Milk => "Milk",
            LiquidType.MilkFoam => "MilkFoam",
            LiquidType.Water => "Water",
            _ => throw new ArgumentOutOfRangeException(nameof(liquidType), liquidType, null)
        };


    public static Color ToColor(this LiquidType liquidType) =>
        liquidType switch
        {
            LiquidType.Espresso => Colors.Get(liquidType.ToStringNoWhitespaces()),
            LiquidType.Milk => Colors.Get(liquidType.ToStringNoWhitespaces()),
            LiquidType.MilkFoam => Colors.Get(liquidType.ToStringNoWhitespaces()),
            LiquidType.Water => Colors.Get(liquidType.ToStringNoWhitespaces()),
            _ => throw new ArgumentOutOfRangeException(nameof(liquidType), liquidType, null)
        };
}

