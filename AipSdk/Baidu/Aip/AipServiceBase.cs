﻿/*
 * Copyright 2017 Baidu, Inc.
 *
 * Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance with
 * the License. You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software distributed under the License is distributed on
 * an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the License for the
 * specific language governing permissions and limitations under the License.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Http;

namespace Baidu.Aip
{
    using Options = Dictionary<string, string>;

    public abstract class AipServiceBase
    {
        protected readonly SemaphoreSlim AuthLock = new SemaphoreSlim(1,1);
        protected volatile bool HasDoneAuthoried; // 是否已经走过鉴权流程
        protected volatile bool IsDev;
        private static HttpClient apiClient = new HttpClient();

        protected AipServiceBase(string apiKey, string secretKey)
        {
            ApiKey = apiKey;
            SecretKey = secretKey;
            ExpireAt = DateTime.Now;
            DebugLog = false;
        }

        protected string Token { get; set; }
        protected DateTime ExpireAt { get; set; }

        public string ApiKey { get; set; }
        public string SecretKey { get; set; }
        public bool DebugLog { get; set; }


        protected async virtual Task DoAuthorization()
        {

            if (!NeetAuth())
                return;

            await AuthLock.WaitAsync();
            try
            {
                var resp = await Auth.OpenApiFetchToken(ApiKey, SecretKey);

                if (resp != null)
                {
                    ExpireAt = DateTime.Now.AddSeconds((int)resp["expires_in"] - 1);
                    var scopes = resp["scope"].ToString().Split(' ');
                    if (scopes.ToList().Exists(v => Consts.AipScopes.Contains(v)))
                    {
                        IsDev = true;
                        Token = (string)resp["access_token"];
                    }
                }
                HasDoneAuthoried = true;
            }
            finally
            {
                AuthLock.Release();
            }


        }

        protected virtual bool NeetAuth()
        {
            if (!HasDoneAuthoried)
                return true;

            if (IsDev)
                return DateTime.Now >= ExpireAt;
            return false;
        }


        protected async Task PreAction()
        {
           await  DoAuthorization();
        }

        protected async virtual Task<JObject> PostAction(AipHttpRequest aipReq)
        {
            var respStr = await SendRequet(aipReq);
//		    Console.WriteLine(respStr);
            JObject respObj;
            try
            {
                respObj = JsonConvert.DeserializeObject(respStr) as JObject;
            }
            catch (Exception e)
            {
                // 非json应该抛异常
                throw new AipException(e.Message + ": " + respStr);
            }

            if (respObj == null)
                throw new AipException("Empty response, please check input");

            // 服务失败，不抛异常
//	        if (respObj["error_code"] != null)
//	        {
//	            Console.WriteLine((string)respObj["error_msg"]);
//	            throw new AipException((int)respObj["error_code"], (string)respObj["error_msg"]);
//	        }
            return respObj;
        }

        protected virtual HttpRequestMessage GenerateWebRequest(AipHttpRequest aipRequest)
        {
            return IsDev
                ? aipRequest.GenerateDevWebRequest(Token)
                : aipRequest.GenerateCloudRequest(ApiKey, SecretKey);
        }

        protected async Task<string> SendRequet(AipHttpRequest aipRequest)
        {
            HttpResponseMessage resp = await SendRequetRaw(aipRequest);
            return await  resp.Content.ReadAsStringAsync();
          //  return Utils.StreamToString(SendRequetRaw(aipRequest).GetResponseStream(), aipRequest.ContentEncoding);
        }

        protected async Task<HttpResponseMessage> SendRequetRaw(AipHttpRequest aipRequest)
        {
            var webReq = GenerateWebRequest(aipRequest);
            Log(webReq.RequestUri.ToString());
            HttpResponseMessage resp;
            try
            {
                resp = await apiClient.SendAsync(webReq);
            }
            catch (HttpRequestException e)
            {
                // 网络请求失败应该抛异常
                throw new AipException((int) 100, e.Message);
            }

            if (resp.StatusCode != HttpStatusCode.OK)
                throw new AipException((int) resp.StatusCode, "Server response code：" + (int) resp.StatusCode);

            return resp;
        }

        protected void CheckNotNull(object obj, string name)
        {
            if (obj == null)
                throw new AipException(name + " cannot be null.");
        }

        protected string ImagesToParams(IEnumerable<byte[]> images)
        {
            return images
                .Select(Convert.ToBase64String)
                .Aggregate((a, b) => a + "," + b);
        }

        protected string StrJoin(IEnumerable<string> data, string sep = ",")
        {
            return data.Aggregate((a, b) => a + sep + b);
        }

        protected virtual void Log(string msg)
        {
            if (DebugLog)
            {
                var dateStr = DateTime.Now.ToString("[yyyyMMdd HH:mm:ss] ");
                Console.WriteLine(dateStr + msg);
            }
        }

        public class Type
        {
            public Type(string url)
            {
                Url = url;
            }

            public string Url { get; set; }
        }
    }
}