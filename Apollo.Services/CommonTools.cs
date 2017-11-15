using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;


namespace Apollo.Services
{
    public class CommonTools
    {
        public static void sendPushNotif(string message, string tagKey ,string tagValue)
        {
            var request = WebRequest.Create("https://onesignal.com/api/v1/notifications") as HttpWebRequest;

            request.KeepAlive = true;
            request.Method = "POST";
            request.ContentType = "application/json; charset=utf-8";

            request.Headers.Add("authorization", "Basic YTcwMzk5MmUtZTY0YS00ZTIyLWE2ZDItZDIzZjgxOTAyMDdk");

            var serializer = new JavaScriptSerializer();
            var obj = new
            {
                app_id = "4877ccbf-8df0-4fc6-bf12-752b8adaf227",
                contents = new { en = message },
                included_segments = new string[] { "All" },
                filters = new object[] { new { field = "tag", key = tagKey, value = tagValue } },
            headings = new { en = "New Customer message"}
            };
            var param = serializer.Serialize(obj);
            byte[] byteArray = Encoding.UTF8.GetBytes(param);

            string responseContent = null;

            try
            {
                using (var writer = request.GetRequestStream())
                {
                    writer.Write(byteArray, 0, byteArray.Length);
                }

                using (var response = request.GetResponse() as HttpWebResponse)
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        responseContent = reader.ReadToEnd();
                    }
                }
            }
            catch (WebException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(new StreamReader(ex.Response.GetResponseStream()).ReadToEnd());
            }
            System.Diagnostics.Debug.WriteLine(responseContent);
        }
    }
}
