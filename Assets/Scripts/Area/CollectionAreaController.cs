using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionAreaController : MonoBehaviour
{
    [SerializeField] private AmmoCreationArea _ammoCreationArea;
    [SerializeField] private Transform CollectionBackground;
    [Header("Container Controller")] [SerializeField]
    private ContainerController _containerController;
    private void OnValidate()
    {
        CollectionBackground.position = transform.position;
    }

    public ContainerController GetContainerController()
    {
        return _containerController;
    }
    public List<AmmoController> CollectAmmoList()
    {
        List<AmmoController> tempAmmoList = new List<AmmoController>(_ammoCreationArea.GetAmmoList());
        _ammoCreationArea.ResetAmmoList();
        return tempAmmoList;
    }
    
}
