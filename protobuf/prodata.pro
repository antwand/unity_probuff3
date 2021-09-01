syntax = "proto3";

message ProTest
{
	int32 id  = 1;
	string nick = 2;
}


message Header {
  int32 id = 1;
  bytes data = 2;
  int32 seq = 3; // 序列，默认0
  int32 msgType = 4; // 0=push, 1=req, 2=resp  
}

// 心跳消息
message C0001_HeartbeatReqMessage {
  float sendTime = 1;
}

message C0001_HeartbeatRespMessage {
  float sendTime = 1;
}

// 时钟同步
message C0002_ClockReqMessage{
  int64 reqClock = 1;
}

message C0002_ClockRespMessage{
  int64 reqClock = 1;// 请求中的reqClock,服务器主动同步时钟时此值为0
  int64 serverClock = 2;
}

//提示消息
message C0003_TipNotifyMessage {
  string content = 1;
}
