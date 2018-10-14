using System;
using System.Collections.Generic;
using System.Linq;

namespace TheRealBrokenBreaker
{
    class BrokenBreaker
    {
        public static void GetBrokenLinks(string uri)
        {
            
            var links = new List<string>(); 
       
            var crawler = new Crawler(uri);
            links = crawler.FindLinks();
            

            Console.WriteLine($"Found {links.Count()} links on the page {uri}.");
            Console.WriteLine("Starting to test link...");
            TestLinks(links.ToArray());     
        }
        public static void TestLinks(string[] links)
        {
            var badLinks = Request.TestURI(links);
            foreach(var link in badLinks)
            {
               Console.WriteLine($"{link} is not responding well");
            }
            Console.WriteLine(badLinks.Length > 0 ? $"Done searching, found {badLinks.Length}" : "No broken links found");
        }
    }
}
