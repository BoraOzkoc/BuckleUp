using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ContainerController : MonoBehaviour
{
    private List<AmmoController> collectedAmmoList = new List<AmmoController>();

    public bool GotAmmo()
    {
        return collectedAmmoList.Count > 0;
    }

    public void TransferAmmo(VehicleController vehicleController)
    {
        
        if (vehicleController.GetOrderAmount() > 0 && GetAmmoCount()>0)
        {
           AmmoController removedAmmo =  RemoveAmmo(collectedAmmoList[0]);
           vehicleController.CollectAmmo(removedAmmo);
        }
    }
    public void AddAmmo(AmmoController newAmmo)
    {
        collectedAmmoList.Add(newAmmo);
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
