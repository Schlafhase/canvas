using Canvas;

namespace CanvasTest
{
	public partial class Form1 : Form
	{
		private Canvas.Canvas _canvas;

		public Form1()
		{
			InitializeComponent();
			pictureBox1.Width = Width;
			pictureBox1.Height = Height;
			_canvas = new(Width, Height);
			pictureBox1.Image = new Bitmap(Width, Height);

			_canvas.Children.Add(new Square(10, 10, 20, 20, Color.Red));
		}

		private void pictureBox1_Click(object sender, EventArgs e)
		{
			Image img = pictureBox1.Image;
			using Graphics g = Graphics.FromImage(img);
			_canvas.Put(g);
			pictureBox1.Image = img;
		}
	}
}