using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SaveManager : MonoBehaviour
{
    
    [System.Serializable]
    public class AreaList
    {
        public List<AmmoCreationArea.SaveData> AreaData;
    }

    [System.Serializable]
    public class Resource
    {
        public int Gold;
    }

    public delegate void LoadCompletedEvent();
    public static event LoadCompletedEvent LoadCompleted;
    
    public static SaveManager Instance;


    public List<AmmoCreationArea> SaveList = new List<AmmoCreationArea>();
    public AreaList areaList = new AreaList();
    [SerializeField] private ResourceManager _resourceManager;
    private string _areaSaveName = "AreaSave", _reSourceSaveName = "ResourceSave";
    private Resource _resource = new Resource();
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.Log("Destroying duplicate SaveManager Instance");
            Destroy(this);
        }
        else
        {
            //Debug.Log("Setting SaveManager Instance");
            Instance = this;
        }
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus)
        {
            SaveAreas();
            SaveResource();
        }
    }

    private void Start()
    {
        LoadAllSystems();
    }

    private void SaveResource()
    {
        int amount = _resourceManager.GetResourceAmount();
        _resource.Gold = amount;

        string json = JsonUtility.ToJson(_resource);
        SaveSystem.Save(json, _reSourceSaveName);
    }

    private void SaveAreas()
    {
        for (int i = 0; i < SaveList.Count; i++)
        {
            areaList.AreaData[i].IsLocked = SaveList[i].IsLocked();
            areaList.AreaData[i].AmmoAmount = SaveList[i].GetContainerController().GetAmmoCount();
        }

        string json = JsonUtility.ToJson(areaList);
        SaveSystem.Save(json, _areaSaveName);
    }

    private void LoadAllSystems()
    {
        string areaSaveString = SaveSystem.Load(_areaSaveName);
        string resourceSaveString = SaveSystem.Load(_reSourceSaveName);

        LoadAreas(areaSaveString);
        LoadResources(resourceSaveString);
    }

    private void LoadResources(string saveString)
    {
        if (saveString != null)
        {
            Resource resource = JsonUtility.FromJson<Resource>(saveString);
            _resource.Gold = resource.Gold;
            _resourceManager.Load(_resource);
        }
    }

    private void LoadAreas(string saveString)
    {
        if (saveString != null)
        {
            if (areaList.AreaData.Count == SaveList.Count)
            {
                AreaList itemList = JsonUtility.FromJson<AreaList>(saveString);
                areaList.AreaData = itemList.AreaData;


                for (int i = 0; i < SaveList.Count; i++)
                {
                    AmmoCreationArea.SaveData tempObj = areaList.AreaData[i];

                    SaveList[i].LoadArea(tempObj.IsLocked, tempObj.AmmoAmount);
                }
            }
            if (LoadCompleted != null) LoadCompleted();

        }
        else
        {
            for (int i = 0; i < SaveList.Count; i++)
            {
                bool firstArea = i == 0;
                if (firstArea)
                {
                    SaveList[i].SetLock(false);
                    SaveList[i].PrepareFirstArea();

                }
                else
                {
                    SaveList[i].SetLock(true);
                }

                SaveList[i].CheckLock();
            }
            if (LoadCompleted != null) LoadCompleted();

        }

    }
}