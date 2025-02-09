using System.Drawing;
using Canvas.Components.Interfaces.RectangleSized;

namespace Canvas.Components.Interfaces.Relative;

public class RelativeRectangleSizedKeepAspectRatioComponent<T> : CanvasComponent where T : ICanvasComponent, IRectangleSizedComponent
{
	protected readonly T _component;
	protected System.Drawing.Rectangle _boundaries;
	
	private double _size;
	private double _aspectRatio;
	
	private int _width => (int)(Size * _boundaries.Width);
	private int _height => (int)(Size / _aspectRatio * _boundaries.Width);
	
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
	
	public System.Drawing.Rectangle Boundaries
	{
		get => _boundaries;
		set
		{
			_boundaries = value;
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

	public RelativeRectangleSizedKeepAspectRatioComponent(T component, int margin = 0)
	{
		_component = component;
		Margin = margin;
		_boundaries = new System.Drawing.Rectangle(0, 0, 0, 0);
		updateBoundaries();
	}

	private void updateBoundaries()
	{
		if (Parent is not null)
		{
			Boundaries = new System.Drawing.Rectangle(Margin, Margin, Parent.Width - 2*Margin, Parent.Height - 2*Margin);
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