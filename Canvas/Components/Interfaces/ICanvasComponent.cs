using System.Drawing;

namespace Canvas.Components.Interfaces;

public interface ICanvasComponent
{
	/// <summary>
	///     Parent of the component. The component should call the <see cref="Canvas.Update"/> method of the parent when they need to be
	///     updated.
	/// </summary>
	Canvas? Parent { get; set; }
	bool SuppressUpdate { get; set; }
	/// <summary>
	///     Puts the component on a bitmap.
	/// </summary>
	/// <param name="g">Graphics of the bitmap to put the component on.</param>
	void Put(Graphics g);
}