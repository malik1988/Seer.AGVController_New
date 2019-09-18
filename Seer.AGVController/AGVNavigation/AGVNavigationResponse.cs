using Newtonsoft.Json;
using Seer.AGVController;
using System;
using System.Collections.Generic;

namespace AGV.AGVController
{
    [Serializable]
    public class AGVNavigationResponse : AGVResponseBase
    {
        [JsonProperty("path")]
        public List<string> Path { get; set; }
    }
}
