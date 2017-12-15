using Baidu.Aip.Demo;
using System;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            
            var result = FaceDemo.FaceMatch().Result;
            Console.WriteLine(result.ToString());

            result = FaceDemo.FaceRegister().Result;
            Console.WriteLine(result.ToString());

            result = FaceDemo.FaceVerify().Result;
            Console.WriteLine(result.ToString());

            result = FaceDemo.FaceIdentify().Result;
            Console.WriteLine(result.ToString());
            Console.ReadLine();

        }
    }
}
