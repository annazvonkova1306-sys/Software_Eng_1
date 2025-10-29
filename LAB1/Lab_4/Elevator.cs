namespace ElevatorSystem
{
    public sealed class Elevator
    {
        private static Elevator _instance;
        private static readonly object _lock = new object();
        
        public int CurrentFloor { get; private set; } = 1;
        public ElevatorState State { get; private set; } = ElevatorState.Stopped;
        public int MaxFloor { get; private set; }
        public int MinFloor { get; } = 1;
        public List<int> RequestedFloors { get; private set; }
        public int Capacity { get; } = 8;
        public int CurrentPassengers { get; private set; }

        private Elevator(int maxFloor)
        {
            MaxFloor = maxFloor;
            RequestedFloors = new List<int>();
            CurrentPassengers = 0;
        }

        public static Elevator GetInstance(int maxFloor = 10)
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new Elevator(maxFloor);
                    }
                }
            }
            return _instance;
        }

        public void CallToFloor(int floorNumber)
        {
            if (IsValidFloor(floorNumber))
            {
                if (!RequestedFloors.Contains(floorNumber))
                {
                    RequestedFloors.Add(floorNumber);
                    Console.WriteLine($"Лифт вызван на этаж {floorNumber}");
                }
                ProcessRequests();
            }
            else
            {
                Console.WriteLine($"Неверный номер этажа: {floorNumber}. Допустимые этажи: {MinFloor}-{MaxFloor}");
            }
        }

        public void MoveToFloor(int targetFloor)
        {
            if (!IsValidFloor(targetFloor))
            {
                Console.WriteLine($"Неверный номер этажа: {targetFloor}");
                return;
            }

            if (targetFloor == CurrentFloor)
            {
                Console.WriteLine($"Лифт уже на этаже {CurrentFloor}");
                return;
            }

            State = targetFloor > CurrentFloor ? ElevatorState.MovingUp : ElevatorState.MovingDown;
            Console.WriteLine($"\nЛифт отправляется с этажа {CurrentFloor} на этаж {targetFloor}");

            int direction = targetFloor > CurrentFloor ? 1 : -1;
            for (int floor = CurrentFloor + direction; 
                 direction > 0 ? floor <= targetFloor : floor >= targetFloor; 
                 floor += direction)
            {
                System.Threading.Thread.Sleep(1000); // Имитация движения
                CurrentFloor = floor;
                Console.WriteLine($"Лифт проезжает этаж {CurrentFloor}");

                if (RequestedFloors.Contains(CurrentFloor))
                {
                    StopAtFloor();
                }
            }

            if (CurrentFloor == targetFloor && State != ElevatorState.Stopped)
            {
                StopAtFloor();
            }
        }

        private void StopAtFloor()
        {
            State = ElevatorState.Stopped;
            RequestedFloors.Remove(CurrentFloor);
            Console.WriteLine($"\n✅ Лифт остановился на этаже {CurrentFloor}");
            Console.WriteLine($"Двери открываются...");
            System.Threading.Thread.Sleep(500);
            Console.WriteLine($"Двери закрываются...");
        }

        private void ProcessRequests()
        {
            if (RequestedFloors.Any())
            {
                int nextFloor = RequestedFloors.OrderBy(f => Math.Abs(f - CurrentFloor)).First();
                MoveToFloor(nextFloor);
            }
        }

        public void AddPassengers(int count)
        {
            if (CurrentPassengers + count <= Capacity)
            {
                CurrentPassengers += count;
                Console.WriteLine($"В лифт вошло {count} человек. Теперь в лифте: {CurrentPassengers}/{Capacity}");
            }
            else
            {
                Console.WriteLine($"Перегруз! Не может войти {count} человек. Свободно мест: {Capacity - CurrentPassengers}");
            }
        }

        public void RemovePassengers(int count)
        {
            CurrentPassengers = Math.Max(0, CurrentPassengers - count);
            Console.WriteLine($"Из лифта вышло {count} человек. Теперь в лифте: {CurrentPassengers}/{Capacity}");
        }

        public void EmergencyStop()
        {
            State = ElevatorState.EmergencyStopped;
            Console.WriteLine("\n🚨 АВАРИЙНАЯ ОСТАНОВКА ЛИФТА!");
            Console.WriteLine($"Лифт остановлен на этаже {CurrentFloor}");
        }

        public void ResetEmergency()
        {
            if (State == ElevatorState.EmergencyStopped)
            {
                State = ElevatorState.Stopped;
                Console.WriteLine("Аварийный режим сброшен. Лифт готов к работе.");
            }
        }

        private bool IsValidFloor(int floorNumber)
        {
            return floorNumber >= MinFloor && floorNumber <= MaxFloor;
        }

        public void DisplayStatus()
        {
            Console.WriteLine($"\n=== СТАТУС ЛИФТА ===");
            Console.WriteLine($"Текущий этаж: {CurrentFloor}");
            Console.WriteLine($"Состояние: {State}");
            Console.WriteLine($"Пассажиров: {CurrentPassengers}/{Capacity}");
            Console.WriteLine($"Запрошенные этажи: {(RequestedFloors.Any() ? string.Join(", ", RequestedFloors) : "нет")}");
        }
    }

    public enum ElevatorState
    {
        Stopped,
        MovingUp,
        MovingDown,
        EmergencyStopped
    }
}