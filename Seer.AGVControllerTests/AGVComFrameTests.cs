﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Seer.AGVController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Seer.AGVController.Tests
{
    [TestClass()]
    public class AGVComFrameTests
    {
        [TestMethod()]
        public void ParseTestSendFrame()
        {
            byte[] data = new byte[] { 0x5A, 0x01, 0x00, 0x01, 0x00, 0x00, 0x00, 0x1C, 0x07, 0xD2, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x7B, 0x22, 0x78, 0x22, 0x3A, 0x31, 0x30, 0x2E, 0x30, 0x2C, 0x22, 0x79, 0x22, 0x3A, 0x33, 0x2E, 0x30, 0x2C, 0x22, 0x61, 0x6E, 0x67, 0x6C, 0x65, 0x22, 0x3A, 0x30, 0x7D };
            AGVComFrame frame = AGVComFrame.Parse(data);
            string dataStr = frame.DataString;
            Assert.IsNotNull(dataStr);
        }
        [TestMethod()]
        public void ParseTestRecvFrame()
        {
            byte[] data = new byte[] {0x5A,0x01,0x00,0x01,0x00,0x00,0x00,0x3C,0x2A,0xFC,0x00,0x00,0x00,0x00,0x00,0x00,
0x7B,0x22,0x72,0x65,0x74,0x5F,0x63,0x6F,0x64,0x65,0x22,0x3A,0x30,0x2C,0x22,0x78,0x22,0x3A,0x36,0x2E,0x30,0x2C,0x22,0x79,0x22,0x3A,0x32,0x2E,0x30,0x2C,
0x22,0x61,0x6E,0x67,0x6C,0x65,0x22,0x3A,0x31,0x2E,0x35,0x37,0x2C,0x22,0x63,0x6F,0x6E,0x66,0x69,0x64,0x65,0x6E,0x63,0x65,0x22,0x3A,0x30,0x2E,0x39,0x7D,};
            AGVComFrame frame = AGVComFrame.Parse(data);
            string dataStr = frame.DataString;
            string expStr = "{\"ret_code\": 0, \"x\": 6.0,\"y\": 2.0,\"angle\": 1.57,\"confidence\": 0.9}";
            AGVStatusPositionFrame pos = frame.DataParse<AGVStatusPositionFrame>();
            Assert.IsTrue(pos.RetCode == AGVErrorCodeTypes.成功);
        }
    }
}