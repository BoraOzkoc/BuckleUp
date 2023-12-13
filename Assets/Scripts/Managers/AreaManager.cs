using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaManager : MonoBehaviour
{
    [SerializeField] private DriveThruManager _driveThruManager;
    [SerializeField] private List<AmmoCreationArea> areaList = new List<AmmoCreationArea>();

    private AreaOrderController _areaOrderController;


    private void Awake()
    {
        Init();
        _areaOrderController = GetComponent<AreaOrderController>();

    }

    private void OnEnable()
    {
        SaveManager.LoadCompleted += TriggerVehicles;
        SaveManager.LoadCompleted += TriggerAreas;
    }

    private void OnDisable()
    {
        SaveManager.LoadCompleted -= TriggerVehicles;
        SaveManager.LoadCompleted -= TriggerAreas;
    }

    public AreaOrderController GetAreaOrderController()
    {
        return _areaOrderController;
    }
    private void Init()
    {
        foreach (AmmoCreationArea creationArea in areaList)
        {
            creationArea.Init(this);
        }
    }

    public List<AmmoCreationArea> GetAreaList()
    {
        return areaList;
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
        if (!ammoCreationArea.IsLocked()) vehicleSpawner.UnlockVehicle(index);
        
    }
    
    public void TriggerVehicles()
    {
        VehicleSpawner vehicleSpawner = _driveThruManager.GetVehicleSpawner();

        vehicleSpawner.SpawnVehicles();
        _driveThruManager.StartConvoy();
    }

    private void TriggerAreas()
    {
        for (int i = 0; i < areaList.Count; i++)
        {
            areaList[i].GetContainerController().UpdateText();
        }
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