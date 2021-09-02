using UnityEngine;
using System.Collections;
using System.IO;
using System;
using System.Text;
//数据转换
public class MyMemoryStream :MemoryStream
{
    public MyMemoryStream(byte[] buffer):base (buffer )
    {

    }
    public MyMemoryStream()
    {

    }
    #region Short
    /// <summary>
    /// 从流中读取一个short数据
    /// </summary>
    /// <returns></returns>
    public short  ReadShort()
    {
        byte[] arr = new byte[2];
        base.Read(arr, 0, 2);
        return BitConverter.ToInt16(arr, 0);
    }
    /// <summary>
    /// 把一个short数据写入流
    /// </summary>
    /// <param name="vaule"></param>
    public void WriteShort(short vaule)
    {
        byte[] arr = BitConverter.GetBytes (vaule);
        base.Write(arr, 0, arr.Length);
    }
    #endregion
    #region UShort
    /// <summary>
    /// 从流中读取一个short数据
    /// </summary>
    /// <returns></returns>
    public ushort ReadUShort()
    {
        byte[] arr = new byte[2];
        base.Read(arr, 0, 2);
        return BitConverter.ToUInt16(arr, 0);
    }
    /// <summary>
    /// 把一个short数据写入流
    /// </summary>
    /// <param name="vaule"></param>
    public void WriteUShort(ushort  vaule)
    {
        byte[] arr = BitConverter.GetBytes(vaule);
        base.Write(arr, 0, arr.Length);
    }
    #endregion
    #region Int
    /// <summary>
    /// 从流中读取一个short数据
    /// </summary>
    /// <returns></returns>
    public int  ReadInt()
    {
        byte[] arr = new byte[4];
        base.Read(arr, 0, 4);
        return BitConverter.ToInt32 (arr, 0);
    }
    /// <summary>
    /// 把一个short数据写入流
    /// </summary>
    /// <param name="vaule"></param>
    public void WriteInt(int  vaule)
    {
        byte[] arr = BitConverter.GetBytes(vaule);
        base.Write(arr, 0, arr.Length);
    }
    #endregion
    #region UInt
    /// <summary>
    /// 从流中读取一个short数据
    /// </summary>
    /// <returns></returns>
    public uint  ReadUInt()
    {
        byte[] arr = new byte[4];
        base.Read(arr, 0, 4);
        return BitConverter.ToUInt32(arr, 0);
    }
    /// <summary>
    /// 把一个short数据写入流
    /// </summary>
    /// <param name="vaule"></param>
    public void WriteUInt(uint  vaule)
    {
        byte[] arr = BitConverter.GetBytes(vaule);
        base.Write(arr, 0, arr.Length);
    }
    #endregion
    #region Long
    /// <summary>
    /// 从流中读取一个short数据
    /// </summary>
    /// <returns></returns>
    public long  ReadLong()
    {
        byte[] arr = new byte[8];
        base.Read(arr, 0, 8);
        return BitConverter.ToInt64(arr, 0);
    }
    /// <summary>
    /// 把一个short数据写入流
    /// </summary>
    /// <param name="vaule"></param>
    public void WriteLong(long  vaule)
    {
        byte[] arr = BitConverter.GetBytes(vaule);
        base.Write(arr, 0, arr.Length);
    }
    #endregion
    #region ULong
    /// <summary>
    /// 从流中读取一个short数据
    /// </summary>
    /// <returns></returns>
    public ulong  ReadULong()
    {
        byte[] arr = new byte[4];
        base.Read(arr, 0, 4);
        return BitConverter.ToUInt64(arr, 0);
    }
    /// <summary>
    /// 把一个short数据写入流
    /// </summary>
    /// <param name="vaule"></param>
    public void WriteULong(ulong vaule)
    {
        byte[] arr = BitConverter.GetBytes(vaule);
        base.Write(arr, 0, arr.Length);
    }
    #endregion
    #region Float
    /// <summary>
    /// 从流中读取一个short数据
    /// </summary>
    /// <returns></returns>
    public float  ReadFloat()
    {
        byte[] arr = new byte[4];
        base.Read(arr, 0, 4);
        return BitConverter.ToSingle (arr, 0);
    }
    /// <summary>
    /// 把一个short数据写入流
    /// </summary>
    /// <param name="vaule"></param>
    public void WriteFloat(float  vaule)
    {
        byte[] arr = BitConverter.GetBytes(vaule);
        base.Write(arr, 0, arr.Length);
    }
    #endregion
    #region Double
    /// <summary>
    /// 从流中读取一个short数据
    /// </summary>
    /// <returns></returns>
    public double  ReadDouble()
    {
        byte[] arr = new byte[8];
        base.Read(arr, 0, 8);
        return BitConverter.ToDouble (arr, 0);
    }
    /// <summary>
    /// 把一个short数据写入流
    /// </summary>
    /// <param name="vaule"></param>
    public void WriteDouble(double  vaule)
    {
        byte[] arr = BitConverter.GetBytes(vaule);
        base.Write(arr, 0, arr.Length);
    }
    #endregion
    #region Bool
    /// <summary>
    /// 从流中读取一个short数据
    /// </summary>
    /// <returns></returns>
    public bool  ReadBool()
    {
        return base.ReadByte() == 1;
    }
    /// <summary>
    /// 把一个short数据写入流
    /// </summary>
    /// <param name="vaule"></param>
    public void WriteBool(bool  vaule)
    {    
        base.WriteByte((byte )(vaule ==true ?1:0));
    }
    #endregion
    #region String
    /// <summary>
    /// 从流中读取一个short数据
    /// </summary>
    /// <returns></returns>
    public string  ReadUTF8String()
    {
        ushort len = this.ReadUShort();
        byte[] arr = new byte[len];
        base.Read(arr, 0, len);
        return Encoding.UTF8.GetString(arr);
    }
    /// <summary>
    /// 把一个short数据写入流
    /// </summary>
    /// <param name="vaule"></param>
    public void WriteString(string  vaule)
    {
        byte[] arr = Encoding.UTF8.GetBytes(vaule);
        if(arr.Length >65535)
        {
            throw new InvalidCastException("字符串超出范围");
        }
        this.WriteUShort((ushort)arr.Length);
        base.Write(arr, 0, arr.Length);
    }
    #endregion
}
