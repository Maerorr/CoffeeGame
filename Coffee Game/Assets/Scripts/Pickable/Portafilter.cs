using UnityEngine;

public class Portafilter : Pickable
{
    private float coffeeAmount = 0f;
    private bool used = false;

    public void AddCoffee(float c)
    {
        coffeeAmount += c;
    }

    public void UseCoffee()
    {
        used = true;
    }

    public void Clear()
    {
        coffeeAmount = 0f;
        used = false;
    }
}
