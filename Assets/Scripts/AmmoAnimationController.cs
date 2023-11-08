using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;

public class AmmoAnimationController : MonoBehaviour
{
    private Tween _rotationTween, _moveTween;

    private void Awake()
    {
        StartAnimation();
    }

    private void StartAnimation()
    {
        //_moveTween = transform.DOMoveY(1, 0.55f).SetEase(Ease.InCirc).SetLoops(-1, LoopType.Yoyo);
        _rotationTween = transform.DORotate(new Vector3(0, 360, 0), 2, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear).SetRelative()
            .SetLoops(-1, LoopType.Incremental);
    }

    public void StopAnimation()
    {
        _moveTween.Kill();
        _rotationTween.Kill();
        transform.rotation = quaternion.identity;
    }
}