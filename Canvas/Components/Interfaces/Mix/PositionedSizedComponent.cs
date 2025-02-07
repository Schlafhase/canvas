using System.Runtime.Versioning;
using Canvas.Components.Interfaces.Positioned;
using Canvas.Components.Interfaces.Sized;

namespace Canvas.Components.Interfaces.Mix;

/// <summary>
///     For components that have both a position and a size.
/// </summary>
[SupportedOSPlatform("windows")]
public abstract class PositionedSizedComponent : PositionedComponent, ISizedComponent
{
	protected int _size;

	/// <summary>
	///     Size of the component.
	/// </summary>
	public int Size
	{
		get => _size;
		set
		{
			_size = value;

			if (!SuppressUpdate)
			{
				Parent?.Update();
			}
		}
	}
}