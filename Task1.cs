using System;
using System.Collections.Generic;

namespace SiteCoreTechnicalAssignment
{
    public interface IGeometricFigure
    {
        void Move(double deltaX, double deltaY);
        void Rotate(double angle, Point center);
    }

    public class Point : IGeometricFigure
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        public void Move(double deltaX, double deltaY)
        {
            X += deltaX;
            Y += deltaY;
        }

        public void Rotate(double angle, Point center)
        {
            double radians = angle * Math.PI / 180;
            double cosAngle = Math.Cos(radians);
            double sinAngle = Math.Sin(radians);

            double dx = X - center.X;
            double dy = Y - center.Y;

            X = center.X + (dx * cosAngle - dy * sinAngle);
            Y = center.Y + (dx * sinAngle + dy * cosAngle);
        }
    }

    public class Line : IGeometricFigure
    {
        private Point Start { get; set; }
        private Point End { get; set; }

        public Line(Point start, Point end)
        {
            Start = start;
            End = end;
        }

        public void Move(double deltaX, double deltaY)
        {
            Start.Move(deltaX, deltaY);
            End.Move(deltaX, deltaY);
        }

        public void Rotate(double angle, Point center)
        {
            Start.Rotate(angle, center);
            End.Rotate(angle, center);
        }
    }

    public class Circle : IGeometricFigure
    {
        public Point Center { get; set; }
        public double Radius { get; private set; }

        public Circle(Point center, double radius)
        {
            Center = center;
            Radius = radius;
        }

        public void Move(double deltaX, double deltaY)
        {
            Center.Move(deltaX, deltaY);
        }

        public void Rotate(double angle, Point center)
        {
            Center.Rotate(angle, center);
        }
    }

    public class Aggregation
    {
        private List<IGeometricFigure> Figures { get; set; } = new List<IGeometricFigure>();

        public void AddFigure(IGeometricFigure figure)
        {
            Figures.Add(figure);
        }

        public void Move(double deltaX, double deltaY)
        {
            foreach (var figure in Figures)
            {
                figure.Move(deltaX, deltaY);
            }
        }

        public void Rotate(double angle, Point center)
        {
            foreach (var figure in Figures)
            {
                figure.Rotate(angle, center);
            }
        }
    }
}