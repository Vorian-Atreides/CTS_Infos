using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media.Imaging;

namespace SiffertGaston.CTSInfos.Client.WindowsPhone.NoAds.Graphical.View
{
	public partial class ZoomableImage : PhoneApplicationPage
	{
		public string	Source { get; set; }

		const double	MaxScale = 10;

		double			_scale = 1.0;
		double			_minScale;
		double			_coercedScale;
		double			_originalScale;

		Size			_viewportSize;
		bool			_pinching;
		Point			_screenMidpoint;
		Point			_relativeMidpoint;
		BitmapImage		_bitmap; 

		public ZoomableImage()
		{
			InitializeComponent();
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			base.OnNavigatedTo(e);
			Source = NavigationContext.QueryString["source"];
		}

		private void ViewportControl_ManipulationStarted(object sender, System.Windows.Input.ManipulationStartedEventArgs e)
		{
			_pinching = false;
			_originalScale = _scale;
		}

		private void ViewportControl_ManipulationDelta(object sender, System.Windows.Input.ManipulationDeltaEventArgs e)
		{
			if (e.PinchManipulation != null)
			{
				e.Handled = true;

				if (!_pinching)
				{
					_pinching = true;
					Point center = e.PinchManipulation.Original.Center;
					_relativeMidpoint = new Point(center.X / image.ActualWidth, center.Y / image.ActualHeight);

					var xform = image.TransformToVisual(viewport);
					_screenMidpoint = xform.Transform(center);
				}

				_scale = _originalScale * e.PinchManipulation.CumulativeScale;

				CoerceScale(false);
				ResizeImage(false);
			}
			else if (_pinching)
			{
				_pinching = false;
				_originalScale = _scale = _coercedScale;
			}
		}

		private void ViewportControl_ManipulationCompleted(object sender, System.Windows.Input.ManipulationCompletedEventArgs e)
		{
			_pinching = false;
			_scale = _coercedScale;
		}

		private void ViewportControl_ViewportChanged(object sender, System.Windows.Controls.Primitives.ViewportChangedEventArgs e)
		{
			Size newSize = new Size(viewport.Viewport.Width, viewport.Viewport.Height);
			if (newSize != _viewportSize)
			{
				_viewportSize = newSize;
				CoerceScale(true);
				ResizeImage(false);
			}
		}

		private void image_ImageOpened(object sender, RoutedEventArgs e)
		{
			_bitmap = (BitmapImage) image.Source;

			// Set scale to the minimum, and then save it. 
			_scale = 0;
			CoerceScale(true);
			_scale = _coercedScale;

			ResizeImage(true);
		}

		void ResizeImage(bool center)
		{
			if (_coercedScale != 0 && _bitmap != null)
			{
				double newWidth = canvas.Width = Math.Round(_bitmap.PixelWidth * _coercedScale);
				double newHeight = canvas.Height = Math.Round(_bitmap.PixelHeight * _coercedScale);

				xform.ScaleX = xform.ScaleY = _coercedScale;

				viewport.Bounds = new Rect(0, 0, newWidth, newHeight);

				if (center)
				{
					viewport.SetViewportOrigin(
						new Point(
							Math.Round((newWidth - viewport.ActualWidth) / 2),
							Math.Round((newHeight - viewport.ActualHeight) / 2)
							));
				}
				else
				{
					Point newImgMid = new Point(newWidth * _relativeMidpoint.X, newHeight * _relativeMidpoint.Y);
					Point origin = new Point(newImgMid.X - _screenMidpoint.X, newImgMid.Y - _screenMidpoint.Y);
					viewport.SetViewportOrigin(origin);
				}
			}
		}

		void CoerceScale(bool recompute)
		{
			if (recompute && _bitmap != null && viewport != null)
			{
				// Calculate the minimum scale to fit the viewport 
				double minX = viewport.ActualWidth / _bitmap.PixelWidth;
				double minY = viewport.ActualHeight / _bitmap.PixelHeight;

				_minScale = Math.Min(minX, minY);
			}

			_coercedScale = Math.Min(MaxScale, Math.Max(_scale, _minScale));

		}
	}
}