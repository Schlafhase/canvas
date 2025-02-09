using System.Drawing;
using System.Runtime.Versioning;
using Canvas.Components.Interfaces.Mix;
using CSharpMath.SkiaSharp;
using SkiaSharp;

namespace Canvas.Components;

[SupportedOSPlatform("windows")]
public class Equation : PositionedSizedComponent
{
	private readonly MathPainter _painter = new();

	public Equation(string content, int size, int x = 0, int y = 0)
	{
		Content = content;
		Size = size;
		Quality = 20;
		X = x;
		Y = y;
	}

	public string Content
	{
		get => _painter.LaTeX;
		set => _painter.LaTeX = value;
	}

	public float Quality
	{
		get => _painter.FontSize;
		set => _painter.FontSize = value;
	}
	
	public SKColor Color
	{
		get => _painter.TextColor;
		set => _painter.TextColor = value;
	}

	public override void Put(Graphics g)
	{
		if (Size <= 0)
		{
			return;
		}

		using Stream? png = _painter.DrawAsStream();

		if (png == null)
		{
			return;
		}

		using Image image = Image.FromStream(png);
		
		(int width, int height) dimensions = (image.Width, image.Height);
		double aspectRatio = (double)dimensions.width / dimensions.height;
		
		int newWidth = Size;
		int newHeight = (int)(Size / aspectRatio);
		
		g.DrawImage(image, X, Y, newWidth, newHeight);
	}
}