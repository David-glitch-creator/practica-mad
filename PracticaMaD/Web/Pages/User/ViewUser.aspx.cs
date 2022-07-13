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
    public partial class ViewUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            long userId;

            int startIndex, count;

            lnkPrevious.Visible = false;
            lnkNext.Visible = false;
            lblNoImages.Visible = false;
            btnFollow.Visible = false;
            btnUnfollow.Visible = false;
            lnkRegisterToFollow.Visible = false;

            /* Get UserId */
            try
            {
                userId = Int32.Parse(Request.Params.Get("userId"));
            }
            catch (ArgumentNullException)
            {
                
                return;
            }

            /* If userId is our userId, we are redirected to our profile */
            if (SessionManager.IsUserAuthenticated(Context)&&(userId == SessionManager.GetUserInfo(Context).UserId))
            {
                Response.Redirect(Response.
                        ApplyAppPathModifier("~/Pages/User/MyProfile.aspx"));
            }

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

            IUserService userService = ioCManager.Resolve<IUserService>();

            IImageService imageService = ioCManager.Resolve<IImageService>();

            UserInfo userInfo = userService.GetUserInfo(userId);

            lblLoginName.Text = userInfo.LoginName;
            lblFirstName.Text = userInfo.FirstName;
            lblLastName.Text = userInfo.Lastname;

            if (SessionManager.IsUserAuthenticated(Context))
            {
                UserInfo myInfo = SessionManager.GetUserInfo(Context);

                if (userService.IsFollow(userId, myInfo.UserId))
                {
                    btnUnfollow.Visible = true;
                }
                else
                {
                    btnFollow.Visible = true;
                }
            }
            else
            {
                lnkRegisterToFollow.Visible = true;
            }

            ImageBlock imageBlock = imageService.GetImagesByUser(userInfo.UserId, startIndex, count);

            if (imageBlock.Images.Count == 0)
            {
                lblNoImages.Visible = true;
                return;
            }

            this.gvImagesViewUser.DataSource = imageBlock.Images;
            this.gvImagesViewUser.DataBind();

            /* "Previous" link */
            if ((startIndex - count) >= 0)
            {
                String url =
                    "/Pages/User/ViewUser.aspx" +
                    "?userId=" + userId +
                    "&startIndex=" + (startIndex - count) + "&count=" +
                    count;

                this.lnkPrevious.NavigateUrl =
                    Response.ApplyAppPathModifier(url);
                this.lnkPrevious.Visible = true;
            }

            /* "Next" link */
            if (imageBlock.ExistMoreImages)
            {
                String url =
                    "/Pages/User/ViewUser.aspx" +
                    "?userId=" + userId +
                    "&startIndex=" + (startIndex + count) +
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

        protected void BtnFollow_Click(object sender, EventArgs e)
        {
            UserInfo myInfo = SessionManager.GetUserInfo(Context);

            IIoCManager ioCManager = (IIoCManager)Application["managerIoC"];

            IUserService userService = ioCManager.Resolve<IUserService>();

            long userId = Int32.Parse(Request.Params.Get("userId"));

            userService.FollowUser(userId, myInfo.UserId);

            Response.Redirect(Response.
                        ApplyAppPathModifier("~/Pages/User/ViewUser.aspx?userId=" + userId));
        }

        protected void BtnUnfollow_Click(object sender, EventArgs e)
        {
            UserInfo myInfo = SessionManager.GetUserInfo(Context);

            IIoCManager ioCManager = (IIoCManager)Application["managerIoC"];

            IUserService userService = ioCManager.Resolve<IUserService>();

            long userId = Int32.Parse(Request.Params.Get("userId"));

            userService.UnfollowUser(userId, myInfo.UserId);

            Response.Redirect(Response.
                        ApplyAppPathModifier("~/Pages/User/ViewUser.aspx?userId=" + userId));
        }
    }
}