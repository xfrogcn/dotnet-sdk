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
            var client = new Face.Face("Api Key", "Secret Key");
            var image1 = File.ReadAllBytes(Path.Combine(AppContext.BaseDirectory,"images\\1.jpg"));
            var image2 = File.ReadAllBytes(Path.Combine(AppContext.BaseDirectory, "images\\2.jpg"));
            var image3 = File.ReadAllBytes(Path.Combine(AppContext.BaseDirectory, "images\\3.jpg"));

            var images = new[] {image1, image2, image3};

            // 人脸对比
            var result = await client.FaceMatch(images);

            return result;
        }

        public static void FaceDetect()
        {
            var client = new Face.Face("Api Key", "Secret Key");
            var image = File.ReadAllBytes("图片文件路径");
            var options = new Dictionary<string, object>
            {
                {"face_fields", "beauty,age"}
            };
            var result = client.FaceDetect(image, options);
        }

        public async static Task<JObject> FaceRegister()
        {
            var client = new Face.Face("Api Key", "Secret Key");
            var image1 = File.ReadAllBytes(Path.Combine(AppContext.BaseDirectory, "images\\1.jpg"));

            var result = await client.User.Register(image1, "TEST", "TEST", new[] { "YAOKONGJIAN_USER" });

            return result;
        }

        public static void FaceUpdate()
        {
            var client = new Face.Face("Api Key", "Secret Key");
            var image1 = File.ReadAllBytes("图片文件路径");

            var result = client.User.Update(image1, "uid", "groupId", "new user info");
        }

        public static void FaceDelete()
        {
            var client = new Face.Face("Api Key", "Secret Key");
            var result = client.User.Delete("uid");
            result = client.User.Delete("uid", new[] {"group1"});
        }

        public async static Task<JObject> FaceVerify()
        {
            var client = new Face.Face("Api Key", "Secret Key");
            var image1 = File.ReadAllBytes(Path.Combine(AppContext.BaseDirectory, "images\\1.jpg"));

            var result = await client.User.Verify(image1, "TEST", new[] { "YAOKONGJIAN_USER" }, 1);

            return result;
        }

        public async static Task<JObject> FaceIdentify()
        {
            var client = new Face.Face("Api Key", "Secret Key");
            var image1 = File.ReadAllBytes(Path.Combine(AppContext.BaseDirectory, "images\\1.jpg"));

            var result = await client.User.Identify(image1, new[] { "YAOKONGJIAN_USER" }, 1, 1);

            return result;
        }

        public static void UserInfo()
        {
            var client = new Face.Face("Api Key", "Secret Key");
            var result = client.User.GetInfo("uid");
        }

        public static void GroupList()
        {
            var client = new Face.Face("Api Key", "Secret Key");
            var result = client.Group.GetAllGroups(0, 100);
        }

        public static void GroupUsers()
        {
            var client = new Face.Face("Api Key", "Secret Key");
            var result = client.Group.GetUsers("groupId", 0, 100);
        }

        public static void GroupAddUser()
        {
            var client = new Face.Face("Api Key", "Secret Key");
            var result = client.Group.AddUser(new[] {"toGroupId"}, "uid", "fromGroupId");
        }

        public static void GroupDeleteUser()
        {
            var client = new Face.Face("Api Key", "Secret Key");
            var result = client.Group.DeleteUser(new[] {"groupId"}, "uid");
        }
    }
}