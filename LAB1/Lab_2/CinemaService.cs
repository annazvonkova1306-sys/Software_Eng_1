namespace CinemaSystem
{
    public class CinemaService
    {
        private List<Film> _availableFilms;

        public CinemaService()
        {
            InitializeFilms();
        }

        private void InitializeFilms()
        {
            _availableFilms = new List<Film>
            {
                new Film("Властелин Колец", new Dictionary<string, string>
                {
                    ["русский"] = "Ты не пройдешь! Бегите, глупцы...",
                    ["английский"] = "You shall not pass! Fly, you fools..."
                }),
                new Film("Гарри Поттер", new Dictionary<string, string>
                {
                    ["русский"] = "Учишься балету, Поттер?",
                    ["английский"] = "Having a ballet lesson, Potter?"
                }),
                new Film("Сумерки", new Dictionary<string, string>
                {
                    ["русский"] = "Лев влюбился в овечку, глупая овечка, ну а лев просто мазохист",
                    ["английский"] = "The lion fell in love with the lamb, stupid lamb, and the lion is just a masochist"
                })
            };
        }

        public void DisplayAvailableFilms()
        {
            Console.WriteLine("\n=== Доступные фильмы ===");
            for (int i = 0; i < _availableFilms.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {_availableFilms[i].Title}");
            }
            Console.WriteLine("========================\n");
        }

        public Film GetFilmByIndex(int index)
        {
            if (index >= 0 && index < _availableFilms.Count)
                return _availableFilms[index];
            return null;
        }

        public int FilmsCount => _availableFilms.Count;
    }
}