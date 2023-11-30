using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance;
    [SerializeField]private int _gold;
    [SerializeField] private bool _useSetGold;
    [SerializeField] private TextMeshProUGUI _goldText;
    [SerializeField] private ResourcePoolController _resourcePoolController;
    [SerializeField] private Transform _playerPos;
    public enum Type
    {
        Gold,
    }
    //Singleton
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.Log("Destroying duplicate ResourceManager Instance");
            Destroy(this);
        }
        else
        {
            //Debug.Log("Setting ResourceManager Instance");
            Instance = this;
        }
    }

    private void Start()
    {
        if(_useSetGold)SetGold(_gold);
    }

    public ResourcePoolController GetResourcePoolController()
    {
        return _resourcePoolController;
    }
    public int GetGold()
    {
        return _gold;
    }
    private void UpdateGoldText()
    {
        _goldText.text = _gold.ToString();
    }
    public int GetResourceAmount()
    {
        return _gold;
    }
    public void Load(SaveManager.Resource resource)
    {
       if(!_useSetGold) SetGold(resource.Gold);
        UpdateGoldText();
    }
    
    public void SetGold(int amount)
    {
        _gold = amount;
        UpdateGoldText();

    }
    public void AddGold(int amount = 1)
    {
        _gold += amount;
        UpdateGoldText();
    }

    public void RemoveGold(int amount = 1)
    {
        _gold -= amount;
        UpdateGoldText();

    }
}
