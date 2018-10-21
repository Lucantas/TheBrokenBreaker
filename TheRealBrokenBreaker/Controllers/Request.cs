using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace TheRealBrokenBreaker.Controllers
{
    class Request
    {
        public static string[] TestURI(string[] URIs)
        {
            var badLinks = new List<string>();
            HttpResponseMessage response;
            foreach (var uri in URIs)
            {
                using (var client = new HttpClient())
                {
                    if (uri.ToUpperInvariant().Contains("HTTP"))
                    {
                        Console.WriteLine($"Sending GET request to {uri} ...");
                        try
                        {
                            response = client.GetAsync(uri).Result;
                            if (response.StatusCode.ToString() == "NotFound")
                            {
                                badLinks.Add(uri);
                            }
                        }
                        catch(Exception e)
                        {
                            Console.WriteLine("Erros when loading the page");
                            Console.WriteLine(e);
                        }
                    }
                }
            }
            return badLinks.ToArray();
        }
    }
}

