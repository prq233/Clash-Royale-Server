namespace GL.Servers.DataStream
{
    using System;
    using GL.Servers.Extensions;

    public class ChecksumEncoder
    {
        public int Checksum;
        public int BefChecksum;

        public bool Enabled;

        public ByteStream ByteStream;

        /// <summary>
        /// Gets if this instance is checksum only mode.
        /// </summary>
        public virtual bool IsCheckSumOnlyMode
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChecksumEncoder"/> class.
        /// </summary>
        /// <param name="ByteStream">The byte stream.</param>
        public ChecksumEncoder(ByteStream ByteStream)
        {
            this.Enabled = true;
            this.ByteStream = ByteStream;
        }

        /// <summary>
        /// Writes a byte value.
        /// </summary>
        public virtual void AddByte(byte Value)
        {
            this.Checksum = this.RotateRight(this.Checksum, 31) + Value + 11;
            this.ByteStream?.AddByte(Value);
        }

        /// <summary>
        /// Writes a boolean value.
        /// </summary>
        public virtual void AddBoolean(bool Value)
        {
            this.Checksum = this.RotateRight(this.Checksum, 31) + (Value ? 13 : 7);
            this.ByteStream?.AddBoolean(Value);
        }

        /// <summary>
        /// Writes a single boolean value.
        /// </summary>
        public virtual void AddBool(bool Value)
        {
            this.Checksum = this.RotateRight(this.Checksum, 31) + (Value ? 13 : 7);
            this.ByteStream?.AddBool(Value);
        }

        /// <summary>
        /// Writes a short value.
        /// </summary>
        public virtual void AddShort(short Value)
        {
            this.Checksum = this.RotateRight(this.Checksum, 31) + Value + 19;
            this.ByteStream?.AddShort(Value);
        }

        /// <summary>
        /// Writes a int value.
        /// </summary>
        public virtual void AddInt(int Value)
        {
            this.Checksum = this.RotateRight(this.Checksum, 31) + Value + 9;
            this.ByteStream?.AddInt(Value);
        }

        /// <summary>
        /// Writes a long value.
        /// </summary>
        public virtual void AddLong(long Value)
        {
            this.Checksum = (int)((Value >> 32) + this.RotateRight((int)(Value >> 32) + this.RotateRight((int)Value, 31) + 67, 31) + 91);
            this.ByteStream?.AddLong(Value);
        }

        /// <summary>
        /// Writes a byte array.
        /// </summary>
        public virtual void AddBytes(byte[] Buffer)
        {
            int Ror = this.RotateRight(this.Checksum, 31);

            if (Buffer != null)
            {
                this.Checksum = Ror + Buffer.Length + 28;
            }
            else
                this.Checksum = Ror + 27;

            this.ByteStream?.AddBytes(Buffer);
        }

        /// <summary>
        /// Writes a string.
        /// </summary>
        public virtual void AddString(string String)
        {
            int Ror = this.RotateRight(this.Checksum, 31);

            if (String != null)
            {
                this.Checksum = Ror + String.Length + 28;
            }
            else
                this.Checksum = Ror + 27;

            this.ByteStream?.AddString(String);
        }

        /// <summary>
        /// Writes a string reference.
        /// </summary>
        public virtual void AddStringReference(string String)
        {
            if (String == null)
            {
                throw new ArgumentNullException("String");
            }

            this.Checksum = this.RotateRight(this.Checksum, 31) + String.Length + 9;
            this.ByteStream?.AddStringReference(String);
        }

        /// <summary>
        /// Writes a vint.
        /// </summary>
        public virtual void AddVInt(int Value)
        {
            this.Checksum = this.RotateRight(this.Checksum, 31) + Value + 33;
            this.ByteStream?.AddVInt(Value);
        }

        /// <summary>
        /// Adds range to byte stream.
        /// </summary>
        public virtual void AddRange(byte[] Packet)
        {
            this.ByteStream?.AddRange(Packet);
        }

        /// <summary>
        /// Sets if encoder is enabled.
        /// </summary>
        public void EnableCheckSum(bool Value)
        {
            if (!this.Enabled || Value)
            {
                if (!this.Enabled && Value)
                {
                    this.Checksum = this.BefChecksum;
                }
            }
            else
                this.BefChecksum = this.Checksum;

            this.Enabled = Value;
        }

        /// <summary>
        /// Resets the checksum of this instance.
        /// </summary>
        public void ResetChecksum()
        {
            this.Checksum = 0;
        }

        /// <summary>
        /// Sets the bytestream instance.
        /// </summary>
        public void SetByteStream(ByteStream ByteStream)
        {
            this.ByteStream = ByteStream;
        }

        /// <summary>
        /// Rotates the integer.
        /// </summary>
        /// <param name="Value">The integer, aka checksum.</param>
        /// <param name="Count">The rotation count.</param>
        private int RotateRight(int Value, int Count)
        {
            return Value << Count | Value >> (32 - Count);
        }

        ~ChecksumEncoder()
        {
            this.ByteStream = null;
        }
    }
}