using System.Runtime.Versioning;
using Canvas.Components.Interfaces.Positioned;
using Canvas.Components.Interfaces.RectangleSized;

namespace Canvas.Components.Interfaces.Mix;

/// <summary>
///     Class for components are have both a position and a width and height.
/// </summary>
[SupportedOSPlatform("windows")]
public abstract class PositionedRectangleSizedComponent : PositionedComponent, IRectangleSizedComponent
{
	private int _height;
	private int _width;

	/// <summary>
	///     Width of the component.
	/// </summary>
	public virtual int Width
	{
		get => _width;
		set
		{
			_width = value;

			if (SuppressUpdate)
			{
				return;
			}

			Parent?.Update();

			if (this is Canvas canvas)
			{
				canvas.Update();
			}
		}
	}

	/// <summary>
	///     Height of the component.
	/// </summary>
	public virtual int Height
	{
		get => _height;
		set
		{
			_height = value;

			if (SuppressUpdate)
			{
				return;
			}

			Parent?.Update();

			if (this is Canvas canvas)
			{
				canvas.Update();
			}
		}
	}
}