using System;

namespace ULTRANET.Core
{
    [Flags]
    public enum PacketFlag
    {
        None = 0,
        Player = 1 << 0,
        Enemy = 1 << 1,
        Room = 1 << 2,
        Weapon = 1 << 3,
        Projectile = 1 << 4,
    }

    public class PacketFlagConverter
    {
        public static uint ToUInt32(PacketFlag flags)
        {
            return (uint)flags;
        }

        public static PacketFlag FromUInt32(uint value)
        {
            return (PacketFlag)value;
        }
    }
}