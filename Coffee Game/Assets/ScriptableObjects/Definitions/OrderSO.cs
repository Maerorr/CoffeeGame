using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Order", menuName = "ScriptableObject/Order")]
public class OrderSO : ScriptableObject
{
    public List<OrderableDrinks> orderDrinks;
}
