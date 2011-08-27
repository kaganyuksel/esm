using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ESM
{
    public partial class Ayuda : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] != null)
            {
                var countp = from des in new ESM.Model.ESMBDDataContext().AyudaByPregunta
                             where des.IdPregunta == Convert.ToInt32(Request.QueryString["id"])
                             select des;
                if (countp.Count() != 0)
                {
                    var desc = (from des in new ESM.Model.ESMBDDataContext().AyudaByPregunta
                                where des.IdPregunta == Convert.ToInt32(Request.QueryString["id"])
                                select des).Single();

                    {
                        var q = clist;

                        if ((bool)desc.PEI)
                            q.Items[0].Selected = true;
                        if ((bool)desc.PMI)
                            q.Items[1].Selected = true;
                        if ((bool)desc.Manual_de_Convivencia)
                            q.Items[2].Selected = true;
                        if ((bool)desc.Planes_de_Estudio)
                            q.Items[3].Selected = true;
                    }
                    lblDescripcion.Text = desc.Descripcion;
                }
                else
                    lblDescripcion.Text = "No Existe una Descripción Disponible.";


            }
            else
                lblDescripcion.Text = "El Identificador de la pregunta no es correcto.";
        }
    }
}