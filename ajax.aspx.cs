using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESM.Objetos;

namespace ESM
{
    public partial class ajax : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Response.Clear();
                if (Request.QueryString["resultados"] != null && Convert.ToBoolean(Request.QueryString["resultados"]))
                    AlmacenarResultado();
                else if (Request.QueryString["actividades"] != null && Convert.ToBoolean(Request.QueryString["actividades"]))
                    AlmacenarActividad();

            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void AlmacenarResultado()
        {
            int idResultado = Convert.ToInt32(Request.QueryString["idresultado"]);
            string causa = Request.QueryString["causa"].ToString();
            string resultado = Request.QueryString["resultado"].ToString();

            CEfectos objCEfectos = new CEfectos();

            objCEfectos.Update(idResultado, causa, resultado);
        }

        protected void AlmacenarActividad()
        {
            try
            {

                int idResultado = Convert.ToInt32(Request.QueryString["idresultado"]);
                string actividad = Request.QueryString["actividad"].ToString();
                decimal presupuesto = Convert.ToInt32(Request.QueryString["presupuesto"]);

                CActividades objCActividades = new CActividades();

                objCActividades.Add(idResultado, actividad, presupuesto);

            }
            catch (Exception) { }

        }
    }
}