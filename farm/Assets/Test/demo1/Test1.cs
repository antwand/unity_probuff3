using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class Test1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }


  Socket socketSend;
    public void bt_connect_Click()
    {
        try
        {
            string ipAdress = "192.168.0.60";
            int port = 20100;

            //创建客户端Socket，获得远程ip和端口号
            socketSend = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress _ip = IPAddress.Parse(ipAdress);
            IPEndPoint _point = new IPEndPoint(_ip, port);

            socketSend.Connect(_point);
            Debug.Log("连接成功!");
            //开启新的线程，不停的接收服务器发来的消息
            Thread c_thread = new Thread(Received);
            c_thread.IsBackground = true;
            c_thread.Start();
        }
        catch (Exception)
        {
            Debug.Log("IP或者端口号错误...");
        }

    }

    /// <summary>
    /// 接收服务端返回的消息
    /// </summary>
    void Received()
    {
        while (true)
        {
            try
            {
                byte[] buffer = new byte[1024 * 1024 * 3];
                //实际接收到的有效字节数
                int len = socketSend.Receive(buffer);
                if (len == 0)
                {
                    break;
                }
                string str = Encoding.UTF8.GetString(buffer, 0, len);
                Debug.Log("客户端打印：" + socketSend.RemoteEndPoint + ":" + str);
            }
            catch { }
        }
    }


    //设置包  
    private byte[] makedata(byte[] data){
        byte[] buff = null;
        //向流中写入我们自己的数据类信息
        using (MyMemoryStream ms = new MyMemoryStream())
        {
            ms.WriteInt(data.Length); //包头  包的长度 
            ms.Write(data,0,data.Length); // 包体  
            buff = ms.ToArray();
        }

        return buff;
    }



    //2设置包  
    private byte[] makedata2(byte[] data){
        byte[] buff = null;
        
        //包头 
        //byte[] data2 = new byte[4];
        byte[]  data2   =   System.BitConverter.GetBytes(data.Length); 
        //data2 = Encoding..GetBytes(data.Length);

        //包体 


        return buff;
    }


    //发送数据  
    public void bt_send_Click()
    {
        try
        {
            //先发送一个字符串 
            string msg = "你好呀";
            byte[] data = new byte[1024 * 1024 * 3];
            data = Encoding.UTF8.GetBytes(msg);

            //错误：：：服务器收到的包头 长度是一个很长的数字 ？？？
            byte[] buff = this.makedata(data);
            //socketSend.Send(buff);

            Debug.Log("发送数据");



            //构造
            int msgID = 1;
            TSocketMessage ts = new TSocketMessage(msgID, data);
            MarshalEndian marshalEndian = new MarshalEndian();
            byte[] marshalEndian_buff = marshalEndian.Encode(ts);
            socketSend.Send(marshalEndian_buff);

        }
        catch { }
    }

}
