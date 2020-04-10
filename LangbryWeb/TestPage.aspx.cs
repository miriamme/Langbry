using Langbry;
using Langbry.Config;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebTest
{
	public partial class TestPage : LanguagePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			this.lblTest.Text = Lang["2"];
			this.lblGetCode.Text = Lang.ContainsKey(LangCode("Register")) == true ? Lang[LangCode("Register")] : LangCode("Register");
		}
	}
}