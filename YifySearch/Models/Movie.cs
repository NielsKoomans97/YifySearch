using Newtonsoft.Json;
using System;

namespace YifySearch.Models
{
    public partial class Movie
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("imdb_code")]
        public string ImdbCode { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("title_english")]
        public string TitleEnglish { get; set; }

        [JsonProperty("title_long")]
        public string TitleLong { get; set; }

        [JsonProperty("slug")]
        public string Slug { get; set; }

        [JsonProperty("year")]
        public long Year { get; set; }

        [JsonProperty("rating")]
        public decimal Rating { get; set; }

        [JsonProperty("runtime")]
        public long Runtime { get; set; }

        [JsonProperty("genres")]
        public string[] Genres { get; set; }

        [JsonProperty("summary")]
        public string Summary { get; set; }

        [JsonProperty("description_full")]
        public string DescriptionFull { get; set; }

        [JsonProperty("synopsis")]
        public string Synopsis { get; set; }

        [JsonProperty("yt_trailer_code")]
        public string YtTrailerCode { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("mpa_rating")]
        public string MpaRating { get; set; }

        [JsonProperty("background_image")]
        public Uri BackgroundImage { get; set; }

        [JsonProperty("background_image_original")]
        public Uri BackgroundImageOriginal { get; set; }

        [JsonProperty("small_cover_image")]
        public Uri SmallCoverImage { get; set; }

        [JsonProperty("medium_cover_image")]
        public Uri MediumCoverImage { get; set; }

        [JsonProperty("large_cover_image")]
        public Uri LargeCoverImage { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("torrents")]
        public Torrent[] Torrents { get; set; }

        [JsonProperty("date_uploaded")]
        public string DateUploaded { get; set; }

        [JsonProperty("date_uploaded_unix")]
        public long DateUploadedUnix { get; set; }
    }
}