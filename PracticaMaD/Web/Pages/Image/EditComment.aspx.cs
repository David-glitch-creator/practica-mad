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
    public partial class EditComment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            long commentId;

            lblNoComment.Visible = false;
            lblNoPermission.Visible = false;

            lclCommentText.Visible = false;
            txtCommentText.Visible = false;
            btnEditComment.Visible = false;

            lnkBackToComments.Visible = false;

            try
            {
                commentId = Int32.Parse(Request.Params.Get("commentId"));
            }
            catch (ArgumentNullException)
            {
                lblNoComment.Visible = true;
                return;
            }

            IIoCManager ioCManager = (IIoCManager)Application["managerIoC"];

            ICommentService commentService = ioCManager.Resolve<ICommentService>();

            CommentDto comment = commentService.GetCommentById(commentId);

            if (SessionManager.GetUserInfo(Context).UserId != comment.AuthorId)
            {
                lblNoPermission.Visible = true;
                return;
            }

            IImageService imageService = ioCManager.Resolve<IImageService>();

            ImageDto imageDto = imageService.GetImageById(comment.ImageId);

            Byte[] arr = imageDto.ImageFile;
            Image.ImageUrl = "data:image;base64," + Convert.ToBase64String(arr);

            Image.Attributes.Add("width", "220");
            Image.Attributes.Add("height", "220");

            lclCommentText.Visible = true;
            txtCommentText.Visible = true;
            btnEditComment.Visible = true;

            if (!IsPostBack)
            {
                txtCommentText.Text = comment.CommentText;
            }

            lnkBackToComments.NavigateUrl = Response.
                ApplyAppPathModifier("~/Pages/Image/ViewComments.aspx?imageId=" + comment.ImageId);
            lnkBackToComments.Visible = true;
        }

        protected void BtnEditComment_Click(object sender, EventArgs e)
        {
            long commentId = Int32.Parse(Request.Params.Get("commentId"));

            IIoCManager ioCManager = (IIoCManager)Application["managerIoC"];

            ICommentService commentService = ioCManager.Resolve<ICommentService>();

            String commentText = txtCommentText.Text;

            long imageId = commentService.GetCommentById(commentId).ImageId;

            commentService.UpdateComment(commentId, commentText);

            Response.Redirect(Response.
                        ApplyAppPathModifier("~/Pages/Image/ViewComments.aspx?imageId=" + imageId));
        }
    }
}