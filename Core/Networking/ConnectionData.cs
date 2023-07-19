using System;

namespace ULTRANET.Core.Networking
{
    [Serializable]
    public struct ConnectionData
    {
        public Player Player { get; set; }
    }

    [Serializable]
    public struct DisconnectionData
    {
        public Player Player { get; set; }
        public string Reason { get; set; }
    }
}