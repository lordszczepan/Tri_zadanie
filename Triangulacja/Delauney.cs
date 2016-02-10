using System;
using System.Collections.Generic;

namespace Triangulacja
{
    public class Delauney
    {
        public static List<Geometry.Triangle> Triangulate(List<Triangulacja.Geometry.Point> Vertex)
        {
            int nv = Vertex.Count;
            if (nv < 3)
                throw new ArgumentException("Potrzeba conajmniej 3 punktów do triangulacji");

            int trimax = 4 * nv;

            double xmin = Vertex[0].X;
            double ymin = Vertex[0].Y;
            double xmax = xmin;
            double ymax = ymin;
            for (int i = 1; i < nv; i++)
            {
                if (Vertex[i].X < xmin) xmin = Vertex[i].X;
                if (Vertex[i].X > xmax) xmax = Vertex[i].X;
                if (Vertex[i].Y < ymin) ymin = Vertex[i].Y;
                if (Vertex[i].Y > ymax) ymax = Vertex[i].Y;
            }

            double dx = xmax - xmin;
            double dy = ymax - ymin;
            double dmax = (dx > dy) ? dx : dy;

            double xmid = (xmax + xmin) * 0.5;
            double ymid = (ymax + ymin) * 0.5;

            Vertex.Add(new Triangulacja.Geometry.Point((xmid - 2 * dmax), (ymid - dmax)));
            Vertex.Add(new Triangulacja.Geometry.Point(xmid, (ymid + 2 * dmax)));
            Vertex.Add(new Triangulacja.Geometry.Point((xmid + 2 * dmax), (ymid - dmax)));
            List<Geometry.Triangle> Triangle = new List<Geometry.Triangle>();
            Triangle.Add(new Geometry.Triangle(nv, nv + 1, nv + 2));

            for (int i = 0; i < nv; i++)
            {
                List<Geometry.Edge> Edges = new List<Geometry.Edge>();

                for (int j = 0; j < Triangle.Count; j++)
                {
                    if (InCircle(Vertex[i], Vertex[Triangle[j].p1], Vertex[Triangle[j].p2], Vertex[Triangle[j].p3]))
                    {
                        Edges.Add(new Geometry.Edge(Triangle[j].p1, Triangle[j].p2));
                        Edges.Add(new Geometry.Edge(Triangle[j].p2, Triangle[j].p3));
                        Edges.Add(new Geometry.Edge(Triangle[j].p3, Triangle[j].p1));
                        Triangle.RemoveAt(j);
                        j--;
                    }
                }
                if (i >= nv) continue;

                for (int j = Edges.Count - 2; j >= 0; j--)
                {
                    for (int k = Edges.Count - 1; k >= j + 1; k--)
                    {
                        if (Edges[j].Equals(Edges[k]))
                        {
                            Edges.RemoveAt(k);
                            Edges.RemoveAt(j);
                            k--;
                            continue;
                        }
                    }
                }


                for (int j = 0; j < Edges.Count; j++)
                {
                    if (Triangle.Count >= trimax)
                        throw new ApplicationException("Przekroczono dozwolone krawędzie");
                    Triangle.Add(new Geometry.Triangle(Edges[j].p1, Edges[j].p2, i));
                }
                Edges.Clear();
                Edges = null;
            }

            for (int i = Triangle.Count - 1; i >= 0; i--)
            {
                if (Triangle[i].p1 >= nv || Triangle[i].p2 >= nv || Triangle[i].p3 >= nv)
                    Triangle.RemoveAt(i);
            }

            Vertex.RemoveAt(Vertex.Count - 1);
            Vertex.RemoveAt(Vertex.Count - 1);
            Vertex.RemoveAt(Vertex.Count - 1);
            Triangle.TrimExcess();
            return Triangle;
        }


        private static bool InCircle(Geometry.Point p, Geometry.Point p1, Geometry.Point p2, Geometry.Point p3)
        {


            if (System.Math.Abs(p1.Y - p2.Y) < double.Epsilon && System.Math.Abs(p2.Y - p3.Y) < double.Epsilon)
            {

                return false;
            }

            double m1, m2;
            double mx1, mx2;
            double my1, my2;
            double xc, yc;

            if (System.Math.Abs(p2.Y - p1.Y) < double.Epsilon)
            {
                m2 = -(p3.X - p2.X) / (p3.Y - p2.Y);
                mx2 = (p2.X + p3.X) * 0.5;
                my2 = (p2.Y + p3.Y) * 0.5;
                xc = (p2.X + p1.X) * 0.5;
                yc = m2 * (xc - mx2) + my2;
            }
            else if (System.Math.Abs(p3.Y - p2.Y) < double.Epsilon)
            {
                m1 = -(p2.X - p1.X) / (p2.Y - p1.Y);
                mx1 = (p1.X + p2.X) * 0.5;
                my1 = (p1.Y + p2.Y) * 0.5;
                xc = (p3.X + p2.X) * 0.5;
                yc = m1 * (xc - mx1) + my1;
            }
            else
            {
                m1 = -(p2.X - p1.X) / (p2.Y - p1.Y);
                m2 = -(p3.X - p2.X) / (p3.Y - p2.Y);
                mx1 = (p1.X + p2.X) * 0.5;
                mx2 = (p2.X + p3.X) * 0.5;
                my1 = (p1.Y + p2.Y) * 0.5;
                my2 = (p2.Y + p3.Y) * 0.5;
                xc = (m1 * mx1 - m2 * mx2 + my2 - my1) / (m1 - m2);
                yc = m1 * (xc - mx1) + my1;
            }

            double dx = p2.X - xc;
            double dy = p2.Y - yc;
            double rsqr = dx * dx + dy * dy;
            dx = p.X - xc;
            dy = p.Y - yc;
            double drsqr = dx * dx + dy * dy;

            return (drsqr <= rsqr);
        }
    }
}
