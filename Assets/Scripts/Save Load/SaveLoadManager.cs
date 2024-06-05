using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class SaveLoadManager : Singleton<SaveLoadManager>
{
    private string folderPath;

    public List<ISaveable> dataList = new List<ISaveable>();

    public Dictionary<string, SaveData> saveDataDic = new Dictionary<string, SaveData>();

    protected override void Awake()
    {
        base.Awake();
        folderPath = Application.persistentDataPath + "/SAVE/";
    }

    private void OnEnable()
    {
        EventSystem.startGameEvent += OnStartGameEvent;
    }

    private void OnDisable()
    {
        EventSystem.startGameEvent -= OnStartGameEvent;
    }

    public void DoRegister(ISaveable saveable)
    {
        dataList.Add(saveable);
    }


    public void OnStartGameEvent(int gameWeek)
    {
        var resultPath = folderPath + "data.sav";
        if (File.Exists(resultPath))
        {
            File.Delete(resultPath);
        }
    }

    /// <summary>
    /// 序列化存储
    /// </summary>
    public void Serialize()
    {
        saveDataDic.Clear();

        foreach (var saveable in dataList)
        {
            saveDataDic.Add(saveable.GetType().Name, saveable.GenerateSaveData());
        }

        var resultPath = folderPath + "data.sav";
        var jsonData = JsonConvert.SerializeObject(saveDataDic, Formatting.Indented);

        //创建文件夹
        if (File.Exists(resultPath) == false)
        {
            Directory.CreateDirectory(folderPath);
        }

        File.WriteAllText(resultPath, jsonData);
    }

    /// <summary>
    /// 反序列化
    /// </summary>
    public void AntiSerializeObject()
    {
        var resultPath = folderPath + "data.sav";

        if (File.Exists(resultPath) == false) return;

        var stringData = File.ReadAllText(resultPath);
        var jsonData = JsonConvert.DeserializeObject<Dictionary<string, SaveData>>(stringData);

        foreach (var saveable in dataList)
        {
            saveable.ReadGameData(jsonData[saveable.GetType().Name]);
        }
    }
}