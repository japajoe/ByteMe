using System.Text;

namespace ByteMe
{
    public enum TextEncoding
    {
        UTF8,
        UTF32,
        Unicode,
        ASCII
    }

    public sealed class TextEncoder
    {
        private UTF8Encoding utf8 = new UTF8Encoding();
        private UTF32Encoding utf32 = new UTF32Encoding();
        private UnicodeEncoding unicode = new UnicodeEncoding();
        private ASCIIEncoding ascii = new ASCIIEncoding();

        public TextEncoder()
        {
            utf8 = new UTF8Encoding();
            utf32 = new UTF32Encoding();
            unicode = new UnicodeEncoding();
            ascii = new ASCIIEncoding();
        }

        public int GetBytes(string value, int charIndex, int charCount, byte[] buffer, int offset, TextEncoding encoding)
        {
            int numBytes = 0;

            switch(encoding)
            {
                case TextEncoding.UTF8:
                    numBytes = utf8.GetBytes(value, charIndex, charCount, buffer, offset);
                    break;
                case TextEncoding.UTF32:
                    numBytes = utf32.GetBytes(value, charIndex, charCount, buffer, offset);
                    break;
                case TextEncoding.Unicode:
                    numBytes = unicode.GetBytes(value, charIndex, charCount, buffer, offset);
                    break;
                case TextEncoding.ASCII:
                    numBytes = ascii.GetBytes(value, charIndex, charCount, buffer, offset);
                    break;
                default:
                    return 0;
            }

            return numBytes;  
        }

        public string ToString(byte[] buffer, int offset, int length, TextEncoding encoding)
        {
            switch(encoding)
            {
                case TextEncoding.UTF8:
                    return utf8.GetString(buffer, offset, length);
                case TextEncoding.UTF32:
                    return utf32.GetString(buffer, offset, length);
                case TextEncoding.Unicode:
                    return unicode.GetString(buffer, offset, length);
                case TextEncoding.ASCII:
                    return ascii.GetString(buffer, offset, length);
                default:
                    return string.Empty;
            }
        }

        public unsafe int GetByteCount(string value, int charIndex, int charCount, TextEncoding encoding)
        {
            int numBytes = 0;

            fixed(char *ptr = value)
            {
                char *chars = &ptr[charIndex];

                switch(encoding)
                {
                    case TextEncoding.UTF8:
                        numBytes = utf8.GetByteCount(chars, charCount);
                        break;
                    case TextEncoding.UTF32:
                        numBytes = utf32.GetByteCount(chars, charCount);
                        break;
                    case TextEncoding.Unicode:
                        numBytes = unicode.GetByteCount(chars, charCount);
                        break;
                    case TextEncoding.ASCII:
                        numBytes = ascii.GetByteCount(chars, charCount);
                        break;
                    default:
                        return 0;
                }
            }

            return numBytes;    
        }
    }
}