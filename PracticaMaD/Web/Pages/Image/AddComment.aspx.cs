using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.CommentService;
using Es.Udc.DotNet.PracticaMaD.Model.ImageService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.Image
{
    public partial class AddComment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            long imageId;

            lblNoImage.Visible = false;

            lclCommentText.Visible = false;
            txtCommentText.Visible = false;
            btnAddComment.Visible = false;

            try
            {
                imageId = Int32.Parse(Request.Params.Get("imageId"));
            }
            catch (ArgumentNullException)
            {
                lblNoImage.Visible = true;
                return;
            }

            IIoCManager ioCManager = (IIoCManager)Application["managerIoC"];

            IImageService imageService = ioCManager.Resolve<IImageService>();

            ImageDto imageDto = imageService.GetImageById(imageId);

            Byte[] arr = imageDto.ImageFile;
            Image.ImageUrl = "data:image;base64," + Convert.ToBase64String(arr);

            Image.Attributes.Add("width", "220");
            Image.Attributes.Add("height", "220");

            lclCommentText.Visible = true;
            txtCommentText.Visible = true;
            btnAddComment.Visible = true;
        }

        protected void BtnAddComment_Click(object sender, EventArgs e)
        {
            long imageId = Int32.Parse(Request.Params.Get("imageId"));

            IIoCManager ioCManager = (IIoCManager)Application["managerIoC"];

            ICommentService commentService = ioCManager.Resolve<ICommentService>();

            long userId = SessionManager.GetUserInfo(Context).UserId;

            String commentText = txtCommentText.Text;

            commentService.CommentImage(userId, imageId, commentText);

            Response.Redirect(Response.
                        ApplyAppPathModifier("~/Pages/Image/ViewComments.aspx?imageId=" + imageId));
        }
    }
}