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
        /// <param name="maxDifference">A number, suppose how many differences will be found. It'll not continue if the number of differences is over than this number. </param>
		public static bool Compare(this object original, object target, int maxDifference = 1)
		{
			bool result;

            // 备份当前允许的最大不同点数，便于比较完成后恢复
            int defaultValue = GlobalSetting.CompareComponent.MaxDifferences;
            GlobalSetting.CompareComponent.MaxDifferences = maxDifference;

            result = GlobalSetting.CompareComponent.Compare(original, target);

            GlobalSetting.CompareComponent.MaxDifferences = defaultValue;

			return result;
		}

        /// <summary>
        /// <para>Compare the specified original and target and get the details of difference return.</para>
        /// <para>当前对象和目标对象进行比较，判断是否相同，并返回详细的对比明细。</para>
        /// <para>使用GlobalSetting.CompareComponent属性定义的CompareComponent对象进行比较操作。</para>
        /// <para>支持结构体、IList、IDictionary、字段、属性、Dataset、Datatable、URI、Runtime Type</para>
        /// </summary>
        /// <param name="original">The current object.</param>
        /// <param name="target">the target object to compare.</param>
        /// <param name="maxDifference">A number, suppose how many differences will be found. It'll not continue if the number of differences is over than this number. </param>
        public static string CompareForDetails(this object original, object target, int maxDifference = 1)
        {
            string result;

            // 备份当前允许的最大不同点数，便于比较完成后恢复
            int defaultValue = GlobalSetting.CompareComponent.MaxDifferences;
            GlobalSetting.CompareComponent.MaxDifferences = maxDifference;

            bool flag = GlobalSetting.CompareComponent.Compare(original, target);

            if(flag == true)
            {
                result = "There is no difference between them.";
            }
            else
            {
                result = GlobalSetting.CompareComponent.DifferencesString;
            }

            GlobalSetting.CompareComponent.MaxDifferences = defaultValue;

            return result;
        }
	}
}

