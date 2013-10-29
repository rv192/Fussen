using System;
using System.ComponentModel.Composition;

namespace Fussen.Core.Extensions
{
	public class UniversalDetector : IUniversalDetector
	{
		[Import("Mozilla.NUniversalCharDet.UniversalDetector.MEFProxy")]
		public dynamic Instance { get; set;}

		public UniversalDetector ()
		{
			this.Mefy ();
		}

		#region IUniversalDetector implementation

		public bool IsDone ()
		{
			return Instance.IsDone ();
		}

		public void HandleData (byte[] buf, int offset, int length)
		{
			Instance.HandleData (buf, offset, length);
		}

		public void DataEnd ()
		{
			Instance.DataEnd ();
		}

		public string GetDetectedCharset ()
		{
			return Instance.GetDetectedCharset ();
		}

		public void Reset ()
		{
			Instance.Reset ();
		}

		#endregion
	}
}

