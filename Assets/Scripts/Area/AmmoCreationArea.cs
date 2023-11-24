using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class AmmoCreationArea : AreaController
{
    [System.Serializable]
    public class SaveData
    {
        public bool IsLocked;
        public string Name;
    }

    [SerializeField] private AmmoController _ammoPrefab;
    [SerializeField] private Transform _spawnLocation;
    [SerializeField] private List<AmmoController> _ammoList = new List<AmmoController>();
    [SerializeField] private GameObject _model;

    [Header("Collection Area")] [SerializeField]
    private CollectionAreaController _collectionAreaController;

    [Header("Locked Area Info")] [SerializeField]
    private int _unlockAmount;

    [Header("Grid Limits")] [SerializeField]
    private Vector3 _limits;

    [SerializeField] private int _maxSpawn;
    [SerializeField] private Image _lockedImage;

    [Header("Grid Padding")] [SerializeField]
    private int _padding;


    private int x_Count, y_Count, z_Count, _id;
    private Coroutine _spawnCoroutine;
    private bool isLocked, _isLoaded;

    private AreaManager _areaManager;

    private MoneyCollectionArea _moneyCollectionArea;

    public void Init(AreaManager areaManager)
    {
        _areaManager = areaManager;
        _moneyCollectionArea = GetComponent<MoneyCollectionArea>();
        _moneyCollectionArea.Init(this);
    }

    public void LoadArea(bool state, string name)
    {
        SetLock(state);
        gameObject.name = name;
        _isLoaded = true;
        CheckLock();
        _areaManager.CheckLock(this);
    }

    public int GetUnlockAmount()
    {
        return _unlockAmount;
    }

    public bool IsLoaded()
    {
        return _isLoaded;
    }

    public List<AmmoController> GetAmmoList()
    {
        return _ammoList;
    }

    public override void CheckLock()
    {
        base.CheckLock();
        if (isLocked)
        {
            LockArea();
        }
        else
        {
            UnlockArea();
        }
    }

    public override void LockArea()
    {
        base.LockArea();
        isLocked = true;
        _collectionAreaController.gameObject.SetActive(false);
        _lockedImage.gameObject.SetActive(true);
        _model.SetActive(false);
    }

    public override void UnlockArea()
    {
        base.UnlockArea();
        isLocked = false;
        _collectionAreaController.gameObject.SetActive(true);
        _lockedImage.gameObject.SetActive(false);
        _model.SetActive(true);
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

    public override bool TriggerArea(AmmoCollector ammoCollector)
    {
        base.TriggerArea(ammoCollector);
        if (isLocked)
        {
            _moneyCollectionArea.TriggerArea(ammoCollector);
        }
        else
        {
            AreaTriggered();
        }

        return !isLocked;
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