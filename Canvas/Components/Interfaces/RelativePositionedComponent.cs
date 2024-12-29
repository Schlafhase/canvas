using System.Drawing;

namespace Canvas.Components.Interfaces;

public class RelativePositionedComponent<T> : CanvasComponent where T : PositionedComponent
{
    private System.Drawing.Rectangle _boundaries;

    public System.Drawing.Rectangle Boundaries
    {
        get => _boundaries;
        set
        {
            _boundaries = value;
            X = _x;
            Y = _y;
        }
    }

    private double _x;

    public double X
    {
        get => _x;
        set
        {
            _x = value;
            _component.X = (int)Math.Round((Boundaries.Width - Boundaries.X) * _x + Boundaries.X);
            if (Centered && _component is PositionedRectangleSizedComponent positionedSizedComponent)
            {
                _component.X -= positionedSizedComponent.Width / 2;
            }
        }
    }

    private double _y;

    public double Y
    {
        get => _y;
        set
        {
            _y = value;
            _component.Y = (int)Math.Round((Boundaries.Height - Boundaries.Y) * _y + Boundaries.Y);
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
    /// Only works with <see cref="RectangleSizedComponent"/>.
    /// </summary>
    public bool Centered { get; set; } = false;

    private readonly T _component;

    public override Canvas? Parent
    {
        get => _component.Parent;
        set => _component.Parent = value;
    }

    public RelativePositionedComponent(T component, int margin = 0)
    {
        _component = component;
        this.Margin = margin;
        _boundaries = new System.Drawing.Rectangle(0, 0, 0, 0);
        X = 0f;
        Y = 0f;
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