using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoCollector : MonoBehaviour
{
    [SerializeField] private List<AmmoController> _ammoControllerList = new List<AmmoController>();
    [SerializeField] private Transform _stackLocation;


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("triggered");
        if (other.TryGetComponent<AmmoController>(out AmmoController ammoController))
        {
            AddAmmo(ammoController);
        }
    }
    public void AddAmmo(AmmoController ammoController)
    {
        if (!ListAlreadyContains(ammoController))
        {
            _ammoControllerList.Add(ammoController);
            ammoController.GetCollected(_stackLocation);
        }
    }

    public void RemoveAmmo(AmmoController ammoController)
    {
        if (ListAlreadyContains(ammoController))
        {
            _ammoControllerList.Remove(ammoController);
        }    }

    private bool ListAlreadyContains(AmmoController ammoController)
    {
        bool alreadyInTheList = _ammoControllerList.Contains(ammoController);
        
        return alreadyInTheList;
    }
}
