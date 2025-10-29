namespace ShapePrototype
{
    public class Square : Shape
    {
        public Square(bool isSuper = false)
        {
            Name = isSuper ? "Супер-Квадрат" : "Квадрат";
            Cells = isSuper ? 8 : 4;
            IsSuper = isSuper;
        }

        public override void Display()
        {
            string symbol = IsSuper ? "■" : "□";
            Console.WriteLine($"Фигура: {Name}");
            Console.WriteLine($"Клетки: {Cells}");
            Console.WriteLine($"Внешний вид:");
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < (IsSuper ? 4 : 2); j++)
                {
                    Console.Write(symbol + " ");
                }
                Console.WriteLine();
            }
            if (IsSuper)
            {
                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        Console.Write(symbol + " ");
                    }
                    Console.WriteLine();
                }
            }
            Console.WriteLine();
        }
    }

    public class Line : Shape
    {
        public Line(bool isSuper = false)
        {
            Name = isSuper ? "Супер-Линия" : "Линия";
            Cells = isSuper ? 8 : 4;
            IsSuper = isSuper;
        }

        public override void Display()
        {
            string symbol = IsSuper ? "|" : "-";
            Console.WriteLine($"Фигура: {Name}");
            Console.WriteLine($"Клетки: {Cells}");
            Console.WriteLine($"Внешний вид:");
            if (IsSuper)
            {
                for (int i = 0; i < 8; i++)
                {
                    Console.WriteLine("    " + symbol);
                }
            }
            else
            {
                for (int i = 0; i < 4; i++)
                {
                    Console.Write(symbol + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }

    public class Triangle : Shape
    {
        public Triangle(bool isSuper = false)
        {
            Name = isSuper ? "Супер-Треугольник" : "Треугольник";
            Cells = isSuper ? 10 : 5;
            IsSuper = isSuper;
        }

        public override void Display()
        {
            string symbol = IsSuper ? "▲" : "△";
            Console.WriteLine($"Фигура: {Name}");
            Console.WriteLine($"Клетки: {Cells}");
            Console.WriteLine($"Внешний вид:");
            
            if (IsSuper)
            {
                Console.WriteLine("    " + symbol);
                Console.WriteLine("   " + symbol + " " + symbol);
                Console.WriteLine("  " + symbol + "   " + symbol);
                Console.WriteLine(" " + symbol + " " + symbol + " " + symbol + " " + symbol);
                Console.WriteLine(symbol + " " + symbol + " " + symbol + " " + symbol + " " + symbol);
            }
            else
            {
                Console.WriteLine("  " + symbol);
                Console.WriteLine(" " + symbol + " " + symbol);
                Console.WriteLine(symbol + " " + symbol + " " + symbol);
            }
            Console.WriteLine();
        }
    }

    public class ZShape : Shape
    {
        public ZShape(bool isSuper = false)
        {
            Name = isSuper ? "Супер-Z" : "Z-фигура";
            Cells = isSuper ? 12 : 6;
            IsSuper = isSuper;
        }

        public override void Display()
        {
            string symbol = IsSuper ? "Z" : "z";
            Console.WriteLine($"Фигура: {Name}");
            Console.WriteLine($"Клетки: {Cells}");
            Console.WriteLine($"Внешний вид:");
            
            if (IsSuper)
            {
                Console.WriteLine(symbol + " " + symbol + " " + symbol + "     ");
                Console.WriteLine("  " + symbol + " " + symbol + " " + symbol + "   ");
                Console.WriteLine("    " + symbol + " " + symbol + " " + symbol);
            }
            else
            {
                Console.WriteLine(symbol + " " + symbol + "   ");
                Console.WriteLine("  " + symbol + " " + symbol);
            }
            Console.WriteLine();
        }
    }
}