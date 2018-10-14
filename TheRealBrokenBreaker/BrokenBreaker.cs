using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using HtmlAgilityPack;
using System.Threading.Tasks;

namespace TheRealBrokenBreaker
{
    class BrokenBreaker
    {
        public static void GetBrokenLinks(string uri)
        {
            HtmlWeb web;
            HtmlDocument dom;
            web = new HtmlWeb();
            var links = new List<string>(); 
       
            string href;
            dom = web.Load(uri);
            var anchors = dom.DocumentNode.SelectNodes("//a");   
            if (anchors == null)
                Console.WriteLine("no anchors on the document");
            else
            foreach(var anchor in anchors)
            {
                if (anchor.Attributes["href"] != null)
                {
                    href = anchor.Attributes["href"].Value;
                    if (href == "")
                        Console.WriteLine("href empty");
                    else if (href[0] == '/')
                    {
                        links.Add(uri + href.Split('/')[1]);
                    }
                    else{
                        links.Add(href);
                    }
                }
            }
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
