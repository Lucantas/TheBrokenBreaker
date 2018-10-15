using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace TheRealBrokenBreaker
{
    class Request
    {
        public static string[] TestURI(string[] URIs)
        {
            var badLinks = new List<string>();
            foreach(var uri in URIs)
            {
                using (var client = new HttpClient())
                {
                    if (uri.ToUpperInvariant().Contains("HTTP"))
                    {
                        System.Console.WriteLine($"Sending GET request to {uri} ...");
                        var response = client.GetAsync(uri).Result;
                        if (response.StatusCode.ToString() == "NotFound")
                        {
                            badLinks.Add(uri);
                        }
                    }
                }
            }
            return badLinks.ToArray();
        }
        public async static Task<string[]> TestURI2(string[] URIs) {
            var client = new HttpClient();
            var badLinks = new List<string>();
            foreach(var uri in URIs)
            {
                Task<HttpResponseMessage> request;
                request = client.GetAsync(uri);
                var response = await request;
                var statusCode = response.StatusCode;
                if (statusCode.ToString() != "OK")
                {
                    badLinks.Add(uri);
                }
            }
            return badLinks.ToArray();
            //Console.WriteLine($"URI: {uri}, Status: {statusCode.ToString()}");
        }
    }
}

