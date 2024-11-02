using System.Drawing;
using Canvas.Components.Interfaces;

namespace Canvas
{
    public class Canvas : ICanvasComponent
    {
        #region Properties
        private int _x;
        private int _y;

        public int X
        {
            get => _x;
            set
            {
                _x = value;
                
                if (SuppressUpdate) return;
                Parent?.OnUpdate?.Invoke();
                OnUpdate?.Invoke();
            }
        }

        public int Y
        {
            get => _y;
            set
            {
                _y = value;
                
                if (SuppressUpdate) return;
                Parent?.OnUpdate?.Invoke();
                OnUpdate?.Invoke();
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
                
                if (SuppressUpdate) return;
                Parent?.OnUpdate?.Invoke();
                OnUpdate?.Invoke();
            }
        }

        public int Height
        {
            get => _height;
            set
            {
                _height = value;
                
                if (SuppressUpdate) return;
                Parent?.OnUpdate?.Invoke();
                OnUpdate?.Invoke();
            }
        }
        
        public bool SuppressUpdate { get; set; } = false;
        public Color BackgroundColor { get; set; } = Color.Transparent;

        private List<ICanvasComponent> _children = new();
        public List<ICanvasComponent> Children
        {
            get => _children;
            set
            {
                _children = value;
                _children.ForEach(c => c.Parent = this);
            }
        }

        public Canvas? Parent { get; set; }
        /// <summary>
        /// Children should call this method when they need to be updated.
        /// </summary>
        public Action? OnUpdate;
        
        #endregion

        public Canvas(int x, int y, int width, int height, List<ICanvasComponent> children)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            Children = children;
        }

        public Canvas(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            Children = new();
        }

        public Canvas(int width, int height, List<ICanvasComponent> children)
        {
            X = 0;
            Y = 0;
            Width = width;
            Height = height;
            Children = children;
        }

        public Canvas(int width, int height)
        {
            X = 0;
            Y = 0;
            Width = width;
            Height = height;
            Children = new();
        }
        
        public void AddChild(ICanvasComponent child)
        {
            Children.Add(child);
            child.Parent = this;
        }
        
        public void RemoveChild(ICanvasComponent child)
        {
            Children.Remove(child);
            child.Parent = null;
        }

        public void Put(Graphics g)
        {
            using Bitmap bitmap = new Bitmap(Width, Height);
            using Graphics g2 = Graphics.FromImage(bitmap);
            foreach (ICanvasComponent c in Children)
            {
                c.Put(g2);
            }
            g.Clear(BackgroundColor);
            g.DrawImage(bitmap, X, Y);
        }
    }
}