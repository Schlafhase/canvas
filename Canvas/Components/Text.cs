using System.Drawing;
using Canvas.Components.Interfaces;
using Canvas.Components.Interfaces.Mix;

namespace Canvas.Components;

public class Text : PositionedSizedComponent
{
	public Text(string content, FontFamily fontFamily, int fontSize, int x = 0, int y = 0, Brush? brush = null)
	{
		Content = content;
		FontFamily = fontFamily;
		FontSize = fontSize;
		Brush = brush ?? Brushes.Black;
		X = x;
		Y = y;
	}

	public string Content { get; set; }
	public FontFamily FontFamily { get; set; }
	public FontStyle FontStyle { get; set; } = FontStyle.Regular;

	public int FontSize
	{
		get => Size;
		set => Size = value;
	}

	public Brush Brush { get; set; }

	private Font _font => new(FontFamily, FontSize, FontStyle);

	public override void Put(Graphics g)
	{
		if (Size <= 0)
		{
			return;
		}
		
		if (Parent is { Width: int width, Height: int height })
		{
			RectangleF layoutRectangle = new(X, Y, width - X, height - Y);
			g.DrawString(Content, _font, Brush, layoutRectangle);
		}
		else
		{
			g.DrawString(Content, _font, Brush, X, Y);
		}
	}
}