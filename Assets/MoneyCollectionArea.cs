using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyCollectionArea : MonoBehaviour
{
    private int _neededGoldAmount;
    private AmmoCreationArea _ammoCreationArea;

    public void Init(AmmoCreationArea area)
    {
        _ammoCreationArea = area;
        SetNeededGoldAmount(_ammoCreationArea.GetUnlockAmount());
    }
    private void SetNeededGoldAmount(int amount)
    {
        _neededGoldAmount = amount;
    }

    private int GetNeededGoldAmount()
    {
        return _neededGoldAmount;
    }

    public void TriggerArea(AmmoCollector ammoCollector)
    {
        int goldAmount = ResourceManager.Instance.GetGold();
        if (goldAmount > GetNeededGoldAmount())
        {
            UnlockArea();
        }
    }

    private void UnlockArea()
    {
        _ammoCreationArea.UnlockArea();
    }
}