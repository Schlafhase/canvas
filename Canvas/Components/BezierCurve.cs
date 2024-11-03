using System.Drawing;
using Canvas.Components.Interfaces;

namespace Canvas.Components;

public class BezierCurve : ICanvasComponent
{
    public Canvas? Parent { get; set; }
    public bool SuppressUpdate { get; set; }
    public List<Point> Points { get; set; }
    public Pen Pen { get; set; } = Pens.Black;

    public BezierCurve(List<Point> points)
    {
        Points = points;
    }
    
    public void Put(Graphics g)
    {
        g.DrawBeziers(Pen, Points.ToArray());
    }
}