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

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _fillImage.fillAmount = 0;

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerMovement>(out PlayerMovement playerMovement))
        {
            AreaEntered();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<PlayerMovement>(out PlayerMovement playerMovement))
        {
            AreaExit();
        }
    }

    public virtual void TriggerArea()
    {
        Debug.Log("base executed");
    }
    public void AreaEntered()
    {
        _playerEntered = true;
        _fillTween = _fillImage.DOFillAmount(1, 1f).OnComplete(TriggerArea);
    }

    public void AreaExit()
    {
        _playerEntered = false;
        _fillImage.DOKill();
        _fillImage.fillAmount = 0;
    }
}
