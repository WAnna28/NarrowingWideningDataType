using System;

namespace NarrowingWideningDataType
{
    class Program
    {
        static void Main()
        {
            ShowWidening();
            ShowNarrowing();
            UsingChecked();

            // DO TOGETHER
            // Setting Project-wide Overflow Checking
            // The C# compiler supports the /checked flag. When it’s enabled, all your arithmetic will
            // be evaluated for overflow without the need to make use of the C# checked keyword.
            // If overflow has been discovered, you will still receive a runtime exception.
            /*
                <PropertyGroup>
                    <CheckForOverflowUnderflow>true</CheckForOverflowUnderflow>
                </PropertyGroup>
            */

            // DO TOGETHER
            // Setting Project-wide Overflow Checking (Visual Studio)

            UsingUnchecked();
        }

        static void ShowWidening()
        {
            // Widening is the term used to define an implicit upward cast that does not result in a loss of data.
            short numb1 = 20, numb2 = 21;
            Console.WriteLine("{0} * {1} = {2}", numb1, numb2, Multiply(numb1, numb2));
        }

        static void ShowNarrowing()
        {
            // Compiler error below
            // The CoreCLR was unable to apply a narrowing operation
            short numb3 = 2020, numb4 = 2021;
            short answer = (short)Multiply(numb3, numb4);

            // When you want to inform the compiler that you are willing to deal
            // with a possible loss of data because of a narrowing operation,
            // you must apply an explicit cast using the C# casting operator, ().
            // Explicitly cast the int into a short (and allow loss of data).
            short answer22 = unchecked((short)Multiply(numb3, numb4));
            Console.WriteLine("\n{0} * {1} = {2}", numb3, numb4, Multiply(numb3, numb4));
            Console.WriteLine("{0} * {1} = {2}", numb3, numb4, answer22);

            // Explicitly cast the int into a byte (no loss of data).
            byte myByte = 0;
            int myInt = 200;
            myByte = (byte)myInt;
            Console.WriteLine("\nValue of myByte: {0}", myByte);
        }

        static int Multiply(int x, int y)
        {
            return x * y;
        }

        static void UsingChecked()
        {
            byte b1 = 100;
            byte b2 = 250;
            byte sum = (byte)Add(b1, b2);
            // sum should hold the value 350. However, we find the value 94!
            Console.WriteLine("\nsum = {0}", sum);

            // This time, tell the compiler to add CIL code
            // to throw an exception if overflow/underflow
            // takes place.
            // https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.conv_u1?view=net-5.0
            try
            {
                byte sum2 = checked((byte)Add(b1, b2));
                Console.WriteLine("sum = {0}", sum2);
            }
            catch (OverflowException ex)
            {
                Console.WriteLine(ex.Message);
            }

            // If you want to force overflow checking to occur over a block of code statements,
            // you can do so by defining a “checked scope” as follows:
            try
            {
                checked
                {
                    byte sum3 = (byte)Add(b1, b2);
                    Console.WriteLine("sum = {0}", sum3);
                }
            }
            catch (OverflowException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static int Add(int x, int y)
        {
            return x + y;
        }

        static void UsingUnchecked()
        {
            // Given that the /checked flag will evaluate all arithmetic logic, C# provides the
            // unchecked keyword to disable the throwing of an overflow exception on a case-by -case basis.
            // This keyword’s use is identical to that of the checked keyword,
            // in that you can specify a single statement or a block of statements.
            byte b1 = 100;
            byte b2 = 250;

            unchecked
            {
                byte sum = (byte)(b1 + b2);
                Console.WriteLine("\nsum = {0} ", sum);
            }

            Console.ReadLine();
        }
    }
}