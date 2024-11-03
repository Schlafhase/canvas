using System.Drawing;
using Canvas.Components.Interfaces;

namespace Canvas.Components
{
    public class Square : IPositionedComponent, IRectangleSizedComponent
    {
        private int _x;
        private int _y;

        public int X
        {
            get => _x;
            set
            {
                _x = value;
                if (!SuppressUpdate) Parent?.Update();
            }
        }

        public int Y
        {
            get => _y;
            set
            {
                _y = value;
                if (!SuppressUpdate) Parent?.Update();
            }
        }

        private int _width;
        private int _height;

        public int Width
        {
            get => _width;
            set
            {
                _width = value;
                if (!SuppressUpdate) Parent?.Update();
            }
        }

        public int Height
        {
            get => _height;
            set
            {
                _height = value;
                if (!SuppressUpdate) Parent?.Update();
            }
        }

        public Canvas? Parent { get; set; }

        public bool SuppressUpdate { get; set; } = false;
        public Color Color { get; set; }
        private readonly Brush _brush;

        public Square(int x, int y, int width, int height, Color color)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            Color = color;
            _brush = new SolidBrush(Color);
        }

        public void Put(Graphics g)
        {
            g.FillRectangle(_brush, X, Y, Width, Height);
        }
    }
}