namespace CinemaSystem
{
    public class FilmOrder
    {
        public Film Film { get; set; }
        public AudioTrack AudioTrack { get; set; }
        public Subtitle Subtitle { get; set; }

        public FilmOrder(Film film, string language)
        {
            Film = film;
            AudioTrack = new AudioTrack(language);
            Subtitle = new Subtitle(language);
        }

        public void ChangeLanguage(string newLanguage)
        {
            AudioTrack.Language = newLanguage;
            Subtitle.Language = newLanguage;
        }

        public void DisplayOrder()
        {
            Console.WriteLine($"\n=== Ваш заказ ===");
            Console.WriteLine($"Фильм: {Film.Title}");
            Console.WriteLine($"Язык озвучки: {AudioTrack.Language}");
            Console.WriteLine($"Субтитры: {Subtitle.Language}");
            Console.WriteLine($"Цитата: {Film.GetQuote(AudioTrack.Language)}");
            Console.WriteLine("=================\n");
        }
    }
}