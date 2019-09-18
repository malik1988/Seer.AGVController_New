using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Seer.AGVController
{
    [Serializable]
    public class AGVStatusPositionFrame : AGVResponseBase
    {
        /// <summary>
        /// 机器人的 x 坐标, 单位 m
        /// </summary>
        [JsonProperty("x")]
        public double X { get; set; }
        /// <summary>
        /// 机器人的 y 坐标, 单位 m
        /// </summary>
        [JsonProperty("y")]
        public double Y { get; set; }
        /// <summary>
        /// 机器人的 angle 坐标, 单位 rad
        /// </summary>
        [JsonProperty("angle")]
        public double Angle { get; set; }
        /// <summary>
        /// 机器人的定位置信度, 范围 [0, 1]
        /// </summary>
        [JsonProperty("confidence")]
        public double Confidence { get; set; }
        /// <summary>
        /// 机器人当前所在站点的 ID（该判断比较严格，机器人必须很靠近某一个站点(<50cm)，否则为空字符，即不处于任何站点）
        /// </summary>
        [JsonProperty("current_station")]
        public double CurrentStation { get; set; }
        /// <summary>
        /// 机器人上一个所在站点的 ID
        /// </summary>
        [JsonProperty("last_station")]
        public double LastStation { get; set; }
    }
}
