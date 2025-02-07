using System.Drawing;
using Canvas.Components.Interfaces;
using Canvas.Components.Interfaces.Mix;

namespace Canvas.Components;

public class BitmapImage : PositionedRectangleSizedComponent
{
	private Bitmap _bitmap;

	public BitmapImage(string filePath, int x, int y, int width, int height) : this(
		Image.FromFile(filePath) as Bitmap, x, y, width, height) { }

	public BitmapImage(Bitmap bitmap, int x, int y, int width, int height)
	{
		Bitmap = bitmap;
		X = x;
		Y = y;
		Width = width;
		Height = height;
	}

	public Bitmap Bitmap
	{
		get => _bitmap;
		set
		{
			_bitmap = value;

			if (!SuppressUpdate)
			{
				Parent?.Update();
			}
		}
	}

	public override void Put(Graphics g)
	{
		g.DrawImage(Bitmap, X, Y, Width, Height);
	}
}