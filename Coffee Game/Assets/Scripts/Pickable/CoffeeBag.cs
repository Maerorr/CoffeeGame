using System;
using DG.Tweening;
using UnityEngine;

public class CoffeeBag : Pickable
{
    [SerializeField] CoffeeBeansSO beansData;

    [SerializeField] ParticleSystem beansParticles;

    private Transform tr;

    Guid guid;

    private new void Start()
    {
        base.Start();
        guid = Guid.NewGuid();
        tr = transform;
    }

    public override void ExitHover(Hand hand)
    {
        StopPouringBeans();
        EndHighlight();
    }

    public override void HoverEnter(Hand hand)
    {
        Highlight();
    }

    public void StartPouringBeans()
    {
        //tr.rotation = Quaternion.Euler(0f, 0f, 45f);
        beansParticles.Play();
        DOTween.Kill(guid);
        var tween = tr.DOLocalRotateQuaternion(Quaternion.Euler(0f, 0f, 80f), 0.45f).SetEase(Ease.OutBack);
        tween.SetId(guid);
    }

    public void StopPouringBeans()
    {
        beansParticles.Stop();
        //tr.rotation = Quaternion.Euler(0f, 0f, 0f);
        DOTween.Kill(guid);
        var tween = tr.DOLocalRotateQuaternion(Quaternion.Euler(0f, 0f, 0f), 0.45f).SetEase(Ease.OutBack);
        tween.SetId(guid);
    }
}
