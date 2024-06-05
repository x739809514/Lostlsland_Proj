using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemEnums itemName;

    public void ItemClick()
    {
        ItemManager.Instance.AddItemToBag(itemName);
        gameObject.SetActive(false);
    }
}
