using System.ComponentModel;

namespace DataLayer.Models
{
    public enum Resolutions
    {
        [Description("Full screen")]
        FullScreen,
        [Description("1680×1050")]
        Large,
        [Description("1280×1024")]
        Medium,
        [Description("1366×768")]
        Wide,
    }
}
