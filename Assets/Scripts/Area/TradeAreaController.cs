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
            if (_vehicleController.GetOrderAmount() > 0)
            {
                int index = _vehicleController.GetIndex();
                ContainerController containerController = _containerManager.GetContainer(index);
                int difference = containerController.GetAmmoCount() - _vehicleController.GetOrderAmount();
                if (difference > 0)
                {
                    containerController.TransferAmmo(_vehicleController, _vehicleController.GetOrderAmount());
                    _vehicleController.ChangeNeededAmmoAmount(-_vehicleController.GetOrderAmount());
                }
                else if (containerController.GetAmmoCount() == 0)
                {
                    containerController.TriggerWarning();
                }
                else
                {
                    containerController.TransferAmmo(_vehicleController, containerController.GetAmmoCount());
                    _vehicleController.ChangeNeededAmmoAmount(-containerController.GetAmmoCount());
                }
            }
        }
    }
}