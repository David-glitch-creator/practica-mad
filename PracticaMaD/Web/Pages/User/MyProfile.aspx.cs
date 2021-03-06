using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.ImageService;
using Es.Udc.DotNet.PracticaMaD.Model.UserService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using Es.Udc.DotNet.PracticaMaD.Web.Properties;
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

            lnkFollowedUsers.NavigateUrl = Response.
                ApplyAppPathModifier("~/Pages/User/Followed.aspx?userId=" + userInfo.UserId);

            lnkFollowers.NavigateUrl = Response.
                ApplyAppPathModifier("~/Pages/User/Followers.aspx?userId=" + userInfo.UserId);

            int startIndex, count;

            lnkPrevious.Visible = false;
            lnkNext.Visible = false;
            lblNoImages.Visible = false;

            /* Get Start Index */
            try
            {
                startIndex = Int32.Parse(Request.Params.Get("startIndex"));
            }
            catch (ArgumentNullException)
            {
                startIndex = 0;
            }

            /* Get Count */
            try
            {
                count = Int32.Parse(Request.Params.Get("count"));
            }
            catch (ArgumentNullException)
            {
                count = Settings.Default.PracticaMaD_defaultCount;
            }

            IIoCManager ioCManager = (IIoCManager)Application["managerIoC"];

            IImageService imageService = ioCManager.Resolve<IImageService>();

            ImageBlock imageBlock = imageService.GetImagesByUser(userInfo.UserId, startIndex, count);

            if (imageBlock.Images.Count == 0)
            {
                lblNoImages.Visible = true;
                return;
            }

            this.gvImagesMyProfile.DataSource = imageBlock.Images;
            this.gvImagesMyProfile.DataBind();

            /* "Previous" link */
            if ((startIndex - count) >= 0)
            {
                String url =
                    "/Pages/User/MyProfile.aspx" +
                    "?startIndex=" + (startIndex - count) + "&count=" +
                    count;

                this.lnkPrevious.NavigateUrl =
                    Response.ApplyAppPathModifier(url);
                this.lnkPrevious.Visible = true;
            }

            /* "Next" link */
            if (imageBlock.ExistMoreImages)
            {
                String url =
                    "/Pages/User/MyProfile.aspx" +
                    "?startIndex=" + (startIndex + count) +
                    "&count=" + count;

                this.lnkNext.NavigateUrl =
                    Response.ApplyAppPathModifier(url);
                this.lnkNext.Visible = true;
            }

        }

        protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                System.Web.UI.HtmlControls.HtmlImage imageControl =
                    (System.Web.UI.HtmlControls.HtmlImage)e.Row.FindControl("ImageControl");
                imageControl.Src = "data:image/png;base64," +
                    Convert.ToBase64String(((ImageDto)e.Row.DataItem).ImageFile);
            }
        }
    }
}