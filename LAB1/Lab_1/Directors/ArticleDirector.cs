using System;
using System.IO;
using System.Linq;
using System.Text;
using TxtToJsonConverter.Builders;
using TxtToJsonConverter.Models;

namespace TxtToJsonConverter.Directors
{
    public class ArticleDirector
    {
        private readonly IArticleBuilder _builder;

        public ArticleDirector(IArticleBuilder builder)
        {
            _builder = builder;
        }

        public Article ConstructAndValidate(string filePath, out bool isValidHash)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("TXT file not found");

            var lines = File.ReadAllLines(filePath, Encoding.UTF8);
            if (lines.Length < 3)
                throw new InvalidDataException("Invalid TXT format");

            // Парсим заголовок (первая строка)
            _builder.SetTitle(lines[0]);

            // Парсим авторов (вторая строка)
            var authors = lines[1].Split(',')
                .Select(a => a.Trim())
                .Where(a => !string.IsNullOrEmpty(a))
                .ToList();
            _builder.SetAuthors(authors);

            // Содержимое статьи (все строки от 2 до предпоследней)
            var contentLines = lines.Skip(2).Take(lines.Length - 3).ToArray();
            var content = string.Join(Environment.NewLine, contentLines);
            
            // Нормализуем переносы строк для consistent хеширования
            content = content.Replace("\r\n", "\n").Replace("\r", "\n").Replace("\n", Environment.NewLine);
            _builder.SetContent(content);

            // Получаем предоставленный хеш (последняя строка)
            var providedHash = lines[^1].Trim();

            var article = _builder.Build();
            article.HashCode = providedHash;
            
            isValidHash = _builder.ValidateHashCode();
            return article;
        }
    }
}