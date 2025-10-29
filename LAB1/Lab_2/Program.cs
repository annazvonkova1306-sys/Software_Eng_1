using CinemaSystem;

class Program
{
    static void Main(string[] args)
    {
        CinemaService cinemaService = new CinemaService();
        FilmOrder currentOrder = null;

        Console.WriteLine("=== СИСТЕМА КИНОПРОКАТА ===");

        while (true)
        {
            DisplayMenu();
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    OrderFilm(cinemaService, ref currentOrder);
                    break;
                case "2":
                    ChangeLanguage(ref currentOrder);
                    break;
                case "3":
                    DisplayCurrentOrder(currentOrder);
                    break;
                case "4":
                    Console.WriteLine("Спасибо за использование системы! До свидания!");
                    return;
                default:
                    Console.WriteLine("Неверный выбор. Попробуйте снова.");
                    break;
            }
        }
    }

    static void DisplayMenu()
    {
        Console.WriteLine("\n=== ГЛАВНОЕ МЕНЮ ===");
        Console.WriteLine("1. Заказать фильм");
        Console.WriteLine("2. Сменить язык озвучки/субтитров");
        Console.WriteLine("3. Показать текущий заказ");
        Console.WriteLine("4. Выйти");
        Console.Write("Выберите действие: ");
    }

    static void OrderFilm(CinemaService cinemaService, ref FilmOrder currentOrder)
    {
        cinemaService.DisplayAvailableFilms();
        
        Console.Write("Выберите номер фильма: ");
        if (int.TryParse(Console.ReadLine(), out int filmChoice) && filmChoice >= 1 && filmChoice <= cinemaService.FilmsCount)
        {
            Film selectedFilm = cinemaService.GetFilmByIndex(filmChoice - 1);
            
            Console.WriteLine("\n=== Выбор языка ===");
            Console.WriteLine("1. Русский");
            Console.WriteLine("2. Английский");
            Console.Write("Выберите язык: ");
            
            string languageChoice = Console.ReadLine();
            string language = languageChoice == "1" ? "русский" : "английский";
            
            currentOrder = new FilmOrder(selectedFilm, language);
            currentOrder.DisplayOrder();
        }
        else
        {
            Console.WriteLine("Неверный выбор фильма.");
        }
    }

    static void ChangeLanguage(ref FilmOrder currentOrder)
    {
        if (currentOrder == null)
        {
            Console.WriteLine("Сначала закажите фильм!");
            return;
        }

        Console.WriteLine("\n=== Смена языка ===");
        Console.WriteLine($"Текущий язык: {currentOrder.AudioTrack.Language}");
        Console.WriteLine("1. Русский");
        Console.WriteLine("2. Английский");
        Console.Write("Выберите новый язык: ");
        
        string languageChoice = Console.ReadLine();
        string newLanguage = languageChoice == "1" ? "русский" : "английский";
        
        currentOrder.ChangeLanguage(newLanguage);
        Console.WriteLine($"Язык изменен на: {newLanguage}");
        currentOrder.DisplayOrder();
    }

    static void DisplayCurrentOrder(FilmOrder currentOrder)
    {
        if (currentOrder == null)
        {
            Console.WriteLine("Нет активного заказа.");
        }
        else
        {
            currentOrder.DisplayOrder();
        }
    }
}