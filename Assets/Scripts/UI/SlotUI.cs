using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotUI : MonoBehaviour,IPointerClickHandler
{
    public Image slot;

    private bool isClick;
    private ItemDetail currentDetail;

    public void SetItem(ItemDetail itemDetail)
    {
        currentDetail = itemDetail;
        slot.gameObject.SetActive(true);
        slot.sprite = currentDetail.itemSprite;
        slot.SetNativeSize();
    }
    
    public void SetEmpty()
    {
        slot.gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        isClick = !isClick;
        EventSystem.CallItemClick(currentDetail,isClick);
    }
}
