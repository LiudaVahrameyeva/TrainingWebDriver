using HtmlAgilityPack;

namespace TestProject2;

public static class HtmlNodeExtensions
{
    public static string GetSingleNodeText(this HtmlNode parentNode, string xpath)
    {
        return parentNode.SelectSingleNode(xpath).InnerText.Trim();
    }
}