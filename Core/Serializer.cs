using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace ULTRANET.Core
{
    public static class Serializer
    {
        private static readonly BinaryFormatter Formatter = new BinaryFormatter();
        
        public static byte[] Serialize<T>(T input)
        {
            using var memoryStream = new MemoryStream();
            
            if (input != null)
                Formatter.Serialize(memoryStream, input);
            return memoryStream.ToArray();
        }
        
        public static T Deserialize<T>(string input)
            => Deserialize<T>(Encoding.UTF8.GetBytes(input));
        
        public static T Deserialize<T>(byte[] input)
        {
            using var memoryStream = new MemoryStream(input);
            return (T)Formatter.Deserialize(memoryStream);
        }
    }
}