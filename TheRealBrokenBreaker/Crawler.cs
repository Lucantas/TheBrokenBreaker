using System;
using System.Collections.Generic;
using System.Text;
using HtmlAgilityPack;


namespace TheRealBrokenBreaker
{
    class Crawler
    {
        public string Uri { get; set; }
        public HtmlDocument Dom { get; }
        public HtmlWeb Web = new HtmlWeb();
        private List<string> Links = new List<string>();
        public List<string> FindLinks()
        {
            ScanAnchors();
            ScanSrcs();
            ScanButtons();
            ScanActions();
            return Links;
        }
        public Crawler(string uri)
        {
            Dom = Web.Load(uri);
        }
       
        public void ScanAnchors()
        {
            string href;
            var anchors = Dom.DocumentNode.SelectNodes("//a");
            if (anchors != null)
            {
                foreach (var anchor in anchors)
                {
                    if (anchor.Attributes["href"] != null)
                    {
                        href = anchor.Attributes["href"].Value;
                        if (href == "")
                            Console.WriteLine("href empty");
                        else if (href[0] == '/')
                        {
                            Links.Add(Uri + href.Split('/')[1]);
                        }
                        else
                        {
                            Links.Add(href);
                        }
                    }
                }
            }
        }
        public void ScanSrcs()
        {
            string src;
            var images = Dom.DocumentNode.SelectNodes("//img");
            if (images != null)
            {
                foreach (var img in images)
                {
                    if (img.Attributes["src"] != null)
                    {
                        src = img.Attributes["src"].Value;
                        if (src == "")
                            Console.WriteLine("src empty");
                        else if (src[0] == '/')
                        {
                            Links.Add(Uri + src.Split('/')[1]);
                        }
                        else
                        {
                            Links.Add(src);
                        }
                    }
                }
            }
        }
        public void ScanButtons()
        {

        }
        public void ScanActions()
        {

        }
    }
}
