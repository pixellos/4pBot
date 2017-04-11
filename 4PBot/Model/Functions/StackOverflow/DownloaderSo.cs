using System.Net;
using System.Web;
using HtmlAgilityPack;

namespace _4PBot.Model.Functions.StackOverflow
{
    public class Downloader
    {
        public virtual string GetWebString(string escapedTag)
        {
            return new WebClient().DownloadString($"http://stackoverflow.com/questions/tagged/{escapedTag}");
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