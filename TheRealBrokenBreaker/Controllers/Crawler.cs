using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
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
            if (Dom == null)
            {
                Console.WriteLine("Could not load web page");
                return null;
            }
            ScanAnchorTags();
            ScanLinkTags();
            ScanImageTags();
            ScanFormAction();
            return Links;
        }

        public Crawler(string uri)
        {
            if (!IsValidLink(uri))
                Dom = null;
            else
                Dom = Web.Load(uri);
        }
     
        public void ScanAnchorTags()
        {
            var anchors = Dom.DocumentNode.SelectNodes("//a");
            if (anchors != null)
            {
                foreach (var anchor in anchors)
                {
                    TestHref(anchor);
                }
            }
           
        }

        public void ScanLinkTags()
        {
            var links = Dom.DocumentNode.SelectNodes("//link");
            if (links != null)
            {
                foreach (var link in links)
                {
                    TestHref(link);
                }
            }

        }

        public void ScanImageTags()
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

        public void ScanFormAction()
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
        // TODO : Improve the test over the links to correctly add all the
        // hrefs to the list based on their domain specification and target
        void TestHref(HtmlNode element)
        {
            string href;

            if (element.Attributes["href"] != null)
            {
                href = element.Attributes["href"].Value;
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

        public static bool IsValidLink(string link)
        {
            // (https?:\/\/(?:www\.|(?!www))[a-zA-Z0-9][a-zA-Z0-9-]+[a-zA-Z0-9]\.[^\s]{2,}|www\.[a-zA-Z0-9][a-zA-Z0-9-]+[a-zA-Z0-9]\.[^\s]{2,}|https?:\/\/(?:www\.|(?!www))[a-zA-Z0-9]\.[^\s]{2,}|www\.[a-zA-Z0-9]\.[^\s]{2,})
            var regex = new Regex(@"(https?:\/\/(?:www\.|(?!www))[a-zA-Z0-9][a-zA-Z0-9-]+[a-zA-Z0-9]\.[^\s]{2,}|www\.[a-zA-Z0-9][a-zA-Z0-9-]+[a-zA-Z0-9]\.[^\s]{2,}|https?:\/\/(?:www\.|(?!www))[a-zA-Z0-9]\.[^\s]{2,}|www\.[a-zA-Z0-9]\.[^\s]{2,})",
                RegexOptions.Compiled);

            if (!regex.Match(link).Success)
                return false;

            return true;
        }
        public static bool IsValidLink(string link, bool isLargeText)
        {
            // (https?:\/\/(?:www\.|(?!www))[a-zA-Z0-9][a-zA-Z0-9-]+[a-zA-Z0-9]\.[^\s]{2,}|www\.[a-zA-Z0-9][a-zA-Z0-9-]+[a-zA-Z0-9]\.[^\s]{2,}|https?:\/\/(?:www\.|(?!www))[a-zA-Z0-9]\.[^\s]{2,}|www\.[a-zA-Z0-9]\.[^\s]{2,})
            var regex = new Regex(@"(https?:\/\/(?:www\.|(?!www))[a-zA-Z0-9][a-zA-Z0-9-]+[a-zA-Z0-9]\.[^\s]{2,}|www\.[a-zA-Z0-9][a-zA-Z0-9-]+[a-zA-Z0-9]\.[^\s]{2,}|https?:\/\/(?:www\.|(?!www))[a-zA-Z0-9]\.[^\s]{2,}|www\.[a-zA-Z0-9]\.[^\s]{2,})",
                RegexOptions.Compiled);

            if (!isLargeText)
            {
                if (!regex.Match(link).Success)
                    return false;
            }
            return true;
        }
    }
}
