using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    
    [SerializeField]private int _gold;
    [SerializeField] private bool _useSetGold;
    [SerializeField] private TextMeshProUGUI _goldText;
    public enum Type
    {
        Gold,

    }

    private void SetGoldText()
    {
        _goldText.text = _gold.ToString();
    }
    public int GetResourceAmount()
    {
        return _gold;
    }
    public void Load(SaveManager.Resource resource)
    {
        Debug.Log("load for resource worked");
        SetGold(resource.Gold);
        SetGoldText();
    }
    
    public void SetGold(int amount)
    {
        _gold = amount;
    }
    public void AddGold(int amount = 1)
    {
        _gold += amount;
    }

    public void RemoveGold(int amount = 1)
    {
        _gold -= amount;
    }
}
