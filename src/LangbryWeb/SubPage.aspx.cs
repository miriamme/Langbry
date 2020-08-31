using Langbry;

using System;

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