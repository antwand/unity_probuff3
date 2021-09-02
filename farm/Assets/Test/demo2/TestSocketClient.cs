using UnityEngine;
using System.Net.Sockets;
using System.Net;
using System;
using System.Threading;

//https://blog.csdn.net/baidu_39447417/article/details/79840922
namespace LuaFramework.NetWork
{
    public class TestSocketClient
    {
        #region 单利
        private static TestSocketClient instance = null;
        public static TestSocketClient Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TestSocketClient();
                }
                return instance;
            }
        } 
        #endregion

        public Socket client;
        private Thread thread;
        private MessageHandle messageHandle;
        private string ipAdress;
        private int port;
        public bool isConnect = false;
        private byte[] buffer;//接收数据的缓存区
        /// <summary>
        /// 连接服务器外部接口
        /// </summary>
        /// <param name="ipAdress"></param>
        /// <param name="port"></param>
        public void Init(string ipAdress, int port)
        {
            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            messageHandle = new MessageHandle();
            this.ipAdress = ipAdress;
            this.port = port;
            thread = new Thread(ConnectServer);
            thread.Start();
        }

        #region 连接服务器内部
        private void ConnectServer()
        {
            client = null;
            try
            {
                IPAddress[] address = Dns.GetHostAddresses(ipAdress);
                if (address.Length == 0)
                {
                    Debug.LogError("host invalid");
                    return;
                }
                if (address[0].AddressFamily == AddressFamily.InterNetworkV6)
                {
                    client = new Socket(AddressFamily.InterNetworkV6, SocketType.Stream, ProtocolType.Tcp);
                }
                else
                {
                    client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                }
                client.SendTimeout = 1000;//设置发送超时时间，超时断开连接
                client.ReceiveTimeout = 1000;//设置接收超时时间
                client.NoDelay = true;
                client.BeginConnect(address, port, ConnectCallBack, client);//开始异步连接服务器
            }
            catch (Exception e)
            {
                Debug.Log("连接失败，断开连接");
                Close();
            }
        }
        private void ConnectCallBack(IAsyncResult ar)
        {
            try
            {
                Debug.Log("连接服务器成功！！");
                client.EndConnect(ar);//连接服务器成功
                isConnect = true;
                Receive();//开始读取数据
            }
            catch (Exception e)
            {
                Close();
            }
        } 
        #endregion

        #region 接收数据
        private void Receive()
        {
            try
            {
                buffer = new byte[10240];
                client.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, ReceiveCallBack, client);
            }
            catch (Exception e)
            {
                Close();
            }
        }
        private void ReceiveCallBack(IAsyncResult ar)
        {
            try
            {
                int count = client.EndReceive(ar);
                if (count == 0)
                {
                    Close();
                    return;
                }
                messageHandle.HandleMessage(buffer, count);//处理数据
                Receive();//再次接收数据
            }
            catch (Exception e)
            {
                Close();
            }
        }
        #endregion

        #region 发送数据
        public void Send(byte[] data)
        {
            if (client != null && client.Connected)
            {
                Debug.Log("向服务器发送的字节数BeginSend" + data.Length);
                client.BeginSend(data, 0, data.Length, SocketFlags.None, SendCallBack, client);
            }
            else
            {
                Close();
            }
        }
        private void SendCallBack(IAsyncResult ar)
        {
            try
            {
                int count = client.EndSend(ar);
                Debug.Log("向服务器发送的字节数EndSend" + count);
            }
            catch (Exception e)
            {
                Close();
            }
        } 
        #endregion

        /// <summary>
        /// 关闭连接
        /// </summary>
        public void Close()
        {
            //一旦断开连接 走重新登录
            if (thread != null)
            {
                thread.Abort();
                thread = null;
            }
            if (client != null)
            {
                client.Close();
                isConnect = false;
                client = null;
            }
        }

        /// <summary>
        /// 判断是否处于连接
        /// </summary>
        public void IsConnected()
        {
            if (client.Connected == false)
            {
                Close();
            } 
        }

    }
}
