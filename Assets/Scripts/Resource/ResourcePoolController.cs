using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcePoolController : MonoBehaviour
{
    [SerializeField] private GoldController _goldPrefab;
    [SerializeField] private int _spawnAmount, x_Count, y_Count, z_Count;
    [SerializeField] private Vector3 _padding;
    [SerializeField] private Vector3 _limits;
    [SerializeField] private Transform _spawnLocation;
    [SerializeField] private ParticleSystem _glowEffect;
    private List<GoldController> activatedList = new List<GoldController>();
    private List<GoldController> deactivatedList = new List<GoldController>();

    private void Start()
    {
        SpawnGold();
    }

    private void SpawnGold()
    {
        if (deactivatedList.Count < 1)
        {
            while (_spawnAmount > deactivatedList.Count)
            {
                GoldController tempGold =
                    Instantiate(_goldPrefab, transform.position, Quaternion.identity, transform);
                tempGold.Deactivate();
                tempGold.SetPoolController(this);
                deactivatedList.Add(tempGold);
            }
        }
    }

    private void ActivateGlowEffect()
    {
        if (!_glowEffect.isPlaying) _glowEffect.Play();
    }

    private void DeactivateGlowEffect()
    {
        if (_glowEffect.isPlaying)
        {
            _glowEffect.Stop();
        }
    }

    private void MoveGlowEffect()
    {
        Vector3 middlePoint = Vector3.zero;
        for (int i = 0; i < activatedList.Count; i++)
        {
            middlePoint += activatedList[i].transform.position;
        }

        Vector3 targetPos = middlePoint / activatedList.Count;

        _glowEffect.gameObject.transform.position = targetPos;
    }

    // private void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.H))
    //     {
    //         MoveGlowEffect();
    //     }
    // }

    public Vector3 GetSpawnPoint()
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

        SpawnPos.x += _padding.x * x_Count;
        SpawnPos.y += _padding.y * y_Count;
        SpawnPos.z += _padding.z * z_Count;
        x_Count++;
        return SpawnPos;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out AmmoCollector ammoCollector))
        {
            for (int i = 0; i < activatedList.Count; i++)
            {
                activatedList[i].MoveTo(ammoCollector);
                ResetCounts();
            }
        }
    }

    private void ResetCounts()
    {
        x_Count = 0;
        y_Count = 0;
        z_Count = 0;
    }

    public GoldController PullFromList(Transform startLocation)
    {
        GoldController tempGold = deactivatedList[0];
        deactivatedList.Remove(tempGold);
        tempGold.Activate(startLocation.position);
        activatedList.Add(tempGold);

        tempGold.TransferGold(GetSpawnPoint());
        SpawnGold();
        return tempGold;
    }

    public void CheckActiveGoldCount()
    {
        if (activatedList.Count >= 9)
        {
            ActivateGlowEffect();
            MoveGlowEffect();
        }
        else
        {
            DeactivateGlowEffect();
        }
    }
    public void PushToList(GoldController goldController)
    {
        if (activatedList.Contains(goldController)) activatedList.Remove(goldController);
        CheckActiveGoldCount();
        deactivatedList.Add(goldController);
        goldController.Deactivate();
    }
}