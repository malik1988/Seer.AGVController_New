using Newtonsoft.Json;
using Seer.AGVController;
using System;
using System.Collections.Generic;

namespace AGV.AGVController
{
    [Serializable]
   public class AGVStatusNavigationStateFrame:AGVResponseBase
    {
        [JsonProperty("task_status")]
        public int TaskStatus { get; set; }
        [JsonProperty("task_type")]
        public int TaskType { get; set; }
        [JsonProperty("target_id")]
        public string TargetId { get; set; }

        [JsonProperty("target_point")]
        public List<int> TargetPoint { get; set; }
        [JsonProperty("finished_path")]
        public List<string> FinishedPath { get; set; }

        [JsonProperty("UnfinishedPath")]
        public List<string> UnfinishedPath { get; set; }
        [JsonProperty("roboroute_target")]
        public string RoboRouteTarget { get; set; }
        
    }
}
