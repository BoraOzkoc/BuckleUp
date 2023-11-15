using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Mono.CompilerServices.SymbolWriter;
using UnityEngine;

public class VehicleController : MonoBehaviour
{
    public Type VehicleType;
    [SerializeField] private List<GameObject> modelList = new List<GameObject>();

    public enum Type
    {
        Small,
        Medium,
        Heavy
    }

    public void Activate(int index)
    {
        VehicleType = (Type)index;
        ActivateModel();
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    public void MoveVehicle(Vector3 targetPos)
    {
        transform.position = targetPos;
    }

    public void TurnVehicle(Vector3 pos)
    {
        transform.LookAt(pos);
    }

    public void DriveTo(Vector3 pos)
    {
        transform.DOMove(pos, 0.5f).SetEase(Ease.InOutQuart);
    }
    private void ActivateModel()
    {
        int index = (int)VehicleType;
        for (int i = 0; i < modelList.Count; i++)
        {
            modelList[i].SetActive(false);
        }

        gameObject.SetActive(true);
        modelList[index].SetActive(true);
    }
}