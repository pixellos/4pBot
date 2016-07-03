using System.Net;
using System.Web;
using HtmlAgilityPack;

namespace pBot.Model.Functions.Checkers.SOChecker
{
    public class DownloaderSo
    {
        public HtmlDocument Download(string unescapedTag)
        {
            var html = new HtmlDocument();
            var escapedTag = HttpUtility.HtmlEncode(unescapedTag);

            html.LoadHtml(
                new WebClient().DownloadString($"http://stackoverflow.com/questions/tagged/{escapedTag}"));
            return html;
        }
    }
}