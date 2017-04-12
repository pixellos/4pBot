using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using _4PBot.Model.Constants;
using _4PBot.Model.Helper;

namespace _4PBot.Model.Functions._4Programmers
{
    public class Checker
    {
        public const string NoMatchingForumMeessage = "There is no matching forum id! Check your spelling";
        private readonly Dictionary<string, string> NameToID = new Dictionary<string, string>
        {
            {"Delphi i Pascal", "1"},
            {"C/C++", "2"},
            {"Webmastering", "3"},
            {"Inne języki programowania", "4"},
            {"Archiwum", "5"},
            {"Algorytmy i struktury danych", "6"},
            {"Off-Topic", "7"},
            {"Hardware/Software", "8"},
            {"Społeczność", "10"},
            {"Coyote", "11"},
            {"RoadRunner", "13"},
            {"Newbie", "15"},
            {"Test", "22"},
            {"Oferty pracy", "23"},
            {"C# i .NET", "24"},
            {"Java", "25"},
            {"Inżynieria oprogramowania", "26"},
            {"Bazy danych", "27"},
            {"Perełki", "28"},
            {"Yosemite", "29"},
            {"Flame", "33"},
            {"PHP", "35"},
            {"Oceny i recenzje", "36"},
            {"Ogłoszenia drobne", "39"},
            {"Edukacja", "40"},
            {"Kariera", "41"},
            {"Nietuzinkowe tematy", "42"},
            {"Magazyn Programista", "45"},
            {"Szkolenia i konferencje", "46"},
            {"Projekty Forumowe", "50"},
            {"Python", "51"}
        };

        private readonly Dictionary<string, string> IDToForumString = new Dictionary<string, string>
        {
            {"1", "Delphi_Pascal"},
            {"2", "C_i_C++"},
            {"3", "Webmastering"},
            {"4", "Inne"},
            {"5", "Archiwum"},
            {"6", "Algorytmy"},
            {"7", "Off-Topic"},
            {"8", "Hardware_Software"},
            {"10", "Spolecznosc"},
            {"11", "Coyote"},
            {"13", "RoadRunner"},
            {"15", "Newbie"},
            {"22", "Coyote/Test"},
            {"23", "Praca"},
            {"24", "C_i_.NET"},
            {"25", "Java"},
            {"26", "Inzynieria_oprogramowania"},
            {"27", "Bazy_danych"},
            {"28", "Spolecznosc/Perelki"},
            {"29", "Yosemite"},
            {"33", "Flame"},
            {"35", "PHP"},
            {"36", "Off-Topic/Oceny_i_recenzje"},
            {"39", "Ogłoszenia_drobne"},
            {"40", "Edukacja"},
            {"41", "Kariera"},
            {"42", "Nietuzinkowe_tematy"},
            {"45", "Magazyn_Programista"},
            {"46", "Szkolenia_i_konferencje"},
            {"50", "Spolecznosc/Projekty"},
            {"51", "Python"}
        };

        private Downloader Downloader { get; set; }
        public Checker(Downloader downloader)
        {
            this.Downloader = downloader;
        }

        public string GetForumId(string forumName)
        {
            return this.NameToID.First(x => x.Key.ToLower().Contains(forumName.ToLower())).Value;
        }

        public string GetForumUrl(string id)
        {
            return this.IDToForumString.Single(x => x.Key.Equals(id)).Value;
        }

        private string NormalizeSubject(string subject)
        {
            return subject.ToLower()
                .Replace(' ', '_')
                .Replace('(', '_')
                .Replace(')', '_')
                .Replace('[', '_')
                .Replace("#", "")
                .Replace('@', '_')
                .Replace('!', '_')
                .Replace(',', '_')
                .Replace(']', '_')
                .Replace('?', '_')
                .Replace('ą', 'a')
                .Replace('ę', 'e')
                .Replace('ó', 'o')
                .Replace('ł', 'l')
                .Replace('ż', 'z')
                .Replace('ź', 'z')
                .Replace(".", "")
                .Replace("/", "")
                .Replace(",", "")
                .Replace('&', '_')
                .Replace('^', '_')
                .Replace(':', '_')
                .Replace("__", "_")
                .TrimEnd('_');
        }

        public string GetLastMessagesByTag(string requestedTag, bool shouldGiveMessageWhenThereIsNoMatch = true)
        {
            try
            {
                var obj = this.Downloader.DownloadData("", ApiKey.ApiKeyForNewPosts);
                var element = obj.Property1.First(x => x.tags.Contains(requestedTag) || Regex.Unescape(x.forum).Contains(requestedTag));
                var forumId = this.GetForumId(Regex.Unescape(element.forum));
                return $"{element.forum}: {Regex.Unescape(element.subject)}, " +
                       // $"przez {element.first_post.user.name}: " +
                       UrlShortener.GetShortUrl(this.make4pUrlFromJson(forumId, element.topic_id.ToString(), element.subject));
            }
            catch (Exception)
            {
                if (shouldGiveMessageWhenThereIsNoMatch)
                {
                    return $"I cant find any posts with tag {requestedTag}";
                }
                else
                {
                    return "";
                }
            }
        }

        private string make4pUrlFromJson(string jsonForumId, string jsonTopicId, string jsonSubject) =>
            $"http://forum.4programmers.net/{this.GetForumUrl(jsonForumId)}/{jsonTopicId}-{this.NormalizeSubject(jsonSubject)}";

        public string GetLastPostAtCategory(string categoryName)
        {
            try
            {
                var jsonForumId = this.GetForumId(categoryName);
                var obj = this.Downloader.DownloadData(jsonForumId, ApiKey.ApiKeyWithForumIdQuotation);
                var element = obj.Property1.First();
                return $"{element.forum}: {Regex.Unescape(element.subject)}, " +
                       //$"przez {element.first_post.user.name}: " +
                       UrlShortener.GetShortUrl(this.make4pUrlFromJson(jsonForumId, element.topic_id.ToString(), element.subject));
            }
            catch (InvalidOperationException)
            {
                return Checker.NoMatchingForumMeessage;
            }
        }
    }
}