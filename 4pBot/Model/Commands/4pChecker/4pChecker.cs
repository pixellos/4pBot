using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using pBot.Model.Core;

namespace pBot.Model.Commands._4pChecker
{
	class _4pChecker
	{
		private static readonly string _4pAdress = ApiKey.Key;
		private static readonly Dictionary<string, string> NameToID = new Dictionary<string, string>()
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
			{"Python", "51"},
		};

		private static readonly Dictionary<string, string> IDToForumString = new Dictionary<string, string>()
		{
			{"1","Delphi_Pascal"},
			{"2","C_i_C++"},
			{"3","Webmastering"},
			{"4","Inne"},
			{"5","Archiwum"},
			{"6","Algorytmy"},
			{"7","Off-Topic"},
			{"8","Hardware_Software"},
			{"10","Spolecznosc"},
			{"11","Coyote"},
			{"13","RoadRunner"},
			{"15","Newbie"},
			{"22","Test"},
			{"23","Praca"},
			{"24","C_i_.NET"},
			{"25","Java"},
			{"26","Inzynieria_oprogramowania"},
			{"27","Bazy_danych"},
			{"28","Perelki"},
			{"29","Yosemite"},
			{"33","Flame"},
			{"35","PHP"},
			{"36","Oceny_i_recenzje"},
			{"39","Ogłoszenia_drobne"},
			{"40","Edukacja"},
			{"41","Kariera"},
			{"42","Nietuzinkowe_tematy"},
			{"45","Magazyn_Programista"},
			{"46","Szkolenia_i_konferencje"},
			{"50","Projekty"},
			{"51","Python"},
		};

        public static string GetForumId(string str)
		{
			return NameToID.First(x => x.Key.ToLower().Contains(str.ToLower())).Value;
		}

		public static string GetForumUrl(string id)
		{
			return IDToForumString.Single(x => x.Key.Equals(id)).Value;
		}

		private static string MagicWithJson(string json)
		{
			return $"{"{ \"Main\":"} {json} {"}"}";
		}

		private static string MagicWith4PSubject(string subject)
		{
			return subject
                .Replace(' ', '_')
                .Replace('(', '_')
                .Replace(')', '_')
                .Replace('[', '_')
                .Replace('#', '_')
                .Replace('@', '_')
                .Replace('!', '_')
                .Replace(']', '_')
                .Replace('?','_')
                .Replace('!','_')
                .Replace(".","")
                .Replace('&','_')
                .Replace('^','_')
                .Replace(':','_')
                .Replace("__","_")
                .TrimEnd('_');
		}

		public static string GetNewestPost(Command command)
		{
			var forumId = GetForumId(command.Parameters[1]);
			var json = new WebClient().DownloadString(_4pAdress + forumId);

			RootObject obj = JsonConvert.DeserializeObject<RootObject>(MagicWithJson(json));

			var element = obj.Main.First();
			var tags = element.tags.Aggregate("", (current, tag) => current + (tag + ","));

		    return
		        $"[{Regex.Unescape(element.subject)}, {tags}] " +
		        $" przez {element.first_post.user.name}" +
		        $"http://forum.4programmers.net/{GetForumUrl(forumId)}/{element.topic_id}-{MagicWith4PSubject(element.subject)}";
		}
	}
}
