namespace ULTRANET.Core
{
    public static class NetworkKeys
    {
        /// <summary>
        /// Keys only sent by the client
        /// </summary>
        public static class Client
        {
            public const string CONNECT = "CONNECT";
            public const string DISCONNECT = "DISCONCT";
        }

        public const string PLAYER_ID = "PID";
        public const string PING = "PING";
        
        public const string ROOM_CHANGE = "ROOMCHG";
        
        public const string ERROR = "ERROR";
    }
}