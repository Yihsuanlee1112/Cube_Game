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
        /// 遊戲Level
        /// </summary>
        public string UserColor { get; set; }

        /// <summary>
        /// FlowData 构造函数
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="Username"></param>
        /// <param name="languageType"></param>
        /// <param name="Level"></param>
        /// <param name="remindType"></param>
        /// <param name="gameData"></param>
        public GameFlowData(string UserID, Language languageType, string Username, Level Level, string UserColor)
        {
            this.Language = languageType;
            this.UserId = UserID;
            this.UserName = Username;
            this.Level = Level;
            this.UserColor = UserColor;
        }

        public GameFlowData()
        {
        }
    }
}