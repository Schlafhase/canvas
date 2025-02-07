using System.Drawing;
using System.Runtime.Versioning;
using Canvas.Components.Interfaces.RectangleSized;

namespace Canvas.Components.Interfaces.Relative;

[SupportedOSPlatform("windows")]
public sealed class RelativeRectangleSizedComponent<T> : CanvasComponent where T : CanvasComponent, IRectangleSizedComponent
{
    private readonly T _component;
    private System.Drawing.Rectangle _boundaries;

    private double _width;
    private double _height;

    public System.Drawing.Rectangle Boundaries
    {
        get => _boundaries;
        set
        {
            _boundaries = value;
            Width = _width;
            Height = _height;
        }
    }

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

    public int Margin { get; set; }

    public override bool SuppressUpdate 
    {
        get => _component.SuppressUpdate;
        set => _component.SuppressUpdate = value;
    }

    public override Canvas? Parent
    {
        get => _component.Parent;
        set => _component.Parent = value;
    }

	public RelativeRectangleSizedComponent(T component, int margin = 0)
	{
		_component = component;
		Margin = margin;
		_boundaries = new System.Drawing.Rectangle(0, 0, 0, 0);
		Width = 0f;
		Height = 0f;
		updateBoundaries();
	}

    private void updateBoundaries()
	{
		if (Parent is not null)
		{
			Boundaries = new System.Drawing.Rectangle(Margin, Margin, Parent.Width - Margin, Parent.Height - Margin);
		}
	}

	public override void Put(Graphics g)
	{
		SuppressUpdate = true;
		updateBoundaries();
		SuppressUpdate = false;
		_component.Put(g);
	}
}