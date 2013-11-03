using System;
using KellermanSoftware.CompareNetObjects;

using Fussen.Core.Extensions;

namespace Fussen.Testing.Console
{
	class MainClass
	{
		public static void Main (string[] args)
		{
//			ExifViewer info = new ExifViewer ("I:\\百度云\\德希游\\Photo2013\\20130923_1927_20-920T-3283.jpg");
//
            //System.Console.Write(info.ToString());
            // IJsonConvert covert = new JsonConvert();

			// CompareObjects compareObjects = new CompareObjects();

			A a = new A () { BB = "12345", ID = Guid.NewGuid (), Count = 1001 };
            A b = new A() { BB = "1234", ID = a.ID, Count = 100 };

//			bool flag = compareObjects.Compare (a, b);
            //bool flag = a.Compare (b);

            //System.Console.Write (flag);
//			System.Console.Write (compareObjects.DifferencesString);

            System.Console.WriteLine(a.CompareForDetails(b, 1));
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
