using System.Drawing;
using System.Runtime.Versioning;
using Canvas.Components.Interfaces.Sized;

namespace Canvas.Components.Interfaces.Relative;

public enum RelativeSizingOptions
{
	Width,
	Height,
	Both
}

[SupportedOSPlatform("windows")]
public sealed class RelativeSizedComponent<T> : CanvasComponent where T : CanvasComponent, ISizedComponent
{
	private readonly T _component;
	private readonly RelativeSizingOptions _sizingOptions;

	private System.Drawing.Rectangle _boundaries;

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

	public System.Drawing.Rectangle Boundaries
	{
		get => _boundaries;
		set
		{
			_boundaries = value;
			Size = _size;
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

	public RelativeSizedComponent(T component, RelativeSizingOptions sizingOptions, int margin = 0)
	{
		_component = component;
		_sizingOptions = sizingOptions;
		Margin = margin;
		_boundaries = new System.Drawing.Rectangle(0, 0, 0, 0);
		Size = 0;
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