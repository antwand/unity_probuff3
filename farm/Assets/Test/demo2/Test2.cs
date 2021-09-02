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

using LuaFramework.NetWork;
using System.Runtime.InteropServices;
using System.IO;

public class Test2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }


    public void bt_connect_Click()
    {
        Debug.Log("连接");
        string ipAdress = "192.168.0.60";
        int port = 20100;
        TestSocketClient.Instance.Init(ipAdress,port);

    }

    public byte[] IntToBitConverter(int num)
    {
        byte[] bytes = BitConverter.GetBytes(num);
        return bytes;
    }
    public int IntToBitConverter(byte[] bytes)
    {
        int temp = BitConverter.ToInt32(bytes, 0);
        return temp;
    }
    //发送数据  
    public void bt_send_Click()
    {
        //byte[] data = new byte[4];
        //data = this.IntToBitConverter(4);
       // TestSocketClient.Instance.Send(data);
        //Debug.Log(data.Length);


        string message = "abc";
        MemoryStream ms = null;
        using (ms = new MemoryStream())
        {
            ms.Position = 0;
            BinaryWriter writer = new BinaryWriter(ms);

            Int32 msglen = (Int32)message.Length;
            byte[] msglen_bytes = BitConverter.GetBytes(msglen);
            Debug.Log(msglen_bytes.Length);

            writer.Write(msglen_bytes);
            writer.Write(message);
            writer.Flush();

            TestSocketClient.Instance.Send(ms.ToArray());
            //return ms.ToArray();
        }




        /**
        //方法一：
        byte a = 3; //定义变量
        int b = Marshal.SizeOf(a.GetType()); //获取长度
        Debug.Log(b);
        //方法二：
        byte[] myBytes = new byte[5] { 1, 2, 3, 4, 5 };
        BitArray myBA = new BitArray(myBytes);
        int c = myBA.Length;
        **/

        /**
        Console.WriteLine(Encoding.Default.GetByteCount("张三"));//输出：6
                                                               //常用 一个字母，数字 一个字节
        Console.WriteLine(Encoding.Default.GetByteCount("ab")); //输出：2
        **/
    }

}
