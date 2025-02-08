using System.Drawing;
using System.Runtime.Versioning;

namespace Canvas.Components.Interfaces;


/// <summary>
/// Base class for <see cref="ICanvasComponent"/>s
/// </summary>
[SupportedOSPlatform("windows")]
public abstract class CanvasComponent : ICanvasComponent
{
    public virtual Canvas? Parent { get; set; }
	public virtual bool SuppressUpdate { get; set; }
    public abstract void Put(Graphics g);
}