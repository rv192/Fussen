using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;

using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Mozilla.NUniversalCharDet;
using Fussen.Core.DataContract;
using Fussen.Core.Constants;

namespace Fussen.Core
{
	public static class CrawlUtility
	{
		static CrawlUtility()
		{
			System.Net.ServicePointManager.Expect100Continue = false; 

			_Random = new Random ();
		}

		#region Detect Encoding Method
		/// <summary>
		/// Detect a stream and retrun the encoding type of the stream based on probability algorithm.
		/// </summary>
		/// <param name="detectStream"></param>
		/// <returns></returns>
		public static Encoding DetectEncoding(Stream stream)
		{
			Encoding result;

			string encodingString = "UTF-8";
			int nDetLen = 0;
			byte[] detectBuff = new byte[4096];
			UniversalDetector Det = new UniversalDetector(null);

			// 从流中读取内容，直到判断出编码类型为止
			while ((nDetLen = stream.Read(detectBuff, 0, detectBuff.Length)) > 0 && !Det.IsDone())
			{
				Det.HandleData(detectBuff, 0, detectBuff.Length);
			}
			Det.DataEnd();

			if (Det.GetDetectedCharset() != null)
			{
				encodingString = Det.GetDetectedCharset();
			}
			result = Encoding.GetEncoding(encodingString);

			// 重置流的指针到开始位置
			stream.Seek(0, SeekOrigin.Begin);

			return result;
		}

		public static Encoding DetectEncoding(Byte[] bytes)
		{
			String DEFAULT_ENCODING = "UTF-8";
			UniversalDetector detector =
				new UniversalDetector(null);
			detector.HandleData(bytes, 0, bytes.Length);
			detector.DataEnd();

			String encoding = detector.GetDetectedCharset();
			if (encoding == null)
			{
				encoding = DEFAULT_ENCODING;
			}
			detector.Reset();

			return Encoding.GetEncoding(encoding);
		}

		#endregion

		#region Request Web Page in Get way.

		/// <summary>
		/// Request a url address and return html content with right encoding.
		/// </summary>
		/// <param name="uri"></param>
		/// <param name="userAgent">Reference class CorsairStudio.Core.Enums.UserAgentType</param>
		/// <param name="connectionLimit">非常重要：单个进程中允许的HttpWebRequest发起的最大并行数，如果该值过高，容易导致被防火墙拦截</param>
		/// <param name="webProxy">A Proxy</param>
		/// <param name="fakeIP">Fake IP Address</param>
		/// <param name="currentEncoding">Encoding which the target webpage is</param>
		/// <returns></returns>
		internal static MemoryStream WebGet(Uri uri, CookieContainer cookies, bool autoredirect, int connectionLimit, string userAgent, WebProxy webProxy, IPAddress fakeIP, ref Encoding currentEncoding)
		{
			MemoryStream result = null;

			HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(uri);

			#region 设定Proxy, Timeout, Method, FakeIP, Agent
			// 是否允许被服务器端重定向（该值关系到提交后返回的内容）
			request.AllowAutoRedirect = autoredirect;

			// 该值非常重要，关系到是否会被封IP，缺省值2
			request.ServicePoint.ConnectionLimit = connectionLimit;

			request.Timeout = 1000 * 60;
			request.Method = "get";
			request.CookieContainer = cookies;

			if (webProxy != null)
			{
				request.Proxy = webProxy;

				// 若需要用伪造的IP访问目标
				if (fakeIP != null)
				{
					request.Headers.Add("VIA", webProxy.Address.Host);
					request.Headers.Add("X_FORWARDED_FOR", fakeIP.ToString());
				}
			}

			if (request.Proxy != null && request.Proxy.Credentials == null)
			{
				request.Proxy.Credentials = CredentialCache.DefaultCredentials;
			}
			request.UserAgent = userAgent;
			#endregion

			HttpWebResponse response = (HttpWebResponse)request.GetResponse();

			if (response.StatusCode == HttpStatusCode.OK)
			{
				Stream stream = response.GetResponseStream();
				result = new MemoryStream();
				int len = 0;
				byte[] buff = new byte[4096];

				// 读取请求返回的流
				while ((len = stream.Read(buff, 0, buff.Length)) > 0)
				{
					result.Write(buff, 0, len);
				}
				response.Close();

				if (result.Length > 0)
				{
					result.Seek(0, SeekOrigin.Begin);

					// 若没指定编码类型，则进行判定操作
					if (currentEncoding == null)
					{
						// 若ContentType不为空，则根据ContentType获取编码类型
						if (String.IsNullOrEmpty(response.ContentType) == false)
						{
							Match matchContentType = Regex.Match(response.ContentType, "charset=(?<charset>.+)");
							if (matchContentType.Success == true)
							{
								currentEncoding = Encoding.GetEncoding(matchContentType.Groups["charset"].Value);
							}
						}

						// 如果还是未能获取编码，就调用FireFox的判断类推断
						if(currentEncoding == null)
						{
							currentEncoding = DetectEncoding(result);
						}
					}

					// 重置流指针
					result.Seek(0, SeekOrigin.Begin);
				}
			}

			return result;
		}

		/// <summary>
		/// 使用RequestDescription对象进行Web交互提交，提交反馈的结果也在RequestDescription对象的Result和ResultStream属性中
		/// </summary>
		/// <param name="desc">RequestDescription, including some infomations about how to request</param>
		/// <param name="connectionLimit">非常重要：单个进程中允许的HttpWebRequest发起的最大并行数，如果该值过高，容易导致被防火墙拦截</param>
		/// <param name="proxy">代理</param>
		/// <returns></returns>
		public static void WebGet(RequestDescription desc, CookieContainer cookies = null, int connectionLimit = 2, WebProxy proxy = null, IPAddress fakeIP = null)
		{
			string result;
			if (desc.RequestMethod.ToLower() == "get")
			{
				Encoding encoding = desc.Encoding;

				MemoryStream ms = WebGet(desc.RequestUri, cookies, desc.AllowAutoRedirect, connectionLimit, UserAgentType.IE6OnWinXP, proxy, fakeIP, ref encoding);
				desc.ResultStream = ms;

				if (ms != null)
				{
					desc.Encoding = encoding;
					StreamReader reader = new StreamReader(ms, desc.Encoding);
					desc.Result = reader.ReadToEnd();
				}

				result = desc.Result;
			}
			else
			{
				throw new DesignTimeException("The RequestMethod of argument 'desc' isn't 'get', please use the method 'PostWebPage' instead.");
			}
		}

		public static string WebGet(Uri uri, CookieContainer cookies = null, bool autoredirect = true, int connectionLimit = 2, string agent = UserAgentType.IE6OnWinXP, WebProxy proxy = null, IPAddress fakeIP = null)
		{
			string result = null;

			Encoding encoding = null;
			MemoryStream ms = WebGet(uri, cookies, autoredirect, connectionLimit, agent, proxy, fakeIP, ref encoding);

			if (ms != null)
			{
				StreamReader reader = new StreamReader(ms, encoding);
				result = reader.ReadToEnd();
			}

			return result;
		}

		/// <summary>
		/// 使用Get方式从指定页面获取Json对象
		/// </summary>
		/// <typeparam name="Entity"></typeparam>
		/// <param name="uri"></param>
		/// <param name="cookies"></param>
		/// <param name="connectionLimit"></param>
		/// <param name="agent"></param>
		/// <param name="proxy"></param>
		/// <param name="fakeIP"></param>
		/// <returns></returns>
		public static Entity WebGet<Entity>(Uri uri, CookieContainer cookies = null, int connectionLimit = 2, string agent = UserAgentType.IE6OnWinXP, WebProxy proxy = null, IPAddress fakeIP = null)
		{
			Entity result = default(Entity);

			Encoding encoding = null;
			MemoryStream ms = WebGet(uri, cookies, false, connectionLimit, agent, proxy, fakeIP, ref encoding);

			if (ms != null)
			{
				StreamReader reader = new StreamReader(ms, encoding);
				string jsonContent = reader.ReadToEnd();

				result = JsonConvert.DeserializeObject<Entity>(jsonContent);
			}

			return result;
		}
		#endregion

		#region Post Web Page in Post/Get way.
		/// <summary>
		/// Post a webpage.
		/// </summary>
		/// <param name="uri">Target Uri.</param>
		/// <param name="cookies">CookieContainer, should keep the same during all requests process.</param>
		/// <param name="contentType">Request Content Type</param>
		/// <param name="requestMethod">Normally it's Get or Post</param>
		/// <param name="connectionLimit">非常重要：单个进程中允许的HttpWebRequest发起的最大并行数，如果该值过高，容易导致被防火墙拦截</param>
		/// <param name="content"></param>
		/// <param name="userAgent"></param>
		/// <param name="webProxy"></param>
		/// <param name="fakeIP"></param>
		/// <param name="encoding"></param>
		/// <returns></returns>
		public static MemoryStream WebPost(Uri uri, string content, Encoding encoding, bool autoredirect = true, int connectionLimit = 2, CookieContainer cookies = null, string contentType = "application/x-www-form-urlencoded", string requestMethod = "Post", string userAgent = UserAgentType.IE6OnWinXP, WebProxy webProxy = null, IPAddress fakeIP = null)
		{
			MemoryStream result = null;

			HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(uri);

			#region 设定Proxy, Cookies, Timeout, Method, FakeIP, Agent, PostContent
			// 是否允许被服务器端重定向（该值关系到提交后返回的内容）
			request.AllowAutoRedirect = autoredirect;
			// 该值非常重要，关系到是否会被封IP，缺省值2
			request.ServicePoint.ConnectionLimit = connectionLimit;
			request.Timeout = 1000 * 60;
			request.Method = requestMethod;
			request.CookieContainer = cookies;

			if (string.IsNullOrEmpty(contentType) == false)
			{
				request.ContentType = contentType;
			}

			if (webProxy != null)
			{
				request.Proxy = webProxy;

				//// 若需要用伪造的IP访问目标
				if (fakeIP != null)
				{
					request.Headers.Add("VIA", webProxy.Address.Host);
					request.Headers.Add("X_FORWARDED_FOR", fakeIP.ToString());
				}
			}

			if (request.Proxy != null && request.Proxy.Credentials == null)
			{
				request.Proxy.Credentials = CredentialCache.DefaultCredentials;
			}
			request.UserAgent = userAgent;

			// 如果Post的内容不为空，则序列化后存入流中
			if (string.IsNullOrEmpty(content) == false)
			{
				byte[] byteArray = encoding.GetBytes(content);
				Stream streamRequest = request.GetRequestStream();
				// Send the data.
				streamRequest.Write(byteArray, 0, byteArray.Length);    // 写入参数
				streamRequest.Close();
			}

			#endregion

			HttpWebResponse response = (HttpWebResponse)request.GetResponse();

			if (response.StatusCode == HttpStatusCode.OK)
			{
				Stream streamResponse = response.GetResponseStream();
				result = new MemoryStream();
				int len = 0;
				byte[] buff = new byte[4096];

				// 读取请求返回的流
				while ((len = streamResponse.Read(buff, 0, buff.Length)) > 0)
				{
					result.Write(buff, 0, len);
				}
				response.Close();

				if (result.Length > 0)
				{
					// 重置流指针
					result.Seek(0, SeekOrigin.Begin);
				}
			}
			else
			{
				response.Close();
			}

			return result;
		}

		/// <summary>
		/// Post a web page and get json data back.
		/// </summary>
		/// <typeparam name="Entity"></typeparam>
		/// <param name="uri"></param>
		/// <param name="content"></param>
		/// <param name="encoding"></param>
		/// <param name="connectionLimit"></param>
		/// <param name="cookies"></param>
		/// <param name="contentType"></param>
		/// <param name="userAgent"></param>
		/// <param name="webProxy"></param>
		/// <param name="fakeIP"></param>
		/// <returns></returns>
		public static Entity WebPost<Entity>(Uri uri, string content, Encoding encoding, int connectionLimit = 2, CookieContainer cookies = null, string contentType = "application/x-www-form-urlencoded", string userAgent = UserAgentType.IE6OnWinXP, WebProxy webProxy = null, IPAddress fakeIP = null)
		{
			Entity result = default(Entity);

			MemoryStream ms = WebPost(uri, 
			                          content, 
			                          encoding, 
			                          false, 
			                          connectionLimit, 
			                          cookies, 
			                          contentType, 
			                          "Post", 
			                          userAgent, 
			                          webProxy, 
			                          fakeIP);

			if (ms != null)
			{
				StreamReader reader = new StreamReader(ms, encoding);
				string jsonContent = reader.ReadToEnd();

				result = JsonConvert.DeserializeObject<Entity>(jsonContent);
			}

			return result;
		}

		/// <summary>
		/// 使用RequestDescription对象进行Web交互提交，提交反馈的结果也在RequestDescription对象的Result和ResultStream属性中
		/// </summary>
		/// <param name="desc"></param>
		/// <param name="cookies"></param>
		/// <param name="connectionLimit">非常重要：单个进程中允许的HttpWebRequest发起的最大并行数，如果该值过高，容易导致被防火墙拦截</param>
		/// <param name="proxy"></param>
		/// <param name="fakeIP"></param>
		public static void WebPost(RequestDescription desc, CookieContainer cookies = null, int connectionLimit = 2, WebProxy proxy = null, IPAddress fakeIP = null)
		{
			string result;

			MemoryStream ms = WebPost(desc.RequestUri,
			                          desc.PostContent,
			                          desc.Encoding, 
			                          desc.AllowAutoRedirect,
			                          connectionLimit,
			                          cookies,
			                          desc.ContentType,
			                          desc.RequestMethod,
			                          desc.UserAgent,
			                          proxy,
			                          fakeIP);

			if (ms != null)
			{
				desc.ResultStream = ms;
				StreamReader reader = new StreamReader(ms, desc.Encoding);
				result = reader.ReadToEnd();
				desc.Result = result;
			}
		}

		#endregion

		#region Covert Characters between Simplified Chinese and Traditional Chinese
		private const int LOCALE_SYSTEM_DEFAULT = 0x0800;
		private const int LCMAP_SIMPLIFIED_CHINESE = 0x02000000;
		private const int LCMAP_TRADITIONAL_CHINESE = 0x04000000;

		[DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern int LCMapString(int Locale, int dwMapFlags, string lpSrcStr, int cchSrc, [Out] string lpDestStr, int cchDest);

		/// <summary>
		/// 将字符串转换成简体中文
		/// </summary>
		/// <param name="source">输入要转换的字符串</param>
		/// <returns>转换完成后的字符串</returns>
		public static string ToSimplified(string source)
		{
			String target = new String(' ', source.Length);
			int ret = LCMapString(LOCALE_SYSTEM_DEFAULT, LCMAP_SIMPLIFIED_CHINESE, source, source.Length, target, source.Length);
			return target;
		}

		/// <summary>
		/// 将字符串转换为繁体中文
		/// </summary>
		/// <param name="source">输入要转换的字符串</param>
		/// <returns>转换完成后的字符串</returns>
		public static string ToTraditional(string source)
		{
			String target = new String(' ', source.Length);
			int ret = LCMapString(LOCALE_SYSTEM_DEFAULT, LCMAP_TRADITIONAL_CHINESE, source, source.Length, target, source.Length);
			return target;
		}
		#endregion

		public static IPAddress RandomIPAddress()
		{
			IPAddress result;
			int part1 = _Random.Next(20, 250);
			int part2 = _Random.Next(20, 250);
			int part3 = _Random.Next(1, 254);
			int part4 = _Random.Next(1, 254);

			result = IPAddress.Parse(string.Format("{0}.{1}.{2}.{3}", part1, part2, part3, part4));

			return result;
		}

		public static void KeepServiceActived(Uri uri, CancellationToken token, int interval)
		{
			Task task = new Task(() =>
			                     {
				while (true)
				{
					token.WaitHandle.WaitOne(interval);
					CrawlUtility.WebGet(uri);

				}
			}, token);

			task.Start();
		}

		private static Random _Random;
	}
}
