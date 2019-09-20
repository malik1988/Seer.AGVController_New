
using System.Collections.Generic;

namespace Seer.AGVController
{
    public class AgvController
    {
        public string Ip { get; private set; } = "127.0.0.1";
        public string Name { get; set; }
        AGVCommucation comNavi = new AGVCommucation();
        AGVCommucation comStatus = new AGVCommucation();
        public bool IsConnected { get; private set; }

        IAGVOperationApi oprations = new AGVOperations();
        public AgvController()
        { }
        public AgvController(string ip, string name = "")
        {
            Ip = ip;
            Name = name;
        }
        public string Connect()
        {
            string retNavi = comNavi.Connect(Ip, AGVPortTypes.导航);
            if (retNavi == "Success")
            {
                IsConnected = true;
            }
            else
                IsConnected = false;
            string retStatus = comStatus.Connect(Ip, AGVPortTypes.状态);
            if (retStatus == "Success")
            {
                IsConnected = true;
            }
            else
                IsConnected = false;
            return retNavi + ":" + retStatus;
        }
        /// <summary>
        /// 设置路径导航
        /// </summary>
        /// <param name="pos"></param>
        public AGVErrorCodeTypes SetNavi(string pos)
        {

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
                if (null != resp)
                {//导航发送成功
                    return resp.RetCode;
                }
            }
            return AGVErrorCodeTypes.未知错误;
        }
        /// <summary>
        /// 取消导航
        /// </summary>
        public AGVErrorCodeTypes CancelNavi()
        {
            AGVComFrame sendFrame = new AGVComFrame(AGVFrameTypes.导航_取消当前导航, null);
            AGVComFrame recvFrame = comNavi.SendAndGet(sendFrame);

            if (recvFrame != null)
            {
                AGVNavigationResponse resp = recvFrame.DataParse<AGVNavigationResponse>();
                if (null != resp)
                {//导航取消成功
                    return resp.RetCode;
                }
            }
            return AGVErrorCodeTypes.未知错误;
        }
        /// <summary>
        /// 暂停导航
        /// </summary>
        public AGVErrorCodeTypes PauseNavi()
        {
            AGVComFrame sendFrame = new AGVComFrame(AGVFrameTypes.导航_暂停当前导航, null);
            AGVComFrame recvFrame = comNavi.SendAndGet(sendFrame);

            if (recvFrame != null)
            {
                AGVNavigationResponse resp = recvFrame.DataParse<AGVNavigationResponse>();
                if (null != resp)
                {//导航取消成功
                    return resp.RetCode;
                }
            }
            return AGVErrorCodeTypes.未知错误;
        }
        /// <summary>
        /// 继续当前导航
        /// </summary>
        public AGVErrorCodeTypes ContinueNavi()
        {
            AGVComFrame sendFrame = new AGVComFrame(AGVFrameTypes.导航_继续当前导航, null);
            AGVComFrame recvFrame = comNavi.SendAndGet(sendFrame);

            if (recvFrame != null)
            {
                AGVNavigationResponse resp = recvFrame.DataParse<AGVNavigationResponse>();
                if (null != resp)
                {//导航取消成功
                    return resp.RetCode;
                }
            }
            return AGVErrorCodeTypes.未知错误;
        }
        /// <summary>
        /// 获取导航路径
        /// </summary>
        /// <param name="target">目的地</param>
        /// <returns></returns>
        public List<string> GetPathNavi(string target)
        {
            AGVComFrame sendFrame = new AGVComFrame(AGVFrameTypes.导航_获取路径导航的路径, new { id = target });
            AGVComFrame recvFrame = comNavi.SendAndGet(sendFrame);

            if (recvFrame != null)
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

            if (recvFrame != null)
            {
                AGVStatusNavigationStateFrame resp = recvFrame.DataParse<AGVStatusNavigationStateFrame>();
                if (null != resp && resp.RetCode == AGVErrorCodeTypes.成功)
                {//导航获取成功
                    return resp;
                }
            }
            return null;
        }

        public AGVStatusPositionFrame GetPosition()
        {
            AGVComFrame recvFrame = comStatus.SendAndGet(new AGVComFrame(AGVFrameTypes.状态_查询机器人位置, null));

            if (recvFrame != null)
            {
                AGVStatusPositionFrame resp = recvFrame.DataParse<AGVStatusPositionFrame>();
                if (null != resp && resp.RetCode == AGVErrorCodeTypes.成功)
                {//导航获取成功
                    return resp;
                }
            }
            return null;
        }
    }
}
