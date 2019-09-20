using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Seer.AGVController
{
    /// <summary>
    /// 导航数据帧
    /// </summary>
    [Serializable]
    public class AGVNavigationDataFrame
    {
        /// <summary>
        /// 站点号
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }
        /// <summary>
        /// 动作
        /// </summary>
        [JsonProperty("operation")]
        public string Operation { get; set; }
        /// <summary>
        /// 层数
        /// </summary>
        [JsonProperty("layer")]
        public int Layer { get; set; }
        /// <summary>
        /// 是否识别
        /// </summary>
        [JsonProperty("recognize")]
        public bool Recognize { get; set; }
    }
}
