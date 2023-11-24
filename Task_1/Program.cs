using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security;
using System.Runtime.InteropServices;

namespace Task_1
{
    public class Int32OverflowException : Exception
    { 
        public Int32OverflowException(string message) : base(message) { }

    }
    


    internal class Program
    {

        static int[] ReadDataTxt(string fileName)
        {
            
            try
            {
                int[] data = new int[2];
                using (StreamReader sr = new StreamReader(new FileStream(fileName, FileMode.Open, FileAccess.Read)))
                {
                    data[0] = int.Parse(sr.ReadLine());
                    data[1] = int.Parse(sr.ReadLine());
                }

                return data;

            }
            catch (FileNotFoundException ex)
            {

                using (StreamWriter sw = new StreamWriter("no_file.txt", append: true))
                {
                    sw.WriteLine($"file {fileName} not found");
                }

                throw new FileNotFoundException($"file {fileName} not found");

            }
            catch (FormatException ex)
            {

                using (StreamWriter sw1 = new StreamWriter("bad_data.txt", append: true))
                {
                    sw1.WriteLine($"Error in file {fileName}");
                }

                throw new FormatException($"Error in file {fileName} could not convert string to integers");
            }
       

        }
        static double Average()
        {
            string[] filesToClear = { "no_file.txt", "bad_data.txt", "overflow.txt" };

            foreach (string fileNames in filesToClear)
            {
                using (StreamWriter sw = new StreamWriter(fileNames, false))
                {

                }
            }

            int amount = 0;
            int sum = 0;

            for (int i = 10; i < 30; i++)
            {
                try
                {
                    int[] arr = ReadDataTxt($"{i}.txt");
                    checked
                    {
                        sum += arr[0] * arr[1];
                        amount++;
                    }
                }
                catch (OverflowException ex)
                {
                    using (StreamWriter type = new StreamWriter("overflow.txt", true))
                    {
                        type.WriteLine($"file {i}.txt type not 32 type");
                    }

                    Console.WriteLine($"File {i}.txt: The product of numbers goes beyong the int 32 type");
                }
                catch (Exception ex)
                {

                    Console.WriteLine($"File {i}.txt: {ex.Message}");
                }

            }
            try
            {

                return sum / amount;
            }
            catch (Exception)
            {

                throw new Exception("sum and amount is equal 0");
            }
        }

        static void Main(string[] args)
        {


            Console.WriteLine("average = " + Average());
            Console.ReadKey();


        }
    }
}
