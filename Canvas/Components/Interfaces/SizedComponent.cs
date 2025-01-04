using System.Runtime.Versioning;

namespace Canvas.Components.Interfaces
{
	[SupportedOSPlatform("windows")]
	public abstract class SizedComponent : CanvasComponent
	{
		protected int _size;

		/// <summary>
		/// Size of the component.
		/// </summary>
		public int Size
		{
			get => _size;
			set
			{
				_size = value;
				if (!SuppressUpdate)
				{
					Parent?.Update();
				}
			}
		}
	}
}
