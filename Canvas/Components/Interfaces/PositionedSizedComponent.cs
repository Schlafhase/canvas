namespace Canvas.Components.Interfaces;

/// <summary>
/// For components that have both a position and a size.
/// </summary>
public abstract class PositionedSizedComponent : PositionedComponent
{
	protected int _size;

	/// <summary>
	/// Size of the component.
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