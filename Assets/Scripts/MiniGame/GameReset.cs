
    using System;
    using DG.Tweening;
    using UnityEngine;

    public class GameReset : Interaction
    {
        private Transform gearSprite;

        private void Awake()
        {
            gearSprite = transform.GetChild(0);
        }

        public override void CheckEmpty()
        {
            gearSprite.DOPunchRotation(Vector3.forward * 180, 1, 1, 0);
            MiniGameController.Instance.ResetGame();
        }
    }
