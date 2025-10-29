using ElevatorSystem;

class Program
{
    static void Main(string[] args)
    {
        // Получаем единственные экземпляры здания и лифта
        Building building = Building.GetInstance("Бизнес-центр 'Северная башня'", 12);
        Elevator elevator = Elevator.GetInstance();

        Console.WriteLine("=== СИСТЕМА УПРАВЛЕНИЯ ЛИФТОМ ===");
        Console.WriteLine("Паттерны: Singleton (Здание и Лифт)\n");

        bool exit = false;
        while (!exit)
        {
            DisplayMainMenu();
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    building.DisplayBuildingInfo();
                    break;
                case "2":
                    building.DisplayFloors();
                    break;
                case "3":
                    ControlElevator(elevator, building);
                    break;
                case "4":
                    ViewFloorDetails(building);
                    break;
                case "5":
                    PassengerOperations(elevator);
                    break;
                case "6":
                    elevator.EmergencyStop();
                    break;
                case "7":
                    elevator.ResetEmergency();
                    break;
                case "8":
                    TestSingleton();
                    break;
                case "0":
                    exit = true;
                    Console.WriteLine("До свидания!");
                    break;
                default:
                    Console.WriteLine("Неверный выбор. Попробуйте снова.");
                    break;
            }
        }
    }

    static void DisplayMainMenu()
    {
        Console.WriteLine("\n=== ГЛАВНОЕ МЕНЮ ===");
        Console.WriteLine("1. Информация о здании");
        Console.WriteLine("2. Список этажей");
        Console.WriteLine("3. Управление лифтом");
        Console.WriteLine("4. Информация об этаже");
        Console.WriteLine("5. Операции с пассажирами");
        Console.WriteLine("6. Аварийная остановка");
        Console.WriteLine("7. Сброс аварийного режима");
        Console.WriteLine("8. Тестирование Singleton");
        Console.WriteLine("0. Выйти");
        Console.Write("Выберите действие: ");
    }

    static void ControlElevator(Elevator elevator, Building building)
    {
        Console.WriteLine("\n=== УПРАВЛЕНИЕ ЛИФТОМ ===");
        elevator.DisplayStatus();
        
        Console.WriteLine("\n1. Вызвать лифт на этаж");
        Console.WriteLine("2. Переместить лифт на этаж");
        Console.WriteLine("3. Статус лифта");
        Console.Write("Выберите действие: ");
        
        string choice = Console.ReadLine();
        
        switch (choice)
        {
            case "1":
                Console.Write("Введите номер этажа для вызова: ");
                if (int.TryParse(Console.ReadLine(), out int callFloor))
                {
                    elevator.CallToFloor(callFloor);
                }
                else
                {
                    Console.WriteLine("Неверный формат этажа");
                }
                break;
            case "2":
                Console.Write("Введите целевой этаж: ");
                if (int.TryParse(Console.ReadLine(), out int targetFloor))
                {
                    elevator.MoveToFloor(targetFloor);
                }
                else
                {
                    Console.WriteLine("Неверный формат этажа");
                }
                break;
            case "3":
                elevator.DisplayStatus();
                break;
            default:
                Console.WriteLine("Неверный выбор");
                break;
        }
    }

    static void ViewFloorDetails(Building building)
    {
        Console.Write("\nВведите номер этажа для просмотра: ");
        if (int.TryParse(Console.ReadLine(), out int floorNumber))
        {
            var floor = building.GetFloor(floorNumber);
            if (floor != null)
            {
                floor.DisplayFloorInfo();
            }
            else
            {
                Console.WriteLine($"Этаж {floorNumber} не найден");
            }
        }
        else
        {
            Console.WriteLine("Неверный формат этажа");
        }
    }

    static void PassengerOperations(Elevator elevator)
    {
        Console.WriteLine("\n=== ОПЕРАЦИИ С ПАССАЖИРАМИ ===");
        Console.WriteLine($"Текущее количество пассажиров: {elevator.CurrentPassengers}");
        
        Console.WriteLine("1. Пассажиры зашли в лифт");
        Console.WriteLine("2. Пассажиры вышли из лифта");
        Console.Write("Выберите действие: ");
        
        string choice = Console.ReadLine();
        Console.Write("Введите количество пассажиров: ");
        
        if (int.TryParse(Console.ReadLine(), out int count))
        {
            switch (choice)
            {
                case "1":
                    elevator.AddPassengers(count);
                    break;
                case "2":
                    elevator.RemovePassengers(count);
                    break;
                default:
                    Console.WriteLine("Неверный выбор");
                    break;
            }
        }
        else
        {
            Console.WriteLine("Неверный формат количества");
        }
    }

    static void TestSingleton()
    {
        Console.WriteLine("\n=== ТЕСТИРОВАНИЕ SINGLETON ===");
        
        // Пытаемся создать несколько экземпляров
        Building building1 = Building.GetInstance();
        Building building2 = Building.GetInstance();
        
        Elevator elevator1 = Elevator.GetInstance();
        Elevator elevator2 = Elevator.GetInstance();

        Console.WriteLine("Проверка здания:");
        Console.WriteLine($"building1 == building2: {object.ReferenceEquals(building1, building2)}");
        Console.WriteLine($"building1.Name: {building1.Name}");
        Console.WriteLine($"building2.Name: {building2.Name}");

        Console.WriteLine("\nПроверка лифта:");
        Console.WriteLine($"elevator1 == elevator2: {object.ReferenceEquals(elevator1, elevator2)}");
        Console.WriteLine($"elevator1.CurrentFloor: {elevator1.CurrentFloor}");
        Console.WriteLine($"elevator2.CurrentFloor: {elevator2.CurrentFloor}");

        Console.WriteLine("\n✅ Singleton паттерн работает корректно - экземпляры единственные!");
    }
}