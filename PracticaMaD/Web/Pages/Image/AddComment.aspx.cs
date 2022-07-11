using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.CommentService;
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

        }

        protected void BtnAddComment_Click(object sender, EventArgs e)
        {
            long imageId;

            try
            {
                imageId = Int32.Parse(Request.Params.Get("imageId"));
            }
            catch (ArgumentNullException)
            {
                return;
            }

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