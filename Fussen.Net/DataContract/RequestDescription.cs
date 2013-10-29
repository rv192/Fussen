using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.IO;

namespace Fussen.Net.DataContract
{
    /// <summary>
    /// 描述一次Get/Post操作访问任务的实体类
    /// </summary>
    [DataContract]
    public class RequestDescription
    {
        public RequestDescription()
        {
            this.InstanceID = Guid.NewGuid();
            this.Encoding = Encoding.UTF8;
            this.AllowAutoRedirect = true;
        }

        /// <summary>
        /// 唯一编号
        /// </summary>
        [DataMember]
        public Guid InstanceID { get; set; }

        /// <summary>
        /// 执行顺序
        /// </summary>
        [DataMember]
        public int Order { get; set; }

        /// <summary>
        /// 目标Uri
        /// </summary>
        [DataMember]
        public Uri RequestUri { get; set; }

        /// <summary>
        /// Post or Get
        /// </summary>
        [DataMember]
        public string RequestMethod { get; set; }

        /// <summary>
        /// 是否允许服务器端的自动跳转（缺省允许）
        /// </summary>
        [DataMember]
        public bool AllowAutoRedirect { get; set; }

        /// <summary>
        /// Post content string
        /// </summary>
        [DataMember]
        public string PostContent { get; set; }

        /// <summary>
        /// 目标网站编码规范，缺省值UTF-8
        /// </summary>
        [DataMember]
        public Encoding Encoding { get; set; }

        /// <summary>
        /// 提交请求时的ContentType属性
        /// </summary>
        [DataMember]
        public string ContentType { get; set; }

        [DataMember]
        public string UserAgent { get; set; }

        [DataMember]
        public string Result { get; set; }

        [DataMember]
        public MemoryStream ResultStream { get; set; }

        #region 公有静态方法
        /// <summary>
        /// 根据Post的参数字典构建Post的Content字符串
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static string BuildContent(IDictionary<string, string> parameters)
        {
            string result = string.Empty;

            foreach (KeyValuePair<string, string> item in parameters)
            {
                result = string.Format("{0}&{1}={2}", result, item.Key, item.Value);
            }

            result = result.Trim('&');

            return result;
        }

        /// <summary>
        /// 对已存在的Content追加新的参数
        /// </summary>
        /// <param name="content"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string AppendContent(string content, string key, string value)
        {
            string result = content;

            result = string.Format("{0}&{1}={2}", content, key, value);
            result = result.Trim('&');

            return result;
        }

        #endregion
    }
}
