namespace OhtohsEssentials.Web.Sitemap.Models;

public class SitemapUrl
{
    public required string Loc { get; set; }
    public DateTime LastModified { get; set; }
    public required string ChangeFrequency { get; set; }
    public decimal Priority { get; set; }
}
