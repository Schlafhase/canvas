using System.Runtime.Versioning;
using Canvas.Components.Interfaces.Positioned;
using Canvas.Components.Interfaces.Sized;

namespace Canvas.Components.Interfaces.Relative;

[SupportedOSPlatform("windows")]
public sealed class RelativeSizedRelativePositionedComponent<T> : RelativePositionedComponent<T>
	where T : ICanvasComponent, IPositionedComponent, ISizedComponent
{
	public RelativeSizedRelativePositionedComponent(T component,
		RelativeSizingOptions relativeSizingOptions,
		int margin = 0) : base(component, margin)
	{
		_sizingOptions = relativeSizingOptions;
	}

	private readonly RelativeSizingOptions _sizingOptions;
	private double _size;

	public double Size
	{
		get => _size;
		set
		{
			_size = value;

			switch (_sizingOptions)
			{
				case RelativeSizingOptions.Width:
					_component.Size = (int)(_size * _boundaries.Width);
					break;
				case RelativeSizingOptions.Height:
					_component.Size = (int)(_size * _boundaries.Height);
					break;
				case RelativeSizingOptions.Both:
					_component.Size = (int)(_size * _boundaries.Width * _boundaries.Height);
					break;
			}
		}
	}
	
	public override System.Drawing.Rectangle Boundaries
	{
		get => _boundaries;
		set
		{
			_boundaries = value;
			X = _x;
			Y = _y;
			Size = _size;
		}
	}
}