using System;

namespace ByteMe
{
    public static unsafe class BinaryConverter
    {
        private static TextEncoder encoder = new TextEncoder();

        public static bool IsLittleEndian
        {
            get
            {
                uint i = 1;
                byte* c = (byte*)&i;
                return *c > 0;
            }
        }

        private static void memcpy(void* dst, void* src, ulong n)
        {
            Buffer.MemoryCopy(src, dst, n, n);
        }

        private static void MemCopy(byte[] destination, int destinationOffset, byte* source, int sourceOffset, uint length)
        {
            fixed(byte* dest = &destination[destinationOffset])
            {
                memcpy(dest, &source[sourceOffset], (ulong)length);
            }
        }

        public static void GetBytes(long value, byte[] buffer, int offset)
        {
            byte* p = (byte*)&value;
            MemCopy(buffer, offset, p, 0, sizeof(long));
        }

        public static void GetBytes(ulong value, byte[] buffer, int offset)
        {
            byte* p = (byte*)&value;
            MemCopy(buffer, offset, p, 0, sizeof(ulong));
        }

        public static void GetBytes(int value, byte[] buffer, int offset)
        {
            byte* p = (byte*)&value;
            MemCopy(buffer, offset, p, 0, sizeof(int));
        }        

        public static void GetBytes(uint value, byte[] buffer, int offset)
        {
            byte* p = (byte*)&value;
            MemCopy(buffer, offset, p, 0, sizeof(uint));
        }

        public static void GetBytes(short value, byte[] buffer, int offset)
        {
            byte* p = (byte*)&value;
            MemCopy(buffer, offset, p, 0, sizeof(short));
        }

        public static void GetBytes(ushort value, byte[] buffer, int offset)
        {
            byte* p = (byte*)&value;
            MemCopy(buffer, offset, p, 0, sizeof(ushort));
        }

        public static void GetBytes(double value, byte[] buffer, int offset)
        {
            byte* p = (byte*)&value;
            MemCopy(buffer, offset, p, 0, sizeof(double));
        }

        public static void GetBytes(float value, byte[] buffer, int offset)
        {
            byte* p = (byte*)&value;
            MemCopy(buffer, offset, p, 0, sizeof(float));
        }

        public static int GetBytes(string value, int charIndex, int charCount, byte[] buffer, int offset, TextEncoding encoding)
        {
            return encoder.GetBytes(value, charIndex, charCount, buffer, offset, encoding);
        }

        public static int GetBytes(string value, int charCount, byte[] buffer, int offset, TextEncoding encoding)
        {
            return GetBytes(value, 0, charCount, buffer, offset, encoding);
        }

        public static int GetBytes(string value, byte[] buffer, int offset, TextEncoding encoding)
        {
            return GetBytes(value, 0, value.Length, buffer, offset, encoding);
        }

        public static Int64 ToInt64(byte[] bytes, int offset)
        {
            fixed(void* value = &bytes[offset])
            {
                return *(Int64*)value;
            }
        }

        public static UInt64 ToUInt64(byte[] bytes, int offset)
        {
            fixed(void* value = &bytes[offset])
            {
                return *(UInt64*)value;
            }
        }

        public static Int32 ToInt32(byte[] bytes, int offset)
        {
            fixed(void* value = &bytes[offset])
            {
                return *(Int32*)value;
            }
        }

        public static UInt32 ToUInt32(byte[] bytes, int offset)
        {
            fixed(void* value = &bytes[offset])
            {
                return *(UInt32*)value;
            }
        }

        public static Int16 ToInt16(byte[] bytes, int offset)
        {
            fixed(void* value = &bytes[offset])
            {
                return *(Int16*)value;
            }
        }

        public static UInt16 ToUInt16(byte[] bytes, int offset)
        {
            fixed(void* value = &bytes[offset])
            {
                return *(UInt16*)value;
            }
        }

        public static float ToSingle(byte[] bytes, int offset)
        {
            fixed(void* value = &bytes[offset])
            {
                return *(float*)value;
            }
        }

        public static double ToDouble(byte[] bytes, int offset)
        {
            fixed(void* value = &bytes[offset])
            {
                return *(double*)value;
            }
        }

        public static string ToString(byte[] buffer, int offset, int length, TextEncoding encoding)
        {
            return encoder.ToString(buffer, offset, length, encoding);
        }

        public static void FlipBytes(byte[] bytes, int offset, int length)
        {
            fixed(byte* ptr = &bytes[offset])
            {
                FlipBytes(ptr, length);
            }
        }

        private static void FlipBytes(byte* bytes, int length)
        {
            for (int i = 0; i < length / 2; ++i) 
            {
                byte t = bytes[i];
                bytes[i] = bytes[length - i - 1];
                bytes[length - i - 1] = t;
            }
        }

        public static int GetByteCount(string value, TextEncoding encoding)
        {
            return GetByteCount(value, 0, value.Length, encoding);
        }

        public static int GetByteCount(string value, int charCount, TextEncoding encoding)
        {
            return GetByteCount(value, 0, charCount, encoding);
        }

        public static int GetByteCount(string value, int charIndex, int charCount, TextEncoding encoding)
        {
            return encoder.GetByteCount(value, charIndex, charCount, encoding);
        }
    }
}