using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.ServiceModel
{
	public static class ChannelExtension
	{
		/// <summary>
		/// Close the remoting connection for the current WCF proxy instance without exception safely.
		/// 安全的为WCF调用代理对象关闭远程连接
		/// </summary>
		/// <param name="proxy"></param>
		/// <example>
		/// try
		/// {
		///     proxy.Method();
		/// }
		/// catch(Exception ex)
		/// {
		///     // Handle the exception
		/// }
		/// finally
		/// {
		///     proxy.CloseConnection();
		/// }
		/// </example>
		public static void CloseConnection(this System.ServiceModel.ICommunicationObject proxy)
		{
			if (proxy.State != System.ServiceModel.CommunicationState.Opened) { return; }

			try
			{
				proxy.Close();
			}
			catch (System.ServiceModel.CommunicationException)
			{
				proxy.Abort();
			}
			catch (TimeoutException)
			{
				proxy.Abort();
			}
			catch (Exception)
			{
				proxy.Abort();
				throw;
			}
		}
	}
}
