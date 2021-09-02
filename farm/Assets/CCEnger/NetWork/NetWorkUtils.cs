//using protocol;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Google.Protobuf;
using UGCF.Network;


using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
using Google.Protobuf.Reflection;
using System.Runtime.InteropServices;
using System.IO;
using CCEngine;
using System.Text;

namespace UGCF.Network
{

    public class NetWorkUtils
    {



        /// <summary>
        /// 构建消息数据包
        /// </summary>
        /// <param name="c2s">消息主体</param>
        /// <param name="insertLength">是否在消息头部插入消息长度</param>
        public static byte[] BuildPackage(Header c2s, bool insertLength = false)
        {
            byte[] msgBytes = c2s.ToByteArray();

            //开始封装 
            int begin_leng = 4;
            uint len = (uint)(begin_leng + msgBytes.Length);
            byte[] sendBytes = new byte[len];
            byte a = (byte)(len >> 24);
            byte b = (byte)((len & 0xff0000) >> 16);
            byte c = (byte)((len & 0xff00) >> 8);
            byte d = (byte)(len & 0xff);
            sendBytes[0] = a;
            sendBytes[1] = b;
            sendBytes[2] = c;
            sendBytes[3] = d;


            //组装 
            //byte[] sendBytes = new byte[len+2]; //发送信息还有头部信息
            //int id = 1;
            //sendBytes[4] = (byte)(id >> 0);
            //sendBytes[5] = (byte)(id >> 8);
            //begin_leng = begin_leng + 2;

            // 组装协议名称
            //Array.Copy(protoNameBytes, 0, sendBytes, 2, protoNameBytes.Length);
            // 组装协议
            Array.Copy(msgBytes, 0, sendBytes, begin_leng, msgBytes.Length);
            //ByteArray ba = new ByteArray(sendBytes);
            //msgBytes.CopyTo(sendBytes, msgBytes.Length);
            Debug.Log("发送数据长度："+ len);
            Debug.Log(sendBytes.Length);
            ByteConvertHelper.Console(sendBytes);

            return sendBytes;

            //构造
            /**
            string msg = "你好呀";
            byte[] data = new byte[1024 * 1024 * 3];
            data = Encoding.UTF8.GetBytes(msg);

            int msgID = 1;
            TSocketMessage ts = new TSocketMessage(msgID, data);
            MarshalEndian marshalEndian = new MarshalEndian();
            byte[] marshalEndian_buff = marshalEndian.Encode(ts);
            return (marshalEndian_buff);
            **/

            /**
            //内容的长度 
            int msglen = ctsByte.Length;
            byte[] msglen_bytes = ByteConvertHelper.Int32ToBytes((uint)msglen);

            MemoryStream ms = null;
            using (ms = new MemoryStream())
            {
                ms.Position = 0;
                BinaryWriter writer = new BinaryWriter(ms);

                writer.Write(msglen_bytes);//写入长度 
                writer.Write(ctsByte);//数据
                writer.Flush();
                return ms.ToArray();
            }
            **/
        }
    }
}