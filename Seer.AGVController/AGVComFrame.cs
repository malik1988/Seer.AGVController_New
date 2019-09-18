using Newtonsoft.Json;
using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Seer.AGVController
{
    public class AGVComFrame
    {
        #region 帧头定义
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        struct SeerHead
        {
            public byte sync;
            public byte version;
            public UInt16 number;
            public UInt32 length;
            public UInt16 type;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            private byte[] reserved;      //保留,6字节

            //public SeerHead()
            //{
            //    sync = SYNC;
            //    version = 1;
            //    number = (UInt16)(new Random(UInt16.MaxValue).Next());
            //    length = 0;
            //    type = 0;
            //    reserved = new byte[6] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0 };
            //}
            public SeerHead(AGVFrameTypes frameType, UInt32 len)
            {
                sync = SYNC;
                version = 1;
                number = (UInt16)(new Random(UInt16.MaxValue).Next());
                length = len;
                type = (UInt16)frameType;
                reserved = new byte[6] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0 };
            }

        }

        static readonly byte SYNC = 0x5A;
        static readonly byte HEAD_LEN = 1 + 1 + 2 + 4 + 2 + 6;
        static readonly UInt16 TYPE_RESPONSE_OFFSET = 10000;

        SeerHead Head;
        #endregion

        public AGVComFrame() { }
        public AGVComFrame(AGVFrameTypes type, object serialData)
        {
            if (serialData != null)
            {
                string data = JsonConvert.SerializeObject(serialData);
                byte[] bData = ASCIIEncoding.ASCII.GetBytes(data);
                Data = bData;
                Head = new SeerHead(type, (UInt16)bData.Length);
            }
            else
            {
                Head = new SeerHead(type, 0);
                Data = null;
            }
        }

        public AGVFrameTypes FrameType { get; private set; }

        public static int HeadLength { get { return Marshal.SizeOf(typeof(SeerHead)); } }
        public byte[] Data { get; private set; }
        public string DataString
        {
            get
            {
                try
                {
                    return ASCIIEncoding.ASCII.GetString(Data);
                }
                catch { return null; }
            }
        }

        public T DataParse<T>()
        {
            T t = default(T);
            try
            {
                return JsonConvert.DeserializeObject<T>(DataString);
            }
            catch { return t; }
        }
        public byte[] Pack()
        {
            int len = HeadLength;
            if (Data != null)
                len += Data.Length;
            byte[] tmp = null;
            IntPtr ptr = Marshal.AllocHGlobal(HeadLength);
            try
            {
                Marshal.StructureToPtr(Head, ptr, false);
                tmp = new byte[len];
                Marshal.Copy(ptr, tmp, 0, HeadLength);
                if (null != Data)
                {
                    Array.Copy(Data, 0, tmp, HeadLength, Data.Length);
                }
            }
            finally
            {
                Marshal.FreeHGlobal(ptr);
            }
            return tmp;
        }
        public static AGVComFrame Parse(byte[] buf)
        {
            if (buf.Length < HeadLength)
                return null;
            IntPtr ptr = Marshal.AllocHGlobal(HeadLength);
            AGVComFrame tmp = null;
            try
            {
                Marshal.Copy(buf, 0, ptr, HeadLength);
                SeerHead head = Marshal.PtrToStructure<SeerHead>(ptr);
                tmp = new AGVComFrame();
                tmp.FrameType = (AGVFrameTypes)head.type;
                uint length = head.length;
                if (length <= buf.Length - HeadLength)
                {
                    Array.Copy(buf, HeadLength, tmp.Data, 0, head.length);
                }
                else
                    tmp = null;
            }
            finally
            {
                Marshal.FreeHGlobal(ptr);
            }


            return tmp;
        }
    }
}
