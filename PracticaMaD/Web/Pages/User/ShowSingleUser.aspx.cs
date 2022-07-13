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
    public partial class ShowSingleUser : SpecificCulturePage
    {
        private IIoCManager iocManager;
        private IUserService userService;
        private UserInfo userToFollow; //usuario resultante de la busqueda
        
        private UserInfo usuario; //usuario que da la orden de la busqueda
        private String FollowedName;


        protected void Page_Load(object sender, EventArgs e)
        {
            

            //obtenemos el nombre de la persona buscada            
            this.FollowedName = Request.Params.Get("txtName");

            //Obtenemos service
            this.iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            this.userService = iocManager.Resolve<IUserService>();

            //usuario que da la orden de la busqueda
            this.usuario = SessionManager.GetUserInfo(Context);

            
           //obtenemos la persona a seguir
                
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
            //volvemos la la pagina
            String url = String.Format("./ShowFollowers.aspx?txtName={0}", this.FollowedName);
            Response.Redirect(Response.ApplyAppPathModifier(url));
        }

        protected void BtnUnFollowAlias(object sender, EventArgs e)
        {
            //Dejamos de seguir           
            this.userService.UnfollowUser(this.userToFollow.UserId, this.usuario.UserId);
      
            //volvemos la la pagina
            String url = String.Format("./ShowFollowers.aspx?txtName={0}", this.FollowedName);
            Response.Redirect(Response.ApplyAppPathModifier(url));
        }
    }
}