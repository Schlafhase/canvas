using System.Drawing;

namespace Canvas.Components.Interfaces;

/// <summary>
/// Class for components are have both a position and a width and heigth.
/// </summary>
public abstract class PositionedRectangleSizedComponent : PositionedComponent
{
	private int _width;
	private int _height;

	/// <summary>
	/// Width of the component.
	/// </summary>
	public int Width
	{
		get => _width;
		set
		{
			_width = value;
			if (!SuppressUpdate)
			{
				Parent?.Update();
			}
		}
	}

	/// <summary>
	/// Height of the component.
	/// </summary>
	public int Height
	{
		get => _height;
		set
		{
			_height = value;
			if (!SuppressUpdate)
			{
				Parent?.Update();
			}
		}
	}
}