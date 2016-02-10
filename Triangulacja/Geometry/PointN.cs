namespace Triangulacja.Geometry
{
    public class Point<T> : Point
    {
        private T _attr;
        public Point(double x, double y, T attribute)
            : base(x, y)
        {
            _attr = attribute;
        }
        public Point(double x, double y)
            : this(x, y, default(T))
        {
        }
        public T Attribute
        {
            get { return _attr; }
            set { _attr = value; }
        }

    }
}
