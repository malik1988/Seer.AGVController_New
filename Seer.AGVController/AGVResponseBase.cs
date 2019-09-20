using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Seer.AGVController
{
    [Serializable]
    public class AGVResponseBase
    {
        /// <summary>
        /// API 错误码
        /// </summary>
        [JsonProperty("ret_code")]
        public AGVErrorCodeTypes RetCode { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        [JsonProperty("err_msg")]
        public string ErrMsg { get; set; }
    }
}
