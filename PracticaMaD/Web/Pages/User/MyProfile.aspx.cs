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
    public partial class MyProfile : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UserInfo userInfo =
                SessionManager.GetUserInfo(Context);

            lblLoginName.Text = userInfo.LoginName;
            lblFirstName.Text = userInfo.FirstName;
            lblLastName.Text = userInfo.Lastname;
            
        }
    }
}