using System.Drawing;

namespace Canvas.Components.Interfaces;

public class RelativePositionedComponent<T> : ICanvasComponent where T : IPositionedComponent
{
    private Rectangle _boundaries;

    public Rectangle Boundaries
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
            if (Centered && _component is IRectangleSizedComponent sizedComponent)
            {
                _component.X -= sizedComponent.Width / 2;
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
            if (Centered && _component is IRectangleSizedComponent sizedComponent)
            {
                _component.Y -= sizedComponent.Height / 2;
            }
        }
    }

    public int Margin { get; set; }
    public bool SuppressUpdate
    {
        get => _component.SuppressUpdate;
        set => _component.SuppressUpdate = value;
    }

    public bool Centered { get; set; } = false;

    private T _component;

    public Canvas? Parent
    {
        get => _component.Parent;
        set => _component.Parent = value;
    }

    public RelativePositionedComponent(T component, int margin = 0)
    {
        _component = component;
        this.Margin = margin;
        _boundaries = new Rectangle(0, 0, 0, 0);
        X = 0f;
        Y = 0f;
        updateBoundaries();
    }

    private void updateBoundaries()
    {
        if (Parent is not null)
        {
            Boundaries = new Rectangle(Margin, Margin, Parent.Width - Margin, Parent.Height - Margin);
        }
    }

    public void Put(Graphics g)
    {
        SuppressUpdate = true;
        updateBoundaries();
        SuppressUpdate = false;
        _component.Put(g);
    }
}