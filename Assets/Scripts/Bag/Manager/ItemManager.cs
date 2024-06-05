using System;
using System.Collections.Generic;

public class ItemManager : Singleton<ItemManager>, ISaveable
{
    public ItemData itemData;
    private List<ItemEnums> bag = new List<ItemEnums>();

    private void OnEnable()
    {
        EventSystem.itemUse += DeleteItemFromBag;
        EventSystem.afterLoadScene += OnAfterLoadScene;
        EventSystem.startGameEvent += OnStartGameEvent;
    }

    private void Start()
    {
        ISaveable saveable = this;
        saveable.Register();
    }

    private void OnDisable()
    {
        EventSystem.itemUse -= DeleteItemFromBag;
        EventSystem.afterLoadScene -= OnAfterLoadScene;
        EventSystem.startGameEvent -= OnStartGameEvent;
    }

    private void OnStartGameEvent(int obj)
    {
        bag.Clear();
    }

    private void OnAfterLoadScene()
    {
        if (bag.Count == 0)
        {
            EventSystem.CallItemUpdate(null, -1);
        }
        else
        {
            for (int i = 0, count = bag.Count; i < count; i++)
            {
                EventSystem.CallItemUpdate(SelectItemFromIndex(i), i);
            }
        }
    }

    /// <summary>
    /// 背包增加物品
    /// </summary>
    /// <param name="itemName"></param>
    public void AddItemToBag(ItemEnums itemName)
    {
        if (bag.Contains(itemName) == false)
        {
            bag.Add(itemName);
        }

        var item = itemData.GetItemDetail(itemName);
        EventSystem.CallItemUpdate(item, bag.Count - 1);
    }

    /// <summary>
    /// 使用物品后删除
    /// </summary>
    /// <param name="itemName"></param>
    private void DeleteItemFromBag(ItemEnums itemName)
    {
        bag.Remove(itemName);
        //Todo:删除单一物品
        EventSystem.CallItemUpdate(null, -1);
    }

    public ItemDetail SelectItemFromIndex(int index)
    {
        return itemData.GetItemDetail(bag[index]);
    }


    public SaveData GenerateSaveData()
    {
        SaveData data = new SaveData();
        data.bag = bag;
        return data;
    }

    public void ReadGameData(SaveData gameData)
    {
        bag = gameData.bag;
    }
}