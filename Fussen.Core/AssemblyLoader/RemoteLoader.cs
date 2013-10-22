using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Fussen.Core
{
    [Serializable]
    internal class RemoteLoader {
        public IDictionary<String, Assembly> Addins;
        public RemoteLoader() {
            Addins = new Dictionary<String, Assembly>();
        }

        internal Assembly LoadAssembly(Byte[] buff)
        {
            Assembly assembly = Assembly.Load(buff);
            if (!Addins.Keys.Contains(assembly.FullName)) {
                Addins.Add(assembly.FullName, assembly);
            }

            return assembly;
        }

        internal Assembly LoadAssembly(string assemblyString)
        {
            Assembly assembly = Assembly.Load(assemblyString);
            if (!Addins.Keys.Contains(assembly.FullName)) {
                Addins.Add(assembly.FullName, assembly);
            }

            return assembly;
        }

        internal Assembly LoadAssemblyFile(string path)
        {
            Assembly assembly = Assembly.LoadFile(path);
            if (!Addins.Keys.Contains(assembly.FullName)) {
                Addins.Add(assembly.FullName, assembly);
            }

            return assembly;
        }
    }
}
