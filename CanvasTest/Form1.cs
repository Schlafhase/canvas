using Canvas.Components;
using Canvas.Components.Interfaces;
using Rectangle = Canvas.Components.Rectangle;

namespace CanvasTest
{
    public partial class Form1 : Form
    {
        private Canvas.Canvas _canvas;
        private Rectangle _rectangle;
        private BezierCurve _bezier;
        private RelativePositionedComponent<Rectangle> _relativeSquare;
        private Thread animation;
        private bool disposed = false;

        private readonly object _updateLocker = new();
        private readonly object _disposeLocker = new();

        public Form1()
        {
            InitializeComponent();

            _canvas = new Canvas.Canvas(Width, Height);
            pictureBox1.Image = new Bitmap(Width, Height);

            _rectangle = new Rectangle(0, 0, 20, 20, Color.Red);
            _relativeSquare = new RelativePositionedComponent<Rectangle>(_rectangle)
            {
                X = 0.5f,
                Y = 0.5f,
                Centered = true
            };

            GlowDot glowDot = new(100, 100, 30, 70, Color.Red);
            
            
            _bezier = new BezierCurve(new List<Point>() {new Point(400, 40), new Point(100, 100), new Point(200, 0), new Point(300, 100)});

            _canvas.AddChild(_relativeSquare);
            _canvas.AddChild(_bezier);
            _canvas.AddChild(glowDot);
            _canvas.BackgroundColor = Color.Green;
            _canvas.OnUpdate = update;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Form1_Resize(sender, e);
            update();

            DateTime startTime = DateTime.Now;
            animation = new Thread(
                () =>
                {
                    while (!this.IsDisposed)
                    {
                        try
                        {
                            this.Invoke((MethodInvoker)(() =>
                                _relativeSquare.X = Math.Sin((DateTime.Now - startTime).TotalMilliseconds / 1000f) / 2 +
                                                    0.5f));
                            this.Invoke((MethodInvoker)(() =>
                                _relativeSquare.Y = Math.Cos((DateTime.Now - startTime).TotalMilliseconds / 1000f) / 2 +
                                                    0.5f));
                            
                            this.Invoke((MethodInvoker)(() =>
                                _bezier.Points[1] = new Point((int) (100 * Math.Sin((DateTime.Now - startTime).TotalMilliseconds / 1000f)),
                                    (int) (100 * Math.Cos((DateTime.Now - startTime).TotalMilliseconds / 1000f)))));
                        }
                        catch (ObjectDisposedException)
                        {
                            break;
                        }

                        Thread.Sleep(1);
                    }
                });
            animation.Start();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            Size size = new(ClientSize.Width, ClientSize.Height);
            if (size.Height == 0)
            {
                return;
            }

            pictureBox1.Size = size;
            pictureBox1.Image = new Bitmap(pictureBox1.Image, size);
            _canvas.Width = ClientSize.Width;
            _canvas.Height = ClientSize.Height;
        }
        
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            _canvas.Dispose();
        }

        private void update()
        {
            if (disposed)
            {
                return;
            }

            lock (_updateLocker)
            {
                Image img = pictureBox1.Image;
                using Graphics g = Graphics.FromImage(img);
                _canvas.Put(g);
                pictureBox1.Invoke((MethodInvoker)(() => pictureBox1.Image = img));
            }
        }
    }
}