namespace ShapePrototype
{
    public class ShapeGenerator
    {
        private ShapeRegistry _registry;
        private Random _random;

        public ShapeGenerator()
        {
            _registry = new ShapeRegistry();
            _random = new Random();
        }

        public IShape GenerateRandomShape()
        {
            // 30% шанс получить супер-фигуру
            if (_random.Next(100) < 30)
            {
                return _registry.GetRandomSuperShape();
            }
            else
            {
                return _registry.GetRandomRegularShape();
            }
        }

        public List<IShape> GenerateMultipleShapes(int count)
        {
            var shapes = new List<IShape>();
            for (int i = 0; i < count; i++)
            {
                shapes.Add(GenerateRandomShape());
            }
            return shapes;
        }

        public IShape CreateCopy(IShape shape)
        {
            return shape.Clone();
        }
    }
}