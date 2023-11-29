using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using DG.Tweening;
using Mono.CompilerServices.SymbolWriter;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AreaController : MonoBehaviour, IInteractable
{
    public Type AmmoType;
    [SerializeField] private Image _fillImage;
    private bool _playerEntered;
    private Tween _fillTween;
    private Coroutine _timerCoroutine;
    private AmmoCollector _ammoCollector;

    public enum Type
    {
        Small,
        Medium,
        Heavy
    }

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
            _ammoCollector = ammoCollector;
            AreaEntered();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<AmmoCollector>(out AmmoCollector ammoCollector))
        {
            _ammoCollector = null;
            AreaExit();
        }
    }

    protected bool IsPlayerEntered()
    {
        return _playerEntered;
    }

    public virtual bool TriggerArea(AmmoCollector ammoCollector)
    {
        return true;
    }

    public virtual void LockArea()
    {
    }

    public virtual void UnlockArea()
    {
    }

    public virtual void CheckLock()
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

    IEnumerator SpawnTimerCoroutine()
    {
        while (IsPlayerEntered())
        {
            bool canLoop = TriggerArea(_ammoCollector);
            if (canLoop)
            {
                yield return new WaitForSeconds(1);
            }
            else
            {
                yield return null;
            }
        }

        _timerCoroutine = null;
    }
}