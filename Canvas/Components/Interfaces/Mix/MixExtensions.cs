using System.Runtime.Versioning;
using Canvas.Components.Interfaces.Positioned;
using Canvas.Components.Interfaces.RectangleSized;
using Canvas.Components.Interfaces.Relative;
using Canvas.Components.Interfaces.Sized;

namespace Canvas.Components.Interfaces.Mix;

[SupportedOSPlatform("windows")]
public static class MixExtensions
{
	public static RelativeSizedRelativePositionedComponent<T> GetRelativeSizedRelativePositioned<T>(this T component,
		double x = 0,
		double y = 0,
		double size = 0,
		int margin = 0,
		RelativeSizingOptions sizingOptions = RelativeSizingOptions.Width,
		bool centered = false) where T : ICanvasComponent, IPositionedComponent, ISizedComponent
	{
		return new RelativeSizedRelativePositionedComponent<T>(component, sizingOptions, margin)
		{
			X = x,
			Y = y,
			Size = size,
			Centered = centered,
		};
	}
	
	public static RelativeRectangleSizedRelativePositionedComponent<T> GetRelativeRectangleSizedRelativePositioned<T>(this T component,
		double x = 0,
		double y = 0,
		double width = 0,
		double height = 0,
		int margin = 0,
		bool centered = false) where T : ICanvasComponent, IPositionedComponent, IRectangleSizedComponent
	{
		return new RelativeRectangleSizedRelativePositionedComponent<T>(component, margin)
		{
			X = x,
			Y = y,
			Width = width,
			Height = height,
			Centered = centered,
		};
	}
	
	public static RelativeRectangleSizedKeepAspectRatioRelativePositionedComponent<T> GetRelativeRectangleSizedKeepAspectRatioRelativePositioned<T>(this T component,
		double x = 0,
		double y = 0,
		double size = 0,
		double aspectRatio = 1,
		int margin = 0,
		bool centered = false) where T : ICanvasComponent, IPositionedComponent, IRectangleSizedComponent
	{
		return new RelativeRectangleSizedKeepAspectRatioRelativePositionedComponent<T>(component, margin)
		{
			X = x,
			Y = y,
			Size = size,
			AspectRatio = aspectRatio,
			Centered = centered,
		};
	}
}