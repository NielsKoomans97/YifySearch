using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using YifySearch.Enums;
using YifySearch.Events;
using YifySearch.Models;

namespace YifySearch
{
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

        private string GetSorting()
        {
            switch (Sorting)
            {
                case Sorting.Title: return "title";
                case Sorting.Year: return "year";
                case Sorting.Rating: return "rating";
                case Sorting.Peers: return "peers";
                case Sorting.Seeds: return "seeds";
                case Sorting.DownloadCount: return "download_count";
                case Sorting.LikeCount: return "like_count";
                case Sorting.DateAdded: return "date_added";
                default: return string.Empty;
            }
        }

        private string GetOrdering()
        {
            switch (Ordering)
            {
                case Ordering.Descending: return "desc";
                case Ordering.Ascending: return "asc";
                default: return string.Empty;
            }
        }

        private string GetQuality()
        {
            switch (Quality)
            {
                case Quality.HD: return "720p";
                case Quality.FHD: return "1080p";
                case Quality.UHD: return "2160p";
                case Quality.ThreeDimensional: return "3D";
                default: return string.Empty;
            }
        }

        private string BuildUri(int page)
        {
            var builder = new StringBuilder("https://yts.mx/api/v2/list_movies.json");
            builder.Append($"?limit={Limit}");
            builder.Append($"&page={page}");
            builder.Append($"&quality={GetQuality()}");
            builder.Append($"&minimum_rating={MinimumRating}");
            builder.Append($"&query_term={Query}");
            builder.Append($"&genre={BuildGenreList()}");
            builder.Append($"&sort_by={GetSorting()}");
            builder.Append($"&order_by={GetOrdering()}");
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
                var data = item.data;
                var movies = (JArray)data.movies;

                if (movies.Count < 0)
                    SearchCompleted?.Invoke(this, new SearchCompleted(null, "No movies found", "No movies were found with this query. Please check for errors and try again."));

                SearchCompleted?.Invoke(this,
                    new SearchCompleted(movies.ToObject<Movie[]>(), item.status, item.status_message));

                movies.Clear();
            }
        }
    }
}