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
    public partial class MyProfile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UserProfileDetails userProfileDetails =
                SessionManager.FindUserProfileDetails(Context);

            lblFirstName.Text = userProfileDetails.FirstName;
            lblLastName.Text = userProfileDetails.Lastname;
            
        }
    }
}