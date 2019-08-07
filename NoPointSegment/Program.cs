using System;
using System.Collections.Generic;
using System.Text;

namespace Codejam
{
    public enum Axis
    {
        X,
        Y,
        P//for point(parallel to both axis)
    }
    public class Point
    {
        public int x;
        public int y;
        
        public Point(int v1, int v2)
        {
            this.x = v1;
            this.y = v2;
        }
    }
    public class Segment
    {
        public Point A;
        public Point B;

        public int minX;
        public int maxX;
        public int minY;
        public int maxY;
        public Axis _isParallelTo;
        public Intersection _intersection;


        public Segment(Point A, Point B)
        {
            this.A = A;
            this.B = B;

            this.minX = Math.Min(A.x, B.x);
            this.minY = Math.Min(A.y, B.y);
            this.maxX = Math.Max(A.x, B.x);
            this.maxY = Math.Max(A.y, B.y);
            _isParallelTo = (A.x == B.x) ? Axis.Y : Axis.X;
            _intersection = new Intersection(this);
        }
        //Axis GetParallelAxis(Point c1, Point c2)
        //{
        //    if (c1.x == c2.x && c1.y == c2.y)
        //        return Axis.P;
        //    if (c1.x == c2.x)
        //        return Axis.Y;
        //    else
        //        return Axis.X;
        //}
    }
    public class Intersection
    {
        List<IIntersection> intersections = new List<IIntersection>()
        {
            new PointIntersection(),
            new SegmentIntersection()
        };
        private Segment _segment;

        public Intersection(Segment segment)


        {
            this._segment = segment;
        }

        public string GetIntersection(Segment segment)
        {
            string intersectionType;
            foreach (IIntersection _intersection in intersections)
            {
                if (_intersection.CheckIntersection(this._segment, segment, out intersectionType))
                    return intersectionType;
            }
            return "NO";
        }
    }
    public interface IIntersection
    {
        bool CheckIntersection(Segment segment, Segment segment1 ,out string intersectionType);
    }

    class SegmentIntersection : IIntersection
    {
        
        public bool CheckIntersection(Segment s1, Segment s2, out string intersectionType)
        {
            intersectionType = null;
            if (s1._isParallelTo == s2._isParallelTo)
            {
                if (s1._isParallelTo == Axis.P || s2._isParallelTo == Axis.P)
                    return false;
                if (s1._isParallelTo == Axis.X && s1.maxY == s2.maxY)
                {
                    if (s1.minX <= s2.maxX && s1.maxX >= s2.minX || s2.minX <= s1.maxX && s2.maxX >= s1.minX)
                    {
                        intersectionType = "SEGMENT";
                        return true;
                    }
                    return false;
                }
                else if (s1._isParallelTo == Axis.Y && s1.maxX == s2.maxX)
                {
                    if (s1.minY <= s2.maxY && s1.maxY >= s2.minY || s2.minY <= s1.maxY && s2.maxY >= s1.minY)
                    {
                        intersectionType = "SEGMENT";
                        return true;
                    }
                    return false;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
    }

     class PointIntersection : IIntersection
    {
        
        public bool CheckIntersection(Segment s1, Segment s2, out string intersectionType)
        {
            if (s1._isParallelTo == s2._isParallelTo)
            {
                intersectionType = CheckPointIntersectionIfParallel(s1, s2) ? "POINT" : "NO";
                return  CheckPointIntersectionIfParallel(s1, s2);
            }
            else
            {
                intersectionType = CheckPointIntersectionIfPerpendicular(s1, s2)  ? "POINT" : "NO";
                return CheckPointIntersectionIfPerpendicular(s1, s2);
            }

        }

        private bool CheckPointIntersectionIfParallel(Segment s1, Segment s2)
        {
            if (s1._isParallelTo == Axis.X && s1.maxY == s2.maxY)
            {
                if (s1.maxX == s2.minX || s1.minX == s2.maxX)
                    return true;
                return false;
            }
            else if (s1._isParallelTo == Axis.Y && s1.maxX == s2.maxX)
            {
                if (s1.maxY == s2.minY || s1.minY == s2.maxY)
                    return true;
                return false;
            }
            else if (s1._isParallelTo == Axis.P && s1.minX == s2.minX && s1.minY == s2.minY)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool CheckPointIntersectionIfPerpendicular(Segment s1, Segment s2)
        {
            if ((s1._isParallelTo == Axis.X
                && s1.minX <= s2.maxX && s1.maxX >= s2.maxX && s2.minY <= s1.minY && s2.maxY >= s1.minY)
                || (s1._isParallelTo == Axis.Y
                && s1.minY <= s2.maxY && s1.maxY >= s2.maxY && s2.minX <= s1.minX && s2.maxX >= s1.minX))
            {
                return true;
            }
            else
            {
                if (s1._isParallelTo == Axis.P)
                {
                    if (s2._isParallelTo == Axis.X)
                    {
                        return (s2.minX <= s1.minX && s1.minX <= s2.maxX) ? true : false;
                    }
                    else
                    {
                        return (s2.minX <= s1.minX && s1.minX <= s2.maxX) ? true : false;
                    }
                }
                if (s2._isParallelTo == Axis.P)
                {
                    if (s1._isParallelTo == Axis.X)
                    {
                        return (s1.minX <= s2.minX && s2.minX <= s1.maxX) ? true : false;
                    }
                    else
                    {
                        return (s1.minX <= s2.minX && s2.minX <= s1.maxX) ? true : false;
                    }
                }
                return false;
            }
        }
    }

   

    class NoPointSegment
     {
     
        public string Intersection(int[] seg1, int[] seg2)
        {
            Point A = new Point(seg1[0], seg1[1]);
            Point B = new Point(seg1[2], seg1[3]);
            Point C = new Point(seg2[0], seg2[1]);
            Point D = new Point(seg2[2], seg2[3]);

            Segment AB = new Segment(A, B);
            Segment CD = new Segment(C, D);


            return AB._intersection.GetIntersection(CD);
        }

        #region Testing code Do not change
        public static void Main(String[] args)
        {
            string input = Console.ReadLine();
            NoPointSegment solver = new NoPointSegment();
            do
            {
                var segments = input.Split('|');
                var segParts = segments[0].Split(',');
                var seg1 = new int[4] { int.Parse(segParts[0]), int.Parse(segParts[1]), int.Parse(segParts[2]), int.Parse(segParts[3]) };
                segParts = segments[1].Split(',');
                var seg2 = new int[4] { int.Parse(segParts[0]), int.Parse(segParts[1]), int.Parse(segParts[2]), int.Parse(segParts[3]) };
                Console.WriteLine(solver.Intersection(seg1, seg2));
                input = Console.ReadLine();
            } while (input != "-1");
        }
        #endregion
    }
}