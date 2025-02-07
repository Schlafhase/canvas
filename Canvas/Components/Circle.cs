using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.Versioning;
using Canvas.Components.Interfaces;
using Canvas.Components.Interfaces.Mix;

namespace Canvas.Components;

[SupportedOSPlatform("windows")]
public class Circle : PositionedSizedComponent
{
	public Circle(int x, int y, int radius, Brush brush)
	{
		X = x;
		Y = y;
		Radius = radius;
		Brush = brush;
	}

	public void SetGlow(Color color, float radius = 0f, bool brightenCenter = false, int alpha = 255)
	{
		GraphicsPath path = new();
		path.AddEllipse(X - Radius, Y - Radius, Radius * 2, Radius * 2);

		Color brushColor = color;
		PathGradientBrush pthGrBrush = new(path);

		if (brightenCenter)
		{
			const int brightness = 70;

			brushColor = Color.FromArgb(
				Math.Clamp(color.R + brightness, 0, 255),
				Math.Clamp(color.G + brightness, 0, 255),
				Math.Clamp(color.B + brightness, 0, 255));
		}

		pthGrBrush.CenterColor = Color.FromArgb(alpha, brushColor);

		pthGrBrush.SurroundColors = new[] { Color.FromArgb(0, color) };

		if (radius != 0)
		{
			pthGrBrush.FocusScales = new PointF(radius, radius);
		}

		Brush = pthGrBrush;
	}

	public override void Put(Graphics g)
	{
		g.FillEllipse(Brush, X - Radius, Y - Radius, Radius * 2, Radius * 2);
	}

	#region Properties

	public int Radius
	{
		get => Size;
		set => Size = value;
	}

	public Brush Brush { get; set; }

	#endregion
}