using Newtonsoft.Json;
using Seer.AGVController;
using System;
using System.Collections.Generic;

namespace Seer.AGVController
{
    [Serializable]
    public class AGVStatusNavigationStateFrame : AGVResponseBase
    {
        public enum AGVTaskStatus
        {
            NONE = 0,
            WAITING,
            RUNNING,
            SUSPENDED,
            COMPLETED,
            FAILED,
            CANCELED
        }
        /// <summary>
        /// 任务状态
        /// </summary>
        [JsonProperty("task_status")]
        public AGVTaskStatus TaskStatus { get; set; }
        public enum AGVTaskType
        {
            没有导航 = 0,
            自由导航到任意点 = 1,
            自由导航到站点 = 2,
            路径导航到站点 = 3,
            钻货架 = 5,
            跟随 = 6,
            平动转动 = 7,
            磁条导航 = 8,
            其他 = 100
        }
        /// <summary>
        /// 任务类型
        /// </summary>
        [JsonProperty("task_type")]
        public int TaskType { get; set; }
        /// <summary>
        /// 当前导航要去的站点, 
        /// 仅当 task_type 为 2 或 3 时该字段有效, 
        /// task_status 为 RUNNING 时说明正在去这个站点, 
        /// task_status 为 COMPLETED 时说明已经到达这个站点, 
        /// task_status 为 FAILED 时说明去这个站点失败, 
        /// task_status 为 SUSPENDED 说明去这个站点的导航暂停
        /// </summary>
        [JsonProperty("target_id")]
        public string TargetId { get; set; }
        /// <summary>
        /// 当前导航要去的坐标点
        /// </summary>
        [JsonProperty("target_point")]
        public List<int> TargetPoint { get; set; }
        /// <summary>
        /// 当前导航路径上已经经过的站点
        /// </summary>
        [JsonProperty("finished_path")]
        public List<string> FinishedPath { get; set; }
        /// <summary>
        /// 当前导航路径上尚未经过的站点
        /// </summary>
        [JsonProperty("UnfinishedPath")]
        public List<string> UnfinishedPath { get; set; }
        /// <summary>
        /// 调度系统 RoboRoute 让机器人去的最终目标站点 ID
        /// </summary>
        [JsonProperty("roboroute_target")]
        public string RoboRouteTarget { get; set; }

    }
}
