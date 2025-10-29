using ShapePrototype;

class Program
{
    static void Main(string[] args)
    {
        ShapeGenerator generator = new ShapeGenerator();
        
        Console.WriteLine("=== СИСТЕМА ГЕНЕРАЦИИ ФИГУР ===");
        Console.WriteLine("Паттерн: Prototype\n");

        while (true)
        {
            DisplayMenu();
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    GenerateSingleShape(generator);
                    break;
                case "2":
                    GenerateMultipleShapes(generator);
                    break;
                case "3":
                    TestCloning(generator);
                    break;
                case "4":
                    DisplayAllPrototypes();
                    break;
                case "5":
                    Console.WriteLine("До свидания!");
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
        Console.WriteLine("1. Сгенерировать случайную фигуру");
        Console.WriteLine("2. Сгенерировать несколько фигур");
        Console.WriteLine("3. Протестировать клонирование");
        Console.WriteLine("4. Показать все доступные прототипы");
        Console.WriteLine("5. Выйти");
        Console.Write("Выберите действие: ");
    }

    static void GenerateSingleShape(ShapeGenerator generator)
    {
        Console.WriteLine("\n=== ГЕНЕРАЦИЯ СЛУЧАЙНОЙ ФИГУРЫ ===");
        IShape shape = generator.GenerateRandomShape();
        shape.Display();
    }

    static void GenerateMultipleShapes(ShapeGenerator generator)
    {
        Console.Write("\nВведите количество фигур для генерации: ");
        if (int.TryParse(Console.ReadLine(), out int count) && count > 0)
        {
            Console.WriteLine($"\n=== ГЕНЕРАЦИЯ {count} ФИГУР ===");
            var shapes = generator.GenerateMultipleShapes(count);
            
            int regularCount = shapes.Count(s => !s.IsSuper);
            int superCount = shapes.Count(s => s.IsSuper);
            
            for (int i = 0; i < shapes.Count; i++)
            {
                Console.WriteLine($"--- Фигура #{i + 1} ---");
                shapes[i].Display();
            }
            
            Console.WriteLine($"=== СТАТИСТИКА ===");
            Console.WriteLine($"Всего фигур: {shapes.Count}");
            Console.WriteLine($"Обычных фигур: {regularCount}");
            Console.WriteLine($"Супер-фигур: {superCount}");
            Console.WriteLine($"Общее количество клеток: {shapes.Sum(s => s.Cells)}");
        }
        else
        {
            Console.WriteLine("Неверное количество.");
        }
    }

    static void TestCloning(ShapeGenerator generator)
    {
        Console.WriteLine("\n=== ТЕСТИРОВАНИЕ КЛОНИРОВАНИЯ ===");
        
        // Генерируем оригинальную фигуру
        IShape original = generator.GenerateRandomShape();
        Console.WriteLine("--- ОРИГИНАЛЬНАЯ ФИГУРА ---");
        original.Display();

        // Создаем клон
        IShape clone = generator.CreateCopy(original);
        Console.WriteLine("--- КЛОН ФИГУРЫ ---");
        clone.Display();

        // Проверяем, что это разные объекты
        Console.WriteLine("--- ИНФОРМАЦИЯ О КЛОНИРОВАНИИ ---");
        Console.WriteLine($"Оригинал и клон - один объект: {object.ReferenceEquals(original, clone)}");
        Console.WriteLine($"Тип оригинала: {original.GetType().Name}");
        Console.WriteLine($"Тип клона: {clone.GetType().Name}");
        Console.WriteLine($"Название оригинала: {original.Name}");
        Console.WriteLine($"Название клона: {clone.Name}");
        Console.WriteLine("Клонирование прошло успешно!\n");
    }

    static void DisplayAllPrototypes()
    {
        Console.WriteLine("\n=== ВСЕ ДОСТУПНЫЕ ПРОТОТИПЫ ФИГУР ===");
        
        ShapeRegistry registry = new ShapeRegistry();
        var keys = registry.GetAvailableShapeKeys();
        
        foreach (var key in keys)
        {
            try
            {
                IShape shape = registry.GetShape(key);
                shape.Display();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при создании фигуры {key}: {ex.Message}");
            }
        }
    }
}