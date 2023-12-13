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