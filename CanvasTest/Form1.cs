using Canvas.Components;
using Canvas.Components.AnimationUtilities;
using Canvas.Components.Interfaces.Relative;
using SkiaSharp;
using Rectangle = Canvas.Components.Rectangle;

namespace CanvasTest;

public partial class Form1 : Form
{
	private readonly object _disposeLocker = new();

	private readonly object _updateLocker = new();
	private readonly BezierCurve _bezier;
	private readonly Canvas.Canvas _canvas;
	private readonly BitmapImage _rectangle;
	private readonly RelativeRectangleSizedKeepAspectRatioRelativePositionedComponent<BitmapImage> _relativeSquare;
	private readonly RelativeSizedRelativePositionedComponent<Text> _text;
	private Thread? animation;
	private bool disposed;

	public Form1()
	{
		InitializeComponent();

		_canvas = new Canvas.Canvas(Width, Height)
		{
			FrameRate = 120
		};
		pictureBox1.Image = new Bitmap(Width, Height);

		_rectangle = new BitmapImage("C:\\\\Users\\\\linus\\\\OneDrive\\\\Pictures\\\\touching grass.jpg", 0, 0, 1000, 100);
		_relativeSquare = new RelativeRectangleSizedKeepAspectRatioRelativePositionedComponent<BitmapImage>(_rectangle)
		{
			X = 0,
			Y = 0.5,
			Size = 0.5,
			AspectRatio = 2
		};

		GlowDot glowDot = new(100, 100, 30, 70, Color.Red);


		_bezier = new BezierCurve(new List<Point> { new(400, 40), new(100, 100), new(200, 0), new(300, 100) });

		Text text = new("hello world", FontFamily.GenericMonospace, 50);
		_text = new RelativeSizedRelativePositionedComponent<Text>(text, RelativeSizingOptions.Width)
		{
			X = 0.1d,
			Y = 0.1d,
			Size = 0.06
		};

		Equation equation = new(@"F = \frac{G \cdot m_1 \cdot m_2}{d^2}", 200, 0, 100)
		{
			Quality = 200,
			Color = SKColors.White
		};
		
		_canvas.AddChild(equation);
		_canvas.AddChild(_relativeSquare);
		_canvas.AddChild(_bezier);
		_canvas.AddChild(_text);
		_canvas.AddChild(glowDot);
		_canvas.BackgroundColor = Color.Blue;
		
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
				while (!IsDisposed)
				{
					try
					{
						// Invoke((MethodInvoker)(() =>
						// 		   _relativeSquare.X =
						// 			   Math.Sin((DateTime.Now - startTime).TotalMilliseconds / 1000f) / 2 +
						// 			   0.5f));
						// Invoke((MethodInvoker)(() =>
						// 		   _relativeSquare.Y =
						// 			   Math.Cos((DateTime.Now - startTime).TotalMilliseconds / 1000f) / 2 +
						// 			   0.5f));

						Invoke((MethodInvoker)(() =>
								   _bezier.Points[1] = new Point(
									   (int)(100 * Math.Sin((DateTime.Now - startTime).TotalMilliseconds / 1000f)),
									   (int)(100 * Math.Cos((DateTime.Now - startTime).TotalMilliseconds / 1000f)))));
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

	private void Form1_Click(object sender, EventArgs e)
	{
		_relativeSquare.X.InterpolateThreading(x => _relativeSquare.X = x, 0.5);
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
		_canvas.Update();
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