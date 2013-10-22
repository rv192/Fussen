using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.ComponentModel.Composition.Primitives;

namespace Fussen.Core
{
	/// <summary>
	/// 协助MEF组件加载的工具类（待测）
	/// </summary>
	public static class MefyUtility
	{
		public static class GlobalContracts
		{
			/// <summary>
			/// <para>用途：抽象Windows服务的Init，Start, Stop方法</para>
			/// <para>适用：接口CorsairStudio.Core.MEF.IWinService以及实现该接口的类</para>
			/// </summary>
			public const string SERVER_OPERATION_INTERFACE = "CorsairStudio:MEF:Interface:CorsairStudio.Core.MEF.IWinService";
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="obj"></param>
		/// <param name="aliasExtensions"></param>
		public static void Mefy(this object obj, params string[] aliasExtensions)
		{
			//A catalog that can aggregate other catalogs  
			AggregateCatalog aggrCatalog = new AggregateCatalog();

			// 如果参数不为空，则尝试在配置文件中查找MEF化程序集的信息
			if (aliasExtensions != null && aliasExtensions.Count() > 0)
			{
				// 首先在程序集中查找指定的目标
//				IList<string> assemblies = Utility.CoreConfiguration.CommonModule.AssemblyExtensions.Assemblies
//					.Where(e => aliasExtensions.Contains(e.Alias))
//						.Select(e => e.FullName).ToList();
//
//				// 尝试找在目录集下查找指定的目标
//				IList<string> directories = Utility.CoreConfiguration.CommonModule.AssemblyExtensions.Directories
//					.Where(e => aliasExtensions.Contains(e.Alias))
//						.Select(e => e.Path).ToList();
//
//				// 加载指定的程序集
//				LoadAssemblies(aggrCatalog, assemblies);
//
//				// 加载指定的目录下所有程序集
//				LoadDirectories(aggrCatalog, directories);
			}

			//An assembly catalog to load information about part from this assembly  
			var asmCatalog = new AssemblyCatalog(Assembly.GetExecutingAssembly());

			aggrCatalog.Catalogs.Add(asmCatalog);            

			//Create a container  
			CompositionContainer container = new CompositionContainer(aggrCatalog);

			CompositionBatch batch = new CompositionBatch();
			ComposablePart part = AttributedModelServices.CreatePart(obj);
			batch.AddPart(part);

			//Composing the parts 
			container.Compose(batch);
		}

		private static void LoadAssemblies(AggregateCatalog aggrCatalog, IList<string> assemblies)
		{
			Loader loader = Loader.CreateLoader();

			foreach (string assemblyName in assemblies)
			{
				Assembly assembly = loader.Load(assemblyName);
				AssemblyCatalog asmCatalog = new AssemblyCatalog(assembly);

				aggrCatalog.Catalogs.Add(asmCatalog);
			}
		}

		private static void LoadDirectories(AggregateCatalog aggrCatalog, IList<string> directories)
		{
			string appPath = AppDomain.CurrentDomain.BaseDirectory;

			foreach (string directoryName in directories)
			{
				string fullPath = Path.Combine(appPath, directoryName);
				if (Directory.Exists(fullPath) == true)
				{
					DirectoryCatalog catalog = new DirectoryCatalog(fullPath);
					aggrCatalog.Catalogs.Add(catalog);
				}
			}
		}
	}
}
