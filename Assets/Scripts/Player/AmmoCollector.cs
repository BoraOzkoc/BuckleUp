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

            for (int i = 0; i < tempEmmoList.Count; i++)
            {
                TriggerCollectedAmmo(collectionAreaController.GetContainerController(), tempEmmoList[i]);
            }
        }
    }

    private void TriggerCollectedAmmo(ContainerController containerController, AmmoController ammoController)
    {
        ammoController.GetCollected(containerController.transform);
    }
}