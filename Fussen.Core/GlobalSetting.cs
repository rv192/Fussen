using System;
using Fussen.Core.Extensions.Comparasion;
using Fussen.Core.Interfaces;

namespace Fussen.Core
{
	/// <summary>
	/// <para>Configing some global variable for the Fussen.</para>
	/// <para>配置整个Fussen框架的全局变量</para>
	/// </summary>
	public static class GlobalSetting
	{
		private static ICompareObjects _CompareComponent;
		/// <summary>
		/// <para>A component for comparing one object with another one. </para>
		/// <para>用于比较两个对象是否相同的组件，适用于</para>
		/// </summary>
		/// <value>The compare component.</value>
		public static ICompareObjects CompareComponent
		{
			get {

				if (_CompareComponent == null) {
					_CompareComponent = new CompareComponent ();
				}

				return _CompareComponent;
			}
			set {
				_CompareComponent = value;
			}
		}
	}
}

