using System.Configuration;
using System.Web;

namespace Langbry.Config
{
    /// <summary>
    /// 세션에 저장되는 언어타입
    /// </summary>
    public class SessionLanguageTypeElement : ConfigurationElement
    {
        [ConfigurationProperty("value", DefaultValue = "SESSION_LANGUAGE_TYPE", IsRequired = true)]
        public string Value { get { return (string)this["value"]; } set { this["value"] = value; } }
    }

    /// <summary>
    /// 변역파일경로(json)
    /// </summary>
    public class TranslationJsonFilePathElement : ConfigurationElement
    {
        [ConfigurationProperty("value", DefaultValue = "langbry_data.json", IsRequired = true)]
        public string Value
        {
            get
            {
                string path = (string)this["value"];
                return path.IndexOf("\\") == -1 ? HttpRuntime.AppDomainAppPath + path : path;
            }
            set { this["value"] = value; }
        }
    }

    /// <summary>
    /// 클라이언트 뷰 사용여부
    /// </summary>
    public class ViewElement : ConfigurationElement
    {
        /// <summary>
        /// 브라우저 스크립트 사용여부
        /// </summary>
        [ConfigurationProperty("enable", DefaultValue = true, IsRequired = true)]
        public bool Enable { get { return (bool)this["enable"]; } set { this["enable"] = value; } }

        [ConfigurationProperty("JavascriptLanguageFileName")]
        public JavascriptLanguageFileNameElement JavascriptLanguageFileName { get { return (JavascriptLanguageFileNameElement)this["JavascriptLanguageFileName"]; } set { this["JavascriptLanguageFileName"] = value; } }

        [ConfigurationProperty("JavascriptLanguageDataObjectName")]
        public JavascriptLanguageDataObjectNameElement JavascriptLanguageDataObjectName { get { return (JavascriptLanguageDataObjectNameElement)this["JavascriptLanguageDataObjectName"]; } set { this["JavascriptLanguageDataObjectName"] = value; } }

        [ConfigurationProperty("BrowserCookieLanguageCodeName")]
        public BrowserCookieLanguageCodeNameElement BrowserCookieLanguageCodeName { get { return (BrowserCookieLanguageCodeNameElement)this["BrowserCookieLanguageCodeName"]; } set { this["BrowserCookieLanguageCodeName"] = value; } }

        [ConfigurationProperty("HtmlTagName")]
        public HtmlTagNameElement HtmlTagName { get { return (HtmlTagNameElement)this["HtmlTagName"]; } set { this["HtmlTagName"] = value; } }
    }

    /// <summary>
    /// 스크립트 저장 파일 이름
    /// </summary>
    public class JavascriptLanguageFileNameElement : ConfigurationElement
    {
        [ConfigurationProperty("value", DefaultValue = "langbry_app.js", IsRequired = true)]
        public string Value
        {
            get
            {
                string path = (string)this["value"];
                return path.IndexOf("\\") == -1 ? HttpRuntime.AppDomainAppPath + path : path;
            }
            set { this["value"] = value; }
        }
    }

    /// <summary>
    /// 스크립트에 적용되는 번역내용 객체이름
    /// </summary>
    public class JavascriptLanguageDataObjectNameElement : ConfigurationElement
    {
        [ConfigurationProperty("value", DefaultValue = "_langbry_data", IsRequired = true)]
        public string Value { get { return (string)this["value"]; } set { this["value"] = value; } }
    }

    /// <summary>
    /// 브라우저 언어타입 쿠키이름
    /// </summary>
    public class BrowserCookieLanguageCodeNameElement : ConfigurationElement
    {
        [ConfigurationProperty("value", DefaultValue = "langbry_language_code", IsRequired = true)]
        public string Value { get { return (string)this["value"]; } set { this["value"] = value; } }

        [ConfigurationProperty("expireday", DefaultValue = 2, IsRequired = false)]
        public int ExpireDay { get { return (int)this["expireday"]; } set { this["expireday"] = value; } }

        [ConfigurationProperty("domain", DefaultValue = "", IsRequired = false)]
        public string Domain { get { return (string)this["domain"]; } set { this["domain"] = value; } }
    }

    /// <summary>
    /// 적용된 언어리스트 캐시이름
    /// </summary>
    public class AppliedLanguageListCachePrefixNameElement : ConfigurationElement
    {
        [ConfigurationProperty("value", DefaultValue = "APPLIED_LANGUAGELIST_CACHE", IsRequired = true)]
        public string Value { get { return (string)this["value"]; } set { this["value"] = value; } }
    }

    /// <summary>
    /// 적용된 모든언어리스트 캐시이름
    /// </summary>
    public class AppliedLanguageTableCacheNameElement : ConfigurationElement
    {
        [ConfigurationProperty("value", DefaultValue = "APPLIED_LANGUAGETABLE_CACHE", IsRequired = true)]
        public string Value { get { return (string)this["value"]; } set { this["value"] = value; } }
    }

    /// <summary>
    /// html 작성을 위한 tag name
    /// </summary>
    public class HtmlTagNameElement : ConfigurationElement
    {
        [ConfigurationProperty("value", DefaultValue = "langbry", IsRequired = true)]
        public string Value { get { return (string)this["value"]; } set { this["value"] = value; } }
    }

    /// <summary>
    /// 언어변경파일 적용간격시간(ms, 1초=1000)
    /// </summary>
    public class ApplyLanguageFileUpdateTimeElement : ConfigurationElement
    {
        [ConfigurationProperty("value", DefaultValue = "3000", IsRequired = true)]
        public int Value { get { return (int)this["value"]; } set { this["value"] = value; } }
    }
}