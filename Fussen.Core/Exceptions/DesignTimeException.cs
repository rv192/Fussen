using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fussen.Core
{
    /// <summary>
    /// DesignTime Exception. 
    /// It be used to wrap some exceptions when you develop. 
    /// If you know it will have something wrong in somewhere you can suppose in advanced 
    /// And it means someone call function or method in wrong way.
    /// This kind of exceptions should never be catched in produce environment.
    /// 设计时异常.
    /// 用于包装和捕捉开发人员在开发过程中因为功能调用错误导致的开发异常。
    /// 此类异常应该绝对不会出现在正式上线的产品中
    /// </summary>
    [global::System.Serializable]
    public class DesignTimeException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public DesignTimeException() { }
        public DesignTimeException(string message) : base(message) { }
        public DesignTimeException(string message, Exception inner) : base(message, inner) { }
        protected DesignTimeException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
