using Es.Udc.DotNet.ModelUtil.IoC;
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

            int size = fuImageFile.PostedFile.ContentLength;
            byte[] imageFile = new byte[size-1];
            imageFile = fuImageFile.FileBytes;

            long userId = SessionManager.GetUserInfo(Context).UserId;

            imageService.UploadImage(userId, imageTitle, imageDescription,
                new ExifDetails(aperture, exposureTime, iso, whiteBalance), 2, imageFile);
        }

        protected void ComboCategorySelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}