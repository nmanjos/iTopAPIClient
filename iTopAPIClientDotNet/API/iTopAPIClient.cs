using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace iTopAPIClientDotNet.API
{
    public class iTopAPIClient
    {
       

        public static async Task<HttpResponseMessage> iTopAPIWorker(iTopAPIMessage Message)
        {

            HttpResponseMessage res = null;
            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage(HttpMethod.Post, Message.EndPoint))
            {
                string json = JsonConvert.SerializeObject(Message.Create);
                using (HttpContent stringContent = new StringContent(json, Encoding.UTF8, "application/json"))
                {
                    request.Content = stringContent;
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Message.Credentials);
                    stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    using (var response = await client
                        .SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
                        .ConfigureAwait(false))
                    {
                        response.EnsureSuccessStatusCode();
                        res = response;
                    }
                }
            }


            return res;
        }




        public static async Task<ResponseMessage> iTopAPICall(string operation, RequestMessage message)
        {

            iTopAPIMessage msg = new iTopAPIMessage();
            msg.EndPoint=ConfigurationManager.AppSettings["iTopAPI.Endpoint"];
            var creds = new Credentials { Username = ConfigurationManager.AppSettings["iTopAPI.Username"], Password = ConfigurationManager.AppSettings["iTopAPI.Password"] };
            msg.Credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes(creds.Username  ));
            var response = await iTopAPIWorker(msg);
            return JsonConvert.DeserializeObject<ResponseMessage>(response.Content.ReadAsStringAsync().Result);
        }
    }
}