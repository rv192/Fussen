using System;

namespace Fussen.Core.Extensions
{
	public interface IJsonConvert
	{
		T DeserializeObject<T>(string value);
		string SerializeObject(object target);
	}
}

