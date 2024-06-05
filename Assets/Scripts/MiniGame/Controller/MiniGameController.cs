using System;
using UnityEngine;
using UnityEngine.Events;

public class MiniGameController : Singleton<MiniGameController>
    {
        public BallData ballData;
        public Transform[] holders;
        public GameObject lineParent;
        public LineRenderer linePrefab;
        public Ball ballPrefab;
        public BallData[] gameDatas;

        public UnityEvent finishEvent;
        private void OnEnable()
        {
            EventSystem.finishMiniGame += OnCheckGameStateEvent;
        }

        private void OnDisable()
        {
            EventSystem.finishMiniGame -= OnCheckGameStateEvent;
        }

        private void OnCheckGameStateEvent()
        {
            foreach (var ball in FindObjectsOfType<Ball>())
            {
                if (ball.isMatch==false)
                    return;
            }
            
            //游戏结束
            foreach (var holder in holders)
            {
                holder.GetComponent<BoxCollider2D>().enabled = false;
            }
            finishEvent?.Invoke();
            EventSystem.CallPassGameEvent(ballData.gameName);
        }

        private void Start()
        {
            /*DrawLine();
            CreateBall();*/
        }

        private void DrawLine()
        {
            foreach (var line in ballData.BallLines)
            {
                var obj = Instantiate(linePrefab,lineParent.transform);
                obj.SetPosition(0,holders[line.from].position);
                obj.SetPosition(1,holders[line.to].position);

                holders[line.from].GetComponent<Holder>().linkHolder.Add(holders[line.to].GetComponent<Holder>());
                holders[line.to].GetComponent<Holder>().linkHolder.Add(holders[line.from].GetComponent<Holder>());
            }
        }

        private void CreateBall()
        {
            for (int i = 0; i < ballData.startBallOrders.Count; i++)
            {
                if (ballData.startBallOrders[i]==BallName.None)
                {
                    holders[i].GetComponent<Holder>().isEmpty = true;
                    continue;
                }

                Ball ball = Instantiate(ballPrefab, holders[i]);
                holders[i].GetComponent<Holder>().CheckMatch(ball);
                holders[i].GetComponent<Holder>().isEmpty = false;
                ball.SetBall(ballData.GetBallDetail(ballData.startBallOrders[i]));
            }
        }


        public void ResetGame()
        {
            foreach (var holder in holders)
            {
                if (holder.childCount>0)
                {
                    Destroy(holder.GetChild(0).gameObject);
                }
            }
            
            CreateBall();
        }

        
        public void ChooseGameData(int week)
        {
            ballData = gameDatas[week - 1];
            DrawLine();
            CreateBall();
        }
    }
