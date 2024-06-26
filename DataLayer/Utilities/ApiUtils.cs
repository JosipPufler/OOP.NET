using Newtonsoft.Json;
using System.Net;

namespace DataLayer.Utilities
{
    public static class ApiUtils
    {
        public static T FetchJson<T>(string requestPath)
        {
            var webRequest = WebRequest.Create("https://worldcup-vua.nullbit.hr/" + requestPath) as HttpWebRequest;
            if (webRequest == null)
            {
                throw new Exception("Something wen wrong when contacting the API");
            }

            webRequest.ContentType = "application/json";
            webRequest.UserAgent = "Nothing";

            using (var s = webRequest.GetResponse().GetResponseStream())
            {
                using (var sr = new StreamReader(s))
                {
                    var contributorsAsJson = sr.ReadToEnd();
                    return JsonConvert.DeserializeObject<T>(contributorsAsJson);
                }
            }
        }
    }
}
