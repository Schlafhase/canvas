using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Canvas
{
	public class Square : ICanvasComponent
	{
		public int X { get; set; }
		public int Y { get; set; }

		public int Width { get; set; }
		public int Height { get; set; }

		public Color Color { get; set; }
		private Brush _brush;

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
