using System;

namespace ByteMe
{
    public enum WriteSizeType
    {
        OneByte = 1,
        TwoBytes = 2,
        FourBytes = 4
    }

    /// <summary>
    /// Utility class to read/write values from/to a byte array.
    /// </summary>
    public sealed class BinaryStream
    {
        

        private byte[] buffer;
        private int readOffset;
        private int writeOffset;
        private int length = 0;

        /// <summary>
        /// The buffer where data is read from and/or written to.
        /// </summary>
        public byte[] Buffer
        {
            get
            {
                return buffer;
            }
        }

        /// <summary>
        /// The current offset of the read pointer.
        /// </summary>
        public int ReadOffset
        {
            get
            {
                return readOffset;
            }
        }

        /// <summary>
        /// The current offset of the write pointer.
        /// </summary>
        public int WriteOffset
        {
            get
            {
                return writeOffset;
            }
        }

        /// <summary>
        /// The number of bytes that are written into the buffer.
        /// </summary>
        public int Length
        {
            get
            {
                return length;
            }
        }

        /// <summary>
        /// The size in bytes of the buffer.
        /// </summary>
        public int BufferSize
        {
            get
            {
                return buffer.Length;
            }
        }

        /// <summary>
        /// Constructs a new BinaryStream
        /// </summary>
        /// <param name="buffer">The target byte array. This can not be null.</param>
        /// <param name="length">The number of actual bytes in the buffer. This is useful if the buffer already has data inside of it.</param>
        public BinaryStream(byte[] buffer, int length = 0)
        {
            SetBuffer(buffer, length);
        }        

        /// <summary>
        /// Sets the buffer
        /// </summary>
        /// <param name="buffer">The target byte array. This can not be null.</param>
        /// <param name="length">The number of actual bytes in the buffer. This is useful if the buffer already has data inside of it.</param>
        public void SetBuffer(byte[] buffer, int length = 0)
        {
            this.buffer = buffer;
            this.readOffset = 0;
            this.writeOffset = 0;
            this.length = length;
        }        

        /// <summary>
        /// Resets the read/write pointer and the length.
        /// </summary>
        public void Reset()
        {
            this.readOffset = 0;
            this.writeOffset = 0;
            this.length = 0;
        }

        /// <summary>
        /// Sets the read pointer.
        /// </summary>
        /// <param name="offset">The offset of the pointer.</param>
        public void SetReadOffset(int offset)
        {
            this.readOffset = offset;
        }
        
        /// <summary>
        /// Sets the write pointer
        /// </summary>
        /// <param name="offset">The offset of the pointer.</param>
        public void SetWriteOffset(int offset)
        {
            this.writeOffset = offset;
        }

        private void AdvanceWriteOffset(int size)
        {
            //only increase length if current length is equal to the current position
            if ((writeOffset + size) == (length + size))
                length += size;

            writeOffset += size;   
        }

        private void AdvanceReadOffset(int size)
        {
            readOffset += size;
        }

        public void Write(Int64 value)
        {
            BinaryConverter.GetBytes(value, buffer, writeOffset);
            AdvanceWriteOffset(sizeof(Int64));
        }

        public void Write(UInt64 value)
        {
            BinaryConverter.GetBytes(value, buffer, writeOffset);
            AdvanceWriteOffset(sizeof(UInt64));
        }         

        public void Write(Int32 value)
        {
            BinaryConverter.GetBytes(value, buffer, writeOffset);
            AdvanceWriteOffset(sizeof(Int32));
        }

        public void Write(UInt32 value)
        {
            BinaryConverter.GetBytes(value, buffer, writeOffset);
            AdvanceWriteOffset(sizeof(UInt32));
        }

        public void Write(Int16 value)
        {
            BinaryConverter.GetBytes(value, buffer, writeOffset);
            AdvanceWriteOffset(sizeof(Int16));
        }

        public void Write(UInt16 value)
        {
            BinaryConverter.GetBytes(value, buffer, writeOffset);
            AdvanceWriteOffset(sizeof(UInt16));
        }

        public void Write(byte value)
        {
            buffer[writeOffset] = value;
            AdvanceWriteOffset(sizeof(byte));
        }         

        public void Write(bool value)
        {
            buffer[writeOffset] = value == false ? (byte)0 : (byte)1;
            AdvanceWriteOffset(sizeof(byte));
        }

        public void Write(double value)
        {
            BinaryConverter.GetBytes(value, buffer, writeOffset);
            AdvanceWriteOffset(sizeof(double));
        }        

        public void Write(float value)
        {
            BinaryConverter.GetBytes(value, buffer, writeOffset);
            AdvanceWriteOffset(sizeof(float));
        }

        public void Write(byte[] value, int length, bool writeLength = true, WriteSizeType writeSizeType = WriteSizeType.FourBytes)
        {
            if(writeLength)
            {
                int bufferLength = length;

                if(writeSizeType == WriteSizeType.OneByte)
                    BinaryConverter.GetBytes((byte)bufferLength, buffer, writeOffset);
                else if(writeSizeType == WriteSizeType.TwoBytes)
                    BinaryConverter.GetBytes((ushort)bufferLength, buffer, writeOffset);
                else
                    BinaryConverter.GetBytes((uint)bufferLength, buffer, writeOffset);
                
                System.Buffer.BlockCopy(value, 0, buffer, writeOffset + (int)writeSizeType, bufferLength);
                
                
                AdvanceWriteOffset(sizeof(int));
                AdvanceWriteOffset(bufferLength);
            }
            else
            {
                System.Buffer.BlockCopy(value, 0, buffer, writeOffset, length);
                AdvanceWriteOffset(length);
            }            
        }

        public void Write(string value, bool writeLength = true, WriteSizeType writeSizeType = WriteSizeType.FourBytes)
        {
            if(writeLength)
            {
                int stringLength = 0;

                if(writeSizeType == WriteSizeType.OneByte)
                    stringLength = BinaryConverter.GetBytes(value, buffer, writeOffset + sizeof(byte));
                else if(writeSizeType == WriteSizeType.TwoBytes)
                    stringLength = BinaryConverter.GetBytes(value, buffer, writeOffset + sizeof(ushort));
                else
                    stringLength = BinaryConverter.GetBytes(value, buffer, writeOffset + sizeof(uint));
                
                BinaryConverter.GetBytes(stringLength, buffer, writeOffset);
                AdvanceWriteOffset((int)writeSizeType);
                AdvanceWriteOffset(stringLength);
            }
            else
            {
                int stringLength = BinaryConverter.GetBytes(value, buffer, writeOffset);
                AdvanceWriteOffset(stringLength);
            }
        }

        public void Write(string value, int offset, int length, bool writeLength = true, WriteSizeType writeSizeType = WriteSizeType.FourBytes)
        {
            if(writeLength)
            {
                int numBytesWritten = 0;
                
                if(writeSizeType == WriteSizeType.OneByte)
                    numBytesWritten = BinaryConverter.GetBytes(value, offset, length, buffer, writeOffset + sizeof(byte));
                else if(writeSizeType == WriteSizeType.TwoBytes)
                    numBytesWritten = BinaryConverter.GetBytes(value, offset, length, buffer, writeOffset + sizeof(ushort));
                else
                    numBytesWritten = BinaryConverter.GetBytes(value, offset, length, buffer, writeOffset + sizeof(uint));                
                
                BinaryConverter.GetBytes(numBytesWritten, buffer, writeOffset);
                AdvanceWriteOffset((int)writeSizeType);
                AdvanceWriteOffset(numBytesWritten);
            }
            else
            {
                int numBytesWritten = BinaryConverter.GetBytes(value, offset, length, buffer, writeOffset);
                AdvanceWriteOffset(numBytesWritten);
            }
        }      

        public Int64 ReadInt64()
        {
            Int64 value = BitConverter.ToInt64(buffer, readOffset);
            AdvanceReadOffset(sizeof(Int64));
            return value;            
        }

        public UInt64 ReadUInt64()
        {
            UInt64 value = BitConverter.ToUInt64(buffer, readOffset);
            AdvanceReadOffset(sizeof(UInt64));
            return value;            
        }

        public Int32 ReadInt32()
        {
            Int32 value = BitConverter.ToInt32(buffer, readOffset);
            AdvanceReadOffset(sizeof(Int32));
            return value;            
        }

        public UInt32 ReadUInt32()
        {
            UInt32 value = BitConverter.ToUInt32(buffer, readOffset);
            AdvanceReadOffset(sizeof(UInt32));
            return value;            
        }

        public Int16 ReadInt16()
        {
            Int16 value = BitConverter.ToInt16(buffer, readOffset);
            AdvanceReadOffset(sizeof(Int16));
            return value;            
        }

        public UInt16 ReadUInt16()
        {
            UInt16 value = BitConverter.ToUInt16(buffer, readOffset);
            AdvanceReadOffset(sizeof(UInt16));
            return value;            
        }

        public byte ReadByte()
        {
            byte value = buffer[readOffset];
            AdvanceReadOffset(sizeof(byte));
            return value;              
        }

        public bool ReadBool()
        {
            bool value = buffer[readOffset] == 0 ? false : true;
            AdvanceReadOffset(sizeof(byte));
            return value;
        }

        public double ReadDouble()
        {
            double value = BitConverter.ToDouble(buffer, readOffset);
            AdvanceReadOffset(sizeof(double));
            return value;
        }

        public float ReadFloat()
        {
            float value = BitConverter.ToSingle(buffer, readOffset);
            AdvanceReadOffset(sizeof(float));
            return value;
        }

        public void ReadBytes(byte[] buffer, int length, int offset)
        {
            System.Buffer.BlockCopy(this.buffer, readOffset, buffer, offset, length);
            AdvanceReadOffset(length);
        }

        public string ReadString(int length)
        {
            string value = BinaryConverter.ToString(this.buffer, readOffset, length, StringEncoding.UTF8);
            AdvanceReadOffset(length);
            return value;
        }        
    }
}