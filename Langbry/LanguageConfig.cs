using Langbry.Config;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace Langbry
{
	/// <summary>
	/// 언어프로세스 초기화 개체
	/// </summary>
	public sealed class LanguageConfig : ILanguage
	{
		private const string sectionName = "langbry";
		private readonly LangbrySectionHandler sectionInfo;
		private readonly HttpContext httpContext;
		private FileSystemWatcher jsonFileWatcher;

		private HttpApplication httpApplication;
		private IEnumerable<Language> Languages { get; set; }

		public static string CurrentInstanceName { get; private set; } = "";

		/// <summary>
		/// 언어설정 초기화
		/// </summary>
		/// <param name="application"></param>
		/// <param name="key">Application keyName (ex)Application["AppName"]</param>
		/// <param name="context"></param>
		public LanguageConfig(HttpApplication application, string key, HttpContext context)
		{
			sectionInfo = (LangbrySectionHandler)ConfigurationManager.GetSection(sectionName);
			httpApplication = application;
			CurrentInstanceName = CurrentInstanceName == "" ? key : CurrentInstanceName;
			httpContext = context;

			ApplicationInit();
			ReloadLanguageFile();
			Load();
		}

		private void ApplicationInit()
		{
			if (httpApplication != null && httpApplication.Application != null && CurrentInstanceName != "")
			{
				if (httpApplication.Application[CurrentInstanceName] == null)
					httpApplication.Application[CurrentInstanceName] = this;
			}
		}

		private void ReloadLanguageFile()
		{
			FileInfo fi = new FileInfo(sectionInfo.TranslationJsonFilePath.Value);
			jsonFileWatcher = new FileSystemWatcher
			{
				Path = fi.DirectoryName,
				NotifyFilter = NotifyFilters.LastWrite,
				Filter = fi.Name
			};
			jsonFileWatcher.Changed += Watcher_Changed;
			jsonFileWatcher.EnableRaisingEvents = true;
		}

		private void Load()
		{
			JsonFileInit();
			JavascriptEnableInit();
			CacheInit();
		}

		private void JsonFileInit()
		{
			try
			{
				//번역언어파일 초기화
				if (File.Exists(sectionInfo.TranslationJsonFilePath.Value))
				{
					using (StreamReader sr = new StreamReader(sectionInfo.TranslationJsonFilePath.Value))
					{
						Languages = JsonConvert.DeserializeObject<IEnumerable<Language>>(sr.ReadToEnd());
					}
				}
			}
			catch (Exception ex)
			{
				Exception exception = new Exception("번역파일이 없거나 포멧이 정확하지 않습니다. 번역파일 경로 및 포멧을 확인하세요.", ex);
				throw exception;
			}
		}

		private void JavascriptEnableInit()
		{
			if (Languages == null || Languages.Count() == 0 || !sectionInfo.View.Enable) return;

			try
			{
				StringBuilder sb = new StringBuilder();
				sb.Append($"var lang ={{");
				sb.Append($"type: null,");
				sb.Append($" val: function (c) {{");
				sb.Append($"	var code = typeof c === 'number' ? c.toString() : c;");
				sb.Append($"	var v = this.isIE() === true ? this.ieFind({sectionInfo.View.JavascriptLanguageDataObjectName.Value}, function (e) {{return e.code === code;}}) : {sectionInfo.View.JavascriptLanguageDataObjectName.Value}.find(function (n) {{ return n.code === code;}});");
				sb.Append($"	return v === undefined ? code : v[this.type];");
				sb.Append($"}},");
				sb.Append($"load: function () {{");
				sb.Append($"	if ($('{sectionInfo.View.HtmlTagName.Value}')) {{");
				sb.Append($"		$('{sectionInfo.View.HtmlTagName.Value}').each(function (i, e) {{");
				sb.Append($"			$(this).replaceWith(lang.val(e.innerHTML));");
				sb.Append($"		}});");
				sb.Append($"	}}");
				sb.Append($"}},");
				sb.Append($"isIE: function() {{");
				sb.Append($"	return window.navigator.userAgent.indexOf('MSIE ') > 0 || !!window.navigator.userAgent.match(/Trident.*rv\\:11\\./);");
				sb.Append($"}},");
				sb.Append($"ieFind: function (arr, callback) {{");
				sb.Append($"	for (var i = 0; i < arr.length; i++) {{");
				sb.Append($"		var match = callback(arr[i]);");
				sb.Append($"		if (match) return arr[i];");
				sb.Append($"	}}");
				sb.Append($"}},");
				sb.Append($"getCookie: function (cname) {{");
				sb.Append($"	var name = cname + '=';");
				sb.Append($"	var decodedCookie = unescape(document.cookie);");
				sb.Append($"	var ca = decodedCookie.split(';');");
				sb.Append($"	for (var i = 0; i < ca.length; i++) {{");
				sb.Append($"		var c = ca[i];");
				sb.Append($"		while (c.charAt(0) == ' ') c = c.substring(1);");
				sb.Append($"		if (c.indexOf(name) == 0) return c.substring(name.length, c.length);");
				sb.Append($"	}}");
				sb.Append($"	return '';");
				sb.Append($"}}");
				sb.Append($"}};");
				sb.Append($"var {sectionInfo.View.JavascriptLanguageDataObjectName.Value} = {JsonConvert.SerializeObject(Languages)};");
				sb.Append($"$(document).ready(function () {{");
				sb.Append($"	if(!lang.type) lang.type = lang.getCookie('{sectionInfo.View.BrowserCookieLanguageCodeName.Value}') === null ? 'en' : lang.getCookie('{sectionInfo.View.BrowserCookieLanguageCodeName.Value}').toLowerCase();");
				sb.Append($"	lang.load();");
				sb.Append($"}});");

				string path = sectionInfo.View.JavascriptLanguageFileName.Value;
				string directory = path.Substring(0, path.LastIndexOf("\\") + 1);
				if (!Directory.Exists(directory))
					Directory.CreateDirectory(directory);

				using (FileStream fs = File.Create(path))
				{
					Minifier minifier = new Minifier();
					byte[] info = new UTF8Encoding(true).GetBytes(minifier.MinifyJavaScript(sb.ToString()));
					fs.Write(info, 0, info.Length);
				}
			}
			catch (Exception ex)
			{
				Exception exception = new Exception("스크립트 파일 생성처리에서 오류가 발생했습니다.", ex);
				throw exception;
			}
		}

		private void CacheInit()
		{
			if (Languages == null || Languages.Count() == 0) return;

			string langTableCacheName = sectionInfo.AppliedLanguageTableCacheName.Value;
			string langListCachePrefix = sectionInfo.AppliedLanguageListCachePrefixName.Value;
			httpContext.Cache[langTableCacheName] = Languages;

			foreach (string name in Enum.GetNames(typeof(LanguageType)))
			{
				httpContext.Cache[langListCachePrefix + "_" + name] = UserLanguage(
						(LanguageType)Enum.Parse(typeof(LanguageType), name),
						(IEnumerable<Language>)httpContext.Cache[langTableCacheName]);
			}
		}

		private LanguageTable UserLanguage(LanguageType languageType, IEnumerable<Language> languages)
		{
			if (languages == null || languages.Count() == 0) return null;

			Func<Language, string> countryCode = null;
			switch (languageType)
			{
				case LanguageType.KO:
					countryCode = l => l.KO;
					break;
				case LanguageType.EN:
					countryCode = l => l.EN;
					break;
				case LanguageType.TH:
					countryCode = l => l.TH;
					break;
				case LanguageType.ID:
					countryCode = l => l.ID;
					break;
				default:
					countryCode = l => l.EN;
					break;
			}

			return new LanguageTable(languages.ToDictionary(d => d.Code, countryCode));
		}

		public Dictionary<string, string> GetLanguage(System.Web.SessionState.HttpSessionState sessionState)
		{
			LangbrySectionHandler section = (LangbrySectionHandler)ConfigurationManager.GetSection("langbry");
			if (section == null || sessionState == null) return null;
			if (sessionState[section.SessionLanguageType.Value] == null) return null;

			LanguageTable langTable = null;
			LanguageType sessionLangType = (LanguageType)sessionState[section.SessionLanguageType.Value];
			string langListCachePrefix = sectionInfo.AppliedLanguageListCachePrefixName.Value;

			if (httpContext.Cache[langListCachePrefix + "_" + sessionLangType.ToString()] != null)
				langTable = (LanguageTable)httpContext.Cache[langListCachePrefix + "_" + sessionLangType.ToString()];

			return langTable.LanguageDictionary;
		}

		public string GetLanguageCode(string value)
		{
			if (value.Trim() == "") return string.Empty;

			if (httpContext.Cache[sectionInfo.AppliedLanguageTableCacheName.Value] != null)
			{
				Func<Language, bool> countryCode = null;
				Language findLanguage = null;

				foreach (string name in Enum.GetNames(typeof(LanguageType)))
				{
					if (name == LanguageType.KO.ToString()) countryCode = l => l.KO.Equals(value);
					else if (name == LanguageType.EN.ToString()) countryCode = l => l.EN.Equals(value);
					else if (name == LanguageType.TH.ToString()) countryCode = l => l.TH.Equals(value);
					else if (name == LanguageType.ID.ToString()) countryCode = l => l.ID.Equals(value);
					else countryCode = l => l.EN.Equals(value);

					findLanguage = ((IEnumerable<Language>)httpContext.Cache[sectionInfo.AppliedLanguageTableCacheName.Value]).FirstOrDefault(countryCode);
					if (findLanguage != null)
						return findLanguage.Code;
				}
			}
			return value;
		}

		public string GetLanguageCodeCookieName()
		{
			return sectionInfo.View.BrowserCookieLanguageCodeName.Value;
		}

		#region Events

		private void Watcher_Changed(object sender, FileSystemEventArgs e)
		{
			System.Threading.Thread.Sleep(sectionInfo.ApplyLanguageFileUpdateTime.Value);
			Load();
		}

		#endregion
	}
}
