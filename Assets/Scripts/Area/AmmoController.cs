using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class AmmoController : MonoBehaviour, ICollectable
{
    public Type AmmoType;
    [SerializeField] private List<GameObject> _modelList = new List<GameObject>();
    public enum Type
    {
        Small,
        Medium,
        Heavy
    }

    private void Awake()
    {
        ActivateModel();
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

    public void GetCollected(Transform parent)
    {
        transform.SetParent(parent);
        transform.DOLocalMove(Vector3.zero, 0.25f);
    }

    public void GetTransferred(Transform parent)
    {
        transform.SetParent(parent);
        transform.DOLocalMove(Vector3.zero, 0.25f);
    }
}
