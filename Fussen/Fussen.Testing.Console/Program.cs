<<<<<<< HEAD
using System;
using Fussen.Graphics;

namespace Fussen.Testing.Console
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			ExifManager info = new ExifManager ("/Users/William/Pictures/WP Photo Import/Windows Phone_20130918_18_14_29_Pro.jpg");

			System.Console.Write (info.ToString ());

			System.Console.ReadLine ();
		}
	}
}
=======
using System;
using Fussen.Graphics;

namespace Fussen.Testing.Console
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			ExifViewer info = new ExifViewer ("I:\\百度云\\德希游\\Photo2013\\20130923_1927_20-920T-3283.jpg");

			System.Console.Write (info.ToString ());

			System.Console.ReadLine ();
		}
	}
}
>>>>>>> d0cf1d69c01b64b1b46a9c789c1fbbbec5447d94
