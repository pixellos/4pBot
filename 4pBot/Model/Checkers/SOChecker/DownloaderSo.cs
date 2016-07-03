using System.Net;
using System.Web;
using HtmlAgilityPack;

namespace pBot.Model.Functions.Checkers.SOChecker
{
    public class DownloaderSo
    {

        public virtual string GetWebString(string escapedTag)
        {
            return new WebClient().DownloadString($"http://stackoverflow.com/questions/tagged/{escapedTag}");
        }
        public HtmlDocument Download(string unescapedTag)
        {
            var html = new HtmlDocument();
            var escapedTag = HttpUtility.HtmlEncode(unescapedTag);

            html.LoadHtml(GetWebString(escapedTag));
            return html;
        }
    }
}