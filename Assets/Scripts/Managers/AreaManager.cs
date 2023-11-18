using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AreaManager : MonoBehaviour
{
    [SerializeField] private DriveThruManager _driveThruManager;
    [SerializeField] private List<AmmoCreationArea> areaList = new List<AmmoCreationArea>();

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        foreach (AmmoCreationArea creationArea in areaList)
        {
            creationArea.Init(this);
        }
    }

    public void CheckLock(AmmoCreationArea ammoCreationArea)
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
        vehicleSpawner.UnlockVehicle(index,AllAreasLoaded());

    }

    private bool AllAreasLoaded()
    {
        bool allLoaded = true;
        for (int i = 0; i < areaList.Count; i++)
        {
            if (!areaList[i].IsLoaded()) allLoaded = false;
        }

        return allLoaded;
    }
}
