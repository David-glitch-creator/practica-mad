using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.ImageService;
using Es.Udc.DotNet.PracticaMaD.Model.UserService;
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

            IIoCManager ioCManager = (IIoCManager)Application["managerIoC"];

            IImageService imageService = ioCManager.Resolve<IImageService>();

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
        }
    }
}