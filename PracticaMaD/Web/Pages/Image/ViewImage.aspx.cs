using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.CommentService;
using Es.Udc.DotNet.PracticaMaD.Model.ImageService;
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
    public partial class ViewImage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            long imageId;

            lblNoImage.Visible = false;
            lblAuthor.Visible = false;
            lblTitle.Visible = false;
            lblDescription.Visible = false;
            lblExifDetails.Visible = false;
            lblAperture.Visible = false;
            lblExposureTime.Visible = false;
            lblIso.Visible = false;
            lblWhiteBalance.Visible = false;

            lblLikesNumber.Visible = false;

            lnkAddComment.Visible = false;
            lnkComments.Visible = false;

            IIoCManager ioCManager = (IIoCManager)Application["managerIoC"];

            IImageService imageService = ioCManager.Resolve<IImageService>();

            ICommentService commentService = ioCManager.Resolve<CommentService>();

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

            lblAuthor.Text = "<b>" + image.AuthorLogin + "</b>";
            lblAuthor.Visible = true;

            lblTitle.Text = "<b><i>" + image.Title + "</i></b>";
            lblTitle.Visible = true;

            lblDescription.Text = image.ImageDescription;
            lblDescription.Visible = true;

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
                lblLikesNumber.Text = likesNumber + " Me gusta";
                lblLikesNumber.Visible = true;
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
        }

        protected void BtnLikeImage_Click(object sender, EventArgs e)
        {
            UserInfo myInfo = SessionManager.GetUserInfo(Context);

            IIoCManager ioCManager = (IIoCManager)Application["managerIoC"];

            IImageService imageService = ioCManager.Resolve<IImageService>();

            long imageId = Int32.Parse(Request.Params.Get("imageId"));

            imageService.LikeImage(myInfo.UserId, imageId);

            Response.Redirect(Response.
                        ApplyAppPathModifier("~/Pages/Image/ViewImage.aspx?ImageId=" + imageId));
        }
    }
}