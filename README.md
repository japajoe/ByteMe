# ByteMe
C# library for converting from/to binary

# Why?
Because the BitConverter class is not really efficient in terms of memory allocations. Each time you call BitConverter.GetBytes it allocates new memory, which is not something you want to do on a frequent basis. It would've been much more inviting if it let us pass a pre-allocated buffer that we ultimately could reuse. This library does exactly that.