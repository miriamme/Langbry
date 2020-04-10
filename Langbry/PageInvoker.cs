using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.SessionState;

namespace Langbry
{
	internal class PageInvoker : ILanguage
	{
		private readonly HttpApplicationState httpApplicationState;
		private readonly HttpSessionState httpSessionState;
		private readonly LanguageConfig config;

		public PageInvoker(HttpApplicationState httpApplicationState, HttpSessionState httpSessionState)
		{
			this.httpApplicationState = httpApplicationState;
			this.httpSessionState = httpSessionState;
			config = httpApplicationState[LanguageConfig.CurrentInstanceName] == null
				? null
				: (LanguageConfig)httpApplicationState[LanguageConfig.CurrentInstanceName];
		}

		public Dictionary<string, string> GetLanguageDictionary()
		{
			if (config == null)
				return new Dictionary<string, string>();
			else
				return config.GetLanguage(httpSessionState);
		}
		public string GetLanguageCode(string value)
		{
			if (config != null)
				return config.GetLanguageCode(value);
			else
				return value;
		}
		public string GetLanguageCodeCookieName()
		{
			if (config != null)
				return config.GetLanguageCodeCookieName();
			else
				return "";
		}
	}
}
