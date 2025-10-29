namespace ShapePrototype
{
    public abstract class Shape : IShape
    {
        public string Name { get; protected set; }
        public int Cells { get; protected set; }
        public bool IsSuper { get; protected set; }

        public abstract void Display();

        public virtual IShape Clone()
        {
            return (IShape)this.MemberwiseClone();
        }

        object ICloneable.Clone()
        {
            return Clone();
        }
    }
}