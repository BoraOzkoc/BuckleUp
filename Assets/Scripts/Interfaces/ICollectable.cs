using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICollectable
{
    void GetCollected(ContainerController containerController);
    void GetTransferredToVehicle(Transform parent, VehicleController vehicleController, bool isLastAmmo);
}