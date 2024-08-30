using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Order", menuName = "ScriptableObject/Order")]
public class Order : ScriptableObject
{
    public List<DrinkSO> orderDrinks;
}
