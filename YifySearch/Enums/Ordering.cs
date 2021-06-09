using YifySearch.Extensions;

namespace YifySearch.Enums
{
    public enum Ordering
    {
        [EnumString("desc")]
        Descending,

        [EnumString("asc")]
        Ascending
    }
}