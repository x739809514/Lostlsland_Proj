
    using System.Collections.Generic;

    public class Holder : Interaction
    {
        public bool isEmpty;
        public HashSet<Holder> linkHolder=new HashSet<Holder>();
        public BallName correctBall;
        public Ball curBall;

        public void CheckMatch(Ball ball)
        {
            curBall = ball;
            if (ball.ballDetail.ballName==correctBall)
            {
                ball.isMatch = true;
                ball.SetCorrect();
            }
            else
            {
                ball.isMatch = false;
                ball.SetWrong();
            }
        }

        public override void CheckEmpty()
        {
            foreach (var holder in linkHolder)
            {
                if (holder.isEmpty)
                {
                    //移动
                    curBall.transform.position = holder.transform.position;
                    curBall.transform.SetParent(holder.transform);

                    holder.CheckMatch(curBall);
                    curBall = null;
                    
                    isEmpty = true;
                    holder.isEmpty = false;
                }
            }
            EventSystem.CallFinishMiniGame();
        }
    }
