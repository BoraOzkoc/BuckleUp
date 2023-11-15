using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaManager : MonoBehaviour
{
    [SerializeField] private DriveThruManager _driveThruManager;
    [SerializeField] private List<AmmoCreationArea> areaList = new List<AmmoCreationArea>();

    private void CheckLock(AmmoCreationArea ammoCreationArea)
    {
        VehicleSpawner vehicleSpawner = _driveThruManager.GetVehicleSpawner();
        int index = 0;
        switch (ammoCreationArea.AmmoType)
        {
            case AreaController.Type.Small:
                index = 0;
                break;
            case AreaController.Type.Medium:
                index = 1;
                break;
            case AreaController.Type.Heavy:
                index = 2;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        vehicleSpawner.UnlockVehicle(index);

    }
}
