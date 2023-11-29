using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaOrderController : MonoBehaviour
{
  private AreaManager _areaManager;

  private void Awake()
  {
      _areaManager = GetComponent<AreaManager>();
  }

  private void OnEnable()
    {
        SaveManager.LoadCompleted += CheckAllAreaLocks;
    }

    private void OnDisable()
    {
        SaveManager.LoadCompleted -= CheckAllAreaLocks;
    }

    public void CheckAllAreaLocks()
    {
        List<AmmoCreationArea> tempList = new List<AmmoCreationArea>(_areaManager.GetAreaList());
        for (int i = 0; i < tempList.Count; i++)
        {
            if (i > 0)
            {
                if (tempList[i - 1].IsLocked()) tempList[i].Hide(true);
                else tempList[i].Hide(false);
            }
        }
    }
}