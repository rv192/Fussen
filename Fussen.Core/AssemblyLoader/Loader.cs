using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;

namespace Fussen.Core
{
    /// <summary>
    /// In order to Load & Unload assembly dynamically
    /// </summary>
    [Serializable]
    public class Loader {
        public IDictionary<String, Assembly> Addins
        {
            get { return remoteLoader.Addins; }
        }

        static Loader()
        {
            _Loader = new Loader();
        }

        public static Loader CreateLoader()
        {
            return _Loader;
        }

        private Loader() {
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);
            AppDomainSetup setup = new AppDomainSetup();

            Assembly assembly = Assembly.GetExecutingAssembly();
            String applicationPath = Path.GetDirectoryName(assembly.Location);

            setup.ApplicationBase = applicationPath;
            setup.PrivateBinPath = applicationPath;
            setup.ShadowCopyDirectories = applicationPath;
            setup.ApplicationName = "AddinsContainer";
            setup.ShadowCopyFiles = "true";

            AppDomain appDomain = AppDomain.CreateDomain("Addins", null, setup);
            Type type = typeof(RemoteLoader);
            remoteLoader = (RemoteLoader)
                appDomain.CreateInstanceAndUnwrap(Assembly.GetExecutingAssembly().FullName, type.FullName);
        }

        /// <summary>
        /// Event that loading assembly expected when it doesn't exist in the current appDomain.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args) {
            Assembly result = AppDomain.CurrentDomain.GetAssemblies().Where(p => p.FullName == args.Name).FirstOrDefault();
            return result;
        }        

        /// <summary>
        /// Load assembly by binary array
        /// </summary>
        /// <param name="buff"></param>
        /// <returns></returns>
        public Assembly Load(byte[] buff) {
            return remoteLoader.LoadAssembly(buff);
        }

        /// <summary>
        /// Load assembly by assembly string
        /// </summary>
        /// <param name="assemblyString"></param>
        /// <returns></returns>
        public Assembly Load(String assemblyString) {
            return remoteLoader.LoadAssembly(assemblyString);
        }

        /// <summary>
        /// Load assembly by path of assembly file
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public Assembly LoadFile(String path) {
            return remoteLoader.LoadAssemblyFile(path);
        }

        private static Loader _Loader;
        private RemoteLoader remoteLoader;
    }
}
