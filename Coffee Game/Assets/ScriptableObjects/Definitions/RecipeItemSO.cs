using System;
using UnityEngine;

[CreateAssetMenu(fileName = "RecipeItem", menuName = "ScriptableObject/RecipeItem")]
public class RecipeItemSO : ScriptableObject
{
    public string name;
    public Sprite sprite;
    public String description;
}