using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.TagService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.PracticaMaD.Web
{
    public partial class PracticaMaD : System.Web.UI.MasterPage
    {
        public static readonly String USER_SESSION_ATTRIBUTE = "userSession";
        protected void Page_Load(object sender, EventArgs e)
        {
            IIoCManager ioCManager = (IIoCManager)Application["managerIoC"];

            ITagService tagService = ioCManager.Resolve<ITagService>();

            List<TagDto> tags = tagService.GetByPopularity();

            lvTagCloud.DataSource = tags;
            lvTagCloud.DataBind();

            if (!SessionManager.IsUserAuthenticated(Context))
            {

                if (lblDash3 != null)
                    lblDash3.Visible = false;
                if (lnkUpdate != null)
                    lnkUpdate.Visible = false;
                if (lblDash4 != null)
                    lblDash4.Visible = false;
                if (lnkMyProfile != null)
                    lnkMyProfile.Visible = false;
                if (lblDash5 != null)
                    lblDash5.Visible = false;
                if (lnkUpload != null)
                    lnkUpload.Visible = false;
                if (lblDash6 != null)
                    lblDash6.Visible = false;
                if (lnkLogout != null)
                    lnkLogout.Visible = false;

            }
            else
            {
                if (lblWelcome != null)
                    lblWelcome.Text =
                        GetLocalResourceObject("lblWelcome.Hello.Text").ToString()
                        + " " + SessionManager.GetUserSession(Context).FirstName;
                if (lblDash2 != null)
                    lblDash2.Visible = false;
                if (lnkAuthenticate != null)
                    lnkAuthenticate.Visible = false;
            }
        }
    }
}