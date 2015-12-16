using SiffertGaston.CTSInfos.Client.WindowsPhone.NoAds.Resources;

namespace SiffertGaston.CTSInfos.Client.WindowsPhone.NoAds
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