using System;
using System.IO;
using System.Text;
using TxtToJsonConverter.Builders;
using TxtToJsonConverter.Directors;
using TxtToJsonConverter.Models;

namespace TxtToJsonConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("TXT to JSON Converter with Hash Validation");
            Console.WriteLine("===========================================");

            try
            {
                string inputFile = "article.txt";
                string outputFile = "article.json";

                if (!File.Exists(inputFile))
                {
                    CreateCorrectSampleTxtFile(inputFile);
                    Console.WriteLine($"Создан пример файла: {inputFile}");
                }

                var builder = new ArticleBuilder();
                var director = new ArticleDirector(builder);

                var article = director.ConstructAndValidate(inputFile, out bool isValidHash);
                
                Console.WriteLine($"Хеш-код статьи: {(isValidHash ? "валиден" : "невалиден")}");

                if (isValidHash)
                {
                    var json = builder.ToJson();
                    File.WriteAllText(outputFile, json);
                    
                    Console.WriteLine($"Статья успешно конвертирована в: {outputFile}");
                    Console.WriteLine("\nСодержимое JSON:");
                    Console.WriteLine(json);
                }
                else
                {
                    Console.WriteLine("Ошибка: Хеш-код статьи не совпадает!");
                    Console.WriteLine($"Ожидался: {article.HashCode}");
                    
                    // Пересчитываем для отладки
                    var correctHash = HashGenerator.GenerateHash(GetContentForHashing(inputFile));
                    Console.WriteLine($"Правильный хеш: {correctHash}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");
            }

            Console.WriteLine("\nНажмите любую клавишу для выхода...");
            Console.ReadKey();
        }

        static void CreateCorrectSampleTxtFile(string filePath)
        {
            // Содержимое для хеширования (только текст статьи, без заголовка и авторов)
            var contentForHashing = @"В земле была нора, а в норе жил хоббит. Не противная, грязная, сырая нора, 
где со всех сторон торчат хвосты червей и противно пахнет плесенью, но и не сухая, 
голая, песчаная нора, где не на что сесть и нечего съесть. Это была нора хоббита, 
а значит, благоустроенная нора.
Нора была устроена как надо: с круглой, похожей на иллюминатор дверью, выкрашенной 
в зеленый цвет, с блестящей медной ручкой ровно посредине. Дверь открывалась в просторный 
зал, похожий на туннель: очень удобный зал, без дыма, хорошо меблированный, 
со стульями, столами и крючками для шляп и пальто — хоббит очень любил гостей.
Туннель вился дальше и уходил глубоко под холм, но не в подземелье, а в уютные комнатки.
Пол был выстлан плитками и коврами, стены обшиты панелями. Везде были поставлены 
отлично отполированные стулья, на стенах висели вешалки для шляп и пальто — хоббит 
очень любил гости. Полка была заставлена картами, а на камине стояли часы с маятником.
В норе было много чуланов (все они были битком набиты припасами), круглые окошки 
смотрели из стены на сад и луга, спускавшиеся к реке.";

            // Генерируем правильный хеш
            var correctHash = HashGenerator.GenerateHash(contentForHashing);

            // Создаем полный файл
            var fullContent = @"Хоббит, или Туда и Обратно
Джон Рональд Руэл Толкин
В земле была нора, а в норе жил хоббит. Не противная, грязная, сырая нора, 
где со всех сторон торчат хвосты червей и противно пахнет плесенью, но и не сухая, 
голая, песчаная нора, где не на что сесть и нечего съесть. Это была нора хоббита, 
а значит, благоустроенная нора.
Нора была устроена как надо: с круглой, похожей на иллюминатор дверью, выкрашенной 
в зеленый цвет, с блестящей медной ручкой ровно посредине. Дверь открывалась в просторный 
зал, похожий на туннель: очень удобный зал, без дыма, хорошо меблированный, 
со стульями, столами и крючками для шляп и пальто — хоббит очень любил гостей.
Туннель вился дальше и уходил глубоко под холм, но не в подземелье, а в уютные комнатки.
Пол был выстлан плитками и коврами, стены обшиты панелями. Везде были поставлены 
отлично отполированные стулья, на стенах висели вешалки для шляп и пальто — хоббит 
очень любил гости. Полка была заставлена картами, а на камине стояли часы с маятником.
В норе было много чуланов (все они были битком набиты припасами), круглые окошки 
смотрели из стены на сад и луга, спускавшиеся к реке.
" + correctHash;

            File.WriteAllText(filePath, fullContent, Encoding.UTF8);
            Console.WriteLine($"Создан файл с правильным хешем: {correctHash}");
        }

        static string GetContentForHashing(string filePath)
        {
            var lines = File.ReadAllLines(filePath, Encoding.UTF8);
            if (lines.Length < 3) return string.Empty;

            // Берем только содержимое статьи (строки 2 до предпоследней)
            var contentLines = lines.Skip(2).Take(lines.Length - 3).ToArray();
            return string.Join(Environment.NewLine, contentLines);
        }
    }
}