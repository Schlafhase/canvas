using Canvas.Components.Interfaces.Positioned;
using Canvas.Components.Interfaces.RectangleSized;

namespace Canvas.Components.Interfaces.Relative;

public class RelativeRectangleSizedKeepAspectRatioRelativePositionedComponent<T> : RelativePositionedComponent<T>
	where T : CanvasComponent, IRectangleSizedComponent, IPositionedComponent
{
	public RelativeRectangleSizedKeepAspectRatioRelativePositionedComponent(T component, int margin = 0) : base(component, margin) { }
	
	private double _size;
	private double _aspectRatio;
	
	private int _width => (int)(Size * _aspectRatio * _boundaries.Width);
	private int _height => (int)(Size / _aspectRatio * _boundaries.Width);
	
	public override System.Drawing.Rectangle Boundaries
	{
		get => _boundaries;
		set
		{
			_boundaries = value;
			X = _x;
			Y = _y;
			_component.Width = _width;
			_component.Height = _height;
		}
	}
	
	public double AspectRatio
	{
		get => _aspectRatio;
		set
		{
			_aspectRatio = value;
			_component.Width = _width;
			_component.Height = _height;
		}
	}

	public double Size
	{
		get => _size;
		set 
		{
			_size = value;
			_component.Width = _width;
			_component.Height = _height;
		}
	}
}