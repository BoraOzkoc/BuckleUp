using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Mono.CompilerServices.SymbolWriter;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class VehicleController : MonoBehaviour
{
    public Type VehicleType;
    [SerializeField] private List<GameObject> modelList = new List<GameObject>();
    [SerializeField] private TextMeshProUGUI _textMeshProUGUI;
    [SerializeField] private List<Image> _orderImageList = new List<Image>();
    private int _orderAmount;
    private DriveThruManager _driveThruManager;
    private TradeAreaController _tradeAreaController;
    private Transform _endPos, _player;

    public enum Type
    {
        Small,
        Medium,
        Heavy
    }

    public void CollectAmmo(AmmoController ammoController, bool isLastAmmo)
    {
        ammoController.GetTransferredToVehicle(transform, this, isLastAmmo);
    }

    private bool OrderFinished()
    {
        return _orderAmount <= 0;
    }

    public int GetOrderAmount()
    {
        return _orderAmount;
    }

    public int GetIndex()
    {
        return (int)VehicleType;
    }

    public void Init(DriveThruManager driveThruManager)
    {
        _driveThruManager = driveThruManager;
        _tradeAreaController = _driveThruManager.GetTradeAreaController();
        _endPos = driveThruManager.GetEndPos();
        _player = driveThruManager.GetPlayer();
    }

    public void Activate(int index)
    {
        VehicleType = (Type)index;
        ActivateModel();
        HideOrder();
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

    private void GiveOrder()
    {
        int orderCount = Random.Range(1, 6);
        _orderAmount = orderCount * ((int)VehicleType + 1);

        ShowOrder();
        _tradeAreaController.SetVehicle(this);
    }

    public void ChangeNeededAmmoAmount(int amount)
    {
        _orderAmount += amount;
    }

    public void UpdateText()
    {
        _textMeshProUGUI.text = "x" + _orderAmount;
    }

    public void CheckOrderAmount()
    {
        if (OrderFinished()) DriveToEnd();
    }

    private void ShowOrder()
    {
        UpdateText();
        _textMeshProUGUI.gameObject.SetActive(true);
        int index = (int)VehicleType;
        _orderImageList[index].gameObject.SetActive(true);
    }

    private void CompleteOrder()
    {
        _driveThruManager.RemoveFromList(this);
    }

    private void HideOrder()
    {
        _textMeshProUGUI.gameObject.SetActive(false);
        for (int i = 0; i < _orderImageList.Count; i++)
        {
            _orderImageList[i].gameObject.SetActive(false);
        }
    }

    public void DriveToPayment(Vector3 pos)
    {
        transform.DOMove(pos, 2).SetEase(Ease.InOutQuart).OnComplete(GiveOrder);
    }

    private void DriveToEnd()
    {
        HideOrder();
        transform.DOMove(_endPos.position, 2).SetEase(Ease.InOutQuart).OnComplete(CompleteOrder);
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