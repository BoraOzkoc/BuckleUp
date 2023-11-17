using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Unity.Mathematics;
using Unity.VisualScripting;
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

    public class Resource
    {
        public int Gold;
    }

    public List<AmmoCreationArea> SaveList = new List<AmmoCreationArea>();
    public AreaList areaList = new AreaList();
    [SerializeField] private ResourceManager _resourceManager;

    private Resource _resource = new Resource();

    private void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus)
        {
            SaveAreas();
            //SaveResource();
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
        SaveSystem.Save(json);
    }

    private void SaveAreas()
    {
        for (int i = 0; i < SaveList.Count; i++)
        {
            areaList.AreaData[i].IsLocked = SaveList[i].IsLocked();
            areaList.AreaData[i].Name = SaveList[i].gameObject.name;
        }

        string json = JsonUtility.ToJson(areaList);
        SaveSystem.Save(json);
    }

    private void LoadAllSystems()
    {
        string saveString = SaveSystem.Load();

        LoadAreas(saveString);
        LoadResources(saveString);
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

                    SaveList[i].GetLoaded(tempObj.IsLocked, tempObj.Name);
                }
            }
        }
        else
        {
        }
    }
}