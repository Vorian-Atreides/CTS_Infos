using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SiffertGaston.Tools
{
	public class XML<Type>
	{
		private XmlSerializer	_serializer;

		public XML()
		{
			_serializer = new XmlSerializer(typeof(Type));
		}

		public void Serialize(Stream stream, Type data)
		{
			try
			{
				_serializer.Serialize(stream, data);
			}
			catch (Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
			}
		}

		public async Task SerializeAsync(Stream stream, Type data)
		{
			await Task.Factory.StartNew(() =>
			{
				try
				{
					_serializer.Serialize(stream, data);
				}
				catch (Exception e)
				{
					System.Diagnostics.Debug.WriteLine(e.Message);
				}
			});
		}

		public Type Unserialize(Stream stream)
		{
			return ((Type) _serializer.Deserialize(stream));
		}

		public async Task<Type> UnserializeAsync(Stream stream)
		{
			object obj = await Task.Factory.StartNew(() =>
				{
					try
					{
						return (_serializer.Deserialize(stream));
					}
					catch (Exception e)
					{
						System.Diagnostics.Debug.WriteLine(e.Message);
					}
					return (null);
				});
			return ((Type) obj);
		}
	}
}
