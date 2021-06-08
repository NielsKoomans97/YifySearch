using System;
using YifySearch.Models;

namespace YifySearch.Events
{
    public class SearchResult
    {
        public Movie[] Movies { get; }

        public SearchResult(Movie[] movies)
        {
            Movies = movies ?? throw new ArgumentNullException(nameof(movies));
        }
    }
}