using System;

public static class EventSystem
{
    /// <summary>
    /// 更新背包中的物品
    /// </summary>
    public static Action<ItemDetail,int> updateItem;
    /// <summary>
    /// 场景加载之前
    /// </summary>
    public static Action beforeUnloadScene;
    /// <summary>
    /// 场景加载之后
    /// </summary>
    public static Action afterLoadScene;
    /// <summary>
    /// 点击物品
    /// </summary>
    public static Action<ItemDetail,bool> itemClick;
    /// <summary>
    /// 使用物品
    /// </summary>
    public static Action<ItemEnums> itemUse;
    /// <summary>
    /// 输出相应对话
    /// </summary>
    public static Action<string> dialogPop;
    /// <summary>
    /// 更新游戏状态
    /// </summary>
    public static Action<GameState> updateGameState;
    /// <summary>
    /// 结束小游戏
    /// </summary>
    public static Action finishMiniGame;
    /// <summary>
    /// 结束游戏
    /// </summary>
    public static Action<string> passGameEvent;
    /// <summary>
    /// 开始游戏周目
    /// </summary>
    public static Action<int> startGameEvent;

    public static void CallItemUpdate(ItemDetail itemDetail,int index)
    {
        updateItem?.Invoke(itemDetail,index);
    }

    public static void CallBeforeUnloadScene()
    {
        beforeUnloadScene?.Invoke();
    }

    public static void CallAfterLoadScene()
    {
        afterLoadScene?.Invoke();
    }

    public static void CallItemClick(ItemDetail itemDetail,bool isSelect)
    {
        itemClick?.Invoke(itemDetail,isSelect);
    }

    public static void CallItemUseClick(ItemEnums itemName)
    {
        itemUse?.Invoke(itemName);
    }

    public static void CallDialogPopEvent(string dialog)
    {
        dialogPop?.Invoke(dialog);
    }

    public static void CallGameStateEvent(GameState state)
    {
        updateGameState?.Invoke(state);
    }

    public static void CallFinishMiniGame()
    {
        finishMiniGame?.Invoke();
    }

    public static void CallPassGameEvent(string gameName)
    {
        passGameEvent?.Invoke(gameName);
    }

    public static void CallGameWeekEvent(int week)
    {
        startGameEvent?.Invoke(week);
    }
}
