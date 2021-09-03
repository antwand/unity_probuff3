using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Google.Protobuf;
using UGCF.Network;
using System;
//using ProtocolBuffers;
using CCEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
   

    //     ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //     //////////////////////////////////////////////////////////////////////////////////////////////////////////////

    //     //自定义 bytestring 类型  
    //     /**
    //    2021/09/02 16:49:13 Encode message: *messages.C0001_HeartbeatReqMessage sendTime:1.6305725e+09
    //     2021/09/02 16:49:13 Send len: [0 0 0 15]
    //     2021/09/02 16:49:13 Send head: [8 2 18 5 13 32 97 194 78 32 1]
    //     2021/09/02 16:49:13 Send all data: [0 0 0 15 8 2 18 5 13 32 97 194 78 32 1]
    //     2021/09/02 16:49:13 Receive: %!s(<nil>)
    //     heartbeat: 1630572544.000000
    //      * 
    //      * */
    //     C0001_HeartbeatReqMessage data = new C0001_HeartbeatReqMessage();
    //     data.SendTime = 1630571100;



    //     //添加到head 
    //     Header head = new Header();
    //     head.Id = 1;
    //     head.Data = data.ToByteString();
    //     head.Seq = 0;
    //     head.MsgType = 1;
    //     byte[] bytedata2 = head.ToByteArray();
    //     Debug.Log("测试==============");
    //     ByteConvertHelper.Console(bytedata2);
    //     //解压缩  
    //     Header pt_2 = Header.Parser.ParseFrom(bytedata2);
    //     //ByteString bytestrdata = pt_2.Data;
    //     //C0001_HeartbeatReqMessage pt_3 = C0001_HeartbeatReqMessage.Parser.ParseFrom(bytestrdata.ToByteArray());

    //     //打印解压缩的
    //     //Debug.Log(pt_3.SendTime);



    //     Debug.Log("反解压======================");
        
    //     byte[] head3 = new byte[] { 8 ,2, 18, 5, 13, 21, 97, 194, 78, 32, 1 };
    //     //byte[] msg = new byte[head2.Length]; 
    //     Header pt_3 = Header.Parser.ParseFrom(head3);
    //     Debug.Log(pt_3.Seq+"==="+pt_3.MsgType +"===" + pt_3.Id);
    //     Debug.Log(pt_3);
    //     ByteString bytestrdata = pt_3.Data;
    //     C0001_HeartbeatReqMessage pt_33 = C0001_HeartbeatReqMessage.Parser.ParseFrom(bytestrdata);
    //     Debug.Log(Convert.ToString(pt_33.SendTime));



    //     ByteConvertHelper.Console(head3);
    //     ByteConvertHelper.Console(head3);

    }


    public void onConet()
    {
        bool isconet = SocketClient.Instance.Init("192.168.0.60", 20100);
    }


    public void onSend()
    {


        // C0001_HeartbeatReqMessage data = new C0001_HeartbeatReqMessage();
        // data.SendTime = 1630572544;

        // Header head = new Header();
        // head.Id = 1;

        // head.Data = data.ToByteString();

        // head.Seq = 0;
        // head.MsgType = 1;

        // SocketClient.Instance.Send(head);
    }
}
