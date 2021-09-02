	using UnityEngine;
using System.Collections;
using System.IO;
namespace LuaFramework.NetWork
{
    /// <summary>
    /// 消息协议类
    /// </summary>
    public class MessageProtocol
    {
        public int oneNumber;//一级协议
        public int twoNumber;//二级协议
        public int length;//实际数据长度
        public byte[] buffer=new byte[] { };//实际消息数据
        public byte[] duoYuBytes=new byte[] { };//多余数据字节数组
        #region 构造
        public MessageProtocol() { }
        public MessageProtocol(int oneNumber, int twoNumber, int length, byte[] buffer)
        {
            this.oneNumber = oneNumber;
            this.twoNumber = twoNumber;
            this.buffer = buffer;
            this.length = this.buffer.Length;
        }
        #endregion

        /// <summary>
        /// 将消息协议对象转化字节数组
        /// </summary>
        /// <returns></returns>
        public byte[] ToBytes()
        {
            byte[] bytes;//自定义字节数组，用以装载消息协议
            using (MemoryStream memorySteam = new MemoryStream())//创建内存流
            {
                BinaryWriter binaryWriter = new BinaryWriter(memorySteam);//以二进制写入器往这个流里写内容
                binaryWriter.Write(this.oneNumber);//写入协议一级标志，占4个字节
                binaryWriter.Write(this.twoNumber);//写入协议二级标志，占4个字节
                binaryWriter.Write(this.length);//写入消息的长度，占4个字节
                if (this.length > 0)
                {
                    binaryWriter.Write(this.buffer);//写入消息实际内容
                }
                bytes = memorySteam.ToArray();
                binaryWriter.Close();
            }
            return bytes;
        }

        /// <summary>
        /// 从字节数组得到消息类对象
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static MessageProtocol FromBytes(byte[] data)
        {
            int dataLength = data.Length;
            MessageProtocol protocol = new MessageProtocol();
            using (MemoryStream memoryStream = new MemoryStream(data))//将字节数组填充至内存流
            {
                BinaryReader binaryReader = new BinaryReader(memoryStream);//以二进制读取器读取该流内容
                protocol.oneNumber = binaryReader.ReadInt32();//读取一级协议，占4字节
                protocol.twoNumber = binaryReader.ReadInt32();//读取二级协议，占4字节
                protocol.length = binaryReader.ReadInt32();//读取数据的长度，占4字节
                //如果【进来的Bytes长度】大于【一个完整的MessageXieYi长度】
                if (dataLength - 12 > protocol.length)
                {
                    protocol.buffer = binaryReader.ReadBytes(protocol.length);//读取实际消息的内容，从第13个字节开始读取，长度是消息的场地
                    protocol.duoYuBytes = binaryReader.ReadBytes(dataLength - 12 - protocol.length);//读取多余字节的数据
                }
                //如果【进来的Bytes长度】等于于【一个完整的MessageXieYi长度】
                if (dataLength - 12 == protocol.length)
                {
                    protocol.buffer = binaryReader.ReadBytes(protocol.length);
                }
                binaryReader.Close();
            }
            return protocol;
        }

        /// <summary>
        /// 按照先后顺序合并2个字节数组，并返回合并后的字节数组
        /// </summary>
        /// <param name="firstBytes">第一个字节数组</param>
        /// <param name="firstIndex">第一个字节数组的开始截取索引</param>
        /// <param name="firstLength">第一个字节数组的截取长度</param>
        /// <param name="secondBytes">第二个字节数组</param>
        /// <param name="secondIndex">第二个字节数组的开始截取索引</param>
        /// <param name="secondLength">第二个字节数组的截取长度</param>
        /// <returns></returns>
        public static byte[] CombineBytes(byte[] firstBytes, int firstIndex, int firstLength, byte[] secondBytes, int secondIndex, int secondLength)
        {
            byte[] buffer;
            using (MemoryStream memoryStream = new MemoryStream())//创建内存流
            {
                BinaryWriter binaryWriter = new BinaryWriter(memoryStream);//创建二进制写入器，往流中写入数据
                binaryWriter.Write(firstBytes, firstIndex, firstLength);//写入第一个字节数组
                binaryWriter.Write(secondBytes, secondIndex, secondLength);//写入第二个字节数组
                buffer = memoryStream.ToArray();
                binaryWriter.Close();
            }
            return buffer;
        }
    }
}


