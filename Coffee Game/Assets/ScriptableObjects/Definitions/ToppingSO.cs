using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Topping", menuName = "ScriptableObject/Topping")]
public class ToppingSO : ScriptableObject
{
    public string name;
    public Sprite sprite;
    public String description;
}
