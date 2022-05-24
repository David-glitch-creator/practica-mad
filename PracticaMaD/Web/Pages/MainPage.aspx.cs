using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.ImageService;
using Es.Udc.DotNet.PracticaMaD.Model.UserService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages
{
    public partial class MainPage : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            IIoCManager ioCManager = (IIoCManager)Application["managerIoC"];

            IImageService imageService = ioCManager.Resolve<IImageService>();

            List<ImageDto> images = imageService.GetAllImages(0, 9).Images;

            this.gvImagesMain.DataSource = images;
            this.gvImagesMain.DataBind();
        }

        protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                System.Web.UI.HtmlControls.HtmlImage imageControl =
                    (System.Web.UI.HtmlControls.HtmlImage)e.Row.FindControl("ImageControl");
                imageControl.Src = "data:image/png;base64," +
                    Convert.ToBase64String(((ImageDto)e.Row.DataItem).ImageFile);


                IIoCManager ioCManager = (IIoCManager)Application["managerIoC"];
                IUserService userService = ioCManager.Resolve<IUserService>();

                long authorId = long.Parse(e.Row.Cells[2].Text);
                String login = userService.GetUserInfo(authorId).LoginName;
                e.Row.Cells[2].Text = login;
            }
        }
    }
}