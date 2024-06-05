using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour, ISaveable
{
    private Dictionary<ItemEnums, bool> objectDic = new Dictionary<ItemEnums, bool>();
    private Dictionary<string, bool> interactionDic = new Dictionary<string, bool>();

    private void OnEnable()
    {
        EventSystem.beforeUnloadScene += OnBeforeUnloadScene;
        EventSystem.afterLoadScene += OnAfterLoadScene;
        EventSystem.updateItem += OnUpdateItemClick;
        EventSystem.startGameEvent += OnStartGameEvent;
    }

    private void Start()
    {
        ISaveable saveable = this;
        saveable.Register();
    }

    private void OnDisable()
    {
        EventSystem.beforeUnloadScene -= OnBeforeUnloadScene;
        EventSystem.afterLoadScene -= OnAfterLoadScene;
        EventSystem.updateItem -= OnUpdateItemClick;
        EventSystem.startGameEvent -= OnStartGameEvent;
    }

    private void OnStartGameEvent(int week)
    {
        objectDic.Clear();
        interactionDic.Clear();
    }

    private void OnBeforeUnloadScene()
    {
        //Item状态保存
        foreach (var item in FindObjectsOfType<Item>())
        {
            if (objectDic.ContainsKey(item.itemName) == false)
            {
                objectDic.Add(item.itemName, true);
            }
        }

        //可交互物体状态保存
        foreach (var interaction in FindObjectsOfType<Interaction>())
        {
            if (interactionDic.ContainsKey(interaction.name))
            {
                interactionDic[interaction.name] = interaction.isDone;
            }
            else
            {
                interactionDic.Add(interaction.name, interaction.isDone);
            }
        }
    }

    private void OnAfterLoadScene()
    {
        //Item状态保存
        foreach (var item in FindObjectsOfType<Item>())
        {
            if (objectDic.ContainsKey(item.itemName) == false)
            {
                objectDic.Add(item.itemName, true);
            }
            else
            {
                item.gameObject.SetActive(objectDic[item.itemName]);
            }
        }

        //可交互物体状态保存
        foreach (var interaction in FindObjectsOfType<Interaction>())
        {
            if (interactionDic.ContainsKey(interaction.name))
            {
                interaction.isDone = interactionDic[interaction.name];
            }
            else
            {
                interactionDic.Add(interaction.name, interaction.isDone);
            }
        }
    }


    private void OnUpdateItemClick(ItemDetail itemDetail, int index)
    {
        if (itemDetail != null && objectDic.ContainsKey(itemDetail.itemName))
        {
            objectDic[itemDetail.itemName] = false;
        }
    }

    public SaveData GenerateSaveData()
    {
        SaveData data = new SaveData();
        data.objectDic = objectDic;
        data.interactionDic = interactionDic;
        return data;
    }

    public void ReadGameData(SaveData gameData)
    {
        objectDic = gameData.objectDic;
        interactionDic = gameData.interactionDic;
    }
}