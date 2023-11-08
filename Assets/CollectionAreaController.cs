using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionAreaController : MonoBehaviour
{
    [SerializeField] private AmmoCreationArea _ammoCreationArea;
    
    public List<AmmoController> CollectAmmoList()
    {
        List<AmmoController> tempAmmoList = new List<AmmoController>(_ammoCreationArea.GetAmmoList());
        _ammoCreationArea.ResetAmmoList();
        return tempAmmoList;
    }
    
}
