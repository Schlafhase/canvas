using System.Runtime.Versioning;
using Canvas.Components.Interfaces.Relative;

namespace Canvas.Components.Interfaces.RectangleSized;

[SupportedOSPlatform("windows")]
public static class RectangleSizedComponentExtensions
{
	public static RelativeRectangleSizedComponent<T> GetRelativeRectangleSized<T>(this T component,
		double width = 0,
		double height = 0,
		int margin = 0) where T : ICanvasComponent, IRectangleSizedComponent
	{
		return new RelativeRectangleSizedComponent<T>(component, margin)
		{
			Width = width,
			Height = height
		};
	}

	public static RelativeRectangleSizedKeepAspectRatioComponent<T> GetRelativeRectangleSizedKeepAspectRatio<T>(
		this T component,
		double size = 0,
		double aspectRatio = 1,
		int margin = 0) where T : ICanvasComponent, IRectangleSizedComponent
	{
		return new RelativeRectangleSizedKeepAspectRatioComponent<T>(component, margin)
		{
			Size = size,
			AspectRatio = aspectRatio
		};
	}
}