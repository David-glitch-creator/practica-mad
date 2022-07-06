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
        private IIoCManager iocManager;
        private IUserService userService;
        private UserInfo userToFollow;
        

        protected void Page_Load(object sender, EventArgs e)
        {
            //Trace.IsEnabled = true;
            //obtenemos la persona a seguir
            String FollowedName = Request.Params.Get("txtName");

                //Obtenemos service
            this.iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            this.userService = iocManager.Resolve<IUserService>();

                //devolvemos el usurio al que se quiere seguir
                try
                {
                    this.userToFollow = userService.FindUserByLoginName(FollowedName);

                    lblTitleName2.Text = this.userToFollow.LoginName;
                    lblName2.Text = this.userToFollow.FirstName;
                    lbllastName2.Text = this.userToFollow.Lastname;
                    lblcountry2.Text = this.userToFollow.Country;
                }
                catch (InstanceNotFoundException)
                {
                    lblFindNameError.Visible = true;
                    lblTitleName2.Visible = false;
                    lblName.Visible = false;
                    lblName2.Visible = false;
                    lbllastName.Visible = false;
                    lbllastName2.Visible = false;
                    lblcountry.Visible = false;
                    lblcountry2.Visible = false;
                    btnFollow.Visible = false;
                }
            
        }

        protected void BtnNameClick(object sender, EventArgs e)
        {
            
            String url = String.Format("./ShowFollowers.aspx?txtName={0}", txtName.Text);
            Response.Redirect(Response.ApplyAppPathModifier(url));
        }

        protected void BtnFindByLoginClick(object sender, EventArgs e)
        {

        }

        protected void BtnShowAllLoginsClick(object sender, EventArgs e)
        {

        }

        protected void BtnFollowAlias(object sender, EventArgs e)
        {
            //usuario que da la orden para seguir a alguien
            UserInfo usuario = SessionManager.GetUserInfo(Context);

            //Obtenemos service           
            userService.FollowUser(this.userToFollow.UserId, usuario.UserId);
            
            //volvemos la la pagina
            String url = String.Format("~/Pages/User/MyProfile.aspx");
            Response.Redirect(Response.ApplyAppPathModifier(url));
        }
    }
}