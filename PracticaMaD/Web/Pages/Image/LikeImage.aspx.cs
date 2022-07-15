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

            try
            {
                imageId = Int32.Parse(Request.Params.Get("imageId"));
            }
            catch (ArgumentNullException)
            {
                Response.Redirect(Response.
                        ApplyAppPathModifier("~/Pages/User/MyProfile.aspx"));
            }

            UserInfo myInfo = SessionManager.GetUserInfo(Context);

            IIoCManager ioCManager = (IIoCManager)Application["managerIoC"];

            IImageService imageService = ioCManager.Resolve<IImageService>();

            imageId = Int32.Parse(Request.Params.Get("imageId"));

            imageService.LikeImage(myInfo.UserId, imageId);

            Response.Redirect(Response.
                        ApplyAppPathModifier("~/Pages/Image/ViewImage.aspx?ImageId=" + imageId));

        }
    }
}