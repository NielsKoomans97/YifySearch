using System;
using YifySearch.Models;

namespace YifySearch.Events
{
    public class SearchCompleted
    {
        public Movie[] Movies { get; }
        public string Status { get; }

        public string StatusMessage
        {
            get;
        }

        public SearchCompleted(Movie[] movies, string status, string statusMessage) : this(movies)
        {
            Status = status ?? throw new ArgumentNullException(nameof(status));
            StatusMessage = statusMessage ?? throw new ArgumentNullException(nameof(statusMessage));
        }

        public SearchCompleted(Movie[] movies)
        {
            Movies = movies ?? throw new ArgumentNullException(nameof(movies));
        }
    }
}