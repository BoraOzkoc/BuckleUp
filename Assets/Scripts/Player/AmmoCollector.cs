using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoCollector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<CollectionAreaController>(out CollectionAreaController collectionAreaController))
        {
            List<AmmoController> tempEmmoList = collectionAreaController.CollectAmmoList();

            StartCoroutine(TransferCoroutine(tempEmmoList, collectionAreaController));
        }
    }


    IEnumerator TransferCoroutine(List<AmmoController> ammoControllerList,
        CollectionAreaController collectionAreaController)
    {
        for (int i = ammoControllerList.Count -1; i >= 0 ; i--)
        {
            TriggerCollectedAmmo(collectionAreaController.GetContainerController(), ammoControllerList[i]);
            yield return new WaitForSeconds(0.01f);
        }

    }

    private void TriggerCollectedAmmo(ContainerController containerController, AmmoController ammoController)
    {
        ammoController.GetCollected(containerController);
    }
}