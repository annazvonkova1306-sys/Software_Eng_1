using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using TxtToJsonConverter.Models;

namespace TxtToJsonConverter.Builders
{
    public class ArticleBuilder : IArticleBuilder
    {
        private Article _article;
        private string _contentForHashing;

        public ArticleBuilder()
        {
            _article = new Article();
        }

        public IArticleBuilder SetTitle(string title)
        {
            _article.Title = title?.Trim();
            return this;
        }

        public IArticleBuilder SetAuthors(List<string> authors)
        {
            _article.Authors = authors ?? new List<string>();
            return this;
        }

        public IArticleBuilder SetContent(string content)
        {
            _article.Content = content?.Trim();
            _contentForHashing = content;
            return this;
        }

        public IArticleBuilder CalculateHashCode()
        {
            if (!string.IsNullOrEmpty(_contentForHashing))
            {
                using (var sha256 = SHA256.Create())
                {
                    var bytes = Encoding.UTF8.GetBytes(_contentForHashing);
                    var hash = sha256.ComputeHash(bytes);
                    _article.HashCode = BitConverter.ToString(hash).Replace("-", "").ToLower();
                }
            }
            return this;
        }

        public bool ValidateHashCode()
        {
            if (string.IsNullOrEmpty(_contentForHashing) || string.IsNullOrEmpty(_article.HashCode))
                return false;

            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(_contentForHashing);
                var currentHash = BitConverter.ToString(sha256.ComputeHash(bytes))
                    .Replace("-", "").ToLower();
                
                return currentHash == _article.HashCode;
            }
        }

        public Article Build()
        {
            return _article;
        }

        public string ToJson()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };
            
            return JsonSerializer.Serialize(_article, options);
        }
    }
}