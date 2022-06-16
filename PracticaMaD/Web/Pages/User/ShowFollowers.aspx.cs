using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.UserService.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;


namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.User
{
    public partial class ShowFollowers : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblNameError.Visible = false;
            /* Get Nombre Persona a seguir */
            String FollowedName = Request.Params.Get("txtName");
        }

        protected void BtnNameClick(object sender, EventArgs e)
        {
            String url = String.Format("./MyProfile.aspx");
            Response.Redirect(Response.ApplyAppPathModifier(url));
        }


    }
}