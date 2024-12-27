using System.Drawing;
using System.Runtime.Versioning;
using Canvas.Components.Interfaces;

namespace Canvas
{
    [SupportedOSPlatform("windows")]
    public class Canvas : PositionedRectangleSizedComponent, IDisposable
    {
        #region Properties
        private bool _disposed;

        
        public Color BackgroundColor { get; set; } = Color.Transparent;

        private List<CanvasComponent> _children = new();

        public List<CanvasComponent> Children
        {
            get => _children;
            set
            {
                _children = value;
                _children.ForEach(c => c.Parent = this);
            }
        }

        /// <summary>
        /// Children should call this method when they need to be updated.
        /// </summary>
        public Action? OnUpdate { private get; set; }

        public int FrameRate { get; set; } = 60;
        private bool _updateQueued;
        private Thread? _updateThread;
        private SynchronizationContext? _syncContext;

        #endregion

        public Canvas(int x, int y, int width, int height, List<CanvasComponent> children)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            Children = children;
            initialize();
        }

        public Canvas(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            Children = new List<CanvasComponent>();
            initialize();
        }

        public Canvas(int width, int height, List<CanvasComponent> children)
        {
            X = 0;
            Y = 0;
            Width = width;
            Height = height;
            Children = children;
            initialize();
        }

        public Canvas(int width, int height)
        {
            X = 0;
            Y = 0;
            Width = width;
            Height = height;
            Children = new List<CanvasComponent>();
            initialize();
        }
        
        private void initialize()
        {
            _syncContext = SynchronizationContext.Current;
            _updateThread = new Thread(() =>
            {
                while (!_disposed)
                {
                    _syncContext?.Post(_ => update(), null);
                    Thread.Sleep(1000 / FrameRate);
                }
            });
            _updateThread.Start();
        }

        public void AddChild(CanvasComponent child)
        {
            Children.Add(child);
            child.Parent = this;
        }

        public void RemoveChild(CanvasComponent child)
        {
            Children.Remove(child);
            child.Parent = null;
        }
        
        public override void Put(Graphics g)
        {
            using Bitmap bitmap = new Bitmap(Width, Height);
            using Graphics g2 = Graphics.FromImage(bitmap);
            foreach (CanvasComponent c in Children)
            {
                c.Put(g2);
            }

            g.Clear(BackgroundColor);
            g.DrawImage(bitmap, X, Y);
        }

        public void Update()
        {
            _updateQueued = true;
        }

        private void update()
        {
            if (!_updateQueued)
            {
                return;
            }

            OnUpdate?.Invoke();
            _updateQueued = false;
        }
        
        public void Dispose()
        {
            _disposed = true;
            GC.SuppressFinalize(this);
        }
    }
}