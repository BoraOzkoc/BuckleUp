using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class VehicleSpawner : MonoBehaviour
{
    [SerializeField] private List<VehicleController> deactivatedVehicleList = new List<VehicleController>();
    [SerializeField] private List<VehicleController> activatedVehicleList = new List<VehicleController>();
    [SerializeField] private VehicleController _vehiclePrefab;
    [SerializeField] private int _vehiclePoolAmount;
    [SerializeField] private bool smallAmmoUnlocked, mediumAmmoUnlocked, heavyAmmoUnlocked;
    private DriveThruManager _driveThruManager;

    private void Awake()
    {
        _driveThruManager = GetComponent<DriveThruManager>();
    }

    public void UnlockVehicle(int index)
    {
        if (index == 0)
        {
            smallAmmoUnlocked = true;
        }
        else if (index == 1)
        {
            mediumAmmoUnlocked = true;
        }
        else if (index == 2)
        {
            heavyAmmoUnlocked = true;
        }
    }

    public void SpawnVehicles()
    {
        if (deactivatedVehicleList.Count < 1)
        {
            while (_vehiclePoolAmount > deactivatedVehicleList.Count)
            {
                VehicleController tempVehicle =
                    Instantiate(_vehiclePrefab, transform.position, quaternion.identity, transform);
                deactivatedVehicleList.Add(tempVehicle);
                tempVehicle.Deactivate();
                tempVehicle.MoveVehicle(transform.position);
                tempVehicle.Init(_driveThruManager);
            }
        }
    }

    public VehicleController PullFromList(Transform startLocation, Transform endLocation)
    {
        if(deactivatedVehicleList.Count > 0)
        {
            VehicleController vehicleController = deactivatedVehicleList[0];
            deactivatedVehicleList.Remove(vehicleController);
            activatedVehicleList.Add(vehicleController);
            vehicleController.MoveVehicle(startLocation.position);
            vehicleController.TurnVehicle(endLocation.position);
            vehicleController.Activate(PickRandomVehicle());
            return vehicleController;
        }
        else
        {
            return null;
        }
    }

    private int PickRandomVehicle()
    {
        int count = 0;
        if (smallAmmoUnlocked) count++;
        if (mediumAmmoUnlocked) count++;
        if (heavyAmmoUnlocked) count++;
        int number = Random.Range(0, count);
        return number;
    }

    public void PushToList(VehicleController vehicleController)
    {
        if (activatedVehicleList.Contains(vehicleController))
        {
            activatedVehicleList.Remove(vehicleController);
            deactivatedVehicleList.Add(vehicleController);
            vehicleController.Deactivate();
            vehicleController.MoveVehicle(transform.position);
        }
    }
}