using TxtToJsonConverter.Models;

namespace TxtToJsonConverter.Builders
{
    public interface IArticleBuilder
    {
        IArticleBuilder SetTitle(string title);
        IArticleBuilder SetAuthors(List<string> authors);
        IArticleBuilder SetContent(string content);
        IArticleBuilder CalculateHashCode();
        Article Build();
        bool ValidateHashCode();
        string ToJson();
    }
}