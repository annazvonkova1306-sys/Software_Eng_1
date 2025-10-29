namespace ElevatorSystem
{
    public sealed class Building
    {
        private static Building _instance;
        private static readonly object _lock = new object();
        
        public List<Floor> Floors { get; private set; }
        public Elevator Elevator { get; private set; }
        public string Name { get; private set; }
        public int TotalFloors => Floors.Count;

        private Building(string name, int floorsCount)
        {
            Name = name;
            Floors = new List<Floor>();
            InitializeFloors(floorsCount);
            Elevator = Elevator.GetInstance(floorsCount);
        }

        public static Building GetInstance(string name = "Офисное здание", int floorsCount = 10)
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new Building(name, floorsCount);
                    }
                }
            }
            return _instance;
        }

        private void InitializeFloors(int floorsCount)
        {
            for (int i = 1; i <= floorsCount; i++)
            {
                string floorType = GetFloorType(i, floorsCount);
                Floors.Add(new Floor(i, floorType));
            }
        }

        private string GetFloorType(int floorNumber, int totalFloors)
        {
            if (floorNumber == 1) return "Лобби";
            if (floorNumber == totalFloors) return "Пентхаус";
            if (floorNumber >= totalFloors - 2) return "Офисные помещения";
            if (floorNumber <= 3) return "Торговые помещения";
            return "Офисные помещения";
        }

        public void DisplayBuildingInfo()
        {
            Console.WriteLine($"\n=== ЗДАНИЕ: {Name} ===");
            Console.WriteLine($"Этажей: {TotalFloors}");
            Console.WriteLine($"Текущий этаж лифта: {Elevator.CurrentFloor}");
            Console.WriteLine($"Состояние лифта: {Elevator.State}");
            Console.WriteLine("------------------------");
        }

        public void DisplayFloors()
        {
            Console.WriteLine("\n=== СПИСОК ЭТАЖЕЙ ===");
            foreach (var floor in Floors)
            {
                string elevatorIndicator = floor.FloorNumber == Elevator.CurrentFloor ? " [ЛИФТ ЗДЕСЬ]" : "";
                Console.WriteLine($"Этаж {floor.FloorNumber}: {floor.Type} - {floor.Description}{elevatorIndicator}");
            }
        }

        public Floor GetFloor(int floorNumber)
        {
            return Floors.FirstOrDefault(f => f.FloorNumber == floorNumber);
        }
    }
}