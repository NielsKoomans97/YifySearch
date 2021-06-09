using YifySearch.Extensions;

namespace YifySearch.Enums
{
    public enum Quality
    {
        [EnumString("720p")]
        HD = 720,

        [EnumString("1080p")]
        FHD = 1080,

        [EnumString("2160p")]
        UHD = 2160,

        [EnumString("3D")]
        ThreeDimensional = 3
    }
}