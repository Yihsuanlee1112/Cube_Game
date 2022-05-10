using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataSync;



namespace GameData
{
    public class GameFlowData : LabDataBase
    {
        /// <summary>
        /// 语言
        /// </summary>
        public Language Language { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserId { get; set; } = "Test01";
        public string UserName { get; set; }

        /// <summary>
        /// 遊戲Level
        /// </summary>
        public Level Level { get; set; }

        /// <summary>
        /// 玩家顏色
        /// </summary>
        public string UserColor { get; set; }
        public string UserFirstColor { get; set; }

        public int EyeSec { get; set; }
        public int HandSec { get; set; }
        public int BreathSec { get; set; }
        public bool LeftHand { get; set; }
        public bool RightHand { get; set; }


        public int GameTimeMin { get; set; }
        //public int _isHaveInterference { get; set; } // 0: 有干擾物, 1: 沒有干擾物
        //public int[] InterferenceStatus { get; set; }
        //public int NumOfInterference { get; set; }
        //public int isAdvanced { get; set; }


        /// <summary>
        /// FlowData 构造函数
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="Username"></param>
        /// <param name="languageType"></param>
        /// <param name="Level"></param>
        /// <param name="UserColor"></param>
        /// <param name="remindType"></param>
        /// <param name="gameData"></param>
        public GameFlowData(string UserID, Language languageType, string Username, Level Level, string UserColor, string UserFirstColor)
        {
            this.Language = languageType;
            this.UserId = UserID;
            this.UserName = Username;
            this.Level = Level;
            this.UserColor = UserColor;
            this.UserFirstColor = UserFirstColor;
        }

        public GameFlowData()
        {
        }
    }
}