using Baidu.Aip.Demo;
using System;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");


            // var result = FaceDemo.GroupList().Result;
            // Console.WriteLine(result.ToString());

            //var result = FaceDemo.GroupUsers().Result;
            //Console.WriteLine(result.ToString());

            //var result = FaceDemo.GroupDeleteUser().Result;
            //Console.WriteLine(result.ToString());

            //var result = FaceDemo.FaceRegister().Result;
            //Console.WriteLine(result.ToString());

            //var result = FaceDemo.FaceMatch().Result;
            //Console.WriteLine(result.ToString());

            //var result = FaceDemo.FaceDetect().Result;
            //Console.WriteLine(result.ToString());

            //var result = FaceDemo.FaceUpdate().Result;
            //Console.WriteLine(result.ToString());

            //var result = FaceDemo.FaceVerify().Result;
            //Console.WriteLine(result.ToString());

            //var result = FaceDemo.FaceIdentify().Result;
            //Console.WriteLine(result.ToString());

            var result = FaceDemo.UserInfo().Result;
            Console.WriteLine(result.ToString());

            //result = FaceDemo.FaceRegister().Result;
            //Console.WriteLine(result.ToString());

            //result = FaceDemo.FaceVerify().Result;
            //Console.WriteLine(result.ToString());

            //result = FaceDemo.FaceIdentify().Result;
            //Console.WriteLine(result.ToString());
            Console.ReadLine();

        }
    }
}
