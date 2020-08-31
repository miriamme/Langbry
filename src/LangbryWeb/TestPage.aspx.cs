using Langbry;

using System;

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