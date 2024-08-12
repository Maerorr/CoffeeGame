using System;
using UnityEngine;

public class Liquid
{
    LiquidType _liquidType;

    public LiquidType liquidType {
        get => _liquidType;
    }

    public string name {
        get => _liquidType.ToString();
    }

    float _amount;

    public float amount {
        get => _amount;
        set {
            if (value <= 0f) _amount = 0f;
            _amount = value;
        }
    }

    public Liquid(LiquidType lType, float val = 0f) {
        _liquidType = lType;
        amount = val;
    }

    public Color color {
        get => _liquidType.ToColor();
    }

    public void Add(float v) {
        _amount += v;
        _amount = Mathf.Clamp(_amount, 0f, float.MaxValue);
    }
}

public enum LiquidType {
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
            LiquidType.Espresso => Colors.Get(liquidType.ToStringNoWhitespaces()), // Dark brown color for espresso
            LiquidType.Milk     => Colors.Get(liquidType.ToStringNoWhitespaces()),           // White color for milk
            LiquidType.MilkFoam => Colors.Get(liquidType.ToStringNoWhitespaces()),// Off-white color for milk foam
            LiquidType.Water    => Colors.Get(liquidType.ToStringNoWhitespaces()),      // Light blue color for water
            _ => throw new ArgumentOutOfRangeException(nameof(liquidType), liquidType, null)
        };
}