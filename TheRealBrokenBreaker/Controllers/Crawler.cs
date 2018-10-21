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
            ScanActions();
            return Links;
        }
        public Crawler(string uri)
        {
            Dom = Web.Load(uri);
        }
        // TODO : Improve the test over the links to correctly add all the
        // hrefs to the list based on their domain specification and target
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

                        else if (href[0] == '#')
                        {
                            Console.WriteLine("local anchor");
                        }
                        else if (href[0] == '/')
                        {
                            Links.Add(Uri + href.Split('/')[1]);
                        }
                        else if (href[0] == ' ')
                        {
                            Links.Add(Uri + href);
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
        public void ScanActions()
        {
            string action;
            var forms = Dom.DocumentNode.SelectNodes("//form");
            if (forms != null)
            {
                foreach (var form in forms)
                {
                    if (form.Attributes["src"] != null)
                    {
                        action = form.Attributes["action"].Value;
                        if (action == "")
                            Console.WriteLine("action empty");

                        else if (action[0] == '/')
                        {
                            Links.Add(Uri + action.Split('/')[1]);
                        }
                        else
                        {
                            Links.Add(action);
                        }
                    }
                }
            }
        }
    }
}
