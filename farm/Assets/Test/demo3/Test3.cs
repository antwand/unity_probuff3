using CCEngine;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEditor.PackageManager;
using UnityEngine;

class Test3 : MonoBehaviour
{
        private struct ReceiveState
        {
            public Socket clientSocket;
            public byte[] buffer;
        }

        private const int MaxCnt = 5;
        private const int Port = 10086;
        private IPEndPoint m_ServerIpEndPoint;
        private Socket m_ClientSocket;
        private byte[] m_Buffer = new byte[1024];
        private byte[] m_HeartBytes = Encoding.Default.GetBytes("Tcp心跳包");

        public void Open()
        {
            string ipAdress = "192.168.0.60";
            //ipAdress = "127.0.0.1";
            int port = 20100;
            //port = 10086;

            m_ClientSocket = new Socket(SocketType.Stream, ProtocolType.Tcp);
            m_ServerIpEndPoint = new IPEndPoint(IPAddress.Parse(ipAdress), port);
            Console.WriteLine($"客户端{m_ClientSocket.LocalEndPoint}开始连接服务器......");
            m_ClientSocket.BeginConnect(m_ServerIpEndPoint, OnConnect, m_ClientSocket);
        }

        public void Close()
        {
            m_ClientSocket.Close();
        }

        private void OnConnect(IAsyncResult result)
        {
            try
            {
                Socket client = (Socket)result.AsyncState;
                client.EndConnect(result);
                Console.WriteLine("连接服务器成功");
                // 开始接收数据
                ReceiveData(client);
                // 定时发送心跳
                //SendHeartBeat();
            }
            catch (SocketException e)
            {
                Console.WriteLine("连接服务器失败，尝试重新连接中......");
                m_ClientSocket.BeginConnect(m_ServerIpEndPoint, OnConnect, m_ClientSocket);
            }
        }

        private void ReceiveData(Socket client)
        {
            client.BeginReceive(m_Buffer, 0, m_Buffer.Length, SocketFlags.None, EndReceive, new ReceiveState
            {
                clientSocket = client,
                buffer = m_Buffer
            });
        }

        private void EndReceive(IAsyncResult result)
        {
            ReceiveState state = (ReceiveState)result.AsyncState;
            Socket client = state.clientSocket;
            byte[] buffer = state.buffer;
            try
            {
                int length = client.EndReceive(result);
                string msgString = Encoding.Default.GetString(buffer, 0, length);
                Console.WriteLine($"服务器发来信息: {msgString}");

                // 继续接收消息
                ReceiveData(client);
            }
            catch (SocketException e)
            {
                Console.WriteLine($"与服务器丢失连接 Error:{e.Message}");
            }
            catch (FormatException e)
            {
                //Console.WriteLine($"服务器发来的数据无法解析......");
            }
        }

        private void SendHeartBeat()
        {
            // 发送心跳包
            while (true)
            {
                m_ClientSocket?.BeginSend(m_HeartBytes, 0, m_HeartBytes.Length, SocketFlags.None, OnEndSend, m_ClientSocket);
                Thread.Sleep(2000);
            }
        }

        private void OnEndSend(IAsyncResult result)
        {
            Socket client = (Socket)result.AsyncState;
            int cnt = client.EndSend(result);
            Console.WriteLine("发送心跳成功");
    }











    /////////////////////////////
    /// 
    public void bt_connect_Click()
    {
        this.Open();
    }
    public void bt_send_Click()
    {
        byte[] message = Encoding.Default.GetBytes("123456789");

        Debug.Log("发送数据");
        Int32 msglen = (Int32)message.Length;
        Debug.Log(msglen);
        byte[] msglen_bytes = BitConverter.GetBytes(msglen);
        Debug.Log(msglen_bytes);
        Debug.Log(msglen_bytes.Length);


        //byte[] data = new byte[4] ;
        byte[] data = BitConverter.GetBytes((Int32)4);
        Debug.Log(data);
        Debug.Log(msglen_bytes);
        data = ByteConvertHelper.Int32ToBytes(14);
        m_ClientSocket?.BeginSend(data, 0, data.Length, SocketFlags.None, OnEndSend, m_ClientSocket);
    }





 }

