using UnityEngine;
using System.Collections;
using System;
namespace LuaFramework.NetWork
{
    public class MessageHandle
    {
        public byte[] buffer = new byte[] { };//数据动态缓存区

        public MessageHandle() { }

        /// <summary>
        /// 处理接收的数据
        /// </summary>
        /// <param name="data"></param>
        /// <param name="count"></param>
        public void HandleMessage(byte[] data, int count)
        {
            buffer = MessageProtocol.CombineBytes(buffer, 0, buffer.Length, data, 0, data.Length);
            while (true)
            {
                MessageProtocol protocol = MessageProtocol.FromBytes(data);
                if (buffer.Length < 12)//接收的数据不到12个字节不处理
                {
                    return;
                }
                else
                {
                    //消息对象的包头
                    protocol = MessageProtocol.FromBytes(buffer);
                    int firstFlag = protocol.oneNumber;
                    int secondFlag = protocol.twoNumber;
                    int msgContentLength = protocol.length;
                    while (buffer.Length - 12 >= msgContentLength)
                    {
                        protocol = null;
                        protocol = MessageProtocol.FromBytes(buffer);
                        //取出消息内容，派发消息
                        Debug.Log(BitConverter.ToString(protocol.buffer));
                        //再讲多余的数据重新复制给动态数组，此时的动态数组只包含多余的字节
                        buffer = protocol.duoYuBytes;
                        if (buffer.Length >= 12)
                        {
                            continue;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
        }
    }

}

