using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Canvas
{
	public interface ICanvasComponent
	{
		int X { get; set; }
		int Y { get; set; }

		int	Width { get; set; }
		int Height { get; set; }

		/// <summary>
		/// Puts the component on a bitmap.
		/// </summary>
		/// <param name="g">Graphics of the bitmap to put the component on.</param>
		void Put(Graphics g);
	}
}
