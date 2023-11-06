using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoCreationArea : AreaController
{
    [SerializeField] private AmmoSO _ammoType;
    [SerializeField] private Transform _spawnLocation;
    private Coroutine _spawnCoroutine;
    protected override void Awake()
    {
        base.Awake();
       //_tempObj = Instantiate(gameObject, transform);
    }

    public override void TriggerArea()
    {
        base.TriggerArea();
        AreaTriggered();
    }

    public void AreaTriggered()
    {
        bool playerEntered = IsPlayerEntered();
        Instantiate(_ammoType.AmmoPrefab,_spawnLocation.position,Quaternion.identity,_spawnLocation);

    }
}
