using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Drink", menuName = "ScriptableObject/Drink")]
public class DrinkSO : ScriptableObject
{
    public CupType cupType;
    public List<Liquid> liquids;

    // todo: possible extras?
}
