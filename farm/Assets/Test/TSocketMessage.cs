using System;

/// <summary>
/// �ײ�ͨ����Ϣ
/// </summary>
public class TSocketMessage : IDisposable
{
    /// <summary>
    /// ��ϢID
    /// </summary>
    public int MsgID;
    /// <summary>
    /// ��Ϣ����
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