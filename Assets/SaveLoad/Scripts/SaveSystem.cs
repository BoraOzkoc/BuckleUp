﻿using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveSystem
{
    private static string SAVE_FOLDER = Application.persistentDataPath + "/Saves/";
    private const string SAVE_EXTENSION = ".txt";

    public static void Awake()
    {

    }
    public static void Init()
    {
        // Test if Save Folder exists
        if (!Directory.Exists(SAVE_FOLDER))
        {
            // Create Save Folder
            Directory.CreateDirectory(SAVE_FOLDER);

        }
    }

    public static void Save(string saveString, string saveName)
    {
        File.WriteAllText(SAVE_FOLDER + saveName + SAVE_EXTENSION, saveString);
    }

    public static string Load(string name)
    {
        Init();
        DirectoryInfo directoryInfo = new DirectoryInfo(SAVE_FOLDER);
        // Get all save files
        FileInfo[] saveFiles = directoryInfo.GetFiles();
        // Cycle through all save files and identify the most recent one
        foreach (FileInfo fileInfo in saveFiles)
        {
            string fileName = fileInfo.Name;
            string test = name + SAVE_EXTENSION;
            
            if (Equals(fileName, test))
            {
                string saveString = File.ReadAllText(directoryInfo + fileInfo.Name);
                return saveString;
            }
            else
            {
            }
        }

        return null;
    }
}