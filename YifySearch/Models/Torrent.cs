using Newtonsoft.Json;
using System;

namespace YifySearch.Models
{
    public partial class Torrent
    {
        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("hash")]
        public string Hash { get; set; }

        [JsonProperty("quality")]
        public string Quality { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("seeds")]
        public long Seeds { get; set; }

        [JsonProperty("peers")]
        public long Peers { get; set; }

        [JsonProperty("size")]
        public string Size { get; set; }

        [JsonProperty("size_bytes")]
        public long SizeBytes { get; set; }

        [JsonProperty("date_uploaded")]
        public string DateUploaded { get; set; }

        [JsonProperty("date_uploaded_unix")]
        public long DateUploadedUnix { get; set; }
    }
}