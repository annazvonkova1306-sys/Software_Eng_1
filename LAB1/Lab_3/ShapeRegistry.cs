namespace ShapePrototype
{
    public class ShapeRegistry
    {
        private Dictionary<string, IShape> _prototypes = new Dictionary<string, IShape>();

        public ShapeRegistry()
        {
            InitializePrototypes();
        }

        private void InitializePrototypes()
        {
            // Обычные фигуры
            _prototypes["square"] = new Square();
            _prototypes["line"] = new Line();
            _prototypes["triangle"] = new Triangle();
            _prototypes["zshape"] = new ZShape();

            // Супер-фигуры
            _prototypes["super_square"] = new Square(true);
            _prototypes["super_line"] = new Line(true);
            _prototypes["super_triangle"] = new Triangle(true);
            _prototypes["super_zshape"] = new ZShape(true);
        }

        public IShape GetShape(string key)
        {
            if (_prototypes.ContainsKey(key))
            {
                return _prototypes[key].Clone();
            }
            throw new ArgumentException($"Фигура с ключом '{key}' не найдена");
        }

        public List<string> GetAvailableShapeKeys()
        {
            return _prototypes.Keys.ToList();
        }

        public IShape GetRandomShape()
        {
            var random = new Random();
            var keys = _prototypes.Keys.ToList();
            var randomKey = keys[random.Next(keys.Count)];
            return GetShape(randomKey);
        }

        public IShape GetRandomRegularShape()
        {
            var random = new Random();
            var regularKeys = _prototypes.Keys.Where(k => !k.StartsWith("super_")).ToList();
            var randomKey = regularKeys[random.Next(regularKeys.Count)];
            return GetShape(randomKey);
        }

        public IShape GetRandomSuperShape()
        {
            var random = new Random();
            var superKeys = _prototypes.Keys.Where(k => k.StartsWith("super_")).ToList();
            var randomKey = superKeys[random.Next(superKeys.Count)];
            return GetShape(randomKey);
        }
    }
}