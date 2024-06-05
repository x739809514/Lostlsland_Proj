
    using System;
    using UnityEngine;

    public class Ball : MonoBehaviour
    {
        private SpriteRenderer spriteRenderer;
        public BallDetail ballDetail;
        public bool isMatch;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void SetBall(BallDetail ball)
        {
            ballDetail = ball;
            if (isMatch)
            {
                SetCorrect();
            }
            else
            {
                SetWrong();
            }
        }

        public void SetCorrect()
        {
            spriteRenderer.sprite = ballDetail.correctSprite;
        }

        public void SetWrong()
        {
            spriteRenderer.sprite = ballDetail.wrongSprite;
        }
    }
