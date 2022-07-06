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
    public partial class Followed : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            lblNoUsersFollowed.Visible = false;

            IIoCManager ioCManager = (IIoCManager)Application["managerIoC"];

            IUserService userService = ioCManager.Resolve<IUserService>();

            UserInfo userInfo =
                SessionManager.GetUserInfo(Context);

            List<UserInfo> followed = userService.ViewFollowedUsers(userInfo.UserId);

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