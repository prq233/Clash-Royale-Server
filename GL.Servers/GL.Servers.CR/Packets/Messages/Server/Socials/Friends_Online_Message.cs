﻿namespace GL.Servers.CR.Packets.Messages.Server.Socials
{
    using GL.Servers.CR.Logic;
    using GL.Servers.CR.Logic.Enums;
    using GL.Servers.Extensions.List;

    internal class Friends_Online_Message : Message
    {
        /// <summary>
        /// Gets the type of this message.
        /// </summary>
        internal override short Type
        {
            get
            {
                return 20108;
            }
        }

        /// <summary>
        /// Gets the service node of this message.
        /// </summary>
        internal override Node ServiceNode
        {
            get
            {
                return Node.Account;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Friends_Online_Message"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Alliance">The alliance.</param>
        public Friends_Online_Message(Device Device) : base(Device)
        {
            // Friends_Online_Message.
        }

        /// <summary>
        /// Encodes the <see cref="Message" />, using the <see cref="Writer" /> instance.
        /// </summary>
        internal override void Encode()
        {
            this.Data.AddVInt(1);
        }
    }
}