using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.CategoryService;
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
    public partial class Upload : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                IIoCManager ioCManager = (IIoCManager)Application["managerIoC"];

                ICategoryService categoryService = ioCManager.Resolve<ICategoryService>();

                this.comboCategory.DataSource = categoryService.FindAll();
                this.comboCategory.DataTextField = "Name";
                this.comboCategory.DataValueField = "CategoryId";
                this.comboCategory.DataBind();
            }
        }

        protected void BtnUploadClick(object sender, EventArgs e)
        {
            IIoCManager ioCManager = (IIoCManager)Application["managerIoC"];

            IImageService imageService = ioCManager.Resolve<IImageService>();


            String imageTitle = txtImageTitle.Text;
            String imageDescription = txtImageDescription.Text;
            String aperture = txtAperture.Text;
            String exposureTime = txtExposureTime.Text;
            String iso = txtIso.Text;
            String whiteBalance = txtWhiteBalance.Text;

            long categoryId = long.Parse(comboCategory.SelectedValue);

            int size = fuImageFile.PostedFile.ContentLength;
            byte[] imageFile = new byte[size-1];
            imageFile = fuImageFile.FileBytes;

            long userId = SessionManager.GetUserInfo(Context).UserId;

            long imageId = imageService.UploadImage(userId, imageTitle, imageDescription,
                new ExifDetails(aperture, exposureTime, iso, whiteBalance), categoryId, imageFile);

            Response.Redirect(Response.
                        ApplyAppPathModifier("~/Pages/Image/ViewImage.aspx?ImageId=" + imageId));
        }

        protected void ComboCategorySelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}