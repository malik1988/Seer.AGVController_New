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
        [JsonProperty("ret_code")]
        public AGVErrorCodeTypes RetCode { get; set; }
        [JsonProperty("err_msg")]
        public string ErrMsg { get; set; }
    }
}
