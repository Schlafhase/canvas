namespace Canvas.Components.Interfaces
{
	public interface IRectangleSizedComponent : ICanvasComponent
	{
		/// <summary>
		/// Width of the component.
		/// </summary>
		int Width { get; set; }

		/// <summary>
		/// Height of the component.
		/// </summary>
		int Height { get; set; }
	}
}
