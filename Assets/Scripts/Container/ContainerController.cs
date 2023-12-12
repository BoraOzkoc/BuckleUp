using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;

public class ContainerController : MonoBehaviour
{
    [SerializeField] private AmmoCreationArea _ammoCreationArea;
    private List<AmmoController> collectedAmmoList = new List<AmmoController>();
    [SerializeField] private TextMeshProUGUI _ammoCountText;

    private void Start()
    {
        UpdateText();
    }

    public void Load(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            AmmoController tempAmmo = _ammoCreationArea.LoadAmmo(transform.position);
            AddAmmo(tempAmmo);
        }
    }

    public bool GotAmmo()
    {
        return collectedAmmoList.Count > 0;
    }

    public void TransferAmmo(VehicleController vehicleController, int amount)
    {
        StartCoroutine(TransferAmmoCoroutine(vehicleController, amount));
    }

    IEnumerator TransferAmmoCoroutine(VehicleController vehicleController,int loopCount)
    {
        for (int i = 0; i < loopCount; i++)
        {
            AmmoController removedAmmo = RemoveAmmo(collectedAmmoList[0]);
            UpdateText();
            vehicleController.CollectAmmo(removedAmmo, (i == (loopCount - 1)));
            yield return new WaitForSeconds(0.1f);
        }
    }
    

    public void TriggerWarning()
    {
        //_ammoCountText.transform.DOShakeScale(0.15f, Vector3.one * 3);
    }
    public void AddAmmo(AmmoController newAmmo)
    {
        collectedAmmoList.Add(newAmmo);
        UpdateText();
    }

    private void UpdateText()
    {
        _ammoCountText.text = "x" + collectedAmmoList.Count.ToString();
    }

    public void TriggerBounceAnimation(int index)
    {
        transform.localScale = Vector3.one;
        transform.DOScale(Vector3.one * 1.5f, 0.3f).SetEase(Ease.InBounce).SetLoops(2, LoopType.Yoyo);
    }

    public int GetAmmoCount()
    {
        return collectedAmmoList.Count;
    }

    public AmmoController RemoveAmmo(AmmoController ammoController)
    {
        if (collectedAmmoList.Contains(ammoController))
        {
            collectedAmmoList.Remove(ammoController);
        }

        return ammoController;
    }
}