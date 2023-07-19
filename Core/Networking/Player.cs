using System;

namespace ULTRANET.Core.Networking
{
    [Serializable]
    public struct Player
    {
        public string Name { get; set; }
        public string Room { get; set; }
        public int Id { get; set; }
    }
}