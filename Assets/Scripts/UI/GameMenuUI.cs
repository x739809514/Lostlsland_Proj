
    using UnityEngine;

    public class GameMenuUI : MonoBehaviour
    {
        public void QuitGame()
        {
            SaveLoadManager.Instance.Serialize();
            Application.Quit();
        }

        /// <summary>
        /// 继续游戏
        /// </summary>
        public void Continue()
        {
            SaveLoadManager.Instance.AntiSerializeObject();
        }

        /// <summary>
        /// 新游戏
        /// </summary>
        /// <param name="gameWeek"></param>
        public void StartGameWeek(int gameWeek)
        {
            EventSystem.CallGameWeekEvent(gameWeek);
        }
    }
