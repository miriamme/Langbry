using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Langbry.Config
{
	internal class LangbrySectionHandler : ConfigurationSection
	{
		[ConfigurationProperty("SessionLanguageType")]
		public SessionLanguageTypeElement SessionLanguageType { get { return (SessionLanguageTypeElement)this["SessionLanguageType"]; } set { this["SessionLanguageType"] = value; } }

		[ConfigurationProperty("TranslationJsonFilePath")]
		public TranslationJsonFilePathElement TranslationJsonFilePath { get { return (TranslationJsonFilePathElement)this["TranslationJsonFilePath"]; } set { this["TranslationJsonFilePath"] = value; } }

		[ConfigurationProperty("View")]
		public ViewElement View { get { return (ViewElement)this["View"]; } set { this["View"] = value; } }

		[ConfigurationProperty("AppliedLanguageListCachePrefixName")]
		public AppliedLanguageListCachePrefixNameElement AppliedLanguageListCachePrefixName { get { return (AppliedLanguageListCachePrefixNameElement)this["AppliedLanguageListCachePrefixName"]; } set { this["AppliedLanguageListCacheName"] = value; } }

		[ConfigurationProperty("AppliedLanguageTableCacheName")]
		public AppliedLanguageTableCacheNameElement AppliedLanguageTableCacheName { get { return (AppliedLanguageTableCacheNameElement)this["AppliedLanguageTableCacheName"]; } set { this["AppliedLanguageTableCacheName"] = value; } }

		[ConfigurationProperty("ApplyLanguageFileUpdateTime")]
		public ApplyLanguageFileUpdateTimeElement ApplyLanguageFileUpdateTime { get { return (ApplyLanguageFileUpdateTimeElement)this["ApplyLanguageFileUpdateTime"]; } set { this["ApplyLanguageFileUpdateTime"] = value; } }
	}
}
