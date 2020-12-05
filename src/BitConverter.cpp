#include "BitConverter.h"
#include <cstring>

Int64 BitConverter::ToInt64(byte* bytes, int startIndex)
{
    long val = 0;
    memcpy(&val, &bytes[startIndex], sizeof(long));
    return val;
}

UInt64 BitConverter::ToUInt64(byte* bytes, int startIndex)
{
    unsigned long val = 0;
    memcpy(&val, &bytes[startIndex], sizeof(unsigned long));
    return val;	
}

Int32 BitConverter::ToInt32(byte* bytes, int startIndex)
{
    Int32 val = 0;
    memcpy(&val, &bytes[startIndex], sizeof(Int32));
    return val;
}

UInt32 BitConverter::ToUInt32(byte* bytes, int startIndex)
{
    UInt32 val = 0;
    memcpy(&val, &bytes[startIndex], sizeof(UInt32));
    return val;
}

Int16 BitConverter::ToInt16(byte* bytes, int startIndex)
{
    Int16 val = 0;
    memcpy(&val, &bytes[startIndex], sizeof(Int16));
    return val;
}

UInt16 BitConverter::ToUInt16(byte* bytes, int startIndex)
{
    UInt16 val = 0;
    memcpy(&val, &bytes[startIndex], sizeof(UInt16));
    return val;
}

float BitConverter::ToSingle(byte* bytes, int startIndex)
{
    float val = 0;
    memcpy(&val, &bytes[startIndex], sizeof(float));
    return val;
}

double BitConverter::ToDouble(byte* bytes, int startIndex)
{
    double val = 0;
    memcpy(&val, &bytes[startIndex], sizeof(double));
    return val;
}

std::string BitConverter::ToString(byte* bytes, int startIndex, size_t length)
{
    char buf[length+1];
    memcpy(buf, &bytes[startIndex], length);
    buf[length] = '\0';
    std::string value(buf);
    return value;
}

void BitConverter::GetBytes(Int64 value, byte* buffer, int startIndex)
{
    memcpy(&buffer[startIndex], &value, sizeof(Int64));
}

void BitConverter::GetBytes(UInt64 value, byte* buffer, int startIndex)
{
    memcpy(&buffer[startIndex], &value, sizeof(UInt64));
}

void BitConverter::GetBytes(Int32 value, byte* buffer, int startIndex)
{
    memcpy(&buffer[startIndex], &value, sizeof(Int32));
}

void BitConverter::GetBytes(UInt32 value, byte* buffer, int startIndex)
{
    memcpy(&buffer[startIndex], &value, sizeof(UInt32));
}

void BitConverter::GetBytes(Int16 value, byte* buffer, int startIndex)
{
    memcpy(&buffer[startIndex], &value, sizeof(Int16));
}

void BitConverter::GetBytes(UInt16 value, byte* buffer, int startIndex)
{
    memcpy(&buffer[startIndex], &value, sizeof(UInt16));
}

void BitConverter::GetBytes(float value, byte* buffer, int startIndex)
{
    memcpy(&buffer[startIndex], &value, sizeof(float));
}

void BitConverter::GetBytes(double value, byte* buffer, int startIndex)
{
    memcpy(&buffer[startIndex], &value, sizeof(double));
}

void BitConverter::GetBytes(const std::string& value, byte* buffer, int startIndex)
{
    const char* v = value.c_str();
    memcpy(&buffer[startIndex], v, value.size());
}
