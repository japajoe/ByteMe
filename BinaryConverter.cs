using System;
using System.Runtime.InteropServices;

namespace ByteMe
{
    public enum StringEncoding
    {
        Unicode,
        UTF8
    }

    public enum ByteOrder
    {
        BigEndian,
        LittleEndian
    }

    public static unsafe class BinaryConverter
    {
        private static System.Text.UTF8Encoding utf8 = new System.Text.UTF8Encoding();
        private static System.Text.UnicodeEncoding unicode = new System.Text.UnicodeEncoding();

        public static bool IsLittleEndian
        {
            get
            {
                uint i = 1;
                byte* c = (byte*)&i;
                return *c > 0;
            }
        }

        [DllImport("libc", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr memcpy(void* dst, void* src, UInt64 n);

        public static void MemCopy(byte[] destination, int destinationOffset, byte[] source, int sourceOffset, uint length)
        {            
            fixed(byte* dest = &destination[destinationOffset])
            {
                fixed(byte* src = &source[sourceOffset])
                {
                    memcpy(dest, src, (UInt64)length);
                }
            }
        }

        public static void MemCopy(byte[] destination, int destinationOffset, byte* source, int sourceOffset, uint length)
        {
            fixed(byte* dest = &destination[destinationOffset])
            {
                memcpy(dest, &source[sourceOffset], (UInt64)length);
            }
        }

        public static void MemCopy(byte* destination, int destinationOffset, byte* source, int sourceOffset, uint length)
        {
            memcpy(&destination[destinationOffset], &source[sourceOffset], (UInt64)length);
        }

        public static void GetBytes(long value, byte[] buffer, int offset)
        {
            byte* p = (byte*)&value;
            MemCopy(buffer, offset, p, 0, sizeof(long));
        }

        public static void GetBytes(long value, byte* buffer, int offset)
        {
            byte* p = (byte*)&value;            
            MemCopy(buffer, offset, p, 0, sizeof(long));
        }

        public static void GetBytes(ulong value, byte[] buffer, int offset)
        {
            byte* p = (byte*)&value;
            MemCopy(buffer, offset, p, 0, sizeof(ulong));
        }

        public static void GetBytes(ulong value, byte* buffer, int offset)
        {
            byte* p = (byte*)&value;
            MemCopy(buffer, offset, p, 0, sizeof(ulong));
        }

        public static void GetBytes(int value, byte[] buffer, int offset)
        {
            byte* p = (byte*)&value;
            MemCopy(buffer, offset, p, 0, sizeof(int));
        }        

        public static void GetBytes(int value, byte* buffer, int offset)
        {
            byte* p = (byte*)&value;
            MemCopy(buffer, offset, p, 0, sizeof(int));
        }

        public static void GetBytes(uint value, byte[] buffer, int offset)
        {
            byte* p = (byte*)&value;
            MemCopy(buffer, offset, p, 0, sizeof(uint));
        }

        public static void GetBytes(uint value, byte* buffer, int offset)
        {
            byte* p = (byte*)&value;
            MemCopy(buffer, offset, p, 0, sizeof(uint));
        }

        public static void GetBytes(short value, byte[] buffer, int offset)
        {
            byte* p = (byte*)&value;
            MemCopy(buffer, offset, p, 0, sizeof(short));
        }

        public static void GetBytes(short value, byte* buffer, int offset)
        {
            byte* p = (byte*)&value;
            MemCopy(buffer, offset, p, 0, sizeof(short));
        }

        public static void GetBytes(ushort value, byte[] buffer, int offset)
        {
            byte* p = (byte*)&value;
            MemCopy(buffer, offset, p, 0, sizeof(ushort));
        }

        public static void GetBytes(ushort value, byte* buffer, int offset)
        {
            byte* p = (byte*)&value;
            MemCopy(buffer, offset, p, 0, sizeof(ushort));
        }

        public static void GetBytes(double value, byte[] buffer, int offset)
        {
            byte* p = (byte*)&value;
            MemCopy(buffer, offset, p, 0, sizeof(double));
        }

        public static void GetBytes(double value, byte* buffer, int offset)
        {
            byte* p = (byte*)&value;
            MemCopy(buffer, offset, p, 0, sizeof(double));
        }

        public static void GetBytes(float value, byte[] buffer, int offset)
        {
            byte* p = (byte*)&value;
            MemCopy(buffer, offset, p, 0, sizeof(float));
        }

        public static void GetBytes(float value, byte* buffer, int offset)
        {
            byte* p = (byte*)&value;
            MemCopy(buffer, offset, p, 0, sizeof(float));
        }

        public static int GetBytes(string v, byte[] buffer, int offset, StringEncoding encoding = StringEncoding.UTF8)
        {
            int numBytes = 0;

            switch (encoding)
            {
                case StringEncoding.Unicode:
                    numBytes = unicode.GetBytes(v, 0, v.Length, buffer, offset);
                    break;
                case StringEncoding.UTF8:
                    numBytes = utf8.GetBytes(v, 0, v.Length, buffer, offset);
                    break;
                default:
                    numBytes = utf8.GetBytes(v, 0, v.Length, buffer, offset);
                    break;
            }

            return numBytes;
        }

        public static int GetBytes(string v, int valueOffset, int valueLength, byte[] buffer, int offset, StringEncoding encoding = StringEncoding.UTF8)
        {
            int numBytesWritten = 0;

            fixed(char* cptr = v)
            {
                fixed (byte* bptr = &buffer[offset])
                {
                    char* p = cptr;
                    p = &p[valueOffset];

                    switch(encoding)
                    {
                        case StringEncoding.Unicode:
                            numBytesWritten = unicode.GetBytes(p, valueLength, bptr, buffer.Length);
                            break;
                        case StringEncoding.UTF8:
                            numBytesWritten = utf8.GetBytes(p, valueLength, bptr, buffer.Length);
                            break;
                        default:
                            numBytesWritten = utf8.GetBytes(p, valueLength, bptr, buffer.Length);
                            break;                                                        
                    }
                }
            }

            return numBytesWritten;
        }        

        public static int GetBytes(string v, byte* buffer, int offset, StringEncoding encoding = StringEncoding.UTF8)
        {
            byte[] bytes = null;

            switch (encoding)
            {
                case StringEncoding.Unicode:
                    bytes = unicode.GetBytes(v);
                    break;
                case StringEncoding.UTF8:
                    bytes = utf8.GetBytes(v);
                    break;
                default:
                    bytes = utf8.GetBytes(v);
                    break;
            }

            fixed(byte* src = &bytes[0])
            {
                memcpy(&buffer[offset], src, (UInt64)bytes.Length);
            }
            
            return bytes.Length;
        }

        public static Int64 ToInt64(byte* bytes, int offset)
        {
            return *(Int64*)bytes[offset];
        }

        public static Int64 ToInt64(byte[] bytes, int offset)
        {
            fixed(void* value = &bytes[offset])
            {
                return *(Int64*)value;
            }
        }        

        public static UInt64 ToUInt64(byte* bytes, int offset)
        {
            return *(UInt64*)bytes[offset];
        }

        public static UInt64 ToUInt64(byte[] bytes, int offset)
        {
            fixed(void* value = &bytes[offset])
            {
                return *(UInt64*)value;
            }
        }

        public static Int32 ToInt32(byte* bytes, int offset)
        {
            return *(Int32*)bytes[offset];
        }

        public static Int32 ToInt32(byte[] bytes, int offset)
        {
            fixed(void* value = &bytes[offset])
            {
                return *(Int32*)value;
            }
        }

        public static UInt32 ToUInt32(byte* bytes, int offset)
        {
            return *(UInt32*)bytes[offset];
        }

        public static UInt32 ToUInt32(byte[] bytes, int offset)
        {
            fixed(void* value = &bytes[offset])
            {
                return *(UInt32*)value;
            }
        }

        public static Int16 ToInt16(byte* bytes, int offset)
        {
            return *(Int16*)bytes[offset];
        }

        public static Int16 ToInt16(byte[] bytes, int offset)
        {
            fixed(void* value = &bytes[offset])
            {
                return *(Int16*)value;
            }
        }

        public static UInt16 ToUInt16(byte* bytes, int offset)
        {
            return *(UInt16*)bytes[offset];
        }

        public static UInt16 ToUInt16(byte[] bytes, int offset)
        {
            fixed(void* value = &bytes[offset])
            {
                return *(UInt16*)value;
            }
        }

        public static float ToSingle(byte* bytes, int offset)
        {
            return *(float*)bytes[offset];
        }

        public static float ToSingle(byte[] bytes, int offset)
        {
            fixed(void* value = &bytes[offset])
            {
                return *(float*)value;
            }
        }

        public static double ToDouble(byte* bytes, int offset)
        {
            return *(double*)bytes[offset];
        }

        public static double ToDouble(byte[] bytes, int offset)
        {
            fixed(void* value = &bytes[offset])
            {
                return *(double*)value;
            }
        }

        public static string ToString(byte[] buffer, int offset, int length, StringEncoding encoding = StringEncoding.UTF8)
        {
            if(encoding == StringEncoding.UTF8)
            {
                return utf8.GetString(buffer, offset, length);
            }
            else if(encoding == StringEncoding.Unicode)
            {
                //Only flip bytes on multi byte characters
                return unicode.GetString(buffer, offset, length);
            }
            else
            {
                return utf8.GetString(buffer, offset, length);
            }
        }


        public static void FlipBytes(byte[] bytes, int offset, int length)
        {
            fixed(byte* ptr = &bytes[offset])
            {
                FlipBytes(ptr, length);
            }
        }

        public static void FlipBytes(byte* bytes, int length)
        {
            for (int i = 0; i < length / 2; ++i) 
            {
                byte t = bytes[i];
                bytes[i] = bytes[length - i - 1];
                bytes[length - i - 1] = t;
            }
        }
    }
}