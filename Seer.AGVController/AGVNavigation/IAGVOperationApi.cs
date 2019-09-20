using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seer.AGVController
{
    public interface IAGVOperationApi
    {
        string Load { get;  }
        string Unload { get;  }
    }
}
