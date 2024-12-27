using System.Drawing;
using System.Runtime.Versioning;
using Canvas.Components.Interfaces;

namespace Canvas.Components;

[SupportedOSPlatform("windows")]
public sealed class GlowDot : PositionedSizedComponent
{
	#region Properties

	public override Canvas? Parent
	{
		get => _innerCircle.Parent;
		set
		{
			_innerCircle.Parent = value;
			_innerGlow.Parent = value;
			_glow.Parent = value;
		}
	}

	public override int X
	{
		get => _innerCircle.X;
		set
		{
			_innerCircle.X = value;
			_innerGlow.X = value;
			_glow.X = value;
		}
	}

	public override int Y
	{
		get => _innerCircle.Y;
		set
		{
			_innerCircle.Y = value;
			_innerGlow.Y = value;
			_glow.Y = value;
		}
	}

	public int Radius
	{
		get => Size;
		set => Size = value;
	}

	public Color Color { get; set; }
	private readonly Color _glowColor;
	private readonly Color _innerGlowColor;

	private readonly Circle _innerCircle;
	private readonly Circle _innerGlow;
	private readonly Circle _glow;

	#endregion

	public GlowDot(int x, int y, int radius, int glowRadius, Color color, int alpha = 255)
	{
		_innerCircle = new Circle(x, y, radius, Brushes.White);
		_innerGlow = new Circle(x, y, glowRadius / 2, Brushes.White);
		_glow = new Circle(x, y, glowRadius, Brushes.White);

		const int brightness = 170;

		_glowColor = Color.FromArgb(
			alpha,
			Math.Clamp(color.R + brightness, 0, 255),
			Math.Clamp(color.G + brightness, 0, 255),
			Math.Clamp(color.B + brightness, 0, 255));

		_innerGlowColor = Color.FromArgb(
			alpha,
			Math.Clamp(color.R + brightness / 2, 0, 255),
			Math.Clamp(color.G + brightness / 2, 0, 255),
			Math.Clamp(color.B + brightness / 2, 0, 255));

		X = x;
		Y = y;
		Radius = radius;
		Color = color;
	}

	public override void Put(Graphics g)
	{
		_innerCircle.SetGlow(_glowColor, 0.8f);
		_innerGlow.SetGlow(_innerGlowColor, 0, true, 30);
		_glow.SetGlow(Color, 0, true, 30);
		_glow.Put(g);
		_innerGlow.Put(g);
		_innerCircle.Put(g);
	}
}