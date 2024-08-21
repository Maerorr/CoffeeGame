using System.Collections.Generic;
using UnityEngine;

public static class Colors
{
    static Dictionary<string, Color> colors = new()
    {
        {"espresso", new Color(0.6f, 0.318f, 0.063f)},
        {"milk", new Color(1f, 0.992f, 0.82f)},
        {"milkfoam", new Color(1f, 0.876f, 0.745f)},
        {"water", new Color(0.49f, 0.788f, 0.902f)}
    };

    static public Color Get(string name)
    {
        string low = name.ToLower();
        if (colors.ContainsKey(low))
        {
            return colors[low];
        }
        else
        {
            return Color.white;
        }
    }
}
