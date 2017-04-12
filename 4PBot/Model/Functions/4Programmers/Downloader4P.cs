using System.Net;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;
using LiteDB;
using System.Configuration;
using System.Linq;
using System.Collections.Generic;

namespace _4PBot.Model.Functions._4Programmers
{
    public class TopicsContiniousDownloading
    {
        private readonly static string ConnectionString = nameof(TopicsContiniousDownloading) + ".db";
        private CancellationTokenSource TokenSource = new CancellationTokenSource();

        private int Id
        {
            get
            {
                return Int32.Parse(ConfigurationManager.AppSettings[nameof(TopicsContiniousDownloading)]);
            }

            set
            {
                ConfigurationManager.AppSettings[nameof(TopicsContiniousDownloading)] = value.ToString();
            }
        }

        public TopicsContiniousDownloading(string urlWithStartId)
        {
            var token = this.TokenSource.Token;
            Func<int, IEnumerable<Post>> fetchData = (id) =>
            {
                var json = new WebClient().DownloadString(urlWithStartId + id);
                return this.Deserialize(json);
            };
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    using (var db = new LiteDatabase(TopicsContiniousDownloading.ConnectionString))
                    {
                        var collection = db.GetCollection<Post>();
                        var posts = fetchData(this.Id);
                        collection.EnsureIndex(x => x.post_id);
                        if (posts?.Any() ?? false)
                        {
                            collection.Insert(posts);
                            this.Id += posts.Count();
                        }
                    }
                    Task.Delay(1000);
                    if (token.IsCancellationRequested)
                    {
                        break;
                    }
                }
            }, token);
        }

        public IEnumerable<Post> Deserialize(string preparedJson)
        {
            return JsonConvert.DeserializeObject<Post[]>(preparedJson);
        }

        public IEnumerable<Post> DownloadData()
        {
            using (var db = new LiteDatabase(TopicsContiniousDownloading.ConnectionString))
            {
                var collection = db.GetCollection<Post>().FindAll().ToArray();
                return collection;
            }
        }

        ~TopicsContiniousDownloading()
        {
            this.TokenSource?.Cancel();
        }
    }
}