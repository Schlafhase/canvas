namespace Canvas.Components.Interfaces
{
	public abstract class PositionedComponent : CanvasComponent
	{
		int _x;
		int _y;

		/// <summary>
		/// X position of the component.
		/// </summary>
		public virtual int X
		{
			get => _x;
			set
			{
				_x = value;
				if (!SuppressUpdate)
				{
					Parent?.Update();
				}
			}
		}

		/// <summary>
		/// Y position of the component.
		/// </summary>
		public virtual int Y
		{
			get => _y;
			set
			{
				_y = value;
				if (!SuppressUpdate)
				{
					Parent?.Update();
				}
			}
		}
	}
}
