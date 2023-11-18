using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriveThruManager : MonoBehaviour
{
    [SerializeField] private List<VehicleController> _vehicleList = new List<VehicleController>();
    [SerializeField] private Transform _start, _end, _paymentLocation;
    private VehicleSpawner _vehicleSpawner;

    private void Awake()
    {
        _vehicleSpawner = GetComponent<VehicleSpawner>();
    }

    public VehicleSpawner GetVehicleSpawner()
    {
        return _vehicleSpawner;
    }

    public void StartConvoy()
    {
        for (int i = 0; i < 3; i++)
        {
           VehicleController spawnedVehicle = _vehicleSpawner.PullFromList(_start,_end);
           _vehicleList.Add(spawnedVehicle);
        }

        MoveFirstVehicle();
    }

    private void MoveFirstVehicle()
    {
        _vehicleList[0].DriveToPayment(_paymentLocation.position);
    }

    public void RemoveFromList(VehicleController vehicleController)
    {
        _vehicleSpawner.PushToList(vehicleController);
        _vehicleList.Remove(vehicleController);
    }
}
