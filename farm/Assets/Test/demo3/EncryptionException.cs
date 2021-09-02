using System;
using System.Runtime.Serialization;

[Serializable]
internal class EncryptionException : Exception
{
    private object defaultCode;
    private string v;

    public EncryptionException()
    {
    }

    public EncryptionException(string message) : base(message)
    {
    }

    public EncryptionException(object defaultCode, string v)
    {
        this.defaultCode = defaultCode;
        this.v = v;
    }

    public EncryptionException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected EncryptionException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}