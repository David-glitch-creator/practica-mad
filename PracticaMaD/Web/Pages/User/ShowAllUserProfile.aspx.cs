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
    public partial class ShowAllUserProfile : SpecificCulturePage
    {



        protected void Page_Load(object sender, EventArgs e)
        {
            //Obtenemos service
            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            IUserService userService = iocManager.Resolve<IUserService>();

            //usuario que da la orden de la busqueda
            UserInfo usuario = SessionManager.GetUserInfo(Context);


        }


    }
}