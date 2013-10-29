using System;
using System.ComponentModel.Composition;

namespace Fussen.Core.Extensions
{
	public interface IUniversalDetector
	{
		bool IsDone();
		void HandleData(byte[] buf, int offset, int length);
		void DataEnd();
		string GetDetectedCharset();
		void Reset();
	}
}

