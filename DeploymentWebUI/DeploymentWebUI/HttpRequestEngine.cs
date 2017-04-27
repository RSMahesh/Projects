using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace DeploymentWebUI
{
    public static class HttpRequestEngine
    {
        private static readonly string HostUrl = "https://api.github.com/";

        public static async Task<HttpResponseMessage> ExecuteGetAsync(string relativeUri)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(HostUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Add("Authorization", "token e7aba87c95671df23f6c5b3dde789efe16d85a49");
               // client.DefaultRequestHeaders.Authorization = DetermineToken();

                return await client.GetAsync(relativeUri);
            }
        }

        public static AuthenticationHeaderValue DetermineToken()
        {
            return new AuthenticationHeaderValue("token", "e7aba87c95671df23f6c5b3dde789efe16d85a49");
        }
    }
}