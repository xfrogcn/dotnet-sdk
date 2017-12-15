/*
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
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace Baidu.Aip.Ocr
{
    /// <summary>
    ///     文字识别
    /// </summary>
    public class Ocr : AipServiceBase
    {
        private const string UrlGeneral = "https://aip.baidubce.com/rest/2.0/ocr/v1/general";
        private const string UrlIdCard = "https://aip.baidubce.com/rest/2.0/ocr/v1/idcard";
        private const string UrlBankCard = "https://aip.baidubce.com/rest/2.0/ocr/v1/bankcard";

        public const string UrlDrivingLicense = "https://aip.baidubce.com/rest/2.0/ocr/v1/driving_license";
        public const string UrlVehicleLicense = "https://aip.baidubce.com/rest/2.0/ocr/v1/vehicle_license";
        public const string UrlPlateLicense = "https://aip.baidubce.com/rest/2.0/ocr/v1/license_plate";
        public const string UrlReceipt = "https://aip.baidubce.com/rest/2.0/ocr/v1/receipt";
        public const string UrlBusinessLicense = "https://aip.baidubce.com/rest/2.0/ocr/v1/business_license";

        public static string UrlBasicGeneral = "https://aip.baidubce.com/rest/2.0/ocr/v1/general_basic";
        public static string UrlWebImage = "https://aip.baidubce.com/rest/2.0/ocr/v1/webimage";

        public static string UrlEnhancedGeneral =
            "https://aip.baidubce.com/rest/2.0/ocr/v1/general_enhanced";

        public static string UrlAccurate = "https://aip.baidubce.com/rest/2.0/ocr/v1/accurate_basic";
        public static string UrlAccurateWithLoc = "https://aip.baidubce.com/rest/2.0/ocr/v1/accurate";

        public Ocr(string apiKey, string secretKey) : base(apiKey, secretKey)
        {
        }

        private AipHttpRequest DefaultRequest(string uri)
        {
            return new AipHttpRequest(uri)
            {
                Method = "POST",
                BodyType = AipHttpRequest.BodyFormat.Formed
            };
        }

        /// <summary>
        ///     通用文字识别(带位置信息)
        /// </summary>
        /// <param name="image">图片字节数组</param>
        /// <param name="options">
        ///     可选参数
        ///     <list type="bullet">
        ///         <item>
        ///             <description> <c>detect_direction</c> : true/false </description>
        ///         </item>
        ///         <item>
        ///             <description>
        ///                 <c>language_type</c> : 识别语言类型，若不传则默认为CHN_ENG。 可选值包括
        ///                 <list type="table">
        ///                     <listheader>
        ///                         <term>值</term>
        ///                         <description>含义</description>
        ///                     </listheader>
        ///                     <item>
        ///                         <term>CHN_ENG</term> <description>中英混合</description>
        ///                     </item>
        ///                     <item>
        ///                         <term>ENG</term> <description>英文</description>
        ///                     </item>
        ///                     <item>
        ///                         <term>POR</term> <description>葡萄牙语</description>
        ///                     </item>
        ///                     <item>
        ///                         <term>FRE</term> <description>法语</description>
        ///                     </item>
        ///                     <item>
        ///                         <term>GER</term> <description>德语</description>
        ///                     </item>
        ///                     <item>
        ///                         <term>ITA</term> <description>意大利语</description>
        ///                     </item>
        ///                     <item>
        ///                         <term>SPA</term> <description>西班牙语</description>
        ///                     </item>
        ///                     <item>
        ///                         <term>RUS</term> <description>俄语</description>
        ///                     </item>
        ///                     <item>
        ///                         <term>JAP</term> <description>日语</description>
        ///                     </item>
        ///                     <item>
        ///                         <term>KOR</term> <description>韩语</description>
        ///                     </item>
        ///                 </list>
        ///             </description>
        ///         </item>
        ///         <item>
        ///             <description> <c>recognize_granularity</c> : big/small </description>
        ///         </item>
        ///         <item>
        ///             <description> <c>mask</c> : 表示mask区域的黑白灰度图片，白色代表选中, base64编码 </description>
        ///         </item>
        ///         <item>
        ///             <description> <c>detect_language</c> : true/false </description>
        ///         </item>
        ///     </list>
        /// </param>
        /// <returns>JObject</returns>
        public async Task<JObject> GeneralWithLocatin(byte[] image, Dictionary<string, object> options = null)
        {
            CheckNotNull(image, "image");
            await PreAction();
            var aipReq = DefaultRequest(UrlGeneral);
            aipReq.Bodys.Add("image", Convert.ToBase64String(image));
            if (options != null)
            {
                options.Remove("image");
                foreach (var pair in options)
                    aipReq.Bodys.Add(pair.Key, pair.Value);
            }
            return await PostAction(aipReq);
        }

        /// <summary>
        ///     通用文字识别(带位置信息)
        /// </summary>
        /// <param name="imageUrl">图片URL</param>
        /// <param name="options">
        ///     可选参数
        ///     <list type="bullet">
        ///         <item>
        ///             <description> <c>detect_direction</c> : true/false </description>
        ///         </item>
        ///         <item>
        ///             <description>
        ///                 <c>language_type</c> : 识别语言类型，若不传则默认为CHN_ENG。 可选值包括
        ///                 <list type="table">
        ///                     <listheader>
        ///                         <term>值</term>
        ///                         <description>含义</description>
        ///                     </listheader>
        ///                     <item>
        ///                         <term>CHN_ENG</term> <description>中英混合</description>
        ///                     </item>
        ///                     <item>
        ///                         <term>ENG</term> <description>英文</description>
        ///                     </item>
        ///                     <item>
        ///                         <term>POR</term> <description>葡萄牙语</description>
        ///                     </item>
        ///                     <item>
        ///                         <term>FRE</term> <description>法语</description>
        ///                     </item>
        ///                     <item>
        ///                         <term>GER</term> <description>德语</description>
        ///                     </item>
        ///                     <item>
        ///                         <term>ITA</term> <description>意大利语</description>
        ///                     </item>
        ///                     <item>
        ///                         <term>SPA</term> <description>西班牙语</description>
        ///                     </item>
        ///                     <item>
        ///                         <term>RUS</term> <description>俄语</description>
        ///                     </item>
        ///                     <item>
        ///                         <term>JAP</term> <description>日语</description>
        ///                     </item>
        ///                     <item>
        ///                         <term>KOR</term> <description>韩语</description>
        ///                     </item>
        ///                 </list>
        ///             </description>
        ///         </item>
        ///         <item>
        ///             <description> <c>recognize_granularity</c> : big/small </description>
        ///         </item>
        ///         <item>
        ///             <description> <c>mask</c> : 表示mask区域的黑白灰度图片，白色代表选中, base64编码 </description>
        ///         </item>
        ///         <item>
        ///             <description> <c>detect_language</c> : true/false </description>
        ///         </item>
        ///     </list>
        /// </param>
        /// <returns>JObject</returns>
        public async Task<JObject> GeneralWithLocatin(string imageUrl, Dictionary<string, object> options = null)
        {
            CheckNotNull(imageUrl, "imageUrl");
            await PreAction();
            var aipReq = DefaultRequest(UrlGeneral);
            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys.Add(pair.Key, pair.Value);
            aipReq.Bodys["url"] = imageUrl;
            return await PostAction(aipReq);
        }


        /// <summary>
        ///     通用文字识别
        /// </summary>
        /// <param name="image">图片字节数组</param>
        /// <param name="options">
        ///     可选参数
        ///     <list type="bullet">
        ///         <item>
        ///             <description> <c>detect_direction</c> : true/false </description>
        ///         </item>
        ///         <item>
        ///             <description>
        ///                 <c>language_type</c> : 识别语言类型，若不传则默认为CHN_ENG。 可选值包括
        ///                 <list type="table">
        ///                     <listheader>
        ///                         <term>值</term>
        ///                         <description>含义</description>
        ///                     </listheader>
        ///                     <item>
        ///                         <term>CHN_ENG</term> <description>中英混合</description>
        ///                     </item>
        ///                     <item>
        ///                         <term>ENG</term> <description>英文</description>
        ///                     </item>
        ///                     <item>
        ///                         <term>POR</term> <description>葡萄牙语</description>
        ///                     </item>
        ///                     <item>
        ///                         <term>FRE</term> <description>法语</description>
        ///                     </item>
        ///                     <item>
        ///                         <term>GER</term> <description>德语</description>
        ///                     </item>
        ///                     <item>
        ///                         <term>ITA</term> <description>意大利语</description>
        ///                     </item>
        ///                     <item>
        ///                         <term>SPA</term> <description>西班牙语</description>
        ///                     </item>
        ///                     <item>
        ///                         <term>RUS</term> <description>俄语</description>
        ///                     </item>
        ///                     <item>
        ///                         <term>JAP</term> <description>日语</description>
        ///                     </item>
        ///                     <item>
        ///                         <term>KOR</term> <description>韩语</description>
        ///                     </item>
        ///                 </list>
        ///             </description>
        ///         </item>
        ///         <item>
        ///             <description> <c>recognize_granularity</c> : big/small </description>
        ///         </item>
        ///         <item>
        ///             <description> <c>mask</c> : 表示mask区域的黑白灰度图片，白色代表选中, base64编码 </description>
        ///         </item>
        ///         <item>
        ///             <description> <c>detect_language</c> : true/false </description>
        ///         </item>
        ///     </list>
        /// </param>
        /// <returns>JObject</returns>
        public async Task<JObject> GeneralBasic(byte[] image, Dictionary<string, object> options = null)
        {
            CheckNotNull(image, "image");
            await PreAction();
            var aipReq = DefaultRequest(UrlBasicGeneral);
            aipReq.Bodys.Add("image", Convert.ToBase64String(image));
            if (options != null)
            {
                options.Remove("image");
                foreach (var pair in options)
                    aipReq.Bodys.Add(pair.Key, pair.Value);
            }
            return await PostAction(aipReq);
        }

        /// <summary>
        ///     通用图片识别。其余参数与GeneralBasic一致。
        /// </summary>
        /// <param name="imageUrl">图片url</param>
        /// <param name="options"></param>
        /// <returns></returns>
        public async Task<JObject> GeneralBasic(string imageUrl, Dictionary<string, object> options = null)
        {
            CheckNotNull(imageUrl, "imageUrl");
            await PreAction();
            var aipReq = DefaultRequest(UrlBasicGeneral);
            aipReq.Bodys["url"] = imageUrl;
            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys.Add(pair.Key, pair.Value);
            return await PostAction(aipReq);
        }

        public async Task<JObject> Accurate(byte[] image, Dictionary<string, object> options = null)
        {
            CheckNotNull(image, "image");
            await PreAction();
            var aipReq = DefaultRequest(UrlAccurate);
            aipReq.Bodys.Add("image", Convert.ToBase64String(image));
            if (options != null)
            {
                options.Remove("image");
                foreach (var pair in options)
                    aipReq.Bodys.Add(pair.Key, pair.Value);
            }
            return await PostAction(aipReq);
        }

        public async Task<JObject> AccurateWithLocation(byte[] image, Dictionary<string, object> options = null)
        {
            CheckNotNull(image, "image");
            await PreAction();
            var aipReq = DefaultRequest(UrlAccurateWithLoc);
            aipReq.Bodys.Add("image", Convert.ToBase64String(image));
            if (options != null)
            {
                options.Remove("image");
                foreach (var pair in options)
                    aipReq.Bodys.Add(pair.Key, pair.Value);
            }
            return await PostAction(aipReq);
        }

        /// <summary>
        ///     网图识别
        /// </summary>
        /// <param name="image">图片字节数组</param>
        /// <param name="options">
        ///     可选参数
        ///     <list type="bullet">
        ///         <item>
        ///             <description> <c>detect_direction</c> : true/false </description>
        ///         </item>
        ///         <item>
        ///             <description>
        ///                 <c>language_type</c> : 识别语言类型，若不传则默认为CHN_ENG。 可选值包括
        ///                 <list type="table">
        ///                     <listheader>
        ///                         <term>值</term>
        ///                         <description>含义</description>
        ///                     </listheader>
        ///                     <item>
        ///                         <term>CHN_ENG</term> <description>中英混合</description>
        ///                     </item>
        ///                     <item>
        ///                         <term>ENG</term> <description>英文</description>
        ///                     </item>
        ///                     <item>
        ///                         <term>POR</term> <description>葡萄牙语</description>
        ///                     </item>
        ///                     <item>
        ///                         <term>FRE</term> <description>法语</description>
        ///                     </item>
        ///                     <item>
        ///                         <term>GER</term> <description>德语</description>
        ///                     </item>
        ///                     <item>
        ///                         <term>ITA</term> <description>意大利语</description>
        ///                     </item>
        ///                     <item>
        ///                         <term>SPA</term> <description>西班牙语</description>
        ///                     </item>
        ///                     <item>
        ///                         <term>RUS</term> <description>俄语</description>
        ///                     </item>
        ///                     <item>
        ///                         <term>JAP</term> <description>日语</description>
        ///                     </item>
        ///                     <item>
        ///                         <term>KOR</term> <description>韩语</description>
        ///                     </item>
        ///                 </list>
        ///             </description>
        ///         </item>
        ///         <item>
        ///             <description> <c>recognize_granularity</c> : big/small </description>
        ///         </item>
        ///         <item>
        ///             <description> <c>mask</c> : 表示mask区域的黑白灰度图片，白色代表选中, base64编码 </description>
        ///         </item>
        ///         <item>
        ///             <description> <c>detect_language</c> : true/false </description>
        ///         </item>
        ///     </list>
        /// </param>
        /// <returns></returns>
        public async Task<JObject> WebImage(byte[] image, Dictionary<string, object> options = null)
        {
            CheckNotNull(image, "image");
            await PreAction();
            var aipReq = DefaultRequest(UrlWebImage);
            aipReq.Bodys.Add("image", Convert.ToBase64String(image));
            if (options != null)
            {
                options.Remove("image");
                foreach (var pair in options)
                    aipReq.Bodys.Add(pair.Key, pair.Value);
            }
            return await PostAction(aipReq);
        }

        /// <summary>
        ///     网图识别
        /// </summary>
        /// <param name="imageUrl">图片URL</param>
        /// <param name="options">
        ///     可选参数
        ///     <list type="bullet">
        ///         <item>
        ///             <description> <c>detect_direction</c> : true/false </description>
        ///         </item>
        ///         <item>
        ///             <description>
        ///                 <c>language_type</c> : 识别语言类型，若不传则默认为CHN_ENG。 可选值包括
        ///                 <list type="table">
        ///                     <listheader>
        ///                         <term>值</term>
        ///                         <description>含义</description>
        ///                     </listheader>
        ///                     <item>
        ///                         <term>CHN_ENG</term> <description>中英混合</description>
        ///                     </item>
        ///                     <item>
        ///                         <term>ENG</term> <description>英文</description>
        ///                     </item>
        ///                     <item>
        ///                         <term>POR</term> <description>葡萄牙语</description>
        ///                     </item>
        ///                     <item>
        ///                         <term>FRE</term> <description>法语</description>
        ///                     </item>
        ///                     <item>
        ///                         <term>GER</term> <description>德语</description>
        ///                     </item>
        ///                     <item>
        ///                         <term>ITA</term> <description>意大利语</description>
        ///                     </item>
        ///                     <item>
        ///                         <term>SPA</term> <description>西班牙语</description>
        ///                     </item>
        ///                     <item>
        ///                         <term>RUS</term> <description>俄语</description>
        ///                     </item>
        ///                     <item>
        ///                         <term>JAP</term> <description>日语</description>
        ///                     </item>
        ///                     <item>
        ///                         <term>KOR</term> <description>韩语</description>
        ///                     </item>
        ///                 </list>
        ///             </description>
        ///         </item>
        ///         <item>
        ///             <description> <c>recognize_granularity</c> : big/small </description>
        ///         </item>
        ///         <item>
        ///             <description> <c>mask</c> : 表示mask区域的黑白灰度图片，白色代表选中, base64编码 </description>
        ///         </item>
        ///         <item>
        ///             <description> <c>detect_language</c> : true/false </description>
        ///         </item>
        ///     </list>
        /// </param>
        /// <returns></returns>
        public async Task<JObject> WebImage(string imageUrl, Dictionary<string, object> options = null)
        {
            CheckNotNull(imageUrl, "imageUrl");
            await PreAction();
            var aipReq = DefaultRequest(UrlWebImage);

            if (options != null)
            {
                options.Remove("image");
                foreach (var pair in options)
                    aipReq.Bodys.Add(pair.Key, pair.Value);
            }
            aipReq.Bodys["url"] = imageUrl;
            return await PostAction(aipReq);
        }


        /// <summary>
        ///     生僻字识别
        /// </summary>
        /// <param name="image">图片字节数组</param>
        /// <param name="options">
        ///     可选参数
        ///     <list type="bullet">
        ///         <item>
        ///             <description> <c>detect_direction</c> : true/false </description>
        ///         </item>
        ///         <item>
        ///             <description>
        ///                 <c>language_type</c> : 识别语言类型，若不传则默认为CHN_ENG。 可选值包括
        ///                 <list type="table">
        ///                     <listheader>
        ///                         <term>值</term>
        ///                         <description>含义</description>
        ///                     </listheader>
        ///                     <item>
        ///                         <term>CHN_ENG</term> <description>中英混合</description>
        ///                     </item>
        ///                     <item>
        ///                         <term>ENG</term> <description>英文</description>
        ///                     </item>
        ///                     <item>
        ///                         <term>POR</term> <description>葡萄牙语</description>
        ///                     </item>
        ///                     <item>
        ///                         <term>FRE</term> <description>法语</description>
        ///                     </item>
        ///                     <item>
        ///                         <term>GER</term> <description>德语</description>
        ///                     </item>
        ///                     <item>
        ///                         <term>ITA</term> <description>意大利语</description>
        ///                     </item>
        ///                     <item>
        ///                         <term>SPA</term> <description>西班牙语</description>
        ///                     </item>
        ///                     <item>
        ///                         <term>RUS</term> <description>俄语</description>
        ///                     </item>
        ///                     <item>
        ///                         <term>JAP</term> <description>日语</description>
        ///                     </item>
        ///                     <item>
        ///                         <term>KOR</term> <description>韩语</description>
        ///                     </item>
        ///                 </list>
        ///             </description>
        ///         </item>
        ///         <item>
        ///             <description> <c>recognize_granularity</c> : big/small </description>
        ///         </item>
        ///         <item>
        ///             <description> <c>mask</c> : 表示mask区域的黑白灰度图片，白色代表选中, base64编码 </description>
        ///         </item>
        ///         <item>
        ///             <description> <c>detect_language</c> : true/false </description>
        ///         </item>
        ///     </list>
        /// </param>
        /// <returns></returns>
        public async Task<JObject> GeneralEnhanced(byte[] image, Dictionary<string, object> options = null)
        {
            CheckNotNull(image, "image");
            await PreAction();
            var aipReq = DefaultRequest(UrlEnhancedGeneral);
            aipReq.Bodys.Add("image", Convert.ToBase64String(image));
            if (options != null)
            {
                options.Remove("image");
                foreach (var pair in options)
                    aipReq.Bodys.Add(pair.Key, pair.Value);
            }
            return await PostAction(aipReq);
        }


        /// <summary>
        ///     生僻字识别
        /// </summary>
        /// <param name="imageUrl">图片URL， 带http前缀</param>
        /// <param name="options">
        ///     可选参数
        ///     <list type="bullet">
        ///         <item>
        ///             <description> <c>detect_direction</c> : true/false </description>
        ///         </item>
        ///         <item>
        ///             <description>
        ///                 <c>language_type</c> : 识别语言类型，若不传则默认为CHN_ENG。 可选值包括
        ///                 <list type="table">
        ///                     <listheader>
        ///                         <term>值</term>
        ///                         <description>含义</description>
        ///                     </listheader>
        ///                     <item>
        ///                         <term>CHN_ENG</term> <description>中英混合</description>
        ///                     </item>
        ///                     <item>
        ///                         <term>ENG</term> <description>英文</description>
        ///                     </item>
        ///                     <item>
        ///                         <term>POR</term> <description>葡萄牙语</description>
        ///                     </item>
        ///                     <item>
        ///                         <term>FRE</term> <description>法语</description>
        ///                     </item>
        ///                     <item>
        ///                         <term>GER</term> <description>德语</description>
        ///                     </item>
        ///                     <item>
        ///                         <term>ITA</term> <description>意大利语</description>
        ///                     </item>
        ///                     <item>
        ///                         <term>SPA</term> <description>西班牙语</description>
        ///                     </item>
        ///                     <item>
        ///                         <term>RUS</term> <description>俄语</description>
        ///                     </item>
        ///                     <item>
        ///                         <term>JAP</term> <description>日语</description>
        ///                     </item>
        ///                     <item>
        ///                         <term>KOR</term> <description>韩语</description>
        ///                     </item>
        ///                 </list>
        ///             </description>
        ///         </item>
        ///         <item>
        ///             <description> <c>recognize_granularity</c> : big/small </description>
        ///         </item>
        ///         <item>
        ///             <description> <c>mask</c> : 表示mask区域的黑白灰度图片，白色代表选中, base64编码 </description>
        ///         </item>
        ///         <item>
        ///             <description> <c>detect_language</c> : true/false </description>
        ///         </item>
        ///     </list>
        /// </param>
        /// <returns></returns>
        public async Task<JObject> GeneralEnhanced(string imageUrl, Dictionary<string, object> options = null)
        {
            CheckNotNull(imageUrl, "imageUrl");
            await PreAction();
            var aipReq = DefaultRequest(UrlEnhancedGeneral);
            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys.Add(pair.Key, pair.Value);
            aipReq.Bodys["url"] = imageUrl;
            return await PostAction(aipReq);
        }


        /// <summary>
        ///     身份证正面识别
        /// </summary>
        /// <param name="image"></param>
        /// <param name="options">可选参数</param>
        /// <returns></returns>
        public async Task<JObject> IdCardFront(byte[] image, Dictionary<string, object> options = null)
        {
            CheckNotNull(image, "image");
            await PreAction();
            var aipReq = DefaultRequest(UrlIdCard);
            if (options != null)
            {
                options.Remove("image");
                foreach (var pair in options)
                    aipReq.Bodys.Add(pair.Key, pair.Value);
            }
            aipReq.Bodys.Add("image", Convert.ToBase64String(image));
            aipReq.Bodys.Add("id_card_side", "front");
            return await PostAction(aipReq);
        }

        /// <summary>
        ///     身份证反面识别
        /// </summary>
        /// <param name="image"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public async Task<JObject> IdCardBack(byte[] image, Dictionary<string, object> options = null)
        {
            CheckNotNull(image, "image");
            await PreAction();
            var aipReq = DefaultRequest(UrlIdCard);
            if (options != null)
            {
                options.Remove("image");
                foreach (var pair in options)
                    aipReq.Bodys.Add(pair.Key, pair.Value);
            }
            aipReq.Bodys.Add("image", Convert.ToBase64String(image));
            aipReq.Bodys.Add("id_card_side", "back");
            return await PostAction(aipReq);
        }

        /// <summary>
        ///     银行卡识别
        /// </summary>
        /// <param name="image"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public async Task<JObject> BankCard(byte[] image, Dictionary<string, object> options = null)
        {
            CheckNotNull(image, "image");
            await PreAction();
            var aipReq = DefaultRequest(UrlBankCard);
            if (options != null)
            {
                options.Remove("image");
                foreach (var pair in options)
                    aipReq.Bodys.Add(pair.Key, pair.Value);
            }
            aipReq.Bodys.Add("image", Convert.ToBase64String(image));
            return await PostAction(aipReq);
        }

        /// <summary>
        ///     驾驶证识别
        /// </summary>
        /// <param name="image"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public async Task<JObject> DrivingLicense(byte[] image, Dictionary<string, object> options = null)
        {
            CheckNotNull(image, "image");
            await PreAction();
            var aipReq = DefaultRequest(UrlDrivingLicense);
            if (options != null)
            {
                options.Remove("image");
                foreach (var pair in options)
                    aipReq.Bodys.Add(pair.Key, pair.Value);
            }
            aipReq.Bodys.Add("image", Convert.ToBase64String(image));
            return await PostAction(aipReq);
        }

        /// <summary>
        ///     行驶证识别
        /// </summary>
        /// <param name="image"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public async Task<JObject> VehicleLicense(byte[] image, Dictionary<string, object> options = null)
        {
            CheckNotNull(image, "image");
            await PreAction();
            var aipReq = DefaultRequest(UrlVehicleLicense);
            if (options != null)
            {
                options.Remove("image");
                foreach (var pair in options)
                    aipReq.Bodys.Add(pair.Key, pair.Value);
            }
            aipReq.Bodys.Add("image", Convert.ToBase64String(image));
            return await PostAction(aipReq);
        }

        /// <summary>
        ///     车牌识别
        /// </summary>
        /// <param name="image"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public async Task<JObject> PlateLicense(byte[] image, Dictionary<string, object> options = null)
        {
            CheckNotNull(image, "image");
            await PreAction();
            var aipReq = DefaultRequest(UrlPlateLicense);
            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys.Add(pair.Key, pair.Value);
            aipReq.Bodys.Add("image", Convert.ToBase64String(image));
            return await PostAction(aipReq);
        }

        /// <summary>
        ///     票据识别
        /// </summary>
        /// <param name="image"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public async Task<JObject> Receipt(byte[] image, Dictionary<string, object> options = null)
        {
            CheckNotNull(image, "image");
            await PreAction();
            var aipReq = DefaultRequest(UrlReceipt);
            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys.Add(pair.Key, pair.Value);
            aipReq.Bodys.Add("image", Convert.ToBase64String(image));
            return await PostAction(aipReq);
        }

        /// <summary>
        ///     营业执照识别
        /// </summary>
        /// <param name="image"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public async Task<JObject> BusinessLicense(byte[] image, Dictionary<string, object> options = null)
        {
            CheckNotNull(image, "image");
            await PreAction();
            var aipReq = DefaultRequest(UrlBusinessLicense);
            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys.Add(pair.Key, pair.Value);
            aipReq.Bodys.Add("image", Convert.ToBase64String(image));
            return await PostAction(aipReq);
        }

        public async Task<JObject> Template(byte[] image, Dictionary<string, object> options = null)
        {
            CheckNotNull(image, "image");
            await PreAction();
            var aipReq = DefaultRequest(UrlBusinessLicense);
            if (options != null)
                foreach (var pair in options)
                    aipReq.Bodys[pair.Key] = pair.Value;
            aipReq.Bodys["image"] = Convert.ToBase64String(image);
            return await PostAction(aipReq);
        }
    }
}