using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriveThruManager : MonoBehaviour
{
    [SerializeField] private List<VehicleController> _vehicleList = new List<VehicleController>();
    [SerializeField] private Transform _start, _end, _paymentLocation;
    [SerializeField] private TradeAreaController _tradeAreaController;
    [SerializeField] private Transform _player;
    private VehicleSpawner _vehicleSpawner;

    private void Awake()
    {
        _vehicleSpawner = GetComponent<VehicleSpawner>();
    }

    public TradeAreaController GetTradeAreaController()
    {
        return _tradeAreaController;
    }

    public VehicleSpawner GetVehicleSpawner()
    {
        return _vehicleSpawner;
    }

    public Transform GetPlayer()
    {
        return _player;
    }

    public void StartConvoy()
    {
        Debug.Log("start convoy called");
        for (int i = 0; i < 3; i++)
        {
            SpawnVehicle();
            
        }

        MoveFirstVehicle();
    }

    private void SpawnVehicle()
    {
        VehicleController spawnedVehicle = _vehicleSpawner.PullFromList(_start, _end);
        spawnedVehicle.Init(this);
        _vehicleList.Add(spawnedVehicle);
    }

    public Transform GetEndPos()
    {
        return _end;
    }

    private void MoveFirstVehicle()
    {
        _vehicleList[0].DriveToPayment(_paymentLocation.position);
    }

    public void RemoveFromList(VehicleController vehicleController)
    {
        _vehicleSpawner.PushToList(vehicleController);
        _vehicleList.Remove(vehicleController);
        UpdateConvoy();
    }

    private void UpdateConvoy()
    {
        SpawnVehicle();
        MoveFirstVehicle();
    }
}