using Seer.AGVController;
using SimpleTCP;
using System;

namespace Seer.AGVController
{
    public class AGVCommucation
    {
        SimpleTcpClient client = new SimpleTcpClient();
        public string Connect(string ip, int port)
        {
            try
            {
                client.Connect(ip, port);
                return "Success";
            }
            catch (Exception e)
            {
                return "Failed," + e.Message;
            }
        }

        public string Connect(string ip, AGVPortTypes port)
        {
            return Connect(ip, (int)port);
        }


        /// <summary>
        /// 发送并获取反馈
        /// </summary>
        /// <param name="frame"></param>
        /// <param name="timeout">超时时间，单位ms</param>
        /// <returns></returns>
        public AGVComFrame SendAndGet(AGVComFrame frame, int timeout = 300)
        {
            if (null != frame)
            {
                var msg = client.WriteLineAndGetReply(frame.Pack(), new TimeSpan(0, 0, 0, 0, timeout));
                if (null == msg)
                    return null;

                return AGVComFrame.Parse(msg.Data);
            }
            else
                return null;
        }


        public void Disconnect()
        {
            client.Disconnect();
        }
    }
    
        
  

 

}
