namespace CinemaSystem
{
    public class Film
    {
        public string Title { get; set; }
        public Dictionary<string, string> Quotes { get; set; }

        public Film(string title, Dictionary<string, string> quotes)
        {
            Title = title;
            Quotes = quotes;
        }

        public string GetQuote(string language)
        {
            return Quotes.ContainsKey(language) ? Quotes[language] : "Quote not found";
        }
    }
}