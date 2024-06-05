using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public RectTransform hand;
    private bool isHand;
    private ItemEnums handItem;
    private Vector3 cursorPos => Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));

    private void OnEnable()
    {
        EventSystem.itemClick += OnItemClickEvent;
        EventSystem.itemUse += DeleteItemFromBag;
    }

    private void OnDisable()
    {
        EventSystem.itemClick -= OnItemClickEvent;
        EventSystem.itemUse -= DeleteItemFromBag;
    }

    private void DeleteItemFromBag(ItemEnums obj)
    {
        isHand = false;
        handItem = ItemEnums.Null;
        hand.gameObject.SetActive(false);
    }

    private void Update()
    {
        var canClick = ObejctMousePosition();
        if (canClick && Input.GetMouseButtonDown(0))
            CursorAction(canClick.gameObject);

        if (hand.gameObject.activeInHierarchy)
        {
            hand.position = Input.mousePosition;
        }
    }

    private void CursorAction(GameObject gameObject)
    {
        switch(gameObject.tag)
        {
            case "teleport":
                var transBtn = gameObject.GetComponent<Translate>();
                transBtn?.TranslateScene();
                break;
            case "item":
                var itemBtn = gameObject.GetComponent<Item>();
                itemBtn?.ItemClick();
                break;
            case "interaction":
                var interaction = gameObject.GetComponent<Interaction>();
                if (isHand)
                    interaction.CheckItem(handItem);
                else
                    interaction.CheckEmpty();
                break;
        }
    }

    private Collider2D ObejctMousePosition()
    {
        return Physics2D.OverlapPoint(cursorPos);
    }
    
    private void OnItemClickEvent(ItemDetail itemDetail, bool isSelect)
    {
        isHand = isSelect;
        if (isSelect)
        {
            handItem = itemDetail.itemName;
        }
        hand.gameObject.SetActive(isSelect);
    }
}
