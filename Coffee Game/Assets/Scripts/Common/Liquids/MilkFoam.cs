// using UnityEngine;

// public class MilkFoam : Liquid
// {
//     private const float TEMPERATURE_GOAL = 55f;
//     private const float SCORE_MARGIN = 25f;
//     private float temperature;
//     public MilkFoam(float temperature, float val = 0) : base(LiquidType.MilkFoam, val)
//     {
//         this.temperature = temperature;
//     }

//     public override float GetQuality()
//     {
//         return Mathf.Clamp01(-Mathf.Abs(amount / SCORE_MARGIN - (TEMPERATURE_GOAL / SCORE_MARGIN)) + 1f);
//     }
// }
