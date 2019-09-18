using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGV.AGVController
{
    public class AGVOperations : IAGVOperationApi
    {
        public  string Load { get { return "ForkLoad"; } }

        public  string Unload { get { return "ForkUnload"; } }
    }
}
