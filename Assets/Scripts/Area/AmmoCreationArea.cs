using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoCreationArea : AreaController
{
    [System.Serializable]
    public class SaveData
    {
        public bool IsLocked;
        public string name;
    }
    
    
    [SerializeField] private AmmoController _ammoPrefab;
    [SerializeField] private Transform _spawnLocation;
    [SerializeField] private List<AmmoController> _ammoList = new List<AmmoController>();

    [Header("Collection Area")] [SerializeField]
    private CollectionAreaController _collectionAreaController;

    [Header("Grid Limits")] [SerializeField]
    private Vector3 _limits;

    [SerializeField] private int _maxSpawn;

    [Header("Grid Padding")] [SerializeField]
    private int _padding;

    private int x_Count, y_Count, z_Count,_id;
    private Coroutine _spawnCoroutine;
    private bool isLocked;

    public void GetLoaded(bool lockState,string name)
    {
        SetLock(lockState);
        gameObject.name = name;
    }
    public List<AmmoController> GetAmmoList()
    {
        return _ammoList;
    }

    public bool IsLocked()
    {
        return isLocked;
    }
    public void SetLock(bool lockState)
    {
        isLocked = lockState;
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
            Instantiate(_ammoPrefab, spawnPos, Quaternion.identity, _spawnLocation);

        if (!_ammoList.Contains(tempAmmo)) _ammoList.Add(tempAmmo);

        int ammoTypeIndex = (int)AmmoType;

        tempAmmo.PreapareAmmo(ammoTypeIndex);
    }

    public void ResetAmmoList()
    {
        x_Count = 0;
        y_Count = 0;
        z_Count = 0;
        _ammoList.Clear();
    }
}
