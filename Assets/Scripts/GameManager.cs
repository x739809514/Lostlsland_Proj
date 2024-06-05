using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour, ISaveable
{
    private Dictionary<string, bool> miniGameStateDic = new Dictionary<string, bool>();
    private int gameWeek;

    private void OnEnable()
    {
        EventSystem.afterLoadScene += OnAfterLoadScene;
        EventSystem.passGameEvent += OnPassGameEvent;
        EventSystem.startGameEvent += OnStartGameEvent;
        EventSystem.CallGameStateEvent(GameState.GamePlay);
    }

    private void OnDisable()
    {
        EventSystem.passGameEvent -= OnPassGameEvent;
        EventSystem.afterLoadScene -= OnAfterLoadScene;
        EventSystem.startGameEvent -= OnStartGameEvent;
    }

    private void Start()
    {
        //注册存储
        ISaveable saveable = this;
        saveable.Register();
    }

    private void OnStartGameEvent(int week)
    {
        gameWeek = week;
        miniGameStateDic.Clear();
    }

    private void OnAfterLoadScene()
    {
        foreach (var miniGame in FindObjectsOfType<MiniGame>())
        {
            if (miniGameStateDic.TryGetValue(miniGame.gameScene, out var isPass))
            {
                miniGame.isPass = isPass;
                miniGame.UpdateMiniGameState();
            }
        }

        var curGame = FindObjectOfType<MiniGameController>();
        curGame?.ChooseGameData(gameWeek);
    }

    private void OnPassGameEvent(string gameName)
    {
        miniGameStateDic[gameName] = true;
    }

    /// <summary>
    /// 生成一份存储数据
    /// </summary>
    /// <returns></returns>
    public SaveData GenerateSaveData()
    {
        SaveData data = new SaveData();
        data.gameWeek = gameWeek;
        data.miniGameStateDic = miniGameStateDic;
        return data;
    }

    public void ReadGameData(SaveData gameData)
    {
        this.gameWeek = gameData.gameWeek;
        this.miniGameStateDic = gameData.miniGameStateDic;
    }
}