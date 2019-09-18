using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seer.AGVController
{
    /// <summary>
    /// API端口
    /// </summary>
    public enum AGVPortTypes : int
    {
        状态 = 19204,
        控制,
        导航,
        配置,
        其他 = 19210
    }
}
