namespace ElevatorSystem
{
    public class Room
    {
        public string Number { get; private set; }
        public string Purpose { get; private set; }
        public double Area { get; private set; }

        public Room(string number, string floorType)
        {
            Number = number;
            Purpose = GenerateRoomPurpose(floorType);
            Area = GenerateRoomArea(floorType);
        }

        private string GenerateRoomPurpose(string floorType)
        {
            var random = new Random();
            
            return floorType switch
            {
                "Лобби" => random.Next(3) switch
                {
                    0 => "Ресепшен",
                    1 => "Зона ожидания",
                    _ => "Информационная стойка"
                },
                "Пентхаус" => random.Next(3) switch
                {
                    0 => "Гостиная с панорамным видом",
                    1 => "Спальня люкс",
                    _ => "Терасса"
                },
                "Торговые помещения" => random.Next(5) switch
                {
                    0 => "Кофейня",
                    1 => "Магазин одежды",
                    2 => "Салон красоты",
                    3 => "Аптека",
                    _ => "Банк"
                },
                "Офисные помещения" => random.Next(6) switch
                {
                    0 => "Офис компании",
                    1 => "Переговорная комната",
                    2 => "Кухня-столовая",
                    3 => "Зона отдыха",
                    4 => "IT-отдел",
                    _ => "Конференц-зал"
                },
                _ => "Стандартное помещение"
            };
        }

        private double GenerateRoomArea(string floorType)
        {
            var random = new Random();
            
            return floorType switch
            {
                "Лобби" => random.Next(100, 300),
                "Пентхаус" => random.Next(80, 150),
                "Торговые помещения" => random.Next(50, 120),
                "Офисные помещения" => random.Next(20, 60),
                _ => random.Next(30, 70)
            };
        }
    }
}