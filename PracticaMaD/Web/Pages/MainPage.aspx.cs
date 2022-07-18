using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.ImageService;
using Es.Udc.DotNet.PracticaMaD.Model.TagService;
using Es.Udc.DotNet.PracticaMaD.Model.UserService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using Es.Udc.DotNet.PracticaMaD.Web.Properties;
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
            int startIndex, count;

            lnkPrevious.Visible = false;
            lnkNext.Visible = false;
            lblNoImages.Visible = false;

            /* Get Start Index */
            try
            {
                startIndex = Int32.Parse(Request.Params.Get("startIndex"));
            }
            catch (ArgumentNullException)
            {
                startIndex = 0;
            }

            /* Get Count */
            try
            {
                count = Int32.Parse(Request.Params.Get("count"));
            }
            catch (ArgumentNullException)
            {
                count = Settings.Default.PracticaMaD_defaultCount;
            }

            IIoCManager ioCManager = (IIoCManager)Application["managerIoC"];

            IImageService imageService = ioCManager.Resolve<IImageService>();

            ImageBlock imageBlock = imageService.GetAllImages(startIndex, count);

            if (imageBlock.Images.Count == 0)
            {
                lblNoImages.Visible = true;
                return;
            }

            this.gvImagesMain.DataSource = imageBlock.Images;
            this.gvImagesMain.DataBind();

            /* "Previous" link */
            if ((startIndex - count) >= 0)
            {
                String url =
                    "/Pages/MainPage.aspx" +
                    "?startIndex=" + (startIndex - count) + "&count=" +
                    count;

                this.lnkPrevious.NavigateUrl =
                    Response.ApplyAppPathModifier(url);
                this.lnkPrevious.Visible = true;
            }

            /* "Next" link */
            if (imageBlock.ExistMoreImages)
            {
                String url =
                    "/Pages/MainPage.aspx" +
                    "?startIndex=" + (startIndex + count) +
                    "&count=" + count;

                this.lnkNext.NavigateUrl =
                    Response.ApplyAppPathModifier(url);
                this.lnkNext.Visible = true;
            }

            ITagService tagService = ioCManager.Resolve<ITagService>();

            List<TagDto> tags = tagService.GetByPopularity();

            lvTagCloud.DataSource = tags;
            lvTagCloud.DataBind();


        }

        protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                System.Web.UI.HtmlControls.HtmlImage imageControl =
                    (System.Web.UI.HtmlControls.HtmlImage)e.Row.FindControl("ImageControl");
                imageControl.Src = "data:image/png;base64," +
                    Convert.ToBase64String(((ImageDto)e.Row.DataItem).ImageFile);
            }
        }
    }
}