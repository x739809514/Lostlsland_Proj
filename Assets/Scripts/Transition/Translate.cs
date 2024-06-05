using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Translate : MonoBehaviour
{
    [SceneName]public string FromName;
    [SceneName]public string ToName;

    public void TranslateScene()
    {
        TransitionManager.Instance.Transition(FromName,ToName);
    }
}
