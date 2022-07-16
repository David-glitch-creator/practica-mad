using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.UserService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.User
{
    public partial class Followers : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            long userId;

            lblUserNotFound.Visible = false;
            lblNoFollowers.Visible = false;

            /* Get UserId */
            try
            {
                userId = Int32.Parse(Request.Params.Get("userId"));
            }
            catch (ArgumentNullException)
            {
                lblUserNotFound.Visible = true;
                return;
            }

            IIoCManager ioCManager = (IIoCManager)Application["managerIoC"];

            IUserService userService = ioCManager.Resolve<IUserService>();

            List<UserInfo> followers = userService.GetFollowers(userId);

            if (followers.Count == 0)
            {
                lblNoFollowers.Visible = true;
                return;
            }

            gvFollowers.DataSource = followers;
            gvFollowers.DataBind();
        }
    }
}