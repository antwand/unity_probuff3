syntax = "proto3";





message Msg_S2C {
    ProtoId protoId = 1; //协议号
    SystemError_S2C systemError_S2C = 2;
	string token =3;
	
	
}



message Msg_C2S {
   ProtoId protoId = 1; //协议号
   
   bytes data = 2;
   int32 seq = 3; // 序列，默认0
	int32 msgType = 4; // 0=push, 1=req, 2=resp  
	
	string token =5;
}


//协议号的枚举   
enum ProtoId {
	NONE =0;
    SYSTEM_HEART = 1001; //心跳
    SYSTEM_ERROR_S2C = 1002; //系统错误
}

message SystemHeart {
}

message SystemError_S2C {
    string code = 1;
}