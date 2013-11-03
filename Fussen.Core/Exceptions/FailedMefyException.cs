using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fussen.Core
{
    [global::System.Serializable]
    public class FailedMefyException : DeploymentException
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public FailedMefyException() { }
        public FailedMefyException(string assemblyName) : base(string.Format("Failed to initialize the components(MEF) with assembly '{0}', please put the specific assembly in or remove the code which used the assembly.", assemblyName))
        {
        }

        public FailedMefyException(string assemblyName, Exception inner) : base(string.Format("Failed to initialize with assembly '{0}'", assemblyName), inner)
        {
        }

        protected FailedMefyException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
