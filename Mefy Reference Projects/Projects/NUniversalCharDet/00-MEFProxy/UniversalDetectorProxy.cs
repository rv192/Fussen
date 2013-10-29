using System;
using System.ComponentModel.Composition;
using Mozilla.NUniversalCharDet;

namespace Fussen.Core.Extensions
{
	[Export("Mozilla.NUniversalCharDet.UniversalDetector.MEFProxy")]
	public class UniversalDetectorProxy
	{
		UniversalDetector _UniversalDetector;
		public UniversalDetectorProxy ()
		{
			this._UniversalDetector = new UniversalDetector (null);
		}

		public bool IsDone()
		{
			return _UniversalDetector.IsDone ();
		}

		public void HandleData(byte[] buf, int offset, int length)
		{
			_UniversalDetector.HandleData (buf, offset, length);
		}

		public void DataEnd()
		{
			_UniversalDetector.DataEnd ();
		}

		public string GetDetectedCharset()
		{
			return _UniversalDetector.GetDetectedCharset ();
		}

		public void Reset()
		{
			_UniversalDetector.Reset ();
		}
	}
}

