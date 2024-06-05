using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MiniGame : MonoBehaviour
{
    public UnityEvent finishGame;
    [SceneName]public string gameScene;
    public bool isPass;

    public void UpdateMiniGameState()
    {
        if (isPass)
        {
            GetComponent<Collider2D>().enabled = false;
            finishGame?.Invoke();
            gameObject.SetActive(false);
        }
    }
}
