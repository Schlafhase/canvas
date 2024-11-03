namespace Canvas.Components.Interfaces
{
	public interface IPositionedComponent : ICanvasComponent
	{
		/// <summary>
		/// X coordinate of the component.
		/// </summary>
		int X { get; set; }

		/// <summary>
		/// Y coordinate of the component.
		/// </summary>
		int Y { get; set; }
	}
}
