using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ESM
{
    public partial class jqgrid_marco_logico : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ban_proyecto_id.Value = Request.QueryString["proyecto_id"].ToString();

            if (ban_proyecto_id.Value != "0" && ban_proyecto_id.Value != "")
            {
                Objetos.CEfectos objCEfectos = new Objetos.CEfectos();

                var colprocesos = objCEfectos.getCausas_Efectos(Convert.ToInt32(ban_proyecto_id.Value));

                string options = "";

                foreach (var item in colprocesos)
                {
                    options = options + item.Id + ":" + item.Causa + ";";
                }

                options = options.Trim(';');

                col_procesos.Value = options;

                Objetos.CSubprocesos objCSubprocesos = new Objetos.CSubprocesos();

                var colsubprocesos = objCSubprocesos.getSubprocesos(Convert.ToInt32(ban_proyecto_id.Value));

                string options_subprocesos = "";

                foreach (var item in colsubprocesos)
                {
                    options_subprocesos = options_subprocesos + item.Id + ":" + item.Subproceso1 + ";";
                }

                options_subprocesos = options_subprocesos.Trim(';');

                col_sub_procesos.Value = options_subprocesos;

                #region actividades

                var colactividades = from a in new Model.ESMBDDataContext().Actividades
                                     where a.Subproceso.Causas_Efecto.Proyecto_id == Convert.ToInt32(ban_proyecto_id.Value)
                                     select a;

                string options_colactividades = "";

                foreach (var item in colactividades)
                {
                    options_colactividades = options_colactividades + item.Id + ":" + item.Actividad + ";";
                }

                options_colactividades = options_colactividades.Trim(';');

                col_actividades.Value = options_colactividades;

                #endregion

                var verbos = from v in new Model.ESMBDDataContext().Verbos
                             select v;

                string options_verbos = "";

                foreach (var item in verbos)
                {
                    options_verbos = options_verbos + item.Id + ":" + item.Verbo1 + ";";
                }

                options_verbos = options_verbos.Trim(';');

                ban_options_verbos.Value = options_verbos;

                var unidades = from u in new Model.ESMBDDataContext().Unidades
                               select u;

                string options_unidades = "";

                foreach (var item in unidades)
                {
                    options_unidades = options_unidades + item.Id + ":" + item.Unidad + ";";
                }

                options_unidades = options_unidades.Trim(';');

                ban_options_unidades.Value = options_unidades;

            }
        }
    }
}