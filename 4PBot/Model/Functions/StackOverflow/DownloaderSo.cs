using System.Net;
using System.Web;
using HtmlAgilityPack;
using System;

namespace _4PBot.Model.Functions.StackOverflow
{
    public class Downloader
    {
        public virtual string GetWebString(string escapedTag)
        {
            try
            {
                return new WebClient().DownloadString($"http://stackoverflow.com/questions/tagged/{escapedTag}");
            }
            catch (WebException we)
            {
                Console.WriteLine("User typed some unsupported string. " + escapedTag + we.ToString());
                return string.Empty;
            }
        }

        public HtmlDocument Download(string unescapedTag)
        {
            var html = new HtmlDocument();
            var escapedTag = HttpUtility.HtmlEncode(unescapedTag);
            html.LoadHtml(this.GetWebString(escapedTag));
            return html;
        }
    }
}