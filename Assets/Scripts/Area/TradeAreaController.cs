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

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent<AmmoCollector>(out AmmoCollector ammoCollector))
        {
            int vehicleNeededAmmoCount = _vehicleController.GetOrderAmount();
            if (vehicleNeededAmmoCount > 0)
            {
                int index = _vehicleController.GetIndex();
                ContainerController containerController = _containerManager.GetContainer(index);
                int containerCount = containerController.GetAmmoCount();

                if (containerCount > 0)
                {
                    if (containerController.GetAmmoCount() > vehicleNeededAmmoCount)
                    {
                        containerController.TransferAmmo(_vehicleController, vehicleNeededAmmoCount);
                        _vehicleController.ChangeNeededAmmoAmount(-vehicleNeededAmmoCount);
                    }
                    else if (containerController.GetAmmoCount() == 0)
                    {
                        containerController.TriggerWarning();
                    }
                    else
                    {
                        containerController.TransferAmmo(_vehicleController, containerCount);
                        _vehicleController.ChangeNeededAmmoAmount(-containerCount);
                    }
                }
            }
        }
    }
}