using GalaSoft.MvvmLight.Ioc;
using Microsoft.Phone.Maps.Controls;
using Microsoft.Phone.Maps.Toolkit;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SiffertGaston.CTSInfos.Client.WindowsPhone.Ads.Graphical.View.Extender
{
	public static class MapExtender
	{
		#region Dependencies

		public static readonly DependencyProperty	ItemsSourceProperty		= DependencyProperty.RegisterAttached("ItemsSource",
			typeof(List<WebService.Models.VelhopStop>), typeof(Map), new PropertyMetadata(new List<WebService.Models.VelhopStop>(), new PropertyChangedCallback(ItemsSourceChanged)));

		public static readonly DependencyProperty	ItemTemplateProperty	= DependencyProperty.RegisterAttached("ItemTemplateProperty",
			typeof(DataTemplate), typeof(Map), new PropertyMetadata(new DataTemplate(), new PropertyChangedCallback(ItemTemplateChanged)));

		public static readonly DependencyProperty	CoordinateProperty		= DependencyProperty.RegisterAttached("CoordinateProperty",
			typeof(GeoCoordinate), typeof(Map), new PropertyMetadata(new GeoCoordinate(Logical.ViewModel.VelhopViewModel._strasLatitude, Logical.ViewModel.VelhopViewModel._strasLongitude),
				new PropertyChangedCallback(CoordinateChanged)));


		#endregion //Dependencies

		#region CallBack

		private static void ItemsSourceChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
		{
			Map map										= obj as Map;
			List<WebService.Models.VelhopStop>	items	= args.NewValue as List<WebService.Models.VelhopStop>;

			if (map == null || items == null || items.Count == 0)
				return;
			map.Layers.Clear();
			addStations(map, items);
			if (SimpleIoc.Default.GetInstance<Logical.ViewModel.ConfigurationViewModel>().Configuration.CanUseLocation)
				addCoordinate(map, GetCoordinate(map));
		}

		private static void ItemTemplateChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
		{
			Map				map			= obj as Map;
			DataTemplate template	= args.NewValue as DataTemplate;

			if (map == null || template == null)
				return;
		}

		private static void CoordinateChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
		{
			Map				map			= obj as Map;
			GeoCoordinate	coordinate	= args.NewValue as GeoCoordinate;

			if (map == null || coordinate == null)
				return;
			map.Layers.Clear();
			addStations(map, GetItemsSource(map));
			if (SimpleIoc.Default.GetInstance<Logical.ViewModel.ConfigurationViewModel>().Configuration.CanUseLocation)
				addCoordinate(map, coordinate);
		}

		#endregion //CallBack

		#region Accessors

		public static List<WebService.Models.VelhopStop> GetItemsSource(DependencyObject obj)
		{
			return ((List<WebService.Models.VelhopStop>) obj.GetValue(ItemsSourceProperty));
		}

		public static void SetItemsSource(DependencyObject obj, object value)
		{
			obj.SetValue(ItemsSourceProperty, value);
		}

		public static GeoCoordinate GetCoordinate(DependencyObject obj)
		{
			return ((GeoCoordinate) obj.GetValue(CoordinateProperty));
		}

		public static void SetCoordinate(DependencyObject obj, object value)
		{
			obj.SetValue(CoordinateProperty, value);
		}

		public static DataTemplate GetItemTemplate(DependencyObject obj)
		{
			return ((DataTemplate) obj.GetValue(ItemTemplateProperty));
		}

		public static void SetItemTemplate(DependencyObject obj, DataTemplate value)
		{
			obj.SetValue(ItemTemplateProperty, value);
		}

		#endregion //Accessors

		private static void addCoordinate(Map map, GeoCoordinate value)
		{
			MapLayer	layer		= new MapLayer();
			MapOverlay	overlay		= new MapOverlay();
			overlay.Content			= new UserLocationMarker();
			overlay.GeoCoordinate	= value;
			layer.Add(overlay);
			map.Layers.Add(layer);
		}

		private static void addStations(Map map, List<WebService.Models.VelhopStop> items)
		{
			foreach (var item in items)
			{
				MapLayer	layer		= new MapLayer();
				MapOverlay	overlay		= new MapOverlay();
				overlay.Content			= new ContentControl() { Content = item, ContentTemplate = GetItemTemplate(map) };
				overlay.GeoCoordinate	= new GeoCoordinate(item.Latitude, item.Longitude);
				layer.Add(overlay);
				map.Layers.Add(layer);
			}
		}

	}
}
