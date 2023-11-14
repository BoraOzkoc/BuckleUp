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

    public List<AmmoCreationArea> SaveList = new List<AmmoCreationArea>();

    private void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus)
        {
            SaveAreas();
        }
    }

    private void Start()
    {
        LoadAreas();
    }

    public AreaList areaList = new AreaList();

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

    private void LoadAreas()
    {
        string saveString = SaveSystem.Load();

        if (saveString != null)
        {
            AreaList itemList = JsonUtility.FromJson<AreaList>(saveString);
            areaList.AreaData = itemList.AreaData;
            
            
            for (int i = 0; i < SaveList.Count; i++)
            {
                AmmoCreationArea.SaveData tempObj = areaList.AreaData[i];

                SaveList[i].GetLoaded(tempObj.IsLocked,tempObj.Name);
                
            }
        }
        else
        {
        }
    }
}