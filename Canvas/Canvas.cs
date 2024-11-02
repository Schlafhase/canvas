using System.Drawing;

namespace Canvas
{
	public class Canvas : ICanvasComponent
	{
		public int X { get; set; }
		public int Y { get; set; }

		public int Width { get; set; }
		public int Height { get; set; }

		public List<ICanvasComponent> Children { get; set; }

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

		public void Put(Graphics g)
		{
			using Bitmap bitmap = new Bitmap(Width, Height);
			using Graphics g2 = Graphics.FromImage(bitmap);
			foreach (ICanvasComponent c in Children)
			{
				c.Put(g2);
			}

			g.DrawImage(bitmap, X, Y);
		}
	}
}