using System;
using System.IO;
using System.Text;
 
namespace CCEngine
{


    /**
     * 
        
     /// <summary>  
        /// 发送数据给服务器  
        /// </summary>  
        public void SendMessage(string data) {
            if(isConnected == false) {
                return;
            }
            try {
                NetBufferWriter writer = new NetBufferWriter();
                writer.WriteString(data);
                m_clientSocket.Send(writer.Finish());
            } catch {
                Disconnect();
            }
        }

        //result是字节数组byte[],从写入两个不同类型的数据
        ByteBuffer buff = new ByteBuffer();
        int protoId = ProtoDic.GetProtoIdByProtoType(0);
        buff.WriteShort((ushort)protoId);
        buff.WriteBytes(ms.ToArray());
        byte[] result = buff.ToBytes();
        ————————————————
        版权声明：本文为CSDN博主「河乐不为」的原创文章，遵循CC 4.0 BY-SA版权协议，转载请附上原文出处链接及本声明。
        原文链接：https://blog.csdn.net/linshuhe1/article/details/51386559


     * **/
    class NetBufferWriter {
        MemoryStream m_stream = null;
        BinaryWriter m_writer = null;
 
        int m_finishLength;
        public int finishLength {
            get { return m_finishLength; }
        }
 
        public NetBufferWriter() {
            m_finishLength = 0;
            m_stream = new MemoryStream();
            m_writer = new BinaryWriter(m_stream);
        }
 
        public void WriteByte(byte v) {
            m_writer.Write(v);
        }
 
        public void WriteInt(int v) {
            m_writer.Write(v);
        }
 
        public void WriteUInt(uint v) {
            m_writer.Write(v);
        }
 
        public void WriteShort(short v) {
            m_writer.Write(v);
        }
 
        public void WriteUShort(ushort v) {
            m_writer.Write(v);
        }
 
        public void WriteLong(long v) {
            m_writer.Write(v);
        }
 
        public void WriteULong(ulong v) {
            m_writer.Write(v);
        }
 
        public void WriteFloat(float v) {
            byte[] temp = BitConverter.GetBytes(v);
            Array.Reverse(temp);
            m_writer.Write(BitConverter.ToSingle(temp, 0));
        }
 
        public void WriteDouble(double v) {
            byte[] temp = BitConverter.GetBytes(v);
            Array.Reverse(temp);
            m_writer.Write(BitConverter.ToDouble(temp, 0));
        }
 
        public void WriteString(string v) {
            byte[] bytes = Encoding.UTF8.GetBytes(v);
            m_writer.Write((ushort)bytes.Length);
            m_writer.Write(bytes);
        }
 
        public void WriteBytes(byte[] v) {
            m_writer.Write(v.Length);
            m_writer.Write(v);
        }
 
        public byte[] ToBytes() {
            m_writer.Flush();
            return m_stream.ToArray();
        }
 
        public void Close() {
            m_writer.Close();
            m_stream.Close();
            m_writer = null;
            m_stream = null;
        }
 
        /// <summary>
        /// 将已写入的数据流，封装成一个新的数据流（现有数据长度+现有数据）
        /// 数据转换，网络发送需要两部分数据，一是数据长度，二是主体数据
        /// </summary>
        public byte[] Finish() {
            byte[] message = ToBytes();
            MemoryStream ms = new MemoryStream();
            ms.Position = 0;
            BinaryWriter writer = new BinaryWriter(ms);
            writer.Write(message.Length+4);
            writer.Write(message);
            writer.Flush();
            byte[] result = ms.ToArray();
            m_finishLength = result.Length;
            return result;
        }
    }
}