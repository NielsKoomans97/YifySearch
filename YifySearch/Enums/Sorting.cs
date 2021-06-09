using YifySearch.Extensions;

namespace YifySearch.Enums
{
    public enum Sorting
    {
        [EnumString("title")]
        Title,

        [EnumString("year")]
        Year,

        [EnumString("rating")]
        Rating,

        [EnumString("peers")]
        Peers,

        [EnumString("seeds")]
        Seeds,

        [EnumString("download_count")]
        DownloadCount,

        [EnumString("like_count")]
        LikeCount,

        [EnumString("date_added")]
        DateAdded
    }
}