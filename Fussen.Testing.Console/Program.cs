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
