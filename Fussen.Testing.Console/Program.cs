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
