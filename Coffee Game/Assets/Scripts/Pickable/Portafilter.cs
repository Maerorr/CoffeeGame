using UnityEngine;

public class Portafilter : Pickable
{
    private float coffeeAmount = 0f;
    private bool used = false;
    public GameObject coffeeGrounds;
    public GameObject liquid;

    private new void Start()
    {
        base.Start();
        
        coffeeGrounds.SetActive(false);
        liquid.SetActive(false);
    }
    
    public void AddCoffee(float c)
    {
        coffeeGrounds.SetActive(true);
        coffeeAmount += c;
    }

    public void ToggleLiquid(bool show)
    {
        liquid.SetActive(show);
    }
    
    public void UseCoffee()
    {
        used = true;
    }

    public void Clear()
    {
        coffeeAmount = 0f;
        coffeeGrounds.SetActive(false);
        used = false;
    }
}
