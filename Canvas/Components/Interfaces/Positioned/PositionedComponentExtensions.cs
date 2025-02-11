using System.Runtime.Versioning;
using Canvas.Components.Interfaces.Relative;

namespace Canvas.Components.Interfaces.Positioned;

[SupportedOSPlatform("windows")]
public static class PositionedComponentExtensions
{
	public static RelativePositionedComponent<T> GetRelativePositioned<T>(this T component,
		double x = 0,
		double y = 0,
		int margin = 0,
		bool centered = false) where T : ICanvasComponent, IPositionedComponent
	{
		return new RelativePositionedComponent<T>(component, margin)
		{
			X = x,
			Y = y,
			Centered = centered
		};
	}
}