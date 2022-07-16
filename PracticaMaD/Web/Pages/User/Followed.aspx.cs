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
    public partial class Followed : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            long userId;

            lblUserNotFound.Visible = false;
            lblNoUsersFollowed.Visible = false;

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

            List<UserInfo> followed = userService.ViewFollowedUsers(userId);

            if (followed.Count == 0)
            {
                lblNoUsersFollowed.Visible = true;
                return;
            }

            gvFollowed.DataSource = followed;
            gvFollowed.DataBind();
        }
    }
}