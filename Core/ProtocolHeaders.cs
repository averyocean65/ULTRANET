namespace ULTRANET.Core
{
    /// <summary>
    /// Protocol Headers for the ULTRANET network.
    /// </summary>
    public static class ProtocolHeaders
    {
        public const string CONNECT = "CONNECT";

        public const string PING = "PING";

        public const string GET_PLAYER = "FETCHPLAYER";
        public const string CHANGE_ROOM = "ROOMCHANGE";

        public const string SET_LOCAL_PLAYER = "LOCALPLAYER";
        public const string PLAYER_TRANSFORM_UPDATE = "PTRANSUP";

        public const string MESSAGE = "MESSAGE";
        public const string ERROR = "ERROR";
    }
}