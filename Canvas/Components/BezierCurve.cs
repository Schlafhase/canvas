using System.Drawing;
using System.Runtime.Versioning;
using Canvas.Components.Interfaces;
using Canvas.Components.Interfaces.Positioned;

namespace Canvas.Components;

[SupportedOSPlatform("windows")]
public class BezierCurve : PositionedComponent
{
	public BezierCurve(List<Point> points)
	{
		Points = points;
	}

	public List<Point> Points { get; set; }
	public double Scale = 1;
	public object PointsLocker { get; } = new();

	public Pen Pen { get; set; } = Pens.Black;

	public override void Put(Graphics g)
	{
		List<Point> points;

		lock (PointsLocker)
		{
			points = Points.ToList();
		}

		if (points.Count == 0)
		{
			points = new List<Point>
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

		if (Scale != 1)
		{
			points = points.Select(point => new Point((int)(point.X * Scale), (int)(point.Y * Scale))).ToList();
		}
		
		if (X != 0 || Y != 0)
		{
			points = points.Select(point => new Point(point.X + X, point.Y + Y)).ToList();
		}

		g.DrawBeziers(Pen, points.ToArray());
	}
}