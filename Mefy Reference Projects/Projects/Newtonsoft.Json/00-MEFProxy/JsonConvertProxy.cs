using System;
using System.ComponentModel.Composition;
using Newtonsoft.Json;

namespace Fussen.Core.Extensions
{
	[Export("Newtonsoft.Json.JsonConvert.MEFPorxy")]
	public class JsonConvertProxy
	{
		public JsonConvertProxy ()
		{
		}

		public T DeserializeObject<T>(string value)
		{
			T result = JsonConvert.DeserializeObject<T> (value);
			return result;
		}

		public string SerializeObject(object target)
		{
			string result;

			result = JsonConvert.SerializeObject (target);

			return result;
		}
	}
}

