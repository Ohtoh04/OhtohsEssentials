using System.ComponentModel;
using System.Xml.Serialization;

namespace OhtohsEssentials.Web.Sitemap.Enums;

public enum ChangeFrequency
{
    [XmlEnum("always")]
    Always,

    [XmlEnum("hourly")]
    Hourly,

    [XmlEnum("daily")]
    Daily,

    [XmlEnum("weekly")]
    Weekly,

    [XmlEnum("monthly")]
    Monthly,

    [XmlEnum("yearly")]
    Yearly,

    [XmlEnum("never")]
    Never
}
