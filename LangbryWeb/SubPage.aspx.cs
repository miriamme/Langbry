﻿using Langbry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebTest
{
	public partial class SubPage : LanguagePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			this.lblTest.Text = Lang["2"];
		}
	}
}
