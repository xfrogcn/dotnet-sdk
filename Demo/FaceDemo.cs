using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Baidu.Aip.Demo
{
    internal class FaceDemo
    {
        public async static Task<JObject> FaceMatch()
        {
            var client = GetClient();
            var image1 = File.ReadAllBytes(Path.Combine(AppContext.BaseDirectory,"images\\1.jpg"));
            var image2 = File.ReadAllBytes(Path.Combine(AppContext.BaseDirectory, "images\\2.jpg"));
            var image3 = File.ReadAllBytes(Path.Combine(AppContext.BaseDirectory, "images\\3.jpg"));

            var images = new[] {image1, image2, image3};

            // 人脸对比
          
            var result = await client.FaceMatch(images);

            return result;
        }

        public async static Task<JObject> FaceDetect()
        {
            var client = GetClient();
            var image = File.ReadAllBytes(Path.Combine(AppContext.BaseDirectory, "images\\1.jpg"));
            var options = new Dictionary<string, object>
            {
                {"face_fields", "beauty,age"}
            };
            var result = await client.FaceDetect(image, options);

            return result;
        }

        public async static Task<JObject> FaceRegister()
        {
            var client = GetClient();
            var image1 = File.ReadAllBytes(Path.Combine(AppContext.BaseDirectory, "images\\1.jpg"));

            var result = await client.User.Register(image1, "TEST", "TEST", new[] { "YAOKONGJIAN_USER" });

            return result;
        }

        public async static Task<JObject> FaceUpdate()
        {
            var client = GetClient();
            var image2 = File.ReadAllBytes(Path.Combine(AppContext.BaseDirectory, "images\\2.jpg"));

            var result = await client.User.Update(image2, "TEST", "YAOKONGJIAN_USER", "TEST2");

            return result;
        }

        public async static Task<JObject> FaceDelete()
        {
            var client = GetClient();
            var result =  await client.User.Delete("TEST");
            // result = client.User.Delete("uid", new[] {"group1"});
            return result;
        }

        public async static Task<JObject> FaceVerify()
        {
            var client = GetClient();
            var image1 = File.ReadAllBytes(Path.Combine(AppContext.BaseDirectory, "images\\1.jpg"));

            var result = await client.User.Verify(image1, "TEST", new[] { "YAOKONGJIAN_USER" }, 1);

            return result;
        }

        public async static Task<JObject> FaceIdentify()
        {
            var client = GetClient();
            var image1 = File.ReadAllBytes(Path.Combine(AppContext.BaseDirectory, "images\\1.jpg"));

            var result = await client.User.Identify(image1, new[] { "YAOKONGJIAN_USER" }, 1, 1);

            return result;
        }

        public async static Task<JObject> UserInfo()
        {
            var client = GetClient();
            return await client.User.GetInfo("TEST");
        }

        public async static Task<JObject> GroupList()
        {
            var client = GetClient();
            return await client.Group.GetAllGroups(0, 100);
        }

        public async static Task<JObject> GroupUsers()
        {
            var client = GetClient();
            return await client.Group.GetUsers("YAOKONGJIAN_USER", 0, 100);
        }

        public static void GroupAddUser()
        {
            var client = new Face.Face("Api Key", "Secret Key");
            var result = client.Group.AddUser(new[] {"toGroupId"}, "uid", "fromGroupId");
        }

        public async static Task<JObject> GroupDeleteUser()
        {
            var client = GetClient();
            return await client.Group.DeleteUser(new[] { "YAOKONGJIAN_USER" }, "TEST");
        }

        private static Face.Face GetClient()
        {
            string json = System.IO.File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "api.json"));
            JObject obj = JObject.Parse(json);

            return new Face.Face((string)obj["ApiKey"], (string)obj["ApiSecret"]);
        }
    }
}