using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.CommentService;
using Es.Udc.DotNet.PracticaMaD.Model.ImageService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.Image
{
    public partial class ViewComments : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            long imageId;

            lblNoComments.Visible = false;
            lnkBackToImage.Visible = false;

            try
            {
                imageId = Int32.Parse(Request.Params.Get("imageId"));
            }
            catch (ArgumentNullException)
            {
                lblNoComments.Visible = true;
                return;
            }

            IIoCManager ioCManager = (IIoCManager)Application["managerIoC"];

            IImageService imageService = ioCManager.Resolve<IImageService>();

            ICommentService commentService = ioCManager.Resolve<ICommentService>();

            ImageDto imageDto = imageService.GetImageById(imageId);

            List<CommentDto> comments = commentService.GetCommentsByImage(imageId);

            Byte[] arr = imageDto.ImageFile;
            Image.ImageUrl = "data:image;base64," + Convert.ToBase64String(arr);

            Image.Attributes.Add("width", "220");
            Image.Attributes.Add("height", "220");

            if (comments.Count == 0)
            {
                lblNoComments.Visible = true;
                return;
            }

            gvViewComments.DataSource = comments;
            gvViewComments.DataBind();

            lnkBackToImage.NavigateUrl = Response.
                ApplyAppPathModifier("~/Pages/Image/ViewImage.aspx?imageId=" + imageId);
            lnkBackToImage.Visible = true;
        }
    }
}