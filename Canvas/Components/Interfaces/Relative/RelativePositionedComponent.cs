using System.Drawing;
using System.Runtime.Versioning;
using Canvas.Components.Interfaces.Mix;
using Canvas.Components.Interfaces.Positioned;
using Canvas.Components.Interfaces.RectangleSized;

namespace Canvas.Components.Interfaces.Relative;

[SupportedOSPlatform("windows")]
public class RelativePositionedComponent<T> : CanvasComponent where T : ICanvasComponent, IPositionedComponent
{
	protected readonly T _component;
	protected System.Drawing.Rectangle _boundaries;

	protected double _x;
	protected double _y;

	public RelativePositionedComponent(T component, int margin = 0)
	{
		_component = component;
		Margin = margin;
		_boundaries = new System.Drawing.Rectangle(0, 0, 0, 0);
		updateBoundaries();
		X = 0f;
		Y = 0f;
	}

	public virtual System.Drawing.Rectangle Boundaries
	{
		get => _boundaries;
		set
		{
			_boundaries = value;
			X = _x;
			Y = _y;
		}
	}

	public double X
	{
		get => _x;
		set
		{
			_x = value;
			_component.X = (int)((Boundaries.Width - Boundaries.X) * _x + Boundaries.X);

			if (Centered && _component is PositionedRectangleSizedComponent positionedSizedComponent)
			{
				_component.X -= positionedSizedComponent.Width / 2;
			}
		}
	}

	public double Y
	{
		get => _y;
		set
		{
			_y = value;
			_component.Y = (int)((Boundaries.Height - Boundaries.Y) * _y + Boundaries.Y);

			if (Centered && _component is PositionedRectangleSizedComponent positionedSizedComponent)
			{
				_component.Y -= positionedSizedComponent.Height / 2;
			}
		}
	}

	public int Margin { get; set; }

	public override bool SuppressUpdate
	{
		get => _component.SuppressUpdate;
		set => _component.SuppressUpdate = value;
	}

    /// <summary>
    ///     Centers the component. This will only have an effect if <see cref="T" /> is a
    ///     <see cref="RectangleSizedComponent" />.
    /// </summary>
    public bool Centered { get; set; } = false;

	public override Canvas? Parent
	{
		get => _component.Parent;
		set
		{
			_component.Parent = value;
		}
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