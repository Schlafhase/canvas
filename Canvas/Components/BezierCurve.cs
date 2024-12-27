using System.Drawing;
using System.Runtime.Versioning;
using Canvas.Components.Interfaces;

namespace Canvas.Components;

[SupportedOSPlatform("windows")]
public class BezierCurve : PositionedComponent
{
    public List<Point> Points { get; set; }
    public object PointsLocker { get; } = new();
    
    public Pen Pen { get; set; } = Pens.Black;

    public BezierCurve(List<Point> points)
    {
        Points = points;
    }

    public override void Put(Graphics g)
    {
        List<Point> points;
        lock (PointsLocker)
        {
            points = Points.ToList();
        }

        if (points.Count == 0)
        {
            points = new List<Point>()
            {
                Point.Empty,
                Point.Empty,
                Point.Empty,
                Point.Empty
            };
        }

        while (points.Count % 3 != 1)
        {
            points.Add(points.Last());
        }
        
        if (X != 0 || Y != 0)
        {
            points = points.Select(point => new Point(point.X + X, point.Y + Y)).ToList();
        }

        g.DrawBeziers(Pen, points.ToArray());
    }
}