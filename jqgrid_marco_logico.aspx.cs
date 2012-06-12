using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Web.UI.HtmlControls;

namespace ESM
{
    public partial class jqgrid_marco_logico : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ban_proyecto_id.Value = Request.QueryString["proyecto_id"].ToString();

                if (ban_proyecto_id.Value != "0" && ban_proyecto_id.Value != "")
                {

                    var proyecto_info = (from p in new ESM.Model.ESMBDDataContext().Proyectos
                                         where p.Id == Convert.ToInt32(ban_proyecto_id.Value)
                                         select p).Single();

                    Objetos.CEfectos objCEfectos = new Objetos.CEfectos();

                    var colprocesos = objCEfectos.getCausas_Efectos(Convert.ToInt32(ban_proyecto_id.Value));

                    string options = "";

                    foreach (var item in colprocesos)
                    {
                        options = options + item.Id + ":" + item.Proceso + ";";
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

                    min_fecha_actividades.Value = Convert.ToDateTime(proyecto_info.Registro_Proyectos.Single().Fecha).ToString("dd/MM/yy");
                }
            }
            catch
            {
                min_fecha_actividades.Value = DateTime.Now.ToShortDateString();
            }
        }


        protected void btnExportar_Click(object sender, ImageClickEventArgs e)
        {
            int proyecto_id = Convert.ToInt32(ban_proyecto_id.Value);

            Model.ESMBDDataContext db = new Model.ESMBDDataContext();

            var procesos = from p in db.Causas_Efectos
                           where p.Proyecto_id == proyecto_id
                           select p;

            string html = "<table border='1' cellspacing='0' style='border: 1px solid #000;'><caption>Marco Lógico</caption>";
            html += "<tr><td style='border: 1px solid #000;'></td><td style='border: 1px solid #000;'></td><td style='border: 1px solid #000;'>INDICADOR</td><td style='border: 1px solid #000;'>MEDIOS DE VERIFICACIÓN</td><td style='border: 1px solid #000;'>SUPUESTOS</td></tr>";
            int color = 0;
            string color_cadena = "D6D6D6";
            foreach (var procesos_item in procesos)
            {
                if (color == 0)
                {
                    color_cadena = "E0E0E0";
                    color++;
                }
                else
                {
                    color_cadena = "ffffff";
                    color = 0;
                }

                html += "<tr style='background: #" + color_cadena + "'><td style='border: 1px solid #000;'><b>PROCESO:</b></td><td style='border: 1px solid #000;'>" + procesos_item.Causa + "</td><td style='border: 1px solid #000;'></td><td style='border: 1px solid #000;'></td><td style='border: 1px solid #000;'></td><tr>";
                var subprocesos = from sp in db.Subprocesos
                                  where sp.Causas_Efecto.Proyecto_id == proyecto_id && sp.Proceso_id == procesos_item.Id
                                  select sp;

                foreach (var subprocesos_item in subprocesos)
                {
                    var actividades = from a in db.Actividades
                                      where a.Subproceso.Causas_Efecto.Proyecto_id == proyecto_id && a.Subproceso_id == subprocesos_item.Id
                                      select a;

                    html += "<tr style='background: #" + color_cadena + "'><td style='border: 1px solid #000;'><b>SUBPROCESO:</b></td><td style='border: 1px solid #000;'>" + subprocesos_item.Subproceso1 + "</td><td style='border: 1px solid #000;'>" + subprocesos_item.Indicador + "</td><td style='border: 1px solid #000;'>" + subprocesos_item.Medios + "</td><td style='border: 1px solid #000;'>" + subprocesos_item.Supuestos + "</td><tr>";
                    int count_actividades = 0;
                    foreach (var actividades_item in actividades)
                    {
                        if (count_actividades == 0)
                        {
                            if (actividades.Count() <= 1)
                                html += "<tr style='background: #" + color_cadena + "'><td style='vertical-align: middle; text-align: center; border: 1px solid #000;' rowspan='" + actividades.Count() + "'><b>ACTIVIDAD:</b></td><td style='border: 1px solid #000;'>" + actividades_item.Actividad + "</td><td style='border: 1px solid #000;'></td><td style='border: 1px solid #000;'></td><td style='border: 1px solid #000;'></td><tr>";
                            else
                                html += "<tr style='background: #" + color_cadena + "'><td style='vertical-align: middle; text-align: center; border: 1px solid #000;' rowspan='" + (actividades.Count() * 2) + "'><b>ACTIVIDAD:</b></td><td style='border: 1px solid #000;'>" + actividades_item.Actividad + "</td><td style='border: 1px solid #000;'></td><td style='border: 1px solid #000;'></td><td style='border: 1px solid #000;'></td><tr>";
                        }
                        else
                            html += "<tr style='background: #" + color_cadena + "'><td style='border: 1px solid #000;'>" + actividades_item.Actividad + "</td><td style='border: 1px solid #000;'></td><td style='border: 1px solid #000;'></td><td style='border: 1px solid #000;'></td><tr>";

                        count_actividades++;

                    }
                }
            }
            html = html + "</table>";

            StringBuilder objsb = new StringBuilder();
            System.IO.StringWriter sw = new System.IO.StringWriter(objsb);
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            System.Web.UI.Page pagina = new System.Web.UI.Page();
            var form = new HtmlForm();



            var proyecto_informacion = (from proy in db.Proyectos
                                        where proy.Id == proyecto_id
                                        select proy).Single();

            form.InnerHtml = "<h1>Proyecto: " + proyecto_informacion.Proyecto1 + "</h1>" + html;

            pagina.EnableEventValidation = false;
            pagina.DesignerInitialize();
            pagina.Controls.Add(form);
            pagina.RenderControl(htw);
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            string nombre = "Marco Logico" + DateTime.Now.ToShortDateString().Replace('/', '-');
            Response.AddHeader("Content-Disposition", "attachment;filename=" + nombre + ".xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            Response.Write(objsb.ToString());
            Response.End();
        }
    }
}