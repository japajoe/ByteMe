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
using ByteMe;

namespace ByteMeExample
{
    public class Program
    {
        public class Employee
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public int Age { get; set; }
            public double Salary { get; set; }

            public Employee(string firstName, string lastName, int age, double salary)
            {
                this.FirstName = firstName;
                this.LastName = lastName;
                this.Age = age;
                this.Salary = salary;
            }

            public Employee(BinaryStream stream)
            {
                Deserialize(stream);
            }

            public int Serialize(BinaryStream stream)
            {
                int firstNameByteCount = BinaryConverter.GetByteCount(FirstName, TextEncoding.UTF8);
                stream.Write(firstNameByteCount);
                stream.Write(FirstName);
                
                int lastNameByteCount = BinaryConverter.GetByteCount(LastName, TextEncoding.UTF8);
                stream.Write(lastNameByteCount);
                stream.Write(LastName);

                stream.Write(Age);
                stream.Write(Salary);

                return stream.Length;
            }

            public void Deserialize(BinaryStream stream)
            {
                int firstNameByteCount = stream.ReadInt32();
                FirstName = stream.ReadString(firstNameByteCount);

                int lastNameByteCount = stream.ReadInt32();
                LastName = stream.ReadString(lastNameByteCount);

                Age = stream.ReadInt32();
                Salary = stream.ReadDouble();
            }
        }

        static void Main()
        {
            byte[] buffer = new byte[1024];
            int bufferSize = CreateAndSerializeEmployees(buffer);
            DeserializeEmployees(buffer, bufferSize);
        }

        static int CreateAndSerializeEmployees(byte[] buffer)
        {
            BinaryStream stream = new BinaryStream(buffer, 0, TextEncoding.UTF8);

            Employee employee1 = new Employee("Bob", "Brown", 26, 2368.35);
            Employee employee2 = new Employee("John", "Doe", 38, 2755.94);
            Employee employee3 = new Employee("Steve", "Smith", 42, 2941.47);

            int numEmployees = 3;
            stream.Write(numEmployees);
            employee1.Serialize(stream);
            employee2.Serialize(stream);
            employee3.Serialize(stream);
            return stream.Length;
        }

        static void DeserializeEmployees(byte[] buffer, int bufferSize)
        {
            BinaryStream stream = new BinaryStream(buffer, bufferSize, TextEncoding.UTF8);

            int numEmployees = stream.ReadInt32();

            for(int i = 0; i < numEmployees; i++)
            {
                Employee employee = new Employee(stream);
                Console.WriteLine("First Name: " + employee.FirstName);
                Console.WriteLine("Last Name: " + employee.LastName);
                Console.WriteLine("Age: " + employee.Age);
                Console.WriteLine("Salary: " + employee.Salary);
                Console.WriteLine("--------------------------------------------------");
            }
        }
    }
}