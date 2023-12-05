using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Mono.CompilerServices.SymbolWriter;
using UnityEngine;

public class AmmoController : MonoBehaviour, ICollectable
{
    public Type AmmoType;
    [SerializeField] private List<GameObject> _modelList = new List<GameObject>();
    private AmmoAnimationController _ammoAnimationController;
    public enum Type
    {
        Small,
        Medium,
        Heavy
    }

    public void PreapareAmmo(int index)
    {
        AmmoType = (Type)index;
        ActivateModel();
    }
    private void Awake()
    {
        _ammoAnimationController = GetComponent<AmmoAnimationController>();
    }

    private void ActivateModel()
    {
        for (int i = 0; i < _modelList.Count; i++)
        {
            int index = (int)AmmoType;
            if (i != index)
            {
                _modelList[i].SetActive(false);
            }
            else _modelList[i].SetActive(true);
            
        }
    }

    
    public void GetCollected(ContainerController containerController)
    {
        containerController.AddAmmo(this);
        transform.SetParent(containerController.transform);
        transform.DOLocalJump(Vector3.zero, 1,1,0.25f);
        
    }

    public void GetTransferred(Transform parent)
    {
        transform.SetParent(parent);
        transform.DOLocalMove(Vector3.zero, 0.25f);
    }

    private void OnValidate()
    {
        ActivateModel();
    }
}
