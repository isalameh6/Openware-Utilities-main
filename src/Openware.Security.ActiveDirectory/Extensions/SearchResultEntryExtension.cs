namespace System.DirectoryServices.Protocols;

/// <summary>
/// Extension Class for <see cref="System.DirectoryServices.Protocols.SearchResultEntry">SearchResultEntry</seealso>
/// </summary>
public static class SearchResultEntryExtension
{
    /// <summary>
    /// Finds the first value of an attribute
    /// </summary>
    /// <param name="searchResultEntry">The <seealso cref="System.DirectoryServices.Protocols.SearchResultEntry">SearchResultEntry</seealso> 
    /// to search through</param>
    /// <param name="attributeName">The attribute name</param>
    /// <param name="defaultValue">The default value to return in case the attribute doesn't exist</param>
    /// <returns></returns>
    public static string FindFirstAttributeValue(this SearchResultEntry searchResultEntry,
        String attributeName, string defaultValue = null)
    {
        if (searchResultEntry.Attributes[attributeName] == null || searchResultEntry.Attributes[attributeName].Count == 0)
        {
            return defaultValue;
        }
        return searchResultEntry.Attributes[attributeName][0].ToString();
    }
}
