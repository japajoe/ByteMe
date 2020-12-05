#ifndef BITCONVERTER_HPP
#define BITCONVERTER_HPP

#include "Primitives.h"
#include <string>

class BitConverter
{
public:
    static Int64 ToInt64(byte* bytes, int startIndex);
    static UInt64 ToUInt64(byte* bytes, int startIndex);
    static Int32 ToInt32(byte* bytes, int startIndex);
    static UInt32 ToUInt32(byte* bytes, int startIndex);
    static Int16 ToInt16(byte* bytes, int startIndex);
    static UInt16 ToUInt16(byte* bytes, int startIndex);
    static float ToSingle(byte* bytes, int startIndex);
    static double ToDouble(byte* bytes, int startIndex);
    static std::string ToString(byte* bytes, int startIndex, size_t length);

    static void GetBytes(Int64 value, byte* buffer, int startIndex);
    static void GetBytes(UInt64 value, byte* buffer, int startIndex);
    static void GetBytes(Int32 value, byte* buffer, int startIndex);
    static void GetBytes(UInt32 value, byte* buffer, int startIndex);
    static void GetBytes(Int16 value, byte* buffer, int startIndex);
    static void GetBytes(UInt16 value, byte* buffer, int startIndex);
    static void GetBytes(float value, byte* buffer, int startIndex);
    static void GetBytes(double value, byte* buffer, int startIndex);
    static void GetBytes(const std::string &value, byte* buffer, int startIndex);
};

#endif
