using Langbry.Config;

using System;
using System.Configuration;
using System.Globalization;
using System.Web;

namespace Langbry
{
    public class LanguageModule : IHttpModule
    {
        private LangbrySectionHandler _section;
        private LanguageType _languageType = LanguageType.EN;

        public void Init(HttpApplication context)
        {
            this._section = (LangbrySectionHandler)ConfigurationManager.GetSection("langbry");
            context.PreRequestHandlerExecute += _application_PreRequestHandlerExecute;
        }

        private void _application_PreRequestHandlerExecute(object sender, EventArgs e)
        {
            HttpApplication app = (HttpApplication)sender;
            HttpCookie cookie = app.Context.Request.Cookies[this._section.View.BrowserCookieLanguageCodeName.Value];

            if (app.Context.Session == null) return;

            if (app.Context.Session[this._section.SessionLanguageType.Value] == null || cookie == null)
            {
                if (app.Context.Request.UserLanguages == null)
                    this._languageType = LanguageType.EN;
                else
                {
                    CultureInfo ci = new CultureInfo(app.Context.Request.UserLanguages[0]);
                    if (ci == null)
                        this._languageType = LanguageType.EN;
                    else
                        this._languageType = (LanguageType)Enum.Parse(typeof(LanguageType), ci.TwoLetterISOLanguageName.ToUpper());
                }

                app.Context.Session[this._section.SessionLanguageType.Value] = this._languageType;
            }
            else
                this._languageType = (LanguageType)Enum.Parse(
                    typeof(LanguageType),
                    app.Context.Session[this._section.SessionLanguageType.Value].ToString());

            if (cookie == null)
            {
                cookie = new HttpCookie(this._section.View.BrowserCookieLanguageCodeName.Value, this._languageType.ToString());
                if (this._section.View.BrowserCookieLanguageCodeName.ExpireDay > 0)
                    cookie.Expires = DateTime.Now.AddDays(this._section.View.BrowserCookieLanguageCodeName.ExpireDay);
                if (this._section.View.BrowserCookieLanguageCodeName.Domain != "")
                    cookie.Domain = this._section.View.BrowserCookieLanguageCodeName.Domain;

                app.Context.Response.Cookies.Add(cookie);
            }
            else if (cookie.Value.ToUpper() != this._languageType.ToString())
            {
                foreach (string name in Enum.GetNames(typeof(LanguageType)))
                {
                    if (name == cookie.Value.ToUpper())
                    {
                        this._languageType = (LanguageType)Enum.Parse(typeof(LanguageType), cookie.Value.ToUpper());
                        break;
                    }
                }
            }

            app.Context.Session[this._section.SessionLanguageType.Value] = this._languageType;
        }

        public void Dispose()
        {
        }
    }
}