using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Xml;
using Newtonsoft.Json;
using RestSharp;

namespace AlumniBook.BLL.SensitiveService
{
    /// <summary>
    /// 敏感词检测
    /// </summary>
    public class SensitiveServiceAplication: ISensitiveServiceAplication
    {
        
        public bool Check(string Msg)
        {
            var TokenAll = AccessToken.getAccessToken();
            JavaScriptSerializer jsonSerialize = new JavaScriptSerializer();
            var acessToken = (jsonSerialize.Deserialize<TokenClass>(TokenAll)).access_token;


            var url = "https://aip.baidubce.com" ;
            //HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            //request.Method = "Post";
            //request.ContentType = "application/x-www-form-urlencoded";
            //byte[] data = Encoding.UTF8.GetBytes("content=" + Msg);
            //using (Stream stream = request.GetRequestStream())
            //{
            //    stream.Write(data, 0, data.Length);
            //}
            //var response = request.GetResponse() as HttpWebResponse;

            //Stream sr = response.GetResponseStream();

            //XmlTextReader Reader = new XmlTextReader(sr);
            //Reader.MoveToContent();
            //string strValue = Reader.ReadInnerXml();
            //Reader.Close();
            //sr.Close();
            var client = new RestClient(url);
            var request = new RestRequest("/rest/2.0/antispam/v2/spam?access_token="+ acessToken + "&content=" + Msg, Method.POST);
            request.AddParameter("content",Msg);
            request.AddHeader("ContentType", "application/x-www-form-urlencoded");
            IRestResponse response = client.Execute(request);
            var content = response.Content;
            var spam = (jsonSerialize.Deserialize<SensitiveResult>(content)).result.spam;
            return spam == 0 ? true : false;
        }
    }

    public static class AccessToken
    {
        // 调用getAccessToken()获取的 access_token建议根据expires_in 时间 设置缓存
        // 返回token示例
        public static String TOKEN = "24.adda70c11b9786206253ddb70affdc46.2592000.1493524354.282335-1234567";

        // 百度云中开通对应服务应用的 API Key 建议开通应用的时候多选服务
        private static String clientId = "LsDncG9QnC7F2KwkXwYYnULG";
        // 百度云中开通对应服务应用的 Secret Key
        private static String clientSecret = "Cxl6QbPLad02DDG8D9Hbhj1fGPP3TjNH";

        public static String getAccessToken()
        {
            String authHost = "https://aip.baidubce.com/oauth/2.0/token";
            HttpClient client = new HttpClient();
            List<KeyValuePair<String, String>> paraList = new List<KeyValuePair<string, string>>();
            paraList.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));
            paraList.Add(new KeyValuePair<string, string>("client_id", clientId));
            paraList.Add(new KeyValuePair<string, string>("client_secret", clientSecret));

            HttpResponseMessage response = client.PostAsync(authHost, new FormUrlEncodedContent(paraList)).Result;
            String result = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine(result);
            return result;
        }
    }
}