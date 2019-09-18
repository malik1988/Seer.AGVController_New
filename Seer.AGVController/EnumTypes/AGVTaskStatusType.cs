using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seer.AGVController
{
    public enum AGVTaskStatusType
    {
        NONE = 0,
        WAITING,
        RUNNING,
        SUSPENDED,
        COMPLETED,
        FAILED,
        CANCELED
    }
}
