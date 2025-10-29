namespace ShapePrototype
{
    public interface IShape : ICloneable
    {
        string Name { get; }
        int Cells { get; }
        bool IsSuper { get; }
        void Display();
        IShape Clone();
    }
}