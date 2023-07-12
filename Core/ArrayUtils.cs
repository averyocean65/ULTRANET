using System.Runtime.Serialization.Formatters.Binary;
using Google.Protobuf;

namespace ULTRANET.Core
{
    public class ArrayUtils
    {
        public static byte[] ToByteArray<T>(T[] data)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using System.IO.MemoryStream stream = new System.IO.MemoryStream();
            formatter.Serialize(stream, data);
            return stream.ToArray();
        }

        public static T[] FromByteArray<T>(byte[] data)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using System.IO.MemoryStream stream = new System.IO.MemoryStream(data);
            return (T[])formatter.Deserialize(stream);
        }

        public static ByteString ToByteString<T>(T[] data)
        {
            return ByteString.CopyFrom(ToByteArray(data));
        }

        public static T[] FromByteString<T>(ByteString data)
        {
            return FromByteArray<T>(data.ToByteArray());
        }
    }
}