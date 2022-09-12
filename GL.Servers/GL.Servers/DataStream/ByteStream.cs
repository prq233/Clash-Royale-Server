﻿namespace GL.Servers.DataStream
{
    using System;
    using System.Text;

    public class ByteStream : ChecksumEncoder
    {
        protected int _Offset;
        protected byte[] Buffer;
        
        protected int BooleanOffset;
        protected int BooleanAdditionalValue;

        /// <summary>
        /// Gets the offset of stream.
        /// </summary>
        public int Offset
        {
            get
            {
                return this._Offset;
            }
        }

        /// <summary>
        /// Gets the offset of stream.
        /// </summary>
        public int Length
        {
            get
            {
                return this.Buffer.Length;
            }
        }

        /// <summary>
        /// Gets if this instance is checksum only mode.
        /// </summary>
        public override bool IsCheckSumOnlyMode
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ByteStream"/> class.
        /// </summary>
        public ByteStream() : base(null)
        {
            this.Buffer = new byte[32];
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ByteStream"/> class.
        /// </summary>
        public ByteStream(int Size) : base(null)
        {
            this.Buffer = new byte[Size];
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ByteStream"/> class.
        /// </summary>
        public ByteStream(byte[] Buffer) : base(null)
        {
            this.Buffer = Buffer;
        }

        /// <summary>
        /// Adds a byte value.
        /// </summary>
        public override void AddByte(byte Value)
        {
            this.EnsureCapacity(1);
            this.Add(Value);
        }

        /// <summary>
        /// Adds a boolean value.
        /// </summary>
        public override void AddBoolean(bool Value)
        {
            if (this.BooleanOffset == 0)
            {
                this.EnsureCapacity(1);
                this.Add(0);
            }

            if (Value)
            {
                this.Buffer[this.Offset - 1] |= (byte)(1 << this.BooleanOffset);
            }
            
            this.BooleanOffset = this.BooleanOffset + 1 & 7;
        }

        /// <summary>
        /// Adds a short value.
        /// </summary>
        public override void AddShort(short Value)
        {
            this.EnsureCapacity(2);

            this.Add((byte)(Value >> 8));
            this.Add((byte)(Value));
        }

        /// <summary>
        /// Adds a ushort value.
        /// </summary>
        public  void AddUShort(ushort Value)
        {
            this.EnsureCapacity(2);

            this.Add((byte)(Value >> 8));
            this.Add((byte)(Value));
        }

        /// <summary>
        /// Adds a int value.
        /// </summary>
        public override void AddInt(int Value)
        {
            this.EnsureCapacity(4);

            this.Add((byte)(Value >> 24));
            this.Add((byte)(Value >> 16));
            this.Add((byte)(Value >> 8));
            this.Add((byte)(Value));
        }

        /// <summary>
        /// Adds a uint value.
        /// </summary>
        public  void AddUInt(uint Value)
        {
            this.EnsureCapacity(4);

            this.Add((byte)(Value >> 24));
            this.Add((byte)(Value >> 16));
            this.Add((byte)(Value >> 8));
            this.Add((byte)(Value));
        }

        /// <summary>
        /// Adds a long value.
        /// </summary>
        public override void AddLong(long Value)
        {
            this.EnsureCapacity(8);

            this.Add((byte)(Value >> 56));
            this.Add((byte)(Value >> 48));
            this.Add((byte)(Value >> 40));
            this.Add((byte)(Value >> 32));
            this.Add((byte)(Value >> 24));
            this.Add((byte)(Value >> 16));
            this.Add((byte)(Value >> 8));
            this.Add((byte)(Value));
        }

        /// <summary>
        /// Adds a long value.
        /// </summary>
        public void AddLong(int hi, int lo)
        {
            this.AddInt(hi);
            this.AddInt(lo);
        }

        /// <summary>
        /// Adds a ulong value.
        /// </summary>
        public  void AddULong(ulong Value)
        {
            this.EnsureCapacity(8);

            this.Add((byte)(Value >> 56));
            this.Add((byte)(Value >> 48));
            this.Add((byte)(Value >> 40));
            this.Add((byte)(Value >> 32));
            this.Add((byte)(Value >> 24));
            this.Add((byte)(Value >> 16));
            this.Add((byte)(Value >> 8));
            this.Add((byte)(Value));
        }

        /// <summary>
        /// Adds a byte array.
        /// </summary>
        public override void AddBytes(byte[] Buffer)
        {
            if (Buffer != null)
            {
                int Length = Buffer.Length;

                if (Length > 0)
                {
                    this.EnsureCapacity(Length + 4);

                    this.AddInt(Length);
                    this.AddRange(Buffer, Length);
                }
                else
                    this.AddInt(0);
            }
            else
            {
                this.AddInt(-1);
            }
        }

        /// <summary>
        /// Adds a string.
        /// </summary>
        public override void AddString(string String)
        {
            if (String != null)
            {
                int Length = String.Length;

                if (Length > 0)
                {
                    this.EnsureCapacity(Length + 4);

                    this.AddInt(Length);
                    this.AddRange(Encoding.UTF8.GetBytes(String), Length);
                }
                else
                    this.AddInt(0);
            }
            else
            {
                this.AddInt(-1);
            }
        }

        /// <summary>
        /// Adds a string reference.
        /// </summary>
        public override void AddStringReference(string String)
        {
            if (String == null)
            {
                throw new ArgumentNullException("ByteStream::AddStringReference() - String cannot be NULL.");
            }

            int Length = String.Length;

            if (Length > 0)
            {
                this.EnsureCapacity(Length + 4);

                this.AddInt(Length);
                this.AddRange(Encoding.UTF8.GetBytes(String), Length);
            }
            else
                this.AddInt(0);
        }

        /// <summary>
        /// Adds a vint.
        /// </summary>
        public override void AddVInt(int Value)
        {
            this.EnsureCapacity(5);

            if (Value >= 0)
            {
                if (Value >= 64)
                {
                    if (Value >= 0x2000)
                    {
                        if (Value >= 0x100000)
                        {
                            if (Value >= 0x8000000)
                            {
                                this.Add((byte)(Value & 0x3F | 0x80));
                                this.Add((byte)((Value >> 6) & 0x7F | 0x80));
                                this.Add((byte)((Value >> 13) & 0x7F | 0x80));
                                this.Add((byte)((Value >> 20) & 0x7F | 0x80));
                                this.Add((byte)((Value >> 27) & 0xF));

                                return;
                            }

                            this.Add((byte)(Value & 0x3F | 0x80));
                            this.Add((byte)((Value >> 6) & 0x7F | 0x80));
                            this.Add((byte)((Value >> 13) & 0x7F | 0x80));
                            this.Add((byte)((Value >> 20) & 0x7F));

                            return;
                        }

                        this.Add((byte)(Value & 0x3F | 0x80));
                        this.Add((byte)((Value >> 6) & 0x7F | 0x80));
                        this.Add((byte)((Value >> 13) & 0x7F));

                        return;
                    }

                    this.Add((byte)(Value & 0x3F | 0x80));
                    this.Add((byte)((Value >> 6) & 0x7F));

                    return;
                }

                this.Add((byte)(Value & 0x3F));
            }
            else
            {
                if (Value <= -0x40)
                {
                    if (Value <= -0x2000)
                    {
                        if (Value <= -0x100000)
                        {
                            if (Value <= -0x8000000)
                            {
                                this.Add((byte)(Value & 0x3F | 0xC0));
                                this.Add((byte)((Value >> 6) & 0x7F | 0x80));
                                this.Add((byte)((Value >> 13) & 0x7F | 0x80));
                                this.Add((byte)((Value >> 20) & 0x7F | 0x80));
                                this.Add((byte)((Value >> 27) & 0xF));

                                return;
                            }

                            this.Add((byte)(Value & 0x3F | 0xC0));
                            this.Add((byte)((Value >> 6) & 0x7F | 0x80));
                            this.Add((byte)((Value >> 13) & 0x7F | 0x80));
                            this.Add((byte)((Value >> 20) & 0x7F));

                            return;
                        }

                        this.Add((byte)(Value & 0x3F | 0xC0));
                        this.Add((byte)((Value >> 6) & 0x7F | 0x80));
                        this.Add((byte)((Value >> 13) & 0x7F));

                        return;
                    }

                    this.Add((byte)(Value & 0x3F | 0xC0));
                    this.Add((byte)((Value >> 6) & 0x7F));
                }
                else
                {
                    this.Add((byte)(Value & 0x3F | 0x40));
                }
            }
        }
        
        /// <summary>
        /// Adds element to buffer.
        /// </summary>
        private void Add(byte Value)
        {
            this.Buffer[this._Offset++] = Value;
        }

        /// <summary>
        /// Adds element to buffer.
        /// </summary>
        public void AddRange(byte[] Buffer)
        {
            this.EnsureCapacity(Buffer.Length);

            Array.Copy(Buffer, 0, this.Buffer, this._Offset, Buffer.Length);
            this._Offset += Buffer.Length;
        }

        /// <summary>
        /// Adds element to buffer.
        /// </summary>
        public void AddRange(byte[] Buffer, int Length)
        {
            this.EnsureCapacity(Buffer.Length);

            Array.Copy(Buffer, 0, this.Buffer, this._Offset, Length);
            this._Offset += Length;
        }

        /// <summary>
        /// Ensures the capacity of buffer.
        /// </summary>
        private void EnsureCapacity(int Count)
        {
            if (this.BooleanOffset > 0)
            {
                this.BooleanOffset = 0;
                this.BooleanAdditionalValue = 0;
            }

            if (this.Buffer.Length < this._Offset + Count)
            {
                byte[] NBuffer = new byte[this.Buffer.Length * 2 > this.Buffer.Length + Count ? this.Buffer.Length * 2 : this.Buffer.Length + Count];
                Array.Copy(this.Buffer, 0, NBuffer, 0, this._Offset);
                this.Buffer = NBuffer;
            }
        }

        /// <summary>
        /// Reads a byte value.
        /// </summary>
        public byte ReadByte()
        {
            return this.Read();
        }

        /// <summary>
        /// Reads a boolean value.
        /// </summary>
        public bool ReadBoolean()
        {
            if (this.BooleanOffset == 0)
            {
                ++this._Offset;
                this.BooleanAdditionalValue = 0;
            }

            this.BooleanAdditionalValue += (8 - this.BooleanOffset) >> 3;
            bool Value = ((1 << this.BooleanOffset) & this.Buffer[this._Offset - 1] + this.BooleanAdditionalValue - 1) != 0;
            this.BooleanOffset = this.BooleanOffset + 1 & 7;

            return Value;
        }

        /// <summary>
        /// Reads a short value.
        /// </summary>
        public short ReadShort()
        {
            return (short)(this.Read() << 8 | this.Read());
        }

        /// <summary>
        /// Reads a ushort value.
        /// </summary>
        public ushort ReadUShot()
        {
            return (ushort)(this.Read() << 8 | this.Read());
        }

        /// <summary>
        /// Reads a int value.
        /// </summary>
        public int ReadInt()
        {
            return this.Read() << 24 | this.Read() << 16 | this.Read() << 8 | this.Read();
        }

        /// <summary>
        /// Reads a uint value.
        /// </summary>
        public uint ReadUInt()
        {
            return (uint)(this.Read() << 24 | this.Read() << 16 | this.Read() << 8 | this.Read());
        }

        /// <summary>
        /// Reads a long value.
        /// </summary>
        public long ReadLong()
        {
            return (long)(this.Read() << 56 | this.Read() << 48 | this.Read() << 40 | this.Read() << 32 |
                           this.Read() << 24 | this.Read() << 16 | this.Read() << 8 | this.Read());
        }

        /// <summary>
        /// Reads a ulong value.
        /// </summary>
        public ulong ReadULong()
        {
            return (ulong)(this.Read() << 56 | this.Read() << 48 | this.Read() << 40 | this.Read() << 32 |
                            this.Read() << 24 | this.Read() << 16 | this.Read() << 8 | this.Read());
        }

        /// <summary>
        /// Reads a byte array.
        /// </summary>
        public byte[] ReadBytes()
        {
            int Length = this.ReadInt();

            if (Length < 0)
            {
                if (Length != -1)
                {
                    throw new Exception("ByteStream::readBytes() - Byte array length is invalid. (" + Length + ")");
                }

                return null;
            }

            return this.ReadRange(Length);
        }

        /// <summary>
        /// Reads a byte array reference.
        /// </summary>
        public byte[] ReadBytesReference()
        {
            int Length = this.ReadInt();

            if (Length < 0)
            {
                throw new Exception("ByteStream::readBytesReference() - Byte array length cannot be inferior than 0! (" + Length + ")");
            }

            return this.ReadRange(Length);
        }

        /// <summary>
        /// Reads a string value.
        /// </summary>
        public string ReadString()
        {
            int Length = this.ReadInt();

            if (Length < 0)
            {
                if (Length != -1)
                {
                    throw new Exception("ByteStream::readString() - String length is invalid. (" + Length + ")");
                }

                return null;
            }

            return Encoding.UTF8.GetString(this.ReadRange(Length));
        }

        /// <summary>
        /// Reads a string reference.
        /// </summary>
        public string ReadStringReference()
        {
            int Length = this.ReadInt();

            if (Length < 0)
            {
                throw new Exception("ByteStream::readStringReference() - String length cannot be inferior than 0! (" + Length + ")");
            }

            return Encoding.UTF8.GetString(this.ReadRange(Length));
        }

        /// <summary>
        /// Reads a vint.
        /// </summary>
        public int ReadVInt()
        {
            byte Byte = this.ReadByte();
            int Result;

            if ((Byte & 0x40) != 0)
            {
                Result = Byte & 0x3F;

                if ((Byte & 0x80) != 0)
                {
                    Result |= ((Byte = this.Read()) & 0x7F) << 6;

                    if ((Byte & 0x80) != 0)
                    {
                        Result |= ((Byte = this.Read()) & 0x7F) << 13;

                        if ((Byte & 0x80) != 0)
                        {
                            Result |= ((Byte = this.Read()) & 0x7F) << 20;

                            if ((Byte & 0x80) != 0)
                            {
                                Result |= ((Byte = this.Read()) & 0x7F) << 27;
                                return (int)(Result | 0x80000000);
                            }

                            return (int)(Result | 0xF8000000);
                        }

                        return (int)(Result | 0xFFF00000);
                    }

                    return (int)(Result | 0xFFFFE000);
                }

                return (int)(Result | 0xFFFFFFC0);
            }
            else
            {
                Result = Byte & 0x3F;

                if ((Byte & 0x80) != 0)
                {
                    Result |= ((Byte = this.Read()) & 0x7F) << 6;

                    if ((Byte & 0x80) != 0)
                    {
                        Result |= ((Byte = this.Read()) & 0x7F) << 13;

                        if ((Byte & 0x80) != 0)
                        {
                            Result |= ((Byte = this.Read()) & 0x7F) << 20;

                            if ((Byte & 0x80) != 0)
                            {
                                Result |= ((Byte = this.Read()) & 0x7F) << 27;
                            }
                        }
                    }
                }
            }

            return Result;
        }

        /// <summary>
        /// Reads a element of buffer.
        /// </summary>
        private byte Read()
        {
            if (this.BooleanOffset > 0)
            {
                this.BooleanOffset = 0;
                this.BooleanAdditionalValue = 0;
            }

            return this.Buffer[this._Offset++];
        }

        /// <summary>
        /// Reads a element of buffer.
        /// </summary>
        private byte[] ReadRange(int Count)
        {
            byte[] Array = new byte[Count];
            System.Buffer.BlockCopy(this.Buffer, this.Offset, Array, 0, Count);
            this._Offset += Count;
            return Array;
        }

        /// <summary>
        /// Sets the byte array of this instance.
        /// </summary>
        public void SetByteArray(byte[] NBuffer)
        {
            this.Buffer = null;
            this.Buffer = NBuffer;

            this._Offset = NBuffer.Length;
            this.BooleanOffset = 0;
            this.BooleanAdditionalValue = 0;
        }

        /// <summary>
        /// Sets the offset of stream.
        /// </summary>
        public void SetOffset(int Offset)
        {
            if (this.BooleanOffset > 0)
            {
                this.BooleanOffset = 0;
                this.BooleanAdditionalValue = 0;
            }

            this._Offset = Offset;
        }

        /// <summary>
        /// Converted this instance to byte array.
        /// </summary>
        public byte[] ToArray()
        {
            byte[] bytes = new byte[this.Offset];
            Array.Copy(this.Buffer, 0, bytes, 0, this.Offset);
            return bytes;
        }

        /// <summary>
        /// Converted this instance to byte array.
        /// </summary>
        public byte[] ToArray(int startOffset)
        {
            byte[] bytes = new byte[this.Offset - startOffset];
            Array.Copy(this.Buffer, startOffset, bytes, 0, this.Offset - startOffset);
            return bytes;
        }

        /// <summary>
        /// Converted this instance to byte array.
        /// </summary>
        public byte[] ToArray(int startOffset, int length)
        {
            byte[] bytes = new byte[length];
            Array.Copy(this.Buffer, startOffset, bytes, 0, length);
            return bytes;
        }
    }
}