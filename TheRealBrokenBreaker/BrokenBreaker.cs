using System;
using System.IO;
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
            var badLinks = TestLinks(links.ToArray());
            WriteBrokenLinks(uri, badLinks);
        }
        public static string[] TestLinks(string[] links)
        {
            var badLinks = Request.TestURI(links);
            Console.WriteLine(badLinks.Length > 0 ? $"Done searching, found {badLinks.Length} broken links" : "No broken links found");
            return badLinks;
        }
        public static void WriteBrokenLinks(string uri, string[] badLinks)
        {
            // search for the pre-determined file to write the broken links 
            using (StreamWriter writer = new StreamWriter(@"C:\Users\lucan\Documentos\workstation\Csharp\brokenLinks.txt"))
            {
                writer.WriteLine($"bad links found on {uri}");
                foreach(var link in badLinks)
                {
                    // write the link using write line to break line after writing
                    writer.WriteLine(link);
                    // look for errors, and show the output to the user
                }
            }
        }
    }
}
