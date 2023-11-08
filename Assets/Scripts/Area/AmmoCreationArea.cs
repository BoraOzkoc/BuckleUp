using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoCreationArea : AreaController
{
    [SerializeField] private AmmoSO _ammoType;
    [SerializeField] private Transform _spawnLocation;
    [SerializeField] private List<AmmoController> _ammoList = new List<AmmoController>();

    [Header("Collection Area")] [SerializeField]
    private CollectionAreaController _collectionAreaController;

    [Header("Grid Limits")] [SerializeField]
    private Vector3 _limits;

    [SerializeField] private int _maxSpawn;

    [Header("Grid Padding")] [SerializeField]
    private int _padding;

    private int x_Count, y_Count, z_Count;
    private Coroutine _spawnCoroutine;

    protected override void Awake()
    {
        base.Awake();
        //_tempObj = Instantiate(gameObject, transform);
    }

    public List<AmmoController> GetAmmoList()
    {
        return _ammoList;
    }
    private void OnValidate()
    {
        _maxSpawn = (int)(_limits.x * _limits.y * _limits.z);
    }

    public override void TriggerArea()
    {
        base.TriggerArea();
        AreaTriggered();
    }

    public void AreaTriggered()
    {
        if (_maxSpawn > _ammoList.Count)
        {
            bool playerEntered = IsPlayerEntered();

            Vector3 SpawnPos = _spawnLocation.position;

            if (x_Count >= _limits.x)
            {
                x_Count = 0;
                z_Count++;

                if (z_Count >= _limits.z)
                {
                    z_Count = 0;
                    y_Count++;

                    if (y_Count >= _limits.y)
                    {
                    }
                }
            }

            SpawnPos.x += _padding * x_Count;
            SpawnPos.y += _padding * y_Count;
            SpawnPos.z += _padding * z_Count;
            SpawnAmmo(SpawnPos);
            x_Count++;
        }
    }

    private void SpawnAmmo(Vector3 spawnPos)
    {
        AmmoController tempAmmo =
            Instantiate(_ammoType.AmmoPrefab, spawnPos, Quaternion.identity, _spawnLocation)
                .GetComponent<AmmoController>();
        
        if (!_ammoList.Contains(tempAmmo)) _ammoList.Add(tempAmmo);
    }

    public void ResetAmmoList()
    {
    }
}