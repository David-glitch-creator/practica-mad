using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.UserService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages
{
    public partial class UserExists : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the Click event of the btnUserExists control.
        /// </summary>
        /// <param name="sender"> The source of the event. </param>
        /// <param name="e"> The <see cref="EventArgs"/> instance containing the event data. </param>
        protected void btnUserExists_Click(object sender, EventArgs e)
        {
            // 1) Obtener contexto de Inyección de Dependencias

            IIoCManager iocManager = (IIoCManager)Application["managerIoC"];

            // 2) Obtención del Servicio

            IUserService userService = iocManager.Resolve<IUserService>();

            // 3) Llamada al caso de uso (lectura de parámetros y actualización vista)

            this.lblUserExists.Visible = false;
            this.lblUserNotExists.Visible = false;

            String loginName =
                txtUserName.Text;

            bool userExists = userService.UserExists(loginName);

            if (userExists)

                this.lblUserExists.Visible = true;
            else

                this.lblUserNotExists.Visible = true;
        }
    }
}