using System;
using System.Collections.Generic;

[Serializable]
public enum OrderableDrinks
{
    Espresso,
    EspressoDoppio,
    Cappuccino,
    Latte,
}

public static class OrderableDrinksExtensions
{
    // Converts enum to string
    public static string ToString(this OrderableDrinks drink)
    {
        switch (drink)
        {
            case OrderableDrinks.Espresso:
                return "Espresso";
            case OrderableDrinks.EspressoDoppio:
                return "Espresso Doppio";
            case OrderableDrinks.Cappuccino:
                return "Cappuccino";
            case OrderableDrinks.Latte:
                return "Latte";
            default:
                throw new ArgumentOutOfRangeException(nameof(drink), drink, null);
        }
    }

    public static string ToStringNoWhitespaces(this OrderableDrinks drink)
    {
        switch (drink)
        {
            case OrderableDrinks.Espresso:
                return "Espresso";
            case OrderableDrinks.EspressoDoppio:
                return "EspressoDoppio";
            case OrderableDrinks.Cappuccino:
                return "Cappuccino";
            case OrderableDrinks.Latte:
                return "Latte";
            default:
                throw new ArgumentOutOfRangeException(nameof(drink), drink, null);
        }
    }

    // Converts string to enum
    public static OrderableDrinks FromString(this string drinkString)
    {
        switch (drinkString)
        {
            case "Espresso":
                return OrderableDrinks.Espresso;
            case "EspressoDoppio":
                return OrderableDrinks.EspressoDoppio;
            case "Cappuccino":
                return OrderableDrinks.Cappuccino;
            case "Latte":
                return OrderableDrinks.Latte;
            default:
                throw new ArgumentException($"Unknown drink type: {drinkString}");
        }
    }

    public static List<Liquid> ToLiquidList(this OrderableDrinks drink)
    {
        List<Liquid> list = new();
        switch (drink)
        {
            case OrderableDrinks.Espresso:
                list.Add(new Liquid(LiquidType.Espresso, 20f));
                return list;
            case OrderableDrinks.EspressoDoppio:
                list.Add(new Liquid(LiquidType.Espresso, 40f));
                return list;
            case OrderableDrinks.Cappuccino:
                list.Add(new Liquid(LiquidType.Espresso, 20f));
                list.Add(new Liquid(LiquidType.Milk, 100f));
                list.Add(new Liquid(LiquidType.MilkFoam, 100f));
                return list;
            case OrderableDrinks.Latte:
                list.Add(new Liquid(LiquidType.Espresso, 20f));
                list.Add(new Liquid(LiquidType.MilkFoam, 200f));
                return list;
            default:
                throw new ArgumentOutOfRangeException(nameof(drink), drink, null);
        }
    }
}