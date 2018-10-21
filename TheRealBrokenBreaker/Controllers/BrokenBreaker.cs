using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using TheRealBrokenBreaker.Models;

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
            if (badLinks.Length > 0)
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
            // note the 'append:true' option set on the StreamWriter constructor, it keeps the old links on the file
            // while inserting new lines
            using (StreamWriter writer = new StreamWriter(AppConfiguration.BrokenLinksFile, append: true))
            {
                // The header of that input, it is the current date followed by the uri chosen by the user
                writer.WriteLine($"{DateTime.Now} | Bad links found on {uri}");
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
