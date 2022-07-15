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
    public partial class ViewCommentDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            long commentID;

            lblNoComment.Visible = false;
            lnkAuthor.Visible = false;
            lblCommentText.Visible = false;
            lblPostedDate.Visible = false;

            btnEditComment.Visible = false;
            btnDeleteComment.Visible = false;

            try
            {
                commentID = Int32.Parse(Request.Params.Get("commentId"));
            }
            catch (ArgumentNullException)
            {
                lblNoComment.Visible = true;
                return;
            }

            IIoCManager ioCManager = (IIoCManager)Application["managerIoC"];

            IImageService imageService = ioCManager.Resolve<IImageService>();

            ICommentService commentService = ioCManager.Resolve<ICommentService>();

            CommentDto comment = commentService.GetCommentById(commentID);

            ImageDto imageDto = imageService.GetImageById(comment.ImageId);

            Byte[] arr = imageDto.ImageFile;
            Image.ImageUrl = "data:image;base64," + Convert.ToBase64String(arr);

            Image.Attributes.Add("width", "220");
            Image.Attributes.Add("height", "220");

            lnkAuthor.Text = comment.AuthorLogin;
            lnkAuthor.NavigateUrl = Response.
                ApplyAppPathModifier("~/Pages/User/ViewUser.aspx?userId=" + imageDto.AuthorId);
            lnkAuthor.Visible = true;

            lblCommentText.Text = comment.CommentText;
            lblCommentText.Visible = true;

            lblPostedDate.Text = comment.PostedDate.ToString();
            lblPostedDate.Visible = true;

            if (SessionManager.IsUserAuthenticated(Context) &&
                (comment.AuthorId == SessionManager.GetUserInfo(Context).UserId))
            {
                btnEditComment.Visible = true;
                btnDeleteComment.Visible = true;
            }
        }

        protected void BtnEditComment_Click(object sender, EventArgs e)
        {
            long commentID = Int32.Parse(Request.Params.Get("commentId"));

            Response.Redirect(Response.
                        ApplyAppPathModifier("~/Pages/Image/EditComment.aspx?commentId=" + commentID));
        }

        protected void BtnDeleteComment_Click(object sender, EventArgs e)
        {
            long commentID = Int32.Parse(Request.Params.Get("commentId"));

            IIoCManager ioCManager = (IIoCManager)Application["managerIoC"];

            ICommentService commentService = ioCManager.Resolve<ICommentService>();

            long imageId = commentService.GetCommentById(commentID).ImageId;

            commentService.DeleteComment(commentID);

            Response.Redirect(Response.
                        ApplyAppPathModifier("~/Pages/Image/ViewComments.aspx?imageId=" + imageId));
        }
    }
}