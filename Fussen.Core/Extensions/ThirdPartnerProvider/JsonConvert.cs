using System;
using System.ComponentModel.Composition;

namespace Fussen.Core.Extensions
{
	public class JsonConvert : IJsonConvert
	{
		[Import("Newtonsoft.Json.JsonConvert.MEFPorxy")]
		public dynamic Instance { get; set;}

		public JsonConvert ()
		{
			this.Mefy ();
		}

		#region IJsonConvert implementation

		public T DeserializeObject<T> (string value)
		{
			return Instance.DeserializeObject<T>(value);
		}

		public string SerializeObject (object target)
		{
			return Instance.SerializeObject (target);
		}

		#endregion
	}
}

