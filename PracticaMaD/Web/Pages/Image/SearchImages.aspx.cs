using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.CategoryService;
using Es.Udc.DotNet.PracticaMaD.Model.ImageService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using Es.Udc.DotNet.PracticaMaD.Web.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.Image
{
    public partial class SearchImages : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int startIndex, count;
            String keywords;
            long category;

            lnkPrevious.Visible = false;
            lnkNext.Visible = false;
            lblNoImages.Visible = false;

            if (!IsPostBack)
            {
                IIoCManager ioCManager1 = (IIoCManager)Application["managerIoC"];

                ICategoryService categoryService = ioCManager1.Resolve<ICategoryService>();

                this.comboCategory.DataSource = categoryService.FindAll();
                this.comboCategory.DataTextField = "Name";
                this.comboCategory.DataValueField = "CategoryId";
                this.comboCategory.DataBind();
                this.comboCategory.Items.Insert(0, new ListItem(string.Empty, string.Empty));
                this.comboCategory.SelectedIndex = 0;
            }

            /* Get Keywords */
            try
            {
                keywords = Request.Params.Get("keywords");
            }
            catch (ArgumentNullException)
            {
                keywords = null;
            }

            /* Get Category */
            try
            {
                category = Int32.Parse(Request.Params.Get("category"));
            }
            catch (ArgumentNullException)
            {
                category = -1;
            }

            if ((keywords == null) && (category == -1))
            {
                return;
            }

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

            ImageBlock imageBlock;

            if (category == -1)
            {
                imageBlock = imageService.SearchImages(keywords, startIndex, count);
            }
            else if(keywords == null)
            {
                imageBlock = imageService.SearchImages(category, startIndex, count);
            }
            else
            {
                imageBlock = imageService.SearchImages(keywords, category, startIndex, count);
            }

            if (imageBlock.Images.Count == 0)
            {
                lblNoImages.Visible = true;
                return;
            }

            this.gvImagesSearch.DataSource = imageBlock.Images;
            this.gvImagesSearch.DataBind();

            /* "Previous" link */
            if ((startIndex - count) >= 0)
            {
                String url =
                    "/Pages/Image/SearchImages.aspx" +
                    "?keywords=" + keywords +
                    "&category=" + category +
                    "&startIndex=" + (startIndex - count) +
                    "&count=" + count;

                this.lnkPrevious.NavigateUrl =
                    Response.ApplyAppPathModifier(url);
                this.lnkPrevious.Visible = true;
            }

            /* "Next" link */
            if (imageBlock.ExistMoreImages)
            {
                String url =
                    "/Pages/Image/SearchImages.aspx" +
                    "?keywords=" + keywords +
                    "&category=" + category +
                    "&startIndex=" + (startIndex + count) +
                    "&count=" + count;

                this.lnkNext.NavigateUrl =
                    Response.ApplyAppPathModifier(url);
                this.lnkNext.Visible = true;
            }
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

        protected void BtnFind_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                String keywords = txtKeywords.Text;

                String url;

                if (comboCategory.SelectedValue!="")
                {
                    long category = long.Parse(comboCategory.SelectedValue);
                    url = String.Format("./SearchImages.aspx?keywords={0}&category={1}", keywords, category);
                }
                else
                {
                    url = String.Format("./SearchImages.aspx?keywords={0}", keywords);
                }

                Response.Redirect(Response.ApplyAppPathModifier(url));
            }
        }
    }
}