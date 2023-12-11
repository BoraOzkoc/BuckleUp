using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class IndicatorController : MonoBehaviour
{
    public static IndicatorController Instance;
    [SerializeField] private GameObject _indicator;
    private Tween _bounceTween;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.Log("Destroying duplicate IndicatorController Instance");
            Destroy(this);
        }
        else
        {
            //Debug.Log("Setting IndicatorController Instance");
            Instance = this;
        }
    }

    public void ActivateIndicator(Vector3 pos)
    {
        pos.y += 5;
        _indicator.transform.position = pos;
        _indicator.SetActive(true);
        StartIndicatorAnimation();
    }

    public void DisableIndicator()
    {
        StopIndicatorAnimation();
        _indicator.SetActive(false);
        _indicator.transform.position = transform.position;
    }

    private void StartIndicatorAnimation()
    {
        _bounceTween = _indicator.transform.DOMoveY(transform.position.y + 1, 1).SetEase(Ease.InBack)
            .SetLoops(-1, LoopType.Yoyo);
    }

    private void StopIndicatorAnimation()
    {
        _bounceTween?.Kill();
    }
}