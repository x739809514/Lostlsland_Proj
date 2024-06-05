using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : Singleton<TransitionManager>, ISaveable
{
    public CanvasGroup fadeCanvasGroup;
    public float fadeDuration;

    private bool isFade;
    private string curScene;
    private string startScene = "H1";
    private bool canTransition;

    private void Start()
    {
        ISaveable saveable = this;
        saveable.Register();
    }

    private void OnEnable()
    {
        EventSystem.updateGameState += OnUpdateGameStateEvent;
        EventSystem.startGameEvent += OnStartNewGameEvent;
    }

    private void OnDisable()
    {
        EventSystem.updateGameState -= OnUpdateGameStateEvent;
        EventSystem.startGameEvent -= OnStartNewGameEvent;
    }

    /// <summary>
    /// 新游戏进入的场景
    /// </summary>
    /// <param name="week"></param>
    private void OnStartNewGameEvent(int week)
    {
        StartCoroutine(TransitionScene("Menu", startScene));
    }

    private void OnUpdateGameStateEvent(GameState obj)
    {
        canTransition = obj == GameState.GamePlay;
    }

    public void Transition(string from, string to)
    {
        if (isFade == false && canTransition)
        {
            StartCoroutine(TransitionScene(from, to));
        }
    }

    private IEnumerator TransitionScene(string from, string to)
    {
        if (from != string.Empty)
        {
            EventSystem.CallBeforeUnloadScene();
            yield return Fade(1.0f);
            yield return SceneManager.UnloadSceneAsync(from);
        }

        yield return SceneManager.LoadSceneAsync(to, LoadSceneMode.Additive);

        var scene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
        SceneManager.SetActiveScene(scene);
        EventSystem.CallAfterLoadScene();
        yield return Fade(0f);
    }

    private IEnumerator Fade(float targetAlpha)
    {
        isFade = true;
        fadeCanvasGroup.blocksRaycasts = true;

        var speed = Mathf.Abs(fadeCanvasGroup.alpha - targetAlpha) / fadeDuration;

        while (Mathf.Approximately(fadeCanvasGroup.alpha, targetAlpha) == false)
        {
            fadeCanvasGroup.alpha = Mathf.MoveTowards(fadeCanvasGroup.alpha, targetAlpha, speed * Time.deltaTime);
            yield return null;
        }

        isFade = false;
        fadeCanvasGroup.blocksRaycasts = false;
    }

    public SaveData GenerateSaveData()
    {
        SaveData data = new SaveData();
        data.curScene = SceneManager.GetActiveScene().name;
        return data;
    }

    public void ReadGameData(SaveData gameData)
    {
        curScene = gameData.curScene;
        
        Transition("Menu", curScene);
    }
}