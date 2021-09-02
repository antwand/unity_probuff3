using System;

/// <summary>
/// 底层通信消息
/// </summary>
public class TSocketMessage : IDisposable
{
    /// <summary>
    /// 消息ID
    /// </summary>
    public int MsgID;
    /// <summary>
    /// 消息内容
    /// </summary>
    public byte[] MsgBuffer;

    public TSocketMessage(int msgID, byte[] msg)
    {
        this.MsgID = msgID;
        this.MsgBuffer = msg;
    }

    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool flag1)
    {
        if (flag1) { this.MsgBuffer = null; }
    }
}