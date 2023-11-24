using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradeAreaController : MonoBehaviour
{
    [SerializeField] private ContainerManager _containerManager;
    private VehicleController _vehicleController;

    public void SetVehicle(VehicleController vehicleController)
    {
        _vehicleController = vehicleController;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<AmmoCollector>(out AmmoCollector ammoCollector))
        {
            int index = _vehicleController.GetIndex();
            ContainerController containerController = _containerManager.GetContainer(index);
            if (containerController.GotAmmo())
            {
                containerController.TransferAmmo(_vehicleController);
            }
        }
    }
}
