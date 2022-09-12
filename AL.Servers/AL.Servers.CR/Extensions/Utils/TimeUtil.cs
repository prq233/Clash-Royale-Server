﻿namespace GL.Servers.CR.Extensions.Utils
{
    using System;

    public static class TimeUtil
    {
        internal static readonly DateTime Unix = new DateTime(1970, 1, 1);

        /// <summary>
        /// Returns the timestamp in secs.
        /// </summary>
        internal static int Timestamp
        {
            get
            {
                return (int) DateTime.UtcNow.Subtract(Unix).TotalSeconds;
            }
        }

        /// <summary>
        /// Returns the timestamp in ms.
        /// </summary>
        internal static long TimestampMS
        {
            get
            {
                return (long) DateTime.UtcNow.Subtract(Unix).TotalMilliseconds;
            }
        }

        /// <summary>
        /// Returns the number of minutes since 1970.
        /// </summary>
        internal static int MinutesSince1970
        {
            get
            {
                return (int) DateTime.UtcNow.Subtract(Unix).TotalMinutes;
            }
        }
    }
}