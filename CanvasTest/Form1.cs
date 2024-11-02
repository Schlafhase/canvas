using Canvas.Components;
using Canvas.Components.Interfaces;

namespace CanvasTest
{
    public partial class Form1 : Form
    {
        private Canvas.Canvas _canvas;
        private Square _square;
        private RelativePositionedComponent<Square> _relativeSquare;
        private Thread animation;
        private bool disposed = false;

        object updateLocker = new();

        public Form1()
        {
            InitializeComponent();

            _canvas = new(Width, Height);
            pictureBox1.Image = new Bitmap(Width, Height);

            _square = new(0, 0, 20, 20, Color.Red);
            _relativeSquare = new(_square);
            _relativeSquare.X = 0.5f;
            _relativeSquare.Y = 0.5f;
            _relativeSquare.Centered = true;

            _canvas.AddChild(_relativeSquare);
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
                    while (!this.Disposing)
                    {
                        //TODO: fix object disposed exception
                        this.Invoke((MethodInvoker)(() => _relativeSquare.X = Math.Sin((DateTime.Now - startTime).TotalSeconds) / 2 + 0.5f));
                        this.Invoke((MethodInvoker)(() => _relativeSquare.Y = Math.Cos((DateTime.Now - startTime).TotalSeconds) / 2 + 0.5f));
                        Thread.Sleep(1);
                    }
                });
            animation.Start();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            Size size = new(ClientSize.Width, ClientSize.Height);
            if (size.Height == 0) return;
            pictureBox1.Size = size;
            pictureBox1.Image = new Bitmap(ClientSize.Width, ClientSize.Height);
            _canvas.Width = ClientSize.Width;
            _canvas.Height = ClientSize.Height;
        }

        private void update()
        {
            lock (updateLocker)
            {
                Image img = pictureBox1.Image;
                using Graphics g = Graphics.FromImage(img);
                _canvas.Put(g);
                pictureBox1.Invoke((MethodInvoker)(() => pictureBox1.Image = img));
            }
        }

         void Dispose()
        {
            
        }
    }
}