
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    [CreateAssetMenu(fileName = "BallDataList_So",menuName = "Ball/BallDataList_So")]
    public class BallData : ScriptableObject
    {
        [SceneName]public string gameName;
        /// <summary>
        /// 球的信息
        /// </summary>
        public List<BallDetail> ballDetails;
        /// <summary>
        /// 球之间的线
        /// </summary>
        public List<BallLine> BallLines;
        /// <summary>
        /// 开始时球的位置
        /// </summary>
        public List<BallName> startBallOrders;

        /// <summary>
        /// 根据球的名字查找球的信息
        /// </summary>
        /// <param name="ballName"></param>
        /// <returns></returns>
        public BallDetail GetBallDetail(BallName ballName)
        {
            return ballDetails.Find(i => i.ballName == ballName);
        }
    }

    [Serializable]
    public class BallDetail
    {
        public BallName ballName;
        public Sprite correctSprite;
        public Sprite wrongSprite;
    }

    [Serializable]
    public class BallLine
    {
        public int from;
        public int to;
    }

    [Serializable]
    public class StartBallOrder
    {
        public BallName ballName;
    }
