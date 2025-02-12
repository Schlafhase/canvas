using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Canvas.Components.AnimationUtilities;

public enum Interpolation
{
	Linear,
	Quadratic,
	Cubic
}

public static class AnimationUtilities
{
	public static void InterpolateThreading(this double startValue,
		Action<double> setValue,
		double newValue,
		double duration = 1000,
		Interpolation type = Interpolation.Cubic)
	{
		new Thread(() => startValue.Interpolate(setValue, newValue, duration, type)).Start();
	}

	public static void Interpolate(this double startValue,
		Action<double> setValue,
		double newValue,
		double durationMilliseconds = 1000,
		Interpolation type = Interpolation.Cubic)
	{
		Func<double, double, double, double> interpolationFunction = type switch
		{
			Interpolation.Linear    => interpolateLinear,
			Interpolation.Quadratic => interpolateQuadratic,
			Interpolation.Cubic     => (a, b, t) => interpolateCubic(t, a, 0, b, 0),
			_                       => throw new ArgumentOutOfRangeException(nameof(type), type, null)
		};

		double t = 0;
		Stopwatch sw = Stopwatch.StartNew();

		while (t < 1)
		{
			t = sw.ElapsedMilliseconds / durationMilliseconds;
			setValue(interpolationFunction(startValue, newValue, t));
			NOP(0.01);
		}

		setValue(newValue);
	}

	private static double interpolateLinear(double a, double b, double t)
	{
		return a + (b - a) * t;
	}

	private static double interpolateQuadratic(double a, double b, double t)
	{
		return a + (b - a) * t * t;
	}

	private static double interpolateCubic(double t, double p0, double m0, double p1, double m1)
	{
		double t2 = t * t;
		double t3 = t2 * t;

		double h00 = 2 * t3 - 3 * t2 + 1;
		double h10 = t3 - 2 * t2 + t;
		double h01 = -2 * t3 + 3 * t2;
		double h11 = t3 - t2;

		return h00 * p0 + h10 * m0 + h01 * p1 + h11 * m1;
	}

	[SuppressMessage("ReSharper", "InconsistentNaming")]
	private static void NOP(double durationSeconds)
	{
		double durationTicks = Math.Round(durationSeconds * Stopwatch.Frequency);
		Stopwatch sw = Stopwatch.StartNew();

		while (sw.ElapsedTicks < durationTicks) { }
	}
}