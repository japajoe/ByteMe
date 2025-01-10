// MIT License

// Copyright (c) 2025 W.M.R Jap-A-Joe

// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:

// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.

// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using System;

namespace ByteMe
{
    /// <summary>
    /// Utility class to read/write values from/to a byte array.
    /// </summary>
    public sealed class BinaryStream
    {
        private byte[] buffer;
        private int readOffset;
        private int writeOffset;
        private int length = 0;
        private TextEncoding encoding;

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
        /// <param name="encoding">The type of encoding to use for reading/writing strings.</param>
        public BinaryStream(byte[] buffer, int length = 0, TextEncoding encoding = TextEncoding.UTF8)
        {
            SetBuffer(buffer, length);
            this.encoding = encoding;
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

        public void Write(Int64 value, ByteOrder byteOrder = ByteOrder.LittleEndian)
        {
            BinaryConverter.GetBytes(value, buffer, writeOffset, byteOrder);
            AdvanceWriteOffset(sizeof(Int64));
        }

        public void Write(UInt64 value, ByteOrder byteOrder = ByteOrder.LittleEndian)
        {
            BinaryConverter.GetBytes(value, buffer, writeOffset, byteOrder);
            AdvanceWriteOffset(sizeof(UInt64));
        }         

        public void Write(Int32 value, ByteOrder byteOrder = ByteOrder.LittleEndian)
        {
            BinaryConverter.GetBytes(value, buffer, writeOffset, byteOrder);
            AdvanceWriteOffset(sizeof(Int32));
        }

        public void Write(UInt32 value, ByteOrder byteOrder = ByteOrder.LittleEndian)
        {
            BinaryConverter.GetBytes(value, buffer, writeOffset, byteOrder);
            AdvanceWriteOffset(sizeof(UInt32));
        }

        public void Write(Int16 value, ByteOrder byteOrder = ByteOrder.LittleEndian)
        {
            BinaryConverter.GetBytes(value, buffer, writeOffset, byteOrder);
            AdvanceWriteOffset(sizeof(Int16));
        }

        public void Write(UInt16 value, ByteOrder byteOrder = ByteOrder.LittleEndian)
        {
            BinaryConverter.GetBytes(value, buffer, writeOffset, byteOrder);
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

        public void Write(double value, ByteOrder byteOrder = ByteOrder.LittleEndian)
        {
            BinaryConverter.GetBytes(value, buffer, writeOffset, byteOrder);
            AdvanceWriteOffset(sizeof(double));
        }        

        public void Write(float value, ByteOrder byteOrder = ByteOrder.LittleEndian)
        {
            BinaryConverter.GetBytes(value, buffer, writeOffset, byteOrder);
            AdvanceWriteOffset(sizeof(float));
        }

        public void Write(string value)
        {
            int numBytes = BinaryConverter.GetBytes(value, 0, value.Length, buffer, writeOffset, encoding);
            AdvanceWriteOffset(numBytes);
        }

        public void Write(string value, int charCount)
        {
            int numBytes = BinaryConverter.GetBytes(value, 0, charCount, buffer, writeOffset, encoding);
            AdvanceWriteOffset(numBytes);
        }

        public void Write(string value, int charIndex, int charCount)
        {
            int numBytes = BinaryConverter.GetBytes(value, charIndex, charCount, buffer, writeOffset, encoding);
            AdvanceWriteOffset(numBytes);
        }  

        public Int64 ReadInt64(ByteOrder byteOrder = ByteOrder.LittleEndian)
        {
            Int64 value = BinaryConverter.ToInt64(buffer, readOffset, byteOrder);
            AdvanceReadOffset(sizeof(Int64));
            return value;            
        }

        public UInt64 ReadUInt64(ByteOrder byteOrder = ByteOrder.LittleEndian)
        {
            UInt64 value = BinaryConverter.ToUInt64(buffer, readOffset, byteOrder);
            AdvanceReadOffset(sizeof(UInt64));
            return value;            
        }

        public Int32 ReadInt32(ByteOrder byteOrder = ByteOrder.LittleEndian)
        {
            Int32 value = BinaryConverter.ToInt32(buffer, readOffset, byteOrder);
            AdvanceReadOffset(sizeof(Int32));
            return value;            
        }

        public UInt32 ReadUInt32(ByteOrder byteOrder = ByteOrder.LittleEndian)
        {
            UInt32 value = BinaryConverter.ToUInt32(buffer, readOffset, byteOrder);
            AdvanceReadOffset(sizeof(UInt32));
            return value;            
        }

        public Int16 ReadInt16(ByteOrder byteOrder = ByteOrder.LittleEndian)
        {
            Int16 value = BinaryConverter.ToInt16(buffer, readOffset, byteOrder);
            AdvanceReadOffset(sizeof(Int16));
            return value;            
        }

        public UInt16 ReadUInt16(ByteOrder byteOrder = ByteOrder.LittleEndian)
        {
            UInt16 value = BinaryConverter.ToUInt16(buffer, readOffset, byteOrder);
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

        public double ReadDouble(ByteOrder byteOrder = ByteOrder.LittleEndian)
        {
            double value = BinaryConverter.ToDouble(buffer, readOffset, byteOrder);
            AdvanceReadOffset(sizeof(double));
            return value;
        }

        public float ReadFloat(ByteOrder byteOrder = ByteOrder.LittleEndian)
        {
            float value = BinaryConverter.ToSingle(buffer, readOffset, byteOrder);
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
            string value = BinaryConverter.ToString(buffer, readOffset, length, encoding);
            AdvanceReadOffset(length);
            return value;
        }        
    }
}