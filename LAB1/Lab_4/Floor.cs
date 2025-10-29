namespace ElevatorSystem
{
    public class Floor
    {
        public int FloorNumber { get; private set; }
        public string Type { get; private set; }
        public string Description { get; private set; }
        public List<Room> Rooms { get; private set; }

        public Floor(int floorNumber, string type)
        {
            FloorNumber = floorNumber;
            Type = type;
            Rooms = new List<Room>();
            Description = GetFloorDescription(type);
            InitializeRooms();
        }

        private string GetFloorDescription(string type)
        {
            return type switch
            {
                "Лобби" => "Главный вход, ресепшен, зона ожидания",
                "Пентхаус" => "Роскошные апартаменты, панорамные окна",
                "Торговые помещения" => "Магазины, кафе, сервисные услуги",
                "Офисные помещения" => "Офисы компаний, переговорные комнаты",
                _ => "Стандартные помещения"
            };
        }

        private void InitializeRooms()
        {
            var random = new Random();
            int roomCount = Type switch
            {
                "Лобби" => 3,
                "Пентхаус" => 2,
                "Торговые помещения" => random.Next(4, 8),
                "Офисные помещения" => random.Next(6, 12),
                _ => random.Next(4, 6)
            };

            for (int i = 1; i <= roomCount; i++)
            {
                Rooms.Add(new Room($"{FloorNumber}{i:D2}", Type));
            }
        }

        public void DisplayFloorInfo()
        {
            Console.WriteLine($"\n=== ЭТАЖ {FloorNumber}: {Type} ===");
            Console.WriteLine($"Описание: {Description}");
            Console.WriteLine($"Помещений: {Rooms.Count}");
            Console.WriteLine("Помещения:");
            foreach (var room in Rooms)
            {
                Console.WriteLine($"  - {room.Number}: {room.Purpose}");
            }
        }

        public void CallElevator()
        {
            var elevator = Elevator.GetInstance();
            elevator.CallToFloor(FloorNumber);
            Console.WriteLine($"С этажа {FloorNumber} вызван лифт");
        }
    }
}