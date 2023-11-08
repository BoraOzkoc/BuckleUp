using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class AreaController : MonoBehaviour, IInteractable
{
    [SerializeField] private Image _fillImage;
    [SerializeField] private bool _playerEntered;
    private Tween _fillTween;
    private Coroutine _timerCoroutine;

    protected virtual void Awake()
    {
        Init();
    }

    private void Init()
    {
        _fillImage.fillAmount = 0;

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<AmmoCollector>(out AmmoCollector ammoCollector))
        {
            AreaEntered();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<AmmoCollector>(out AmmoCollector ammoCollector))
        {
            AreaExit();
        }
    }

    protected bool IsPlayerEntered()
    {
        return _playerEntered;
    }
    public virtual void TriggerArea()
    {
    }
    public void AreaEntered()
    {
        _playerEntered = true;
        _fillTween = _fillImage.DOFillAmount(1, 1f).OnComplete(() =>
        {
            if (_timerCoroutine == null)
            {
                _timerCoroutine = StartCoroutine(SpawnTimerCoroutine());
            }
        });
        
    }

    public void AreaExit()
    {
        _playerEntered = false;
        _fillImage.DOKill();
        _fillImage.fillAmount = 0;
    }

    private void ResetTimer()
    {
        
    }
    IEnumerator SpawnTimerCoroutine()
    {
        while (IsPlayerEntered())
        {
            TriggerArea();

            yield return new WaitForSeconds(1);
            
        }

        _timerCoroutine = null;
    }
}
