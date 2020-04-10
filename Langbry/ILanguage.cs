using Langbry.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Langbry
{
	interface ILanguage
	{
		string GetLanguageCodeCookieName();
		string GetLanguageCode(string value);
	}
}
