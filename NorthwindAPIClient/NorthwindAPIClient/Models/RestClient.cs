using System.Net.Http;
using System.Text;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace NorthwindAPIClient.Models
{
    public class RestClient
    {
        public string BaseUrl { get; set; }
        public string endPoint { get; set; }
        public string RestRequestAll()
        {
            string strResponseValue = string.Empty;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(BaseUrl);

            HttpResponseMessage response = client.GetAsync(endPoint).Result;

            if (response.IsSuccessStatusCode) 
            {
                strResponseValue = response.Content.ReadAsStringAsync().Result;
            }
            else
            {
                strResponseValue = "Error: " + (int)response.StatusCode + " (" + response.ReasonPhrase + ")";
            }

            return strResponseValue;
        }

        public string RestPostObj (Object obj)
        {
            Uri u = new Uri(BaseUrl + endPoint);
            string postBody = JsonConvert.SerializeObject(obj);

            HttpContent c = new StringContent(postBody, Encoding.UTF8, "application/json");

            var response = string.Empty;

            using (var client = new HttpClient())
            {
                var result = client.PostAsync(u, c).Result;
                if (result.IsSuccessStatusCode)
                {
                    response = "Success";
                }
                else
                {
                    response = "Error";
                }
            }
            return response;
        }

        public string RestPutObj(Object obj)
        { 
            Uri u = new Uri(BaseUrl + endPoint);
            string putBody = JsonConvert.SerializeObject(obj);

            HttpContent c = new StringContent(putBody, Encoding.UTF8, "application/json");

            var response = string.Empty;

            using (var client = new HttpClient()) 
            {
                var result = client.PutAsync(u, c).Result;
                if (result.IsSuccessStatusCode) 
                {
                    response = "Success";
                }
                else
                {
                    response = "Error";
                }
            }
            return response;
        }

        public string RestDeleteObj(object id) 
        {
            if (id == null)
            {
                return "Error";
            }

            Uri u = new Uri(BaseUrl + endPoint);
            var response = string.Empty;

            using (var client = new HttpClient()) 
            {
                var result = client.DeleteAsync(u).Result;
                if (result.IsSuccessStatusCode) 
                {
                    return "Success";
                }
                else
                {
                    response = "Error";
                }
            }
            return response;
        }
    }
}
