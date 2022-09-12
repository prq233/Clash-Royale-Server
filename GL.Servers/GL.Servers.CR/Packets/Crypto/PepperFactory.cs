﻿namespace GL.Servers.CR.Packets.Crypto
{
    using System.Collections.Generic;

    internal static class PepperFactory
    {
        internal static readonly Dictionary<int, byte[]> ServerSecretKeys = new Dictionary<int, byte[]>
        {
            {
                14,
                new byte[]
                {
                    0x24, 0x95, 0xA0, 0x86, 0xF1, 0x08, 0x92, 0xD5,
                    0x81, 0x58, 0x60, 0xEB, 0x2F, 0x66, 0x91, 0xF1,
                    0x77, 0x18, 0x95, 0x1E, 0x18, 0x12, 0xBC, 0x94,
                    0x25, 0xF5, 0x0A, 0x4B, 0x59, 0x14, 0xBA, 0xD9
                }
            }
        };
    }
}