using System.Drawing;
using System.Runtime.Versioning;

namespace Canvas.Components.Interfaces;

[SupportedOSPlatform("windows")]
public abstract class CanvasComponent
{
    /// <summary>
    ///     Parent of the component. The component should call the <c>OnUpdate</c> action of the parent when they need to be
    ///     updated.
    /// </summary>

    public virtual Canvas? Parent { get; set; }

	public virtual bool SuppressUpdate { get; set; }

    /// <summary>
    ///     Puts the component on a bitmap.
    /// </summary>
    /// <param name="g">Graphics of the bitmap to put the component on.</param>
    public abstract void Put(Graphics g);
}