using System.Collections.Generic;
using UnityEngine;

public static class Colors
{
    static Dictionary<string, Color> colors = new Dictionary<string, Color>{
        {"espresso", new Color(0.6f, 0.318f, 0.063f)},
        {"milk", new Color(1f, 0.992f, 0.82f)},
        {"milk_foam", new Color(1f, 0.976f, 0.945f)},
        {"water", new Color(0.49f, 0.788f, 0.902f)}
    };

    static public Color Get(string name) {
        if (colors.ContainsKey(name)) {
            return colors[name];
        } else {
            return Color.white;
        }
    }
}
