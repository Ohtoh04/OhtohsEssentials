using System.Xml.Linq;
using OhtohsEssentials.Web.Sitemap.Models;

namespace OhtohsEssentials.Web.Sitemap;

/// <summary>
/// Defines a builder for creating XML sitemaps and managing collections of sitemap URLs.
/// </summary>
public interface ISitemapBuilder
{
    /// <summary>
    /// Builds the sitemap XML document from the current collection of URLs.
    /// </summary>
    /// <param name="ct">Cancellation token (not currently used).</param>
    /// <returns>An <see cref="XDocument"/> representing the sitemap XML.</returns>
    XDocument BuildSitemapXmlAsync(CancellationToken ct = default);

    /// <summary>
    /// Adds a sitemap URL to the builder's collection.
    /// </summary>
    /// <param name="sitemapUrl">The sitemap URL to add.</param>
    /// <returns>The current builder instance for method chaining.</returns>
    ISitemapBuilder Append(SitemapUrl sitemapUrl);

    /// <summary>
    /// Inserts a sitemap URL at a specific position in the collection.
    /// </summary>
    /// <param name="index">The zero-based index at which to insert the URL.</param>
    /// <param name="sitemapUrl">The sitemap URL to insert.</param>
    /// <returns>The current builder instance for method chaining.</returns>
    ISitemapBuilder Insert(int index, SitemapUrl sitemapUrl);

    /// <summary>
    /// Removes a range of sitemap URLs from the collection.
    /// </summary>
    /// <param name="startIndex">The zero-based index of the first URL to remove.</param>
    /// <param name="length">The number of URLs to remove.</param>
    /// <returns>The current builder instance for method chaining.</returns>
    ISitemapBuilder Remove(int startIndex, int length);
}
