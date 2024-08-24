// using UnityEngine;

// public class Espresso : Liquid
// {
//     private float groundsAmountInFilter;

//     public Espresso(float groundsInFilter, float val = 0) : base(LiquidType.Espresso, val)
//     {
//         groundsAmountInFilter = groundsInFilter;
//     }

//     public override float GetQuality()
//     {
//         float ratio = amount / groundsAmountInFilter; // closer to 2:1 is better, less or more gets lower score
//         return Mathf.Clamp01(-Mathf.Abs(ratio / 2f - 1f) + 1f);
//     }
// }