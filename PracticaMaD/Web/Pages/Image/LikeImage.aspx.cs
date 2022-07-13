using Es.Udc.DotNet.ModelUtil.IoC;
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
    public partial class LikeImage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            long imageId;

            lblNoImage.Visible = false;
            btnLikeImage.Visible = false;
            btnDislikeImage.Visible = false;

            try
            {
                imageId = Int32.Parse(Request.Params.Get("imageId"));
            }
            catch (ArgumentNullException)
            {
                lblNoImage.Visible = true;
                return;
            }

            UserInfo myInfo = SessionManager.GetUserInfo(Context);

            IIoCManager ioCManager = (IIoCManager)Application["managerIoC"];

            IImageService imageService = ioCManager.Resolve<IImageService>();

            ImageDto image = imageService.GetImageById(imageId);

            Byte[] arr = image.ImageFile;
            Image.ImageUrl = "data:image;base64," + Convert.ToBase64String(arr);

            Image.Attributes.Add("width", "220");
            Image.Attributes.Add("height", "220");

            if (imageService.DoesLike(myInfo.UserId, imageId))
            {
                btnDislikeImage.Visible = true;
            }
            else
            {
                btnLikeImage.Visible = true;
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

        protected void BtnDislikeImage_Click(object sender, EventArgs e)
        {
            UserInfo myInfo = SessionManager.GetUserInfo(Context);

            IIoCManager ioCManager = (IIoCManager)Application["managerIoC"];

            IImageService imageService = ioCManager.Resolve<IImageService>();

            long imageId = Int32.Parse(Request.Params.Get("imageId"));

            imageService.DislikeImage(myInfo.UserId, imageId);

            Response.Redirect(Response.
                        ApplyAppPathModifier("~/Pages/Image/ViewImage.aspx?ImageId=" + imageId));
        }
    }
}