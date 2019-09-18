using AGV.AGVController;
using System.Collections.Generic;

namespace Seer.AGVController
{
    public class AgvController
    {
        public string Ip { get; private set; } = "127.0.01";
        AGVCommucation comNavi = new AGVCommucation();
        AGVCommucation comStatus = new AGVCommucation();
        bool _IsConnected = false;
        public bool IsConnected { get { return _IsConnected; } }


        IAGVOperationApi oprations = new AGVOperations();
        public string Connect(string ip = "127.0.0.1")
        {
            string retNavi = comNavi.Connect(ip, AGVPortTypes.导航);
            if (retNavi == "Success")
            {
                _IsConnected = true;
                Ip = ip;
            }
            string retStatus = comStatus.Connect(ip, AGVPortTypes.状态);
            if (retStatus == "Success")
            {
                _IsConnected = true;
            }
            return retNavi + ":" + retStatus;
        }
        /// <summary>
        /// 设置路径导航
        /// </summary>
        /// <param name="pos"></param>
        public void SetNavi(string pos)
        {
            if (!_IsConnected)
                return;

            AGVNavigationDataFrame naviData = new AGVNavigationDataFrame()
            {
                Id = pos,
                Operation = oprations.Load,
                Layer = 0,
                Recognize = false
            };
            AGVComFrame sendFrame = new AGVComFrame(AGVFrameTypes.导航_路径导航, naviData);
            AGVComFrame recvFrame = comNavi.SendAndGet(sendFrame);

            if (recvFrame != null)
            {
                AGVNavigationResponse resp = recvFrame.DataParse<AGVNavigationResponse>();
                if (null != resp && resp.RetCode == AGVErrorCodeTypes.成功)
                {//导航发送成功

                }
            }
        }
        /// <summary>
        /// 取消导航
        /// </summary>
        public void CancelNavi()
        {
            AGVComFrame sendFrame = new AGVComFrame(AGVFrameTypes.导航_取消当前导航, null);
            AGVComFrame recvFrame = comNavi.SendAndGet(sendFrame);

            if (recvFrame != null )
            {
                AGVNavigationResponse resp = recvFrame.DataParse<AGVNavigationResponse>();
                if (null != resp && resp.RetCode == AGVErrorCodeTypes.成功)
                {//导航取消成功

                }
            }
        }
        /// <summary>
        /// 暂停导航
        /// </summary>
        public void PauseNavi()
        {
            AGVComFrame sendFrame = new AGVComFrame(AGVFrameTypes.导航_暂停当前导航, null);
            AGVComFrame recvFrame = comNavi.SendAndGet(sendFrame);

            if (recvFrame != null)
            {
                AGVNavigationResponse resp = recvFrame.DataParse<AGVNavigationResponse>();
                if (null != resp && resp.RetCode == AGVErrorCodeTypes.成功)
                {//导航暂停成功

                }
            }
        }
        /// <summary>
        /// 继续当前导航
        /// </summary>
        public void ContinueNavi()
        {
            AGVComFrame sendFrame = new AGVComFrame(AGVFrameTypes.导航_暂停当前导航, null);
            AGVComFrame recvFrame = comNavi.SendAndGet(sendFrame);

            if (recvFrame != null )
            {
                AGVNavigationResponse resp = recvFrame.DataParse<AGVNavigationResponse>();
                if (null != resp && resp.RetCode == AGVErrorCodeTypes.成功)
                {//导航暂停成功

                }
            }
        }
        /// <summary>
        /// 获取导航路径
        /// </summary>
        /// <
        /// <returns></returns>
        public List<string> GetPathNavi(string target)
        {
            AGVComFrame sendFrame = new AGVComFrame(AGVFrameTypes.导航_获取路径导航的路径, null);
            AGVComFrame recvFrame = comNavi.SendAndGet(sendFrame);

            if (recvFrame != null )
            {
                AGVNavigationResponse resp = recvFrame.DataParse<AGVNavigationResponse>();
                if (null != resp && resp.RetCode == AGVErrorCodeTypes.成功)
                {//导航获取成功
                    return resp.Path;
                }
            }
            return null;
        }


        public AGVStatusNavigationStateFrame GetTaskStatus(bool simple = false)
        {
            AGVComFrame sendFrame = new AGVComFrame(AGVFrameTypes.状态_查询机器人导航状态, new { simple = simple });
            AGVComFrame recvFrame = comStatus.SendAndGet(sendFrame);

            if (recvFrame != null )
            {
                AGVStatusNavigationStateFrame resp = recvFrame.DataParse<AGVStatusNavigationStateFrame>();
                if (null != resp && resp.RetCode == AGVErrorCodeTypes.成功)
                {//导航获取成功
                    return resp;
                }
            }
            return null;
        }
    }
}
