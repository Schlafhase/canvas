namespace Canvas.Components.Interfaces
{
	public interface ISizedComponent : ICanvasComponent
	{
		/// <summary>
		/// Size of the component.
		/// </summary>
		int Size { get; set; }
	}
}
