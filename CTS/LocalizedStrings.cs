using SiffertGaston.CTSInfos.Client.WindowsPhone.Ads.Resources;

namespace SiffertGaston.CTSInfos.Client.WindowsPhone.Ads
{
	/// <summary>
	/// Provides access to string resources.
	/// </summary>
	public class LocalizedStrings
	{
		private static AppResources _localizedResources = new AppResources();

		public AppResources LocalizedResources { get { return _localizedResources; } }
	}
}