﻿//using protocol;
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
            //ByteString bytestr = c2s.Data;
           // C0001_HeartbeatReqMessage pt_3 = C0001_HeartbeatReqMessage.Parser.ParseFrom(bytestr.ToByteArray());



            //byte[] b = ProtobufSerilizer.Serialize(c2s);
            byte[] b = c2s.ToByteArray();


           // C0001_HeartbeatReqMessage h = new C0001_HeartbeatReqMessage();
           // h.SendTime = 15f;
           // byte[] hbyte = c2s.ToByteArray();
          //  Debug.Log(hbyte.Length);
          //  Debug.Log(hbyte.Length + 4 + 4 * 3);

            //byte[] data = new byte[] { 1, c2s.ToByteString(), 0,0 };
            //byte[] b = new byte[0];
            if (b == null)
                return new byte[0];


            // ProtobufByteBuffer buf = null;
            //常规情况下，长度一般为4个字节
            //也可以出于安全目的，自由定义该长度，同时也可以插入一定的安全校验数据
            //if (insertLength)
            // {
            // buf = ProtobufByteBuffer.Allocate(b.Length + 4);
            // buf.WriteInt(b.Length + 4);
            //}
            // else
            // {
            // buf = ProtobufByteBuffer.Allocate(b.Length );
            // }



            //旧的  
            // 4 个字节 
            //Byte[] enddata = new byte[b.Length + 4];
            //buf.WriteInt(1);//4  id 


            //buf.WriteBytes(h.ToByteArray()); // data  13 

            //buf.WriteInt(1);// 4  seq
            // buf.WriteInt(1);// 4  msgType


            //新的 
            // buf.WriteBytes(b);//自定义的结构 
            //return buf.GetBytes();




            //NetBufferWriter writer = new NetBufferWriter();
            //writer.WriteString("abc");
            //return writer.Finish();


            string message = "abc";
            MemoryStream ms = null;
            using (ms = new MemoryStream())
            {
                ms.Position = 0;
                BinaryWriter writer = new BinaryWriter(ms);
                ushort msglen = (ushort)message.Length;
                writer.Write(msglen);
                writer.Write(message);
                writer.Flush();
                return ms.ToArray();
            }



           // MemoryStream m_stream = new MemoryStream();
           // BinaryWriter m_writer = new BinaryWriter(m_stream);
           // m_writer.Write(7);
           //m_writer.Write(10);
           //m_clientSocket.Send(writer.Finish());


            /**
            //byte[] msgmsg = new byte[b.Length + 4];
            //MemoryStream ms = new MemoryStream();
            //ms.Write(b.Length + 4);
            MemoryStream stream = new MemoryStream();
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                writer.Write(b.Length+4);
                writer.Write(b);
                //writer.Write(1);
                //writer.Write(1);

                byte[] bytes1 = stream.ToArray();
                return bytes1;
            }
           // byte[] bytes = stream.ToArray();
            byte[] bytes =  stream.GetBuffer();
            return bytes;
            **/
        }
    }
}