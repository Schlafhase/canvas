using System.Runtime.Versioning;

namespace Canvas.Components.Interfaces;

/// <summary>
/// Class for components are have both a position and a width and height.
/// </summary>
[SupportedOSPlatform("windows")]
public abstract class PositionedRectangleSizedComponent : PositionedComponent
{
	private int _width;
	private int _height;

	/// <summary>
	/// Width of the component.
	/// </summary>
	public virtual int Width
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
	public virtual int Height
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