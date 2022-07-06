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
        private UserInfo userToFollow; //usuario resultante de la busqueda
        
        private UserInfo usuario; //usuario que da la orden de la busqueda
        String FollowedName;


        protected void Page_Load(object sender, EventArgs e)
        {
            //Trace.IsEnabled = true;

            //obtenemos el nombre de la persona buscada            
            this.FollowedName = Request.Params.Get("txtName");
            
            //Obtenemos service
            this.iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            this.userService = iocManager.Resolve<IUserService>();

            //usuario que da la orden de la busqueda
            this.usuario = SessionManager.GetUserInfo(Context);

            
            try
                {   //obtenemos la persona a seguir
                    this.userToFollow = this.userService.FindUserByLoginName(this.FollowedName);

                //comprobamos si esta siguiendo al usuario resultante de la busqueda

                //userService.IsFollow(this.userToFollow, this.usuario)

                    lblTitleName2.Text = this.userToFollow.LoginName;
                    lblName2.Text = this.userToFollow.FirstName;
                    lbllastName2.Text = this.userToFollow.Lastname;
                    lblcountry2.Text = this.userToFollow.Country;
                    btnUnFollow.Enabled = userService.IsFollow(this.userToFollow.UserId, this.usuario.UserId);
                    btnFollow.Enabled = !(btnUnFollow.Enabled);

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
                    btnUnFollow.Visible = false;
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

            //Seguimos al usuario           
            this.userService.FollowUser(this.userToFollow.UserId, this.usuario.UserId);
     Trace.Warn("INFO", txtName.Text);
            //volvemos la la pagina
            String url = String.Format("./ShowFollowers.aspx?txtName={0}", this.FollowedName);
            Response.Redirect(Response.ApplyAppPathModifier(url));
        }

        protected void BtnUnFollowAlias(object sender, EventArgs e)
        {
            //Dejamos de seguir           
            this.userService.UnfollowUser(this.userToFollow.UserId, this.usuario.UserId);
      
      Trace.Warn("INFO", txtName.Text);
            //volvemos la la pagina
            String url = String.Format("./ShowFollowers.aspx?txtName={0}", this.FollowedName);
            Response.Redirect(Response.ApplyAppPathModifier(url));
        }
    }
}