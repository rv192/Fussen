using System;

using Fussen.Core.Interfaces;
using Fussen.Core.Extensions.Comparasion;

namespace Fussen.Core.Extensions
{
	/// <summary>
	/// A extension of System.Object. System.Object类的扩展类
	/// </summary>
	public static class ObjectExtension
	{
		/// <summary>
		/// <para>Compare the specified original and target.</para>
		/// <para>当前对象和目标对象进行比较，判断是否相同。</para>
		/// <para>使用GlobalSetting.CompareComponent属性定义的CompareComponent对象进行比较操作。</para>
		/// <para>支持结构体、IList、IDictionary、字段、属性、Dataset、Datatable、URI、Runtime Type</para>
		/// </summary>
		/// <param name="original">The current object.</param>
		/// <param name="target">the target object to compare.</param>
		public static bool Compare(this object original, object target)
		{
			bool result;

            result = GlobalSetting.CompareComponent.Compare(original, target);

			return result;
		}
	}
}

