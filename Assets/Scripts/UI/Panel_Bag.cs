using UnityEngine;
using UnityEngine.UI;

public class Panel_Bag : MonoBehaviour
{
    private Button leftBtn;
    private Button rightBtn;
    /// <summary>
    /// item数量
    /// </summary>
    private int itemCount;
    /// <summary>
    /// 当前item的index，index从0开始
    /// </summary>
    private int curIndex;

    public SlotUI slotUI;

    private void OnEnable()
    {
        EventSystem.updateItem += GetItemDetail;
    }

    private void OnDisable()
    {
        EventSystem.updateItem -= GetItemDetail;
    }

    private void Start()
    {
        leftBtn = transform.Find("leftBtn").GetComponent<Button>();
        rightBtn = transform.Find("rightBtn").GetComponent<Button>();
        
        leftBtn.onClick.AddListener(OnLeftBtnClick);
        rightBtn.onClick.AddListener(OnRightBtnClick);
    }

    private void OnRightBtnClick()
    {
        if (curIndex+1<itemCount)
        {
            curIndex++;
        }
        CheckBtnInteractable(curIndex);
        slotUI.SetItem(ItemManager.Instance.SelectItemFromIndex(curIndex));
    }

    private void OnLeftBtnClick()
    {
        if (curIndex-1>-1)
        {
            curIndex--;
        }
        CheckBtnInteractable(curIndex);
        slotUI.SetItem(ItemManager.Instance.SelectItemFromIndex(curIndex));
    }

    private void OnDestroy()
    {
        leftBtn.onClick.RemoveAllListeners();
        rightBtn.onClick.RemoveAllListeners();
    }

    private void GetItemDetail(ItemDetail itemDetail, int index)
    {
        if (itemDetail==null)
        {
            slotUI.SetEmpty();
            curIndex= -1;
            itemCount = 0;
            leftBtn.interactable = false;
            rightBtn.interactable = false;
        }
        else
        {
            curIndex= index;
            itemCount = index + 1;
            slotUI.SetItem(itemDetail);
        }
        CheckBtnInteractable(curIndex);
    }

    private void CheckBtnInteractable(int cur)
    {
        rightBtn.interactable = leftBtn.interactable = true;
        if (cur+1>=itemCount)
            rightBtn.interactable = false;

        if (cur <= 0)
            leftBtn.interactable = false;
    }
}
