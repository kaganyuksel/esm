using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ESM
{
    public partial class jqgrid_valores : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ban_proyecto_id.Value = Request.QueryString["proyecto_id"].ToString();

            if (ban_proyecto_id.Value != "0" && ban_proyecto_id.Value != "")
            {
                Objetos.CActividades objActividades = new Objetos.CActividades();

                var colindicadores = objActividades.getIndicadoresProyecto(Convert.ToInt32(ban_proyecto_id.Value));

                string options_indicadores = "";

                foreach (var item in colindicadores)
                {
                    options_indicadores = options_indicadores + item.Id + ":" + item.Indicador + ";";
                }

                options_indicadores = options_indicadores.Trim(';');

                col_indicadores.Value = options_indicadores;
            }
        }
    }
}