using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class CoffeeBag : Pickable
{
    [SerializeField] CoffeeBeansSO beansData;

    [SerializeField] ParticleSystem beansParticles;

    private Transform tr;

    private BeanDropZone dropZone = null;

    private int rotateTweenID;
    private Coroutine velocityCoroutine;


    private new void Start()
    {
        base.Start();
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

    private IEnumerator CalculateVelocity()
    {
        Vector3 prevPos = tr.position;
        Vector3 currentPos;
        while (true)
        {
            currentPos = tr.position;
            float velocity = (prevPos - currentPos).magnitude;
            dropZone.BagVelocity = velocity;
            prevPos = currentPos;
            yield return null;
        }
    }

    public void StartPouringBeans()
    {
        beansParticles.Play();
        DOTween.Kill(rotateTweenID);
        var tween = tr.DOLocalRotateQuaternion(Quaternion.Euler(0f, 0f, 80f), 0.45f)
            .SetEase(Ease.OutBack)
            .SetId(this);
        rotateTweenID = tween.intId;

        velocityCoroutine = StartCoroutine(CalculateVelocity());
    }

    public void StopPouringBeans()
    {
        beansParticles.Stop();
        DOTween.Kill(rotateTweenID);
        var tween = tr.DOLocalRotateQuaternion(Quaternion.Euler(0f, 0f, 0f), 0.45f)
            .SetEase(Ease.OutBack)
            .SetId(this);
        rotateTweenID = tween.intId;
        dropZone = null;
        if (velocityCoroutine != null)
        {
            StopCoroutine(velocityCoroutine);
        }
    }

    public void SetDropZone(BeanDropZone zone)
    {
        dropZone = zone;
    }


}
