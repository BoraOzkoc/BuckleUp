using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcePoolController : MonoBehaviour
{
    [SerializeField] private GoldController _goldPrefab;
    [SerializeField] private int _spawnAmount;
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
                deactivatedList.Add(tempGold);
            }
        }
    }

    public GoldController PullFromList(Transform startLocation, Transform endLocation)
    {
        GoldController tempGold = deactivatedList[0];
        deactivatedList.Remove(tempGold);
        tempGold.Activate(startLocation.position);
        activatedList.Add(tempGold);
        tempGold.MoveTo(endLocation.position);
        return tempGold;
    }
    
    public void PushToList(GoldController goldController)
    {
        if (activatedList.Contains(goldController)) activatedList.Remove(goldController);
        deactivatedList.Add(goldController);
        goldController.Deactivate();
    }
}