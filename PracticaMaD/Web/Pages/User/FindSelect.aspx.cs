using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.UserService;
using Es.Udc.DotNet.PracticaMaD.Model.UserService.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;


namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.User
{
    public partial class FindSelect : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void BtnShowAllLoginsClick(object sender, EventArgs e)
        {

        }

        protected void BtnFindByLoginClick(object sender, EventArgs e)
        {
            String url = String.Format("./FindFollowers.aspx");
            Response.Redirect(Response.ApplyAppPathModifier(url));
        }
    }
}