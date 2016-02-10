namespace Triangulacja.Geometry
{

    public class Point
    {

        protected double _X;
        protected double _Y;
        
        public Point(double x, double y)
        {
            _X = x;
            _Y = y;
        }
        
        public double X
        {
            get { return _X; }
            set { _X = value; }
        }
        public double Y
        {
            get { return _Y; }
            set { _Y = value; }
        }
        
        public bool Equals2D(Point other)
        {
            return (X == other.X && Y == other.Y);
        }
    }
}
