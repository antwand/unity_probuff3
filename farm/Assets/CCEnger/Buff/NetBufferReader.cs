using System;
using System.IO;
using System.Text;
 
namespace CCEngine {


    /**

    /// <summary>  
        /// 接收服务器端Socket的消息  
        /// </summary>  
        void RecieveMessage() {
            while(isConnected) {
                try {
                    int receiveLength = m_clientSocket.Receive(m_result);
                    NetBufferReader reader = new NetBufferReader(m_result);
                    string data = reader.ReadString();
                    Debug.Log("服务器返回数据：" + data);
                    if(onGetReceive != null) {
                        onGetReceive(data);
                    }
 
                } catch(Exception ex) {
                    Console.WriteLine(ex.Message);
                    Disconnect();
                    break;
                }
            }
        }

        ————————————————
        版权声明：本文为CSDN博主「王王王渣渣」的原创文章，遵循CC 4.0 BY-SA版权协议，转载请附上原文出处链接及本声明。
        原文链接：https://blog.csdn.net/wangjiangrong/article/details/80844392


     * 
     * **/
    class NetBufferReader {
 
        MemoryStream m_stream = null;
        BinaryReader m_reader = null;
 
        ushort m_dataLength;
 
        public NetBufferReader(byte[] data) {
            if(data != null) {
                m_stream = new MemoryStream(data);
                m_reader = new BinaryReader(m_stream);
 
                m_dataLength = ReadUShort();
            }
        }
 
        public byte ReadByte() {
            return m_reader.ReadByte();
        }
 
        public int ReadInt() {
            return m_reader.ReadInt32();
        }
 
        public uint ReadUInt() {
            return m_reader.ReadUInt32();
        }
 
        public short ReadShort() {
            return m_reader.ReadInt16();
        }
 
        public ushort ReadUShort() {
            return m_reader.ReadUInt16();
        }
 
        public long ReadLong() {
            return m_reader.ReadInt64();
        }
 
        public ulong ReadULong() {
            return m_reader.ReadUInt64();
        }
 
        public float ReadFloat() {
            byte[] temp = BitConverter.GetBytes(m_reader.ReadSingle());
            Array.Reverse(temp);
            return BitConverter.ToSingle(temp, 0);
        }
 
        public double ReadDouble() {
            byte[] temp = BitConverter.GetBytes(m_reader.ReadDouble());
            Array.Reverse(temp);
            return BitConverter.ToDouble(temp, 0);
        }
 
        public string ReadString() {
            ushort len = ReadUShort();
            byte[] buffer = new byte[len];
            buffer = m_reader.ReadBytes(len);
            return Encoding.UTF8.GetString(buffer);
        }
 
        public byte[] ReadBytes() {
            int len = ReadInt();
            return m_reader.ReadBytes(len);
        }
 
        public void Close() {
            if(m_reader != null) {
                m_reader.Close();
            }
            if(m_stream != null) {
                m_stream.Close();
            }
            m_reader = null;
            m_stream = null;
        }
    }
}