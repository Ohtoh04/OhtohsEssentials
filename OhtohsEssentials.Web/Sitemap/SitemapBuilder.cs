using System.Xml.Linq;
using OhtohsEssentials.Web.Sitemap.Models;

namespace OhtohsEssentials.Web.Sitemap;

/// <summary>
/// Provides functionality to build an XML sitemap according to the sitemaps.org protocol,
/// including managing a collection of <see cref="SitemapUrl"/> entries.
/// </summary>
public class SitemapBuilder : ISitemapBuilder
{
    private static readonly XNamespace Ns = "http://www.sitemaps.org/schemas/sitemap/0.9";
    private readonly List<SitemapUrl> _urls = new List<SitemapUrl>();

    /// <inheritdoc />
    public XDocument BuildSitemapXmlAsync(CancellationToken ct = default)
    {
        var urlset = new XElement(Ns + "urlset",
            _urls.Select(url =>
                new XElement(Ns + "url",
                    new XElement(Ns + "loc", url.Loc),
                    new XElement(Ns + "lastmod", url.LastModified.ToString("yyyy-MM-dd")),
                    new XElement(Ns + "changefreq", url.ChangeFrequency),
                    new XElement(Ns + "priority", url.Priority.ToString("0.0"))
                )
            )
        );

        var doc = new XDocument(
            new XDeclaration("1.0", "UTF-8", "yes"),
            urlset
        );

        return doc;
    }

    /// <inheritdoc />
    public ISitemapBuilder Append(SitemapUrl sitemapUrl)
    {
        _urls.Add(sitemapUrl);

        return this;
    }

    /// <inheritdoc />
    public ISitemapBuilder Insert(int index, SitemapUrl sitemapUrl)
    {
        _urls.Insert(index, sitemapUrl);

        return this;
    }

    /// <inheritdoc />
    public ISitemapBuilder Remove(int startIndex, int length)
    {
        _urls.RemoveRange(startIndex, length);

        return this;
    }

    /// <summary>
    /// Builds a sitemap XML document from an external collection of <see cref="SitemapUrl"/> entries.
    /// </summary>
    /// <param name="urls">The collection of sitemap URLs to include in the XML.</param>
    /// <returns>An <see cref="XDocument"/> representing the sitemap XML.</returns>
    public static XDocument Build(IEnumerable<SitemapUrl> urls)
    {
        var urlset = new XElement(Ns + "urlset",
            urls.Select(url =>
                new XElement(Ns + "url",
                    new XElement(Ns + "loc", url.Loc),
                    new XElement(Ns + "lastmod", url.LastModified.ToString("yyyy-MM-dd")),
                    new XElement(Ns + "changefreq", url.ChangeFrequency),
                    new XElement(Ns + "priority", url.Priority.ToString("0.0"))
                )
            )
        );

        var doc = new XDocument(
            new XDeclaration("1.0", "UTF-8", "yes"),
            urlset
        );

        return doc;
    }
}
