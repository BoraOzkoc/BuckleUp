using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SaveManager : MonoBehaviour
{
    public List<AmmoCreationArea> SaveList = new List<AmmoCreationArea>();
    private IDataService DataService = new JsonDataService();
    private bool EncryptionEnabled;

    private void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus)
        {
             DataService.SaveData("/savedata.json", SaveList[0]);
            //  AmmoCreationArea data = DataService.LoadData<AmmoCreationArea>("/savedata.json");
            //
            // Debug.Log(JsonConvert.SerializeObject(data, Formatting.Indented));
        }
    }

    public void SerializeJson()
    {
        if (DataService.SaveData("/savedata.json", SaveList[0]))
        {
            try
            {
                AmmoCreationArea data = DataService.LoadData<AmmoCreationArea>("/savedata.json");
                string test = "Loaded from file:\r\n" + JsonConvert.SerializeObject(data, Formatting.Indented);
                Debug.Log(test);
            }
            catch (Exception e)
            {
                Debug.LogError($"Could not read file! Show something on the UI here!");
                //InputField.text = "<color=#ff0000>Error reading save file!</color>";
            }
        }
        else
        {
            Debug.LogError("Could not save file! Show something on the UI about it!");
            //InputField.text = "<color=#ff0000>Error saving data!</color>";
        }
    }

    private void Awake()
    {
        //SourceDataText.SetText(JsonConvert.SerializeObject(AmmoCreationArea, Formatting.Indented));
    }

    public void ClearData()
    {
        string path = Application.persistentDataPath + "/savedata.json";
        if (File.Exists(path))
        {
            File.Delete(path);
            //InputField.text = "Loaded data goes here";
        }
    }
}