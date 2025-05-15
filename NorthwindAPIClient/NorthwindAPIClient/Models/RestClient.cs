using System.Net.Http;

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
    }
}
