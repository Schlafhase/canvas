﻿using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.Versioning;
using Canvas.Components.Interfaces;
using Canvas.Components.Interfaces.Mix;

namespace Canvas;

[SupportedOSPlatform("windows")]
public sealed class Canvas : PositionedRectangleSizedComponent, IDisposable
{
	public Canvas(int x, int y, int width, int height, List<CanvasComponent> children)
	{
		X = x;
		Y = y;
		Width = width;
		Height = height;
		Children = children;
		initialize();
	}

	public Canvas(int x, int y, int width, int height)
	{
		X = x;
		Y = y;
		Width = width;
		Height = height;
		Children = new List<CanvasComponent>();
		initialize();
	}

	public Canvas(int width, int height, List<CanvasComponent> children)
	{
		X = 0;
		Y = 0;
		Width = width;
		Height = height;
		Children = children;
		initialize();
	}

	public Canvas(int width, int height)
	{
		X = 0;
		Y = 0;
		Width = width;
		Height = height;
		Children = new List<CanvasComponent>();
		initialize();
	}

	public void Dispose()
	{
		_disposed = true;
	}

	private void initialize()
	{
		UpdateSynchronizationContext();
		_updateThread = new Thread(() =>
		{
			while (!_disposed)
			{
				_syncContext?.Post(_ => update(), null);
				NOP(1d / FrameRate);
			}
		})
		{
			IsBackground = true
		};
		_updateThread.Start();
	}

	public void UpdateSynchronizationContext()
	{
		_syncContext = SynchronizationContext.Current;

		foreach (Canvas canvas in Children.OfType<Canvas>())
		{
			canvas.UpdateSynchronizationContext();
		}
	}

	public void AddChild(CanvasComponent child)
	{
		Children.Add(child);
		child.Parent = this;
	}

	public void RemoveChild(CanvasComponent child)
	{
		Children.Remove(child);
		child.Parent = null;
	}

	public override void Put(Graphics g)
	{
		if (Width <= 0 || Height <= 0)
		{
			return;
		}

		using Bitmap bitmap = new(Width, Height);
		using Graphics g2 = Graphics.FromImage(bitmap);
		g2.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
		
		foreach (CanvasComponent c in Children)
		{
			c.Put(g2);
		}

		Region previousClip = g.Clip;
		
		if (Parent is not null)
		{
			g.SetClip(new Rectangle(X, Y, Width, Height), CombineMode.Replace);
		}

		g.Clear(Color.Transparent);
		g.FillRectangle(new SolidBrush(BackgroundColor), X, Y, Width, Height);
		g.DrawImage(bitmap, X, Y);
		
		g.SetClip(previousClip, CombineMode.Replace);
	}

    /// <summary>
    ///     Children should call this method when they need to be updated.
    /// </summary>
    public void Update()
	{
		_updateQueued = true;
	}

	public void ForceUpdate()
	{
		_updateQueued = true;
		update();
	}

	private void update()
	{
		if (!_updateQueued)
		{
			return;
		}
		Debug.WriteLine("Updating canvas");

		OnUpdate?.Invoke();
		_updateQueued = false;
	}

	~Canvas()
	{
		Dispose();
	}

	#region Properties

	private bool _disposed;


	public Color BackgroundColor { get; set; } = Color.Transparent;

	private List<CanvasComponent> _children = new();

	public List<CanvasComponent> Children
	{
		get => _children;
		set
		{
			_children = value;
			_children.ForEach(c => c.Parent = this);
		}
	}

	public Action? OnUpdate { private get; set; }

	public int FrameRate { get; set; } = 60;
	private bool _updateQueued;
	private Thread? _updateThread;
	private SynchronizationContext? _syncContext;

	#endregion
	
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	private static void NOP(double durationSeconds)
	{
		double durationTicks = Math.Round(durationSeconds * Stopwatch.Frequency);
		Stopwatch sw = Stopwatch.StartNew();

		while (sw.ElapsedTicks < durationTicks) { }
	}
}