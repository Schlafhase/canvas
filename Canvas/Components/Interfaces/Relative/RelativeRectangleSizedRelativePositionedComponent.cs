using System.Runtime.Versioning;
using Canvas.Components.Interfaces.Positioned;
using Canvas.Components.Interfaces.RectangleSized;

namespace Canvas.Components.Interfaces.Relative;

[SupportedOSPlatform("windows")]
public sealed class RelativeRectangleSizedRelativePositionedComponent<T> : RelativePositionedComponent<T>
	where T : CanvasComponent, IPositionedComponent, IRectangleSizedComponent
{
	public RelativeRectangleSizedRelativePositionedComponent(T component, int margin = 0) : base(component, margin) { }

	private double _width;
    private double _height;

	public double Width
    {
        get => _width;
        set 
        {
            _width = value;
            _component.Width = (int)(_boundaries.Width * _width);
        }
    }

    public double Height
    {
        get => _height;
        set
        {
            _height = value;
            _component.Height = (int)(_boundaries.Height * _height);
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
            Width = _width;
            Height = _height;
        }
    }
}