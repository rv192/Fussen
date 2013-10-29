using System;
using Fussen.Graphics;
using Fussen.Net;
using Fussen.Core.Extensions;

namespace Fussen.Testing.Console
{
	class MainClass
	{
		public static void Main (string[] args)
		{
//			ExifViewer info = new ExifViewer ("I:\\百度云\\德希游\\Photo2013\\20130923_1927_20-920T-3283.jpg");
//
//			System.Console.Write (info.ToString ());
//			ThirdPartnerProvider provider = new ThirdPartnerProvider ();
//
			IJsonConvert convert = new JsonConvert ();
			A a = new A () { BB = "1234", ID = Guid.NewGuid (), Count = 100 };
			A b = new A () { BB = "1234", ID = Guid.NewGuid (), Count = 100 };

			bool flag = a.Compare (b);
			// System.Console.Write (a1.Compare (a));

			System.Console.ReadLine ();
		}
	}

	class A
	{
		public string BB{ get; set; }

		public Guid ID{ get; set; }

		public int Count{ get; set; }
	}
}
