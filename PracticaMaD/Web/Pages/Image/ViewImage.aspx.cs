using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.CommentService;
using Es.Udc.DotNet.PracticaMaD.Model.ImageService;
using Es.Udc.DotNet.PracticaMaD.Model.TagService;
using Es.Udc.DotNet.PracticaMaD.Model.UserService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.Image
{
    public partial class ViewImage : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            long imageId;

            lblNoImage.Visible = false;
            lnkAuthor.Visible = false;
            lblTitle.Visible = false;
            lblDescription.Visible = false;
            lblCategoryText.Visible = false;
            lblCategoryName.Visible = false;
            lblExifDetails.Visible = false;
            lblAperture.Visible = false;
            lblExposureTime.Visible = false;
            lblIso.Visible = false;
            lblWhiteBalance.Visible = false;

            lblLikesNumber.Visible = false;
            lblLikesText.Visible = false;
            btnLikeImage.Visible = false;
            btnDislikeImage.Visible = false;

            lnkAddComment.Visible = false;
            lnkComments.Visible = false;

            gvTags.Visible = false;
            lnkAddTags.Visible = false;

            btnDeleteImage.Visible = false;

            IIoCManager ioCManager = (IIoCManager)Application["managerIoC"];

            IImageService imageService = ioCManager.Resolve<IImageService>();

            ICommentService commentService = ioCManager.Resolve<CommentService>();

            ITagService tagService = ioCManager.Resolve<ITagService>();

            try
            {
                imageId = Int32.Parse(Request.Params.Get("imageId"));
            }
            catch (ArgumentNullException)
            {
                lblNoImage.Visible = true;
                return;
            }

            ImageDto image = imageService.GetImageById(imageId);

            if (image==null)
            {
                lblNoImage.Visible = true;
                return;
            }

            Byte[] arr = image.ImageFile;
            Image.ImageUrl = "data:image;base64," + Convert.ToBase64String(arr);

            Image.Attributes.Add("width", "220");
            Image.Attributes.Add("height", "220");

            lnkAuthor.Text = "<b>" + image.AuthorLogin + "</b>";
            lnkAuthor.NavigateUrl = Response.
                ApplyAppPathModifier("~/Pages/User/ViewUser.aspx?userId=" + image.AuthorId);
            lnkAuthor.Visible = true;

            lblTitle.Text = "<b><i>" + image.Title + "</i></b>";
            lblTitle.Visible = true;

            lblDescription.Text = image.ImageDescription;
            lblDescription.Visible = true;

            lblCategoryText.Visible = true;
            lblCategoryName.Text = image.CategoryName;
            lblCategoryName.Visible = true;

            lblExifDetails.Visible = true;

            lblAperture.Text += image.Aperture;
            lblAperture.Visible = true;

            lblExposureTime.Text += image.ExposureTime;
            lblExposureTime.Visible = true;

            lblIso.Text += image.Iso;
            lblIso.Visible = true;

            lblWhiteBalance.Text += image.WhiteBalance;
            lblWhiteBalance.Visible = true;

            int likesNumber = imageService.GetNumberOfLikes(imageId);
            if (likesNumber > 0)
            {
                lblLikesNumber.Text = likesNumber.ToString();
                lblLikesNumber.Visible = true;
                lblLikesText.Visible = true;
            }

            if (SessionManager.IsUserAuthenticated(Context))
            {
                UserInfo myInfo = SessionManager.GetUserInfo(Context);

                if (imageService.DoesLike(myInfo.UserId, imageId))
                {
                    btnDislikeImage.Visible = true;
                }
                else
                {
                    btnLikeImage.Visible = true;
                }
            }
            else
            {
                btnLikeImage.Visible = true;
            }

            String urlAddComment = "/Pages/Image/AddComment.aspx?imageId=" + imageId;
            lnkAddComment.NavigateUrl = Response.ApplyAppPathModifier(urlAddComment);
            lnkAddComment.Visible = true;

            if (commentService.HasComments(imageId))
            {
                String urlViewComments =
                    "/Pages/Image/ViewComments.aspx" +
                    "?imageId=" + imageId;

                lnkComments.NavigateUrl =
                    Response.ApplyAppPathModifier(urlViewComments);
                lnkComments.Visible = true;
            }

            List<TagDto> tags = tagService.GetTagsFromImage(imageId);
            if (tags.Count > 0)
            {
                gvTags.DataSource = tags;
                gvTags.DataBind();
                gvTags.Visible = true;
            }

            if (SessionManager.IsUserAuthenticated(Context) &&
                (SessionManager.GetUserInfo(Context).UserId == image.AuthorId))
            {
                lnkAddTags.NavigateUrl = Response.
                    ApplyAppPathModifier("~/Pages/Image/AddTags.aspx?imageId=" + imageId);
                lnkAddTags.Visible = true;
                btnDeleteImage.Visible = true;
            }
        }

        protected void BtnLikeImage_Click(object sender, EventArgs e)
        {
            long imageId = Int32.Parse(Request.Params.Get("imageId"));

            Response.Redirect(Response.
                        ApplyAppPathModifier("~/Pages/Image/LikeImage.aspx?ImageId=" + imageId));
        }

        protected void BtnDislikeImage_Click(object sender, EventArgs e)
        {
            UserInfo myInfo = SessionManager.GetUserInfo(Context);

            IIoCManager ioCManager = (IIoCManager)Application["managerIoC"];

            IImageService imageService = ioCManager.Resolve<IImageService>();

            long imageId = Int32.Parse(Request.Params.Get("imageId"));

            imageService.DislikeImage(myInfo.UserId, imageId);

            Response.Redirect(Response.
                        ApplyAppPathModifier("~/Pages/Image/ViewImage.aspx?ImageId=" + imageId));
        }

        protected void BtnDeleteImage_Click(object sender, EventArgs e)
        {
            long imageId = Int32.Parse(Request.Params.Get("imageId"));

            IIoCManager ioCManager = (IIoCManager)Application["managerIoC"];

            IImageService imageService = ioCManager.Resolve<IImageService>();

            imageService.DeleteImage(imageId);

            Response.Redirect(Response.
                        ApplyAppPathModifier("~/Pages/User/MyProfile.aspx"));
        }
    }
}