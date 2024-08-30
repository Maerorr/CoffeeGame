using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OrderTray : MonoBehaviour
{
    private List<PickableSnapZone> snaps = new();
    [SerializeField] private OrderSO order;

    private List<Cup> cups = new();

    private void Start()
    {
        GetComponentsInChildren(snaps);
        foreach (var snapzone in snaps)
        {
            snapzone.onPickableSnapped.AddListener(PickableSnapped);
            snapzone.onPickableUnSnapped.AddListener(PickableUnsnapped);
        }

        // espresso
        List<Liquid> espresso = new();
        espresso.Add(Liquid.Espresso(20f));

        List<Liquid> espressoDoppio = new();
        espressoDoppio.Add(Liquid.Espresso(30f));

        List<List<Liquid>> list = new List<List<Liquid>> {
            espresso, espressoDoppio
        };
        float score = OrderScoreCalculator.CalculateOrderScore(order, list);
        Debug.Log($"Score (correct order of liquids): {score}");

        Debug.Log("#####################");

        list = new List<List<Liquid>> {
            espressoDoppio, espresso
        };
        score = OrderScoreCalculator.CalculateOrderScore(order, list);
        Debug.Log($"Score (reverse order of liquids): {score}");
    }

    public void PickableSnapped(Pickable p, int id)
    {
        switch (p)
        {
            case Cup c:
                cups.Add(c);
                break;
            default:
                break;
        }
    }

    public void PickableUnsnapped(Pickable p, int id)
    {
        cups.Remove(p as Cup);
    }

    private void CalculateOrderScore(OrderSO order)
    {
        float totalOrderScore = 0f;
        List<int> alreadyChecked = new();
        foreach (var drink in order.orderDrinks)
        {
            for (int i = 0; i < order.orderDrinks.Count; i++)
            {

            }
        }
    }


}
