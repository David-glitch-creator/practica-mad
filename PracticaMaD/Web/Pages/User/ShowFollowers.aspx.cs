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
    public partial class ShowFollowers : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblNameError.Visible = false;


            //obtenemos la persona a seguir
            String FollowedName = Request.Params.Get("txtName");

            //Obtenemos service
            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            IUserService userService = iocManager.Resolve<IUserService>();

            //devolvemos el usurio al que se quiere seguir
            UserInfo user = userService.FindUserByLoginName(FollowedName);
        }

        protected void BtnNameClick(object sender, EventArgs e)
        {            
            String url = String.Format("./MyProfile.aspx");
            Response.Redirect(Response.ApplyAppPathModifier(url));
        }

   
    }
}