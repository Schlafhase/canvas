using System.Runtime.Versioning;

namespace Canvas.Components.Interfaces.RectangleSized;

[SupportedOSPlatform("windows")]
public abstract class RectangleSizedComponent : CanvasComponent, IRectangleSizedComponent
{
	private int _height;
	private int _width;

	/// <summary>
	///     Width of the component.
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
	///     Height of the component.
	/// </summary>
	public int Height
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
		}
	}
}