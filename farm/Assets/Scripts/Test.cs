using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Google.Protobuf;
using UGCF.Network;
//using ProtocolBuffers;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ProTest pt = new ProTest();
        pt.Id = 1;
        pt.Nick = "zhangsan";
        byte[] bytedata = pt.ToByteArray();  //将ProTest对象转换成字节数组

        //将字节数组反序列化转成对象
        ProTest pt_1 = ProTest.Parser.ParseFrom(bytedata);
        Debug.Log(pt_1.Id);
        Debug.Log(pt_1.Nick);




        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //自定义 bytestring 类型  
        C0001_HeartbeatReqMessage data = new C0001_HeartbeatReqMessage();
        data.SendTime = 15f;

        //添加到head 
        Header head = new Header();
        head.Id = 1;
        head.Data = data.ToByteString();
        head.Seq = 1;
        head.MsgType = 1;
        byte[] bytedata2 = head.ToByteArray();

        //解压缩  
        Header pt_2 = Header.Parser.ParseFrom(bytedata2);
        ByteString bytestrdata = pt_2.Data;
        C0001_HeartbeatReqMessage pt_3 = C0001_HeartbeatReqMessage.Parser.ParseFrom(bytestrdata.ToByteArray());

        //打印解压缩的
        Debug.Log(pt_3.SendTime);
    }


    public void onConet()
    {
        bool isconet = SocketClient.Instance.Init("192.168.0.60", 20100);
    }


    public void onSend()
    {
        C0001_HeartbeatReqMessage h = new C0001_HeartbeatReqMessage();
        h.SendTime = 15f;



        //byte[] data2 = h.ToByteArray();
        Header head = new Header();
        head.Id = 1;

        //Google.ProtocolBuffers.ByteString.CopyFrom(h);
        head.Data = h.ToByteString();

        head.Seq = 1;
        head.MsgType = 1;
        SocketClient.Instance.Send(head);
    }
}
