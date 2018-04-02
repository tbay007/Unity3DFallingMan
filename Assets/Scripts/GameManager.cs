using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour, IDisposable {

    static object locker = new object();
    private static string connectionPath = "";

#if UNITY_IOS
    private bool android = false;
#elif UNITY_ANDROID
    private bool android = true;
#endif

    public GameManager()
    {
    }

    public void CreateStorageFile()
    {
        if (android)
        {
            var sqliteFilename = "GameData.db3";
            connectionPath = Application.persistentDataPath + "/" + sqliteFilename;
        }

        if (!File.Exists(connectionPath))
        {
            File.Create(connectionPath).Dispose();
        }
    }

    public Assets.Scripts.Models.GameSettingsModel GetGameSettingsAndHighScores()
    {
        if (android)
        {
            var sqliteFilename = "GameData.db3";
            //string libraryPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            //var path = Path.Combine(libraryPath, sqliteFilename);
            //connectionPath = path;
            connectionPath = Application.persistentDataPath + "/" + sqliteFilename;
        }
        Assets.Scripts.Models.GameSettingsModel model = new Assets.Scripts.Models.GameSettingsModel();
        lock (locker)
        {
            string needsDeserialized = File.ReadAllText(connectionPath);
            model = JsonConvert.DeserializeObject<Assets.Scripts.Models.GameSettingsModel>(needsDeserialized);
            return model;
        }
    }

    public int SaveItem(Assets.Scripts.Models.GameSettingsModel item)
    {
        if (android)
        {
            var sqliteFilename = "GameData.db3";
            connectionPath = Application.persistentDataPath + "/" + sqliteFilename;
        }
        lock (locker)
        {
            if (File.Exists(connectionPath))
            {
                string needsDeserialized = File.ReadAllText(connectionPath);
                var existingSettings = JsonConvert.DeserializeObject<Assets.Scripts.Models.GameSettingsModel>(needsDeserialized);
                if (existingSettings != null)
                {
                    existingSettings.Volume = item.Volume;
                    existingSettings.DateEntered = DateTime.UtcNow.ToString();
                    existingSettings.HighScores = item.HighScores;
                    string serializedObject = JsonConvert.SerializeObject(existingSettings);
                    File.Create(connectionPath).Dispose();
                    File.AppendAllText(connectionPath, serializedObject);
                }
                else
                {
                    existingSettings = item;
                    string serializedObject = JsonConvert.SerializeObject(existingSettings);
                    File.Create(connectionPath).Dispose();
                    File.AppendAllText(connectionPath, serializedObject);
                }
            }
            else if (!File.Exists(connectionPath))
            {
                string serializedObject = JsonConvert.SerializeObject(item);
                File.Create(connectionPath).Dispose();
                File.AppendAllText(connectionPath, serializedObject);
            }
            return 1;
        }
    }

    public void Dispose()
    {

    }
}
