using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.ImageService;
using Es.Udc.DotNet.PracticaMaD.Model.TagService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.Image
{
    public partial class AddTags : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            long imageId;

            lblNoImage.Visible = false;
            lblNoPermission.Visible = false;

            lclTags.Visible = false;
            txtTags.Visible = false;
            btnAddTags.Visible = false;
            btnRemoveTags.Visible = false;

            gvImageTags.Visible = false;

            try
            {
                imageId = Int32.Parse(Request.Params.Get("imageId"));
            }
            catch (ArgumentNullException)
            {
                lblNoImage.Visible = true;
                return;
            }

            long userId = SessionManager.GetUserInfo(Context).UserId;

            IIoCManager ioCManager = (IIoCManager)Application["managerIoC"];

            IImageService imageService = ioCManager.Resolve<IImageService>();

            ITagService tagService = ioCManager.Resolve<ITagService>();

            ImageDto imageDto = imageService.GetImageById(imageId);

            Byte[] arr = imageDto.ImageFile;
            Image.ImageUrl = "data:image;base64," + Convert.ToBase64String(arr);

            Image.Attributes.Add("width", "220");
            Image.Attributes.Add("height", "220");

            if (userId != imageDto.AuthorId)
            {
                lblNoPermission.Visible = true;
                return;
            }

            lclTags.Visible = true;
            txtTags.Visible = true;
            btnAddTags.Visible = true;
            btnRemoveTags.Visible = true;

            gvImageTags.DataSource = tagService.GetTagsFromImage(imageId);
            gvImageTags.DataBind();
            gvImageTags.Visible = true;
        }

        protected void BtnAddTags_Click(object sender, EventArgs e)
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

            ITagService tagService = ioCManager.Resolve<ITagService>();

            String tags = txtTags.Text;

            tagService.AddTagsToImage(tags.Split(';').ToList(), imageId);

            Response.Redirect(Response.
                        ApplyAppPathModifier("~/Pages/Image/ViewImage.aspx?ImageId=" + imageId));
        }

        protected void BtnRemoveTags_Click(object sender, EventArgs e)
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

            ITagService tagService = ioCManager.Resolve<ITagService>();

            String tags = txtTags.Text;

            tagService.RemoveTagsFromImage(tags.Split(';').ToList(), imageId);

            Response.Redirect(Response.
                        ApplyAppPathModifier("~/Pages/Image/ViewImage.aspx?ImageId=" + imageId));
        }
    }
}