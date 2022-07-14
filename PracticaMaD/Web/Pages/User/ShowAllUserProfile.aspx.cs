using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.UserService;
using Es.Udc.DotNet.PracticaMaD.Model.UserService.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using Es.Udc.DotNet.PracticaMaD.Web.Properties;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.User
{
    public partial class ShowAllUserProfile : SpecificCulturePage
    {

        private ObjectDataSource pbpDataSource = new ObjectDataSource();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Trace.IsEnabled = true;
            //Obtenemos service
            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            IUserService userService = iocManager.Resolve<IUserService>();

            //usuario que da la orden de la busqueda
            UserInfo usuario = SessionManager.GetUserInfo(Context);

            pbpDataSource.ObjectCreating += this.PbpDataSource_ObjectCreating;

            pbpDataSource.TypeName =
                 Settings.Default.ObjectDS_ProductService; //seleccionamos el servicio

            pbpDataSource.EnablePaging = true; //paginacion

            pbpDataSource.SelectMethod =
                Settings.Default.ObjectDS_SelectMethod; //seleccionamos el metodo "FindAllUsers"


            pbpDataSource.SelectCountMethod =
                  Settings.Default.ObjectDS_CountMethod; // Total de elementos de la lista "CountAllUsers"

            pbpDataSource.StartRowIndexParameterName =
                    Settings.Default.ObjectDS_StartIndexParameter; //nombre del atributo para inicializar las filas

            pbpDataSource.MaximumRowsParameterName =
                    Settings.Default.ObjectDS_CountParameter; //nombre del atributo "count"

            gvUsers.AllowPaging = true;
            gvUsers.PageSize = Settings.Default.ObjectDS_DefaultCount; //nº de elementos por pagina

            gvUsers.DataSource = pbpDataSource;
            gvUsers.DataBind();

            // Al pulsar el Alias
           /* foreach (GridViewRow row in gvUsers.Rows)
            {
                HyperLink link = row.Cells[0].Controls[0] as HyperLink;

                Trace.Warn("INFOr0", row.Cells[0].Text);
                Trace.Warn("INFOr1", row.Cells[1].Text);
                Trace.Warn("INFOr2", row.Cells[2].Text);

                link.NavigateUrl = "~/Pages/User/ShowSingleUser.aspx?txtName=" + (row.Cells[0].Text);
                ;

            }*/

            gvUsers.Columns[0].Visible = true;
        }

        protected void gvUsersPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvUsers.PageIndex = e.NewPageIndex; //obtenemos el indice 
            gvUsers.DataBind();

            foreach (GridViewRow row in gvUsers.Rows)
            {
                HyperLink link = row.Cells[0].Controls[0] as HyperLink;

                link.NavigateUrl = "~/Pages/User/ShowFollowers.aspx?txtName=" + (gvUsers.Rows[0].Cells[0].Text);

            }

        }

        protected void PbpDataSource_ObjectCreating(object sender, ObjectDataSourceEventArgs e)
        {
            //Obtenemos service
            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            IUserService userService = iocManager.Resolve<IUserService>();

            e.ObjectInstance = userService;

        }
        protected void gvUsers_RowCommand(Object sender, GridViewCommandEventArgs e)
        {

            // Si se utilizan varios campos de columna ButtonField, se
            // utiliza la propiedad CommandName para determinar en qué botón se hizo clic.
            if (e.CommandName == "Select")
            {

                // Convertimos el índice de la fila almacenado 
                // en la propiedad CommandArgument en un número entero
                int index = Convert.ToInt32(e.CommandArgument);

                // seleccionamos la fila
                //GridViewRow selectedRow = gvUsers.Rows[index];



                //obtenemos el alias
                String alias = ((HyperLink)gvUsers.Rows[index].Cells[0].Controls[0]).Text;
                //usuario de la sesion
                UserInfo usuario = SessionManager.GetUserInfo(Context);



                IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
                IUserService userService = iocManager.Resolve<IUserService>();

                UserInfo userAlias = userService.FindUserByLoginName(alias);

                userService.FollowUser(userAlias.UserId, usuario.UserId);

                lblFollow.Text = "Siguiendo a " + alias;
                lblFollow.Visible = true;

            }

        }

        protected void gvUsers_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void BtnFindByLoginClick(object sender, EventArgs e)
        {
            String url = String.Format("./FindFollowers.aspx");
            Response.Redirect(Response.ApplyAppPathModifier(url));
        }

        protected void BtnShowAllLoginsClick(object sender, EventArgs e)
        {

        }
    }
}