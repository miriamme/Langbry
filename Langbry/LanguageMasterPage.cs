using Langbry.Config;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Langbry
{
	public class LanguageMasterPage : System.Web.UI.MasterPage
	{
		private PageInvoker invoker;
		public Dictionary<string, string> Lang { get; private set; }
		public string LangCookieName { get; private set; }
		public string LangCode(string value) => invoker.GetLanguageCode(value);

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			invoker = new PageInvoker(Application, Session);
			Lang = invoker.GetLanguageDictionary();
			LangCookieName = invoker.GetLanguageCodeCookieName();
		}
	}
}
