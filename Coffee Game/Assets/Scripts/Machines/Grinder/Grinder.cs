using System;
using System.Collections;
using System.Data.Common;
using Unity.VisualScripting;
using UnityEngine;

public class Grinder : MonoBehaviour
{
    private Portafilter _portafilter;
    private PortafilterSnapZone pSnap;

    [SerializeField] float grindSpeed = 3f;
    private Coroutine grindCoroutine;

    private float currentBeansAmount = 0f;
    [SerializeField] private float maxBeansAmount = 30f;
    [SerializeField] private MultiColorMeter beansMeter;

    private bool isGrinding = false;

    private void Start()
    {
        pSnap = GetComponentInChildren<PortafilterSnapZone>();
        pSnap.onPortafilterSnapped.AddListener(SetPortafilter);
        pSnap.onPickableUnSnapped.AddListener(PickableUnsnapped);
    }

    private void SetPortafilter(Portafilter p, int _)
    {
        _portafilter = p;
        _portafilter.ToggleMeterVisibility(true);
    }

    private void PickableUnsnapped(Pickable pick, int _)
    {
        if (pick is Portafilter p)
        {
            p.ToggleMeterVisibility(false);
            _portafilter = null;
        }
    }

    public IEnumerator Grind()
    {
        isGrinding = true;
        float beanVal = 0f;
        while (currentBeansAmount > 0f)
        {
            if (_portafilter is null) {
                StopGrinding();
                break;
            }
            beanVal = grindSpeed * Time.deltaTime;
            _portafilter.AddCoffee(beanVal);
            AddBeans(-beanVal);
            yield return new WaitForNextFrameUnit();
        }
    }

    public void GrindButton()
    {
        if (!isGrinding)
        {
            StartGrinding();
        }
        else
        {
            StopGrinding();
        }
    }

    private void StartGrinding()
    {
        if (_portafilter is null) return;
        grindCoroutine = StartCoroutine(Grind());
    }

    public void StopGrinding()
    {
        if (grindCoroutine is not null) StopCoroutine(grindCoroutine);
        isGrinding = false;
    }

    public void AddBeans(float beans)
    {
        currentBeansAmount += beans;
        currentBeansAmount = Mathf.Clamp(currentBeansAmount, 0f, maxBeansAmount);
        beansMeter.SetContent("beans", currentBeansAmount / maxBeansAmount, Colors.Get("espresso"));
    }
}
