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

    private void Start()
    {
        SpawnVehicles();
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

    private void SpawnVehicles()
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
            }
        }
    }

    private void PullFromList()
    {
        VehicleController vehicleController = deactivatedVehicleList[0];
        deactivatedVehicleList.Remove(deactivatedVehicleList[0]);
        vehicleController.Activate(PickRandomVehicle());
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

    private void PushToList(VehicleController vehicleController)
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