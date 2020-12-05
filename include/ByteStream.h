#ifndef BYTESTREAM_HPP
#define BYTESTREAM_HPP

#include "Primitives.h"
#include "BitConverter.h"
#include <cstring>

class ByteStream
{
public:
    ByteStream()
    {
        m_buffer = nullptr;
        m_bufferSize = 0;
        m_length = 0;
        m_position = 0;    
    }

    ByteStream(byte* bytes, int bufferSize)
    {
        SetBuffer(bytes, bufferSize);
    }

    void SetBuffer(byte* bytes, int bufferSize)
    {
        m_buffer = bytes;
        this->m_bufferSize = bufferSize;
        m_length = 0;
        m_position = 0;    
    }

    void Reset()
    {
        m_length = 0;
        m_position = 0;
    }

    int GetPosition() const
    {
        return m_position;
    }

    int GetLength() const
    {
        return m_length;
    }

    void Seek(int index)
    {
        if(index < m_bufferSize)
            m_position = index;
    }

    void ResetPosition()
    {
        m_position = 0;
    }

    int ToArray(byte* buffer)
    {
        if(IsNull())
            return 0;

        memcpy(&buffer[0], &m_buffer[0], m_length);
        return m_length;
    }
    
    void Read(byte* buffer, int length = 0)
    {
        if(IsNull())
            return;

        if(length == 0)
            length = Read<UInt16>();

        memcpy(&buffer[0], &m_buffer[m_position], length);
        IncrementPosition(length);
    }

    byte* GetBuffer() const 
    { 
        return m_buffer; 
    }

    template<typename T>
    T Read()
    {
        T val;
        if(IsNull())
            return val;

        int length = sizeof(T);
        memcpy(&val, &m_buffer[m_position], length);
        IncrementPosition(length);
        return val;
    }

    std::string ReadString(int length = 0)
    {
        if(IsNull())
            return "";

        if(length == 0)
        {
            length = Read<UInt16>();
        }

        std::string value = BitConverter::ToString(m_buffer, m_position, length);
        IncrementPosition(length);
        return value;
    }

    template<typename T>
    void Write(T value)
    {
        if(IsNull())
            return;

        int length = sizeof(T);

        if(IsOutOfRange(length))
            return;

        memcpy(&m_buffer[m_position], &value, length);
        IncrementPositionAndSize(length);     
    }

    void Write(byte* value, int startIndex, int length, bool writeSize = true)
    {
        if(IsNull())
            return;

        if(IsOutOfRange(length))
            return;

        if(writeSize)
        {
            UInt16 size = static_cast<UInt16>(length);
            memcpy(&m_buffer[m_position], &size, sizeof(UInt16));
            IncrementPositionAndSize(sizeof(UInt16));
        }

        memcpy(&m_buffer[m_position], &value[startIndex], length);
        IncrementPositionAndSize(length);
    }

    void WriteString(const std::string& text, bool writeSize = true)
    {
        if(IsNull())
            return;

        UInt16 length = static_cast<UInt16>(text.size());

        if(IsOutOfRange(length))
            return;

        if(writeSize)
        {            
            memcpy(&m_buffer[m_position], &length, sizeof(UInt16));
            IncrementPositionAndSize(sizeof(UInt16));
        }

        BitConverter::GetBytes(text, m_buffer, m_position);
        IncrementPositionAndSize(length);
    }

private:
    byte* m_buffer;
    int m_position;
    int m_length;
    int m_bufferSize;

    void IncrementPositionAndSize(int size)
    {
        if((m_position + size) == (m_length + size))
            m_length += size;

        m_position += size;       
    }

    void IncrementPosition(int size)
    {
        m_position += size;
    }

    bool IsOutOfRange(int size)
    {
        if((m_position + size) >= m_bufferSize)
            return true;
        return false;
    }

    bool IsNull()
    {
        return m_buffer == nullptr ? true : false;
    }
};

#endif