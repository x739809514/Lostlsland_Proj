
    using System;
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using UnityEngine.UI;

    public class MenuUI : MonoBehaviour
    {
        public Button menu;

        private void Start()
        {
            menu.onClick.AddListener(OnMenuClick);
        }

        private void OnMenuClick()
        {
            var curScene = SceneManager.GetActiveScene().name;
            SaveLoadManager.Instance.Serialize();
            TransitionManager.Instance.Transition(curScene,"Menu");
        }
    }
