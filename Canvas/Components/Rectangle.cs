using System.Drawing;
using System.Runtime.Versioning;
using Canvas.Components.Interfaces;
using Canvas.Components.Interfaces.Mix;

namespace Canvas.Components;

[SupportedOSPlatform("windows")]
public sealed class Rectangle : PositionedRectangleSizedComponent
{
	private readonly Brush _brush;

	public Rectangle(int x, int y, int width, int height, Color color)
	{
		X = x;
		Y = y;
		Width = width;
		Height = height;
		Color = color;
		_brush = new SolidBrush(Color);
	}

	public Color Color { get; set; }

	public override void Put(Graphics g)
	{
		g.FillRectangle(_brush, X, Y, Width, Height);
	}
}