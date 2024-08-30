using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OrderScoreCalculator
{
    public static float CalculateMatchScore(Liquid ordered, Liquid prepared)
    {
        if (ordered.liquidType != prepared.liquidType)
        {
            return 0; // No match
        }

        float maxAmount = Math.Max(ordered.amount, prepared.amount);
        float difference = Math.Abs(ordered.amount - prepared.amount);
        return (maxAmount - difference) / maxAmount; // Normalized score
    }

    public static float CalculateDrinkScore(List<Liquid> order, List<Liquid> prepared)
    {
        float totalScore = 0;
        int matchCount = 0;

        foreach (var orderedLiquid in order)
        {
            var matchingLiquid = prepared.FirstOrDefault(l => l.liquidType == orderedLiquid.liquidType);
            if (matchingLiquid != null)
            {
                totalScore += CalculateMatchScore(orderedLiquid, matchingLiquid);
                matchCount++;
            }
            else
            {
                totalScore -= 0.5f; // Penalty for missing liquid
            }
        }

        if (prepared.Count > order.Count)
        {
            totalScore -= 0.5f * (prepared.Count - order.Count); // Penalty for extra liquids
        }

        return totalScore / order.Count;
    }

    public static float CalculateOrderScore(OrderSO orderSO, List<List<Liquid>> preparedDrinks)
    {
        List<List<Liquid>> order = new();
        foreach (var orderDrink in orderSO.orderDrinks)
        {
            var list = orderDrink.ToLiquidList();
            order.Add(list);
        }

        if (order.Count != preparedDrinks.Count)
        {
            throw new ArgumentException("The number of ordered drinks and prepared drinks must match.");
        }

        int drinkCount = order.Count;
        var scores = new float[drinkCount, drinkCount];

        // Calculate all pairwise scores
        for (int i = 0; i < drinkCount; i++)
        {
            for (int j = 0; j < drinkCount; j++)
            {
                scores[i, j] = CalculateDrinkScore(order[i], preparedDrinks[j]);
            }
        }

        // Find the optimal matching using a greedy approach
        var usedOrders = new bool[drinkCount];
        var usedPrepared = new bool[drinkCount];
        float totalScore = 0;

        for (int k = 0; k < drinkCount; k++)
        {
            float maxScore = -1;
            int bestOrder = -1, bestPrepared = -1;

            for (int i = 0; i < drinkCount; i++)
            {
                if (usedOrders[i]) continue;

                for (int j = 0; j < drinkCount; j++)
                {
                    if (usedPrepared[j]) continue;

                    if (scores[i, j] > maxScore)
                    {
                        maxScore = scores[i, j];
                        bestOrder = i;
                        bestPrepared = j;
                    }
                }
            }

            if (bestOrder != -1 && bestPrepared != -1)
            {
                usedOrders[bestOrder] = true;
                usedPrepared[bestPrepared] = true;
                totalScore += maxScore;
            }
        }

        return totalScore / drinkCount; // Average score across all drinks
    }
}
