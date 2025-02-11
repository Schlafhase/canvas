using System.Runtime.Versioning;
using Canvas.Components.Interfaces.Relative;

namespace Canvas.Components.Interfaces.Sized;


[SupportedOSPlatform("windows")]
public static class SizedComponentExtensions
{
	public static RelativeSizedComponent<T> GetRelativeSized<T>(this T component,
		double size = 0,
		RelativeSizingOptions sizingOptions = RelativeSizingOptions.Width,
		int margin = 0) where T : ICanvasComponent, ISizedComponent
	{
		return new RelativeSizedComponent<T>(component, sizingOptions, margin)
		{
			Size = size
		};
	}
}