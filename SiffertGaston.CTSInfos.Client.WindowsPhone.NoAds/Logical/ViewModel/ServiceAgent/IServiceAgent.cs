using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiffertGaston.CTSInfos.Client.WindowsPhone.NoAds.Logical.ViewModel.ServiceAgent
{
	public interface IServiceAgent
	{
	}

	public abstract class AServiceArgent : IServiceAgent
	{
		protected async void Serialize<T>(Tools.XML<T> serializer, string fileName, T data)
		{
			try
			{
				using (IsolatedStorageFile file	= IsolatedStorageFile.GetUserStoreForApplication())
				using (IsolatedStorageFileStream	stream	= file.OpenFile(fileName, FileMode.Create))
				{
					await serializer.SerializeAsync(stream, data);
				}
			}
			catch (Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
			}
		}

		protected async Task<T> Unserialize<T>(Tools.XML<T> serializer, string fileName)
		{
			T data = Activator.CreateInstance<T>();

			try
			{
				using (IsolatedStorageFile file	= IsolatedStorageFile.GetUserStoreForApplication())
				{
					if (file.FileExists(fileName))
					{
						using (IsolatedStorageFileStream stream	= file.OpenFile(fileName, FileMode.Open))
						{
							data = await serializer.UnserializeAsync(stream);
						}
					}
				}
			}
			catch (Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
			}
			return (data);
		}
	}
}
