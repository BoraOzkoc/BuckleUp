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
            int index = _vehicleController.GetIndex();
            ContainerController containerController = _containerManager.GetContainer(index);
            int difference = containerController.GetAmmoCount() - _vehicleController.GetOrderAmount();
            if (difference > 0)
            {
                containerController.TransferAmmo(_vehicleController);
            }
            else if (containerController.GetAmmoCount() == 0)
            {
                Debug.Log("Container is Empty");
            }
            else
            {
                for (int i = 0; i < containerController.GetAmmoCount(); i++)
                {
                    containerController.TransferAmmo(_vehicleController);

                }
                
            }
        }
    }
}
