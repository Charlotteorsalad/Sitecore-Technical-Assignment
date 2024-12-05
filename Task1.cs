using System;
using System.Collections.Generic;

namespace SiteCoreTechnicalAssignment
{
    public interface IGeometricFigure // Interface defining core geometric operations
    {
        void Move(double deltaX, double deltaY); // Movement in 2D space
        void Rotate(double angle, Point center); // Rotation around a point
    }

    public class Point : IGeometricFigure // Basic Point class - fundamental geometric element
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        public void Move(double deltaX, double deltaY) // Simple translation in 2D space
        {
            X += deltaX;
            Y += deltaY;
        }

        public void Rotate(double angle, Point center)
        {
            // Rotation using standard 2D rotation matrix:
            // [cos θ  -sin θ] [x]
            // [sin θ   cos θ] [y]

            double radians = angle * Math.PI / 180; // Convert degrees to radians
            double cosAngle = Math.Cos(radians);
            double sinAngle = Math.Sin(radians);

            // Translate point to origin, rotate, then translate back
            double dx = X - center.X;
            double dy = Y - center.Y;

            X = center.X + (dx * cosAngle - dy * sinAngle);
            Y = center.Y + (dx * sinAngle + dy * cosAngle);
        }
    }

    public class Line : IGeometricFigure // Line defined by two points
    {
        private Point Start { get; set; }
        private Point End { get; set; }

        public Line(Point start, Point end)
        {
            Start = start;
            End = end;
        }

        public void Move(double deltaX, double deltaY) // Move both endpoints
        {
            Start.Move(deltaX, deltaY);
            End.Move(deltaX, deltaY);
        }

        public void Rotate(double angle, Point center) // Rotate both endpoints around center
        {
            Start.Rotate(angle, center);
            End.Rotate(angle, center);
        }
    }

    public class Circle : IGeometricFigure // Circle defined by center point and radius
    {
        public Point Center { get; set; }
        public double Radius { get; private set; }

        public Circle(Point center, double radius)
        {
            Center = center;
            Radius = radius;
        }

        public void Move(double deltaX, double deltaY) // Move center point (radius unchanged)
        {
            Center.Move(deltaX, deltaY);
        }

        public void Rotate(double angle, Point center) // Rotate center point (radius unchanged)
        {
            Center.Rotate(angle, center);
        }
    }

    public class Aggregation // Aggregation allows grouping of multiple geometric figures
    {
        private List<IGeometricFigure> Figures { get; set; } = new List<IGeometricFigure>();

        public void AddFigure(IGeometricFigure figure)
        {
            Figures.Add(figure);
        }

        public void Move(double deltaX, double deltaY) // Move all contained figures
        {
            foreach (var figure in Figures)
            {
                figure.Move(deltaX, deltaY);
            }
        }

        public void Rotate(double angle, Point center) // Rotate all contained figures around specified center
        {
            foreach (var figure in Figures)
            {
                figure.Rotate(angle, center);
            }
        }
    }
}