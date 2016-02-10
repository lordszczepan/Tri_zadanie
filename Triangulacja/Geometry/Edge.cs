using System;

namespace Triangulacja.Geometry
{
    public class Edge : IEquatable<Edge>
    {
        
        public int p1;
        public int p2;
        public Edge(int point1, int point2)
        {
            p1 = point1; p2 = point2;
        }
        public Edge()
            : this(0, 0)
        {
        }

        #region IEquatable<dEdge> Members
        
        public bool Equals(Edge other)
        {
            return
                ((this.p1 == other.p2) && (this.p2 == other.p1)) ||
                ((this.p1 == other.p1) && (this.p2 == other.p2));
        }

        #endregion
    }
}