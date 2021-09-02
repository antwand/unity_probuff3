using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor.PackageManager;
using UnityEngine;
using System;

namespace CCEngine
{
    /// <summary>
    /// 工具类：对象与二进制流间的转换
    /// </summary>
    class ByteConvertHelper
    {

        /// <summary>
        /// 将对象转换为byte数组
        /// </summary>
        /// <param name="obj">被转换对象</param>
        /// <returns>转换后byte数组</returns>
        public static byte[] Object2Bytes(object obj)
        {
            byte[] buff;
            using (MemoryStream ms = new MemoryStream())
            {
                IFormatter iFormatter = new BinaryFormatter();
                iFormatter.Serialize(ms, obj);
                buff = ms.GetBuffer();
            }
            return buff;
        }

        /// <summary>
        /// 将byte数组转换成对象
        /// </summary>
        /// <param name="buff">被转换byte数组</param>
        /// <returns>转换完成后的对象</returns>
        public static object Bytes2Object(byte[] buff)
        {
            object obj;
            using (MemoryStream ms = new MemoryStream(buff))
            {
                IFormatter iFormatter = new BinaryFormatter();
                obj = iFormatter.Deserialize(ms);
            }
            return obj;
        }


        ////////////////////////////////////////////////////////////////////////


        // Start is called before the first frame update
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        ///  //与这个的区别  byte[] data = BitConverter.GetBytes((Int32)6);
        public static byte[] Int32ToBytes(uint number)
        {
            byte[] bs = new byte[4];
            byte a = (byte)(number >> 24);
            byte b = (byte)((number & 0xff0000) >> 16);
            byte c = (byte)((number & 0xff00) >> 8);
            byte d = (byte)(number & 0xff);
            bs[0] = a;
            bs[1] = b;
            bs[2] = c;
            bs[3] = d;
            return bs;
        }


        /**
         * 
         * */
        public static uint BytesToInt32(byte[] bs)
        {
            if (bs == null || bs.Length != 4)
            {
                throw new EncryptionException(ErrorCode.Unknown, "传入数组长度不为4");
            }
            //获取最高八位
            uint num1 = 0;
            num1 = (uint)(Convert.ToInt32(num1) ^ (int)bs[0]);
            num1 = num1 << 24;
            //获取第二高八位
            uint num2 = 0;
            num2 = (uint)(Convert.ToInt32(num2) ^ (int)bs[1]);
            num2 = num2 << 16;
            //获取第二低八位
            uint num3 = 0;
            num3 = (uint)(Convert.ToInt32(num3) ^ (int)bs[2]);
            num3 = num3 << 8;
            //获取低八位
            uint num4 = 0;
            num4 = (uint)(Convert.ToInt32(num4) ^ (int)bs[3]);
            return num1 ^ num2 ^ num3 ^ num4;
        }




        ////////////////////////////////////////////////////////////////////////

        /**
         *  吧 struct 转化为 byte数组 
         * */
        public static byte[] StructToBytes(object structObj)
        {
            int size = Marshal.SizeOf(structObj);
            IntPtr buffer = Marshal.AllocHGlobal(size);
            try
            {
                Marshal.StructureToPtr(structObj, buffer, true);
                byte[] bytes = new byte[size];
                Marshal.Copy(buffer, bytes, 0, size);
                return bytes;
            }
            finally
            {
                Marshal.FreeHGlobal(buffer);
            }
        }
    }
}