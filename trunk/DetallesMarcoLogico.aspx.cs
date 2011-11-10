using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESM.Objetos;
using System.Collections;

namespace ESM
{
    public partial class DetallesMarcoLogico : System.Web.UI.Page
    {
        #region Propiedades Publicas y Privadas

        protected int idproyecto = 0;
        protected bool esProyecto = false;

        #endregion


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["idproyecto"] != null)
            {
                esProyecto = true;
                idproyecto = Convert.ToInt32(Session["idproyecto"]);
            }

        }

        protected void getMediosVerificacion()
        {
            var medios = from m in new ESM.Model.ESMBDDataContext().Medios_de_verificacions
                         select m;

            var list = new System.Web.UI.HtmlControls.HtmlGenericControl("ul");

            int contador = 0;

            foreach (var item in medios)
            {
                var item_list = new System.Web.UI.HtmlControls.HtmlGenericControl("li");

                item_list.ID = "node" + contador;
                item_list.InnerText = item.Medio_de_verificacion;

                contador++;
            }

        }

        protected void lknAgregarIndicador_Click(object sender, EventArgs e)
        {
            if (esProyecto)
                AlmacenarProyecto();
        }

        protected void AlmacenarProyecto()
        {
            try
            {
                Cproyecto objCproyecto = new Cproyecto();

                objCproyecto.Update(idproyecto, txtindicadorg.Text);

            }
            catch (Exception) { /*TODO: JCMM: Controlador Exception*/ }

        }

        protected void AlmacenarResultado()
        {
            try
            {

            }
            catch (Exception) { /*TODO: JCMM: Controlador Exception*/ }

        }
    }
}