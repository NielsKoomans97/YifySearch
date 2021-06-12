using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using YifySearch.Enums;
using YifySearch.Events;
using YifySearch.Extensions;
using YifySearch.Models;

namespace YifySearch
{
    namespace Extensions
    {
        public static class EnumExtensions
        {
            public static string Get(this Enum val)
            {
                EnumStringAttribute[] attributes = (EnumStringAttribute[])val
                    .GetType()
                    .GetField(val.ToString())
                    .GetCustomAttributes(typeof(EnumStringAttribute), false);

                return attributes.Length > 0 ? attributes[0].Value : string.Empty;
            }
        }

        [AttributeUsage(AttributeTargets.Field | AttributeTargets.Enum)]
        public class EnumStringAttribute : Attribute
        {
            public string Value { get; }

            public EnumStringAttribute(string value)
            {
                Value = value ?? throw new ArgumentNullException(nameof(value));
            }
        }
    }

    public class SearchClient
    {
        public event EventHandler<SearchCompleted> SearchCompleted;

        public int Limit { get; set; } = 20;
        public int MinimumRating { get; set; } = 0;
        public string Query { get; set; } = "";
        public Quality Quality { get; set; } = Quality.FHD;
        public Genre[] Genre { get; set; } = { Enums.Genre.Comedy, Enums.Genre.Action };
        public Sorting Sorting { get; set; } = Sorting.Title;
        public Ordering Ordering { get; set; } = Ordering.Descending;
        public bool WithRtRatings { get; set; } = false;

        private string BuildGenreList()
        {
            var builder = new StringBuilder();
            foreach (var genre in Genre)
            {
                builder.Append($"{genre},");
            }

            return builder.ToString();
        }

        private string BuildUri(int page)
        {
            var builder = new StringBuilder("https://yts.mx/api/v2/list_movies.json");
            builder.Append($"?limit={Limit}");
            builder.Append($"&page={page}");
            builder.Append($"&quality={Quality.Get()}");
            builder.Append($"&minimum_rating={MinimumRating}");
            builder.Append($"&query_term={Query}");
            builder.Append($"&genre={BuildGenreList()}");
            builder.Append($"&sort_by={Sorting.Get()}");
            builder.Append($"&order_by={Ordering.Get()}");
            builder.Append($"&with_rt_ratings={WithRtRatings}");

            return builder.ToString();
        }

        public async Task SearchAsync(int page)
        {
            using (var httpClient = new HttpClient())
            using (var stream = await httpClient.GetStreamAsync(BuildUri(page)))
            using (var reader = new StreamReader(stream))
            using (var jtr = new JsonTextReader(reader))
            {
                var js = new JsonSerializer();
                var item = js.Deserialize<dynamic>(jtr);
                var data = (JToken)item.data;

                if (data["movies"] == null && (int)data["movie_count"] > 0)
                {
                    SearchCompleted?.Invoke(this, new SearchCompleted(null, "ERROR", "No movies were found with this query. There probably were no errors, but odds are that the entire array wasn't provided in the response. YTS, fix your shit."));
                    return;
                }

                if ((int)data["movie_count"] < 0)
                {
                    SearchCompleted?.Invoke(this, new SearchCompleted(null, "ERROR", "No movies were found with this query. Please check for errors and try again."));
                    return;
                }

                SearchCompleted?.Invoke(this,
                    new SearchCompleted(data["movies"].ToObject<Movie[]>(), item.status, item.status_message));
            }
        }

        public void SaveConfig(string filePath)
        {
            File.WriteAllText(filePath, JsonConvert.SerializeObject(this));
        }

        public static SearchClient LoadConfig(string filePath)
        {
            return JsonConvert.DeserializeObject<SearchClient>(File.ReadAllText(filePath));
        }
    }
}