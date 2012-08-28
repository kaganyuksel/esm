using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ESM
{
    public partial class Organigama : System.Web.UI.Page
    {
        int proyecto_id = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            proyecto_id = Convert.ToInt32(Request.QueryString["proyecto_id"]);

            var proyecto = (from p in new Model.ESMBDDataContext().Proyectos
                            where p.Id == proyecto_id
                            select p).Single();

            if (Request.QueryString["pdf"] == null)
            {

                style_tables.InnerHtml = "div, h1, table, tr, td { font-family: 'Arial Narrow' , sans-serif;}";

                if (Request.QueryString["problemas"] != null && Convert.ToBoolean(Request.QueryString["problemas"]))
                {
                    titulo_problemas.Visible = true;
                    titulo_objetivos.Visible = false;
                    titulo_plan_accion.Visible = false;

                    titulo_problemas.InnerHtml += " " + proyecto.Proyecto1.ToUpper();

                    org.InnerHtml = UpdateHTMLArbolProblemas();
                }
                else if (Request.QueryString["objetivos"] != null && Convert.ToBoolean(Request.QueryString["objetivos"]))
                {
                    titulo_problemas.Visible = false;
                    titulo_objetivos.Visible = true;
                    titulo_plan_accion.Visible = false;

                    titulo_objetivos.InnerHtml += " " + proyecto.Proyecto1.ToUpper();

                    org_Objetivos.InnerHtml = UpdateHTMLArbolObjetivos();
                }
                else if (Request.QueryString["planaccion"] != null && Convert.ToBoolean(Request.QueryString["planaccion"]))
                {
                    titulo_problemas.Visible = false;
                    titulo_objetivos.Visible = false;
                    titulo_plan_accion.Visible = true;

                    titulo_plan_accion.InnerHtml += " " + proyecto.Proyecto1.ToUpper();

                    planaccion.InnerHtml = generatePlanOperativo(proyecto_id);
                }
            }
            else
            {

                titulo_problemas.Visible = true;

                titulo_problemas.InnerHtml += " " + proyecto.Proyecto1.ToUpper();

                org.InnerHtml = UpdateHTMLArbolProblemasReport();

                titulo_objetivos.Visible = true;

                titulo_objetivos.InnerHtml += " " + proyecto.Proyecto1.ToUpper();

                org_Objetivos.InnerHtml = UpdateHTMLArbolObjetivosReport();

                titulo_plan_accion.Visible = true;

                titulo_plan_accion.InnerHtml += " " + proyecto.Proyecto1.ToUpper();

                planaccion.InnerHtml = generatePlanOperativoReport(proyecto_id);
            }
        }

        protected string generatePlanOperativo(int Proyecto_id)
        {
            Model.ESMBDDataContext db = new Model.ESMBDDataContext();

            var procesos = from p in db.Causas_Efectos
                           where p.Proyecto_id == Proyecto_id
                           select p;
            //<caption style='border: 1px solid #000;'>PLAN ACCIÓN</caption>
            string html = "<h1></h1><br/><table border='1' cellspacing='0' style='border: 1px solid #000; width: 100%; font-size: 0.8em;'>";
            html += "<tr><td style='border: 1px solid #000;'></td><td style='border: 1px solid #000;'></td><td style='border: 1px solid #000;'>INDICADOR</td><td style='border: 1px solid #000;'>MEDIOS DE VERIFICACIÓN</td><td style='border: 1px solid #000;'>SUPUESTOS</td><td style='border: 1px solid #000;'>META</td><td style='border: 1px solid #000;'>FECHA</td><td style='border: 1px solid #000;'>UNIDAD</td><td style='border: 1px solid #000;'>VERBO</td><td style='border: 1px solid #000;'>SSP</td></tr>";
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

                html += "<tr style='background: #" + color_cadena + "'><td style='border: 1px solid #000;'><b>PROCESO:</b></td><td style='border: 1px solid #000;'>" + procesos_item.Causa + "</td><td style='border: 1px solid #000;'></td><td style='border: 1px solid #000;'></td><td style='border: 1px solid #000;'></td><td style='border: 1px solid #000;'></td><td style='border: 1px solid #000;'></td><td style='border: 1px solid #000;'></td><td style='border: 1px solid #000;'></td><td style='border: 1px solid #000;'></td></tr>";
                var subprocesos = from sp in db.Subprocesos
                                  where sp.Causas_Efecto.Proyecto_id == proyecto_id && sp.Proceso_id == procesos_item.Id
                                  select sp;

                foreach (var subprocesos_item in subprocesos)
                {
                    var actividades = from a in db.Actividades
                                      where a.Subproceso.Causas_Efecto.Proyecto_id == proyecto_id && a.Subproceso_id == subprocesos_item.Id
                                      select a;

                    html += "<tr style='background: #" + color_cadena + "'><td style='border: 1px solid #000;'><b>SUBPROCESO:</b></td><td style='border: 1px solid #000;'>" + subprocesos_item.Subproceso1 + "</td><td style='border: 1px solid #000;'>" + subprocesos_item.Indicador + "</td><td style='border: 1px solid #000;'>" + subprocesos_item.Medios + "</td><td style='border: 1px solid #000;'>" + subprocesos_item.Supuestos + "</td><td style='border: 1px solid #000;'></td><td style='border: 1px solid #000;'></td><td style='border: 1px solid #000;'></td><td style='border: 1px solid #000;'></td><td style='border: 1px solid #000;'></td></tr>";

                    foreach (var actividades_item in actividades)
                    {
                        html += "<tr style='background: #" + color_cadena + "'><td style='vertical-align: middle; text-align: center; border: 1px solid #000;' ><b>ACTIVIDAD:</b></td><td style='border: 1px solid #000;'>" + actividades_item.Actividad + "</td><td style='border: 1px solid #000;'></td><td style='border: 1px solid #000;'></td><td style='border: 1px solid #000;'></td><td style='border: 1px solid #000;'></td><td style='border: 1px solid #000;'></td><td style='border: 1px solid #000;'></td><td style='border: 1px solid #000;'></td><td style='border: 1px solid #000;'></td></tr>";

                        var indicadores = from i in db.Indicadores
                                          where i.Actividade.Subproceso.Causas_Efecto.Proyecto_id == proyecto_id && i.Actividad_id == actividades_item.Id
                                          select i;
                        int count_indicadores = 0;
                        foreach (var indicadores_item in indicadores)
                        {
                            string ssp = "";

                            if ((bool)indicadores_item.SSP)
                                ssp = "Si";
                            else if (!(bool)indicadores_item.SSP)
                                ssp = "No";

                            if (count_indicadores == 0)
                            {
                                if (indicadores.Count() <= 1)
                                    html += "<tr style='background: #" + color_cadena + "'><td style='vertical-align: middle; text-align: center; border: 1px solid #000;' rowspan='" + indicadores.Count() + "'><b>INDICADOR:</b></td><td style='border: 1px solid #000;'>" + indicadores_item.Indicador + "</td><td style='border: 1px solid #000;'></td><td style='border: 1px solid #000;'>" + indicadores_item.medios + "</td><td style='border: 1px solid #000;'>" + indicadores_item.supuestos + "</td><td style='border: 1px solid #000;'>" + indicadores_item.meta + "</td><td style='border: 1px solid #000;'>" + Convert.ToDateTime(indicadores_item.fecha_indicador_inicial).ToShortDateString() + "</td><td style='border: 1px solid #000;'>" + indicadores_item.Unidade.Unidad + "</td><td style='border: 1px solid #000;'>" + indicadores_item.Verbo.Verbo1 + "</td><td style='border: 1px solid #000;'>" + ssp + "</td></tr>";
                                else
                                    html += "<tr style='background: #" + color_cadena + "'><td style='vertical-align: middle; text-align: center; border: 1px solid #000;' rowspan='" + (indicadores.Count() * 2) + "'><b>INDICADOR:</b></td><td style='border: 1px solid #000;'>" + indicadores_item.Indicador + "</td><td style='border: 1px solid #000;'></td><td style='border: 1px solid #000;'>" + indicadores_item.medios + "</td><td style='border: 1px solid #000;'>" + indicadores_item.supuestos + "</td><td style='border: 1px solid #000;'>" + indicadores_item.meta + "</td><td style='border: 1px solid #000;'>" + Convert.ToDateTime(indicadores_item.fecha_indicador_inicial).ToShortDateString() + "</td><td style='border: 1px solid #000;'>" + indicadores_item.Unidade.Unidad + "</td><td style='border: 1px solid #000;'>" + indicadores_item.Verbo.Verbo1 + "</td><td style='border: 1px solid #000;'>" + ssp + "</td></tr>";
                            }
                            else
                                html += "<tr style='background: #" + color_cadena + "'><td style='border: 1px solid #000;'>" + indicadores_item.Indicador + "</td><td style='border: 1px solid #000;'></td><td style='border: 1px solid #000;'>" + indicadores_item.medios + "</td><td style='border: 1px solid #000;'>" + indicadores_item.supuestos + "</td><td style='border: 1px solid #000;'>" + indicadores_item.meta + "</td><td style='border: 1px solid #000;'>" + Convert.ToDateTime(indicadores_item.fecha_indicador_inicial).ToShortDateString() + "</td><td style='border: 1px solid #000;'>" + indicadores_item.Unidade.Unidad + "</td><td style='border: 1px solid #000;'>" + indicadores_item.Verbo.Verbo1 + "</td><td style='border: 1px solid #000;'>" + ssp + "</td></tr>";

                            count_indicadores++;
                        }
                    }
                }
            }
            html = html + "</table>";

            return html;
        }

        protected string generatePlanOperativoReport(int Proyecto_id)
        {
            Model.ESMBDDataContext db = new Model.ESMBDDataContext();

            var procesos = from p in db.Causas_Efectos
                           where p.Proyecto_id == Proyecto_id
                           select p;

            string html = "<table border='1' cellspacing='0' style='border: 1px solid #000; width: 900px; font-size: 0.8em;'>";
            html += "<tr><td style='border: 1px solid #000;'></td><td style='border: 1px solid #000; width: 500px;'></td><td style='border: 1px solid #000; width: 50px;'>INDICADOR</td><td style='border: 1px solid #000; width: 50px;'>MEDIOS DE VERIFICACIÓN</td><td style='width: 50px; border: 1px solid #000;'>SUPUESTOS</td><td style=' width: 50px; border: 1px solid #000;'>META</td><td style='border: 1px solid #000; width: 50px;'>FECHA</td><td style='border: 1px solid #000; width: 50px;'>UNIDAD</td><td style='border: 1px solid #000; width: 50px;'>VERBO</td><td style='border: 1px solid #000;'>SSP</td></tr>";
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

                html += "<tr style='background: #" + color_cadena + "'><td style='border: 1px solid #000;'><b>PROCESO:</b></td><td style='width: 500px; border: 1px solid #000;'>" + procesos_item.Causa + "</td><td style='border: 1px solid #000;'></td><td style='border: 1px solid #000;'></td><td style='border: 1px solid #000;'></td><td style='border: 1px solid #000;'></td><td style='border: 1px solid #000;'></td><td style='border: 1px solid #000;'></td><td style='border: 1px solid #000;'></td><td style='border: 1px solid #000;'></td></tr>";
                var subprocesos = from sp in db.Subprocesos
                                  where sp.Causas_Efecto.Proyecto_id == proyecto_id && sp.Proceso_id == procesos_item.Id
                                  select sp;

                foreach (var subprocesos_item in subprocesos)
                {
                    var actividades = from a in db.Actividades
                                      where a.Subproceso.Causas_Efecto.Proyecto_id == proyecto_id && a.Subproceso_id == subprocesos_item.Id
                                      select a;

                    html += "<tr style='background: #" + color_cadena + "'><td style='border: 1px solid #000;'><b>SUBPROCESO:</b></td><td style='width: 500px; border: 1px solid #000;'>" + subprocesos_item.Subproceso1 + "</td><td style='border: 1px solid #000;'>" + subprocesos_item.Indicador + "</td><td style='border: 1px solid #000;'>" + subprocesos_item.Medios + "</td><td style='border: 1px solid #000;'>" + subprocesos_item.Supuestos + "</td><td style='border: 1px solid #000;'></td><td style='border: 1px solid #000;'></td><td style='border: 1px solid #000;'></td><td style='border: 1px solid #000;'></td><td style='border: 1px solid #000;'></td></tr>";

                    foreach (var actividades_item in actividades)
                    {
                        html += "<tr style='background: #" + color_cadena + "'><td style='vertical-align: middle; text-align: center; border: 1px solid #000;' ><b>ACTIVIDAD:</b></td><td style='width: 500px; border: 1px solid #000;'>" + actividades_item.Actividad + "</td><td style='border: 1px solid #000;'></td><td style='border: 1px solid #000;'></td><td style='border: 1px solid #000;'></td><td style='border: 1px solid #000;'></td><td style='border: 1px solid #000;'></td><td style='border: 1px solid #000;'></td><td style='border: 1px solid #000;'></td><td style='border: 1px solid #000;'></td></tr>";

                        var indicadores = from i in db.Indicadores
                                          where i.Actividade.Subproceso.Causas_Efecto.Proyecto_id == proyecto_id && i.Actividad_id == actividades_item.Id
                                          select i;
                        int count_indicadores = 0;
                        foreach (var indicadores_item in indicadores)
                        {
                            string ssp = "";

                            if ((bool)indicadores_item.SSP)
                                ssp = "Si";
                            else if (!(bool)indicadores_item.SSP)
                                ssp = "No";

                            if (count_indicadores == 0)
                            {
                                if (indicadores.Count() <= 1)
                                    html += "<tr style='background: #" + color_cadena + "'><td style='vertical-align: middle; text-align: center; border: 1px solid #000;' rowspan='" + indicadores.Count() + "'><b>INDICADOR:</b></td><td style='width: 500px; border: 1px solid #000;'>" + indicadores_item.Indicador + "</td><td style='border: 1px solid #000;'></td><td style='border: 1px solid #000;'>" + indicadores_item.medios + "</td><td style='border: 1px solid #000;'>" + indicadores_item.supuestos + "</td><td style='border: 1px solid #000;'>" + indicadores_item.meta + "</td><td style='border: 1px solid #000;'>" + Convert.ToDateTime(indicadores_item.fecha_indicador_inicial).ToShortDateString() + "</td><td style='border: 1px solid #000;'>" + indicadores_item.Unidade.Unidad + "</td><td style='border: 1px solid #000;'>" + indicadores_item.Verbo.Verbo1 + "</td><td style='border: 1px solid #000;'>" + ssp + "</td></tr>";
                                else
                                    html += "<tr style='background: #" + color_cadena + "'><td style='vertical-align: middle; text-align: center; border: 1px solid #000;' rowspan='" + indicadores.Count() + "'><b>INDICADOR:</b></td><td style='width: 500px; border: 1px solid #000;'>" + indicadores_item.Indicador + "</td><td style='border: 1px solid #000;'></td><td style='border: 1px solid #000;'>" + indicadores_item.medios + "</td><td style='border: 1px solid #000;'>" + indicadores_item.supuestos + "</td><td style='border: 1px solid #000;'>" + indicadores_item.meta + "</td><td style='border: 1px solid #000;'>" + Convert.ToDateTime(indicadores_item.fecha_indicador_inicial).ToShortDateString() + "</td><td style='border: 1px solid #000;'>" + indicadores_item.Unidade.Unidad + "</td><td style='border: 1px solid #000;'>" + indicadores_item.Verbo.Verbo1 + "</td><td style='border: 1px solid #000;'>" + ssp + "</td></tr>";
                            }
                            else
                                html += "<tr style='background: #" + color_cadena + "'><td style='border: 1px solid #000;'>" + indicadores_item.Indicador + "</td><td style='width: 500px; border: 1px solid #000;'></td><td style='border: 1px solid #000;'>" + indicadores_item.medios + "</td><td style='border: 1px solid #000;'>" + indicadores_item.supuestos + "</td><td style='border: 1px solid #000;'>" + indicadores_item.meta + "</td><td style='border: 1px solid #000;'>" + Convert.ToDateTime(indicadores_item.fecha_indicador_inicial).ToShortDateString() + "</td><td style='border: 1px solid #000;'>" + indicadores_item.Unidade.Unidad + "</td><td style='border: 1px solid #000;'>" + indicadores_item.Verbo.Verbo1 + "</td><td style='border: 1px solid #000;'>" + ssp + "</td></tr>";

                            count_indicadores++;
                        }
                    }
                }
            }
            html = html + "</table>";

            return html;
        }

        protected string UpdateHTMLArbolProblemas()
        {

            var arbolproblemas = from p in new Model.ESMBDDataContext().Causas_Efectos
                                 where p.Proyecto_id == proyecto_id
                                 select p;

            var proyecto = (from p in new Model.ESMBDDataContext().Proyectos
                            where p.Id == proyecto_id
                            select p).Single();

            string html_arbol_problemas = "<table style='font-family: \"Arial Narrow\";'><tr>";
            string problema = "";
            foreach (var item in arbolproblemas)
            {
                problema = item.Proyecto.Problema;
                html_arbol_problemas += "<td style='border: solid 2px #000; width: 80px; height: 80px; background: #D1AC19; color: #000; text-align: center; vertical-align: middle;' colspan='2'><b>" + item.EfectoIndirecto + "</b></td><td></td><td style='width: 5px;'></td>";
            }
            html_arbol_problemas += "</tr><tr style='height: 20px;'>";
            int cant_tds = (arbolproblemas.Count() * 2);
            int colspan_proyecto = (arbolproblemas.Count() * 4);
            int count_tds = 0;
            for (int i = 0; i < (cant_tds * 2); i++)
            {
                if (count_tds == 1)
                {
                    html_arbol_problemas += "<td style='border-left: dashed #000 2px;'></td>";
                    count_tds++;
                }
                else if (count_tds == 3)
                {
                    html_arbol_problemas += "<td></td>";
                    count_tds = 0;
                }
                else
                {
                    html_arbol_problemas += "<td ></td>";
                    count_tds++;
                }
            }
            html_arbol_problemas += "</tr><tr style='height: 20px;'>";
            foreach (var item in arbolproblemas)
            {
                problema = item.Proyecto.Problema;
                html_arbol_problemas += "<td style='border: solid 2px #000; width: 80px; height: 80px; background: #FFC400; color: #000; text-align: center; vertical-align: middle;' colspan='2'><b>" + item.Efecto + "</b></td><td></td><td style='width: 5px;'></td>";
            }
            html_arbol_problemas += "</tr><tr style='height: 20px;'>";

            for (int i = 0; i < (cant_tds * 2); i++)
            {
                if (count_tds == 1)
                {
                    html_arbol_problemas += "<td style='border-left: dashed #000 2px;'></td>";
                    count_tds++;
                }
                else if (count_tds == 3)
                {
                    html_arbol_problemas += "<td></td>";
                    count_tds = 0;
                }
                else
                {
                    html_arbol_problemas += "<td ></td>";
                    count_tds++;
                }
            }
            html_arbol_problemas += "</tr><tr><td style='height:100px; text-align:center; vertical-align: middle; border: dashed #000 2px; background: #0f6db3; color: #fff;' colspan='" + colspan_proyecto.ToString() + "'>" + problema.ToUpper() + "</td></tr><tr style='height: 20px;'>";

            for (int i = 0; i < (cant_tds * 2); i++)
            {
                if (count_tds == 1)
                {
                    html_arbol_problemas += "<td style='border-left: dashed #000 2px;'></td>";
                    count_tds++;
                }
                else if (count_tds == 3)
                {
                    html_arbol_problemas += "<td></td>";
                    count_tds = 0;
                }
                else
                {
                    html_arbol_problemas += "<td ></td>";
                    count_tds++;
                }
            }

            html_arbol_problemas += "</tr><tr>";
            foreach (var item in arbolproblemas)
            {
                problema = item.Proyecto.Problema;
                html_arbol_problemas += "<td style='border: solid 2px #000; width: 80px; height: 80px; background: #1966D1; color: #fff; text-align: center; vertical-align: middle;' colspan='2'><b>" + item.Causa + "</b></td><td></td><td style='width: 5px;'></td>";
            }

            html_arbol_problemas += "</tr><tr style='height: 20px;'>";
            for (int i = 0; i < (cant_tds * 2); i++)
            {
                if (count_tds == 1)
                {
                    html_arbol_problemas += "<td style='border-left: dashed #000 2px;'></td>";
                    count_tds++;
                }
                else if (count_tds == 3)
                {
                    html_arbol_problemas += "<td></td>";
                    count_tds = 0;
                }
                else
                {
                    html_arbol_problemas += "<td ></td>";
                    count_tds++;
                }
            }
            html_arbol_problemas += "</tr><tr style='height: 20px;'>";

            foreach (var item in arbolproblemas)
            {
                problema = item.Proyecto.Problema;
                html_arbol_problemas += "<td style='border: solid 2px #000; width: 80px; height: 80px; background: #941515; color: #fff; text-align: center; vertical-align: middle;' colspan='2'><b>" + item.CausaIndirecta + "</b></td><td></td><td style='width: 5px;'></td>";
            }

            html_arbol_problemas += "</tr></table>";

            return html_arbol_problemas;
        }

        protected string UpdateHTMLArbolProblemasReport()
        {

            var arbolproblemas = from p in new Model.ESMBDDataContext().Causas_Efectos
                                 where p.Proyecto_id == proyecto_id
                                 select p;

            var proyecto = (from p in new Model.ESMBDDataContext().Proyectos
                            where p.Id == proyecto_id
                            select p).Single();

            string html_arbol_problemas = "<table style='width: 800px;'><tr>";
            string problema = "";
            foreach (var item in arbolproblemas)
            {
                problema = item.Proyecto.Problema;
                html_arbol_problemas += "<td style='border: solid 2px #000; max-width: 80px; width: 80px; max-height: 80px; font-size: 10px; height: 80px; background: #D1AC19; color: #000; text-align: justify; vertical-align: middle;' colspan='2'>" + item.EfectoIndirecto + "</td><td></td><td style='width: 5px;'></td>";
            }
            html_arbol_problemas += "</tr><tr style='height: 20px; max-height:20px; font-size: 10px;'>";
            int cant_tds = (arbolproblemas.Count() * 2);
            int colspan_proyecto = (arbolproblemas.Count() * 4);
            int count_tds = 0;
            for (int i = 0; i < (cant_tds * 2); i++)
            {
                if (count_tds == 1)
                {
                    html_arbol_problemas += "<td style='border-left: dashed #000 2px;'></td>";
                    count_tds++;
                }
                else if (count_tds == 3)
                {
                    html_arbol_problemas += "<td></td>";
                    count_tds = 0;
                }
                else
                {
                    html_arbol_problemas += "<td ></td>";
                    count_tds++;
                }
            }
            html_arbol_problemas += "</tr><tr style='height: 20px;'>";
            foreach (var item in arbolproblemas)
            {
                problema = item.Proyecto.Problema;
                html_arbol_problemas += "<td style='border: solid 2px #000; width: 80px; max-width: 80px; max-height: 80px; font-size: 10px; height: 80px; background: #FFC400; color: #000; text-align: justify; vertical-align: middle;' colspan='2'>" + item.Efecto + "</td><td></td><td style='width: 5px;'></td>";
            }
            html_arbol_problemas += "</tr><tr style='height: 20px;'>";

            for (int i = 0; i < (cant_tds * 2); i++)
            {
                if (count_tds == 1)
                {
                    html_arbol_problemas += "<td style='border-left: dashed #000 2px;'></td>";
                    count_tds++;
                }
                else if (count_tds == 3)
                {
                    html_arbol_problemas += "<td></td>";
                    count_tds = 0;
                }
                else
                {
                    html_arbol_problemas += "<td ></td>";
                    count_tds++;
                }
            }
            html_arbol_problemas += "</tr><tr><td style='height:100px; width: 100%; text-align: justify; vertical-align: middle; font-size: 10px; border: dashed #000 2px; background: #0f6db3; color: #fff;' colspan='" + colspan_proyecto.ToString() + "'>" + problema.ToUpper() + "</td></tr><tr style='height: 20px;'>";

            for (int i = 0; i < (cant_tds * 2); i++)
            {
                if (count_tds == 1)
                {
                    html_arbol_problemas += "<td style='border-left: dashed #000 2px;'></td>";
                    count_tds++;
                }
                else if (count_tds == 3)
                {
                    html_arbol_problemas += "<td></td>";
                    count_tds = 0;
                }
                else
                {
                    html_arbol_problemas += "<td ></td>";
                    count_tds++;
                }
            }

            html_arbol_problemas += "</tr><tr>";
            foreach (var item in arbolproblemas)
            {
                problema = item.Proyecto.Problema;
                html_arbol_problemas += "<td style='border: solid 2px #000; width: 80px; max-width: 80px; height: 80px; max-height: 80px; background: #1966D1; color: #fff; text-align: justify; font-size: 10px; vertical-align: middle;' colspan='2'>" + item.Causa + "</td><td></td><td style='width: 5px;'></td>";
            }

            html_arbol_problemas += "</tr><tr style='height: 20px;'>";
            for (int i = 0; i < (cant_tds * 2); i++)
            {
                if (count_tds == 1)
                {
                    html_arbol_problemas += "<td style='border-left: dashed #000 2px;'></td>";
                    count_tds++;
                }
                else if (count_tds == 3)
                {
                    html_arbol_problemas += "<td></td>";
                    count_tds = 0;
                }
                else
                {
                    html_arbol_problemas += "<td ></td>";
                    count_tds++;
                }
            }
            html_arbol_problemas += "</tr><tr style='height: 20px;'>";

            foreach (var item in arbolproblemas)
            {
                problema = item.Proyecto.Problema;
                html_arbol_problemas += "<td style='border: solid 2px #000; max-width: 80px; width: 80px; max-height: 80px; font-size: 10px; height: 80px; background: #941515; color: #fff; text-align: justify; vertical-align: middle;' colspan='2'><b>" + item.CausaIndirecta + "</b></td><td></td><td style='width: 5px;'></td>";
            }

            html_arbol_problemas += "</tr></table>";

            return html_arbol_problemas;
        }

        protected string UpdateHTMLArbolObjetivos()
        {

            var arbolobjetivos = from p in new Model.ESMBDDataContext().Causas_Efectos
                                 where p.Proyecto_id == proyecto_id
                                 select p;

            var proyecto = (from p in new Model.ESMBDDataContext().Proyectos
                            where p.Id == proyecto_id
                            select p).Single();

            string html_arbol_objetivos = "<table><tr>";
            string problema = "";
            foreach (var item in arbolobjetivos)
            {
                problema = item.Proyecto.Problema;
                html_arbol_objetivos += "<td style='border: solid 2px #000; width: 80px; height: 80px; background: #92C414; color: #fff; text-align: center; vertical-align: middle;' colspan='2'><b>" + item.Beneficios + "</b></td><td></td><td style='width: 5px;'></td>";
            }
            html_arbol_objetivos += "</tr><tr style='height: 20px;'>";
            int cant_tds = (arbolobjetivos.Count() * 2);
            int colspan_proyecto = (arbolobjetivos.Count() * 4);
            int count_tds = 0;
            for (int i = 0; i < (cant_tds * 2); i++)
            {
                if (count_tds == 1)
                {
                    html_arbol_objetivos += "<td style='border-left: dashed #000 2px;'></td>";
                    count_tds++;
                }
                else if (count_tds == 3)
                {
                    html_arbol_objetivos += "<td></td>";
                    count_tds = 0;
                }
                else
                {
                    html_arbol_objetivos += "<td ></td>";
                    count_tds++;
                }
            }

            html_arbol_objetivos += "</tr><tr>";

            html_arbol_objetivos += "<td style='height:100px; text-align:center; vertical-align: middle; background: #258A0C; border: dashed #000 2px;' colspan='" + colspan_proyecto.ToString() + "'>" + proyecto.Proposito.ToUpper() + "</td>";

            html_arbol_objetivos += "</tr><tr style='height: 20px;'>";

            for (int i = 0; i < (cant_tds * 2); i++)
            {
                if (count_tds == 1)
                {
                    html_arbol_objetivos += "<td style='border-left: dashed #000 2px;'></td>";
                    count_tds++;
                }
                else if (count_tds == 3)
                {
                    html_arbol_objetivos += "<td></td>";
                    count_tds = 0;
                }
                else
                {
                    html_arbol_objetivos += "<td ></td>";
                    count_tds++;
                }
            }

            html_arbol_objetivos += "</tr><tr><td style='height:100px; text-align:center; vertical-align: middle; border: dashed #000 2px;' colspan='" + colspan_proyecto.ToString() + "'>" + problema.ToUpper() + "</td></tr><tr style='height: 20px;'>";

            for (int i = 0; i < (cant_tds * 2); i++)
            {
                if (count_tds == 1)
                {
                    html_arbol_objetivos += "<td style='border-left: dashed #000 2px;'></td>";
                    count_tds++;
                }
                else if (count_tds == 3)
                {
                    html_arbol_objetivos += "<td></td>";
                    count_tds = 0;
                }
                else
                {
                    html_arbol_objetivos += "<td ></td>";
                    count_tds++;
                }
            }

            html_arbol_objetivos += "</tr><tr>";
            foreach (var item in arbolobjetivos)
            {
                problema = item.Proyecto.Problema;
                html_arbol_objetivos += "<td style='border: solid 2px #000; width: 80px; height: 80px; background: #0571AB; color: #fff; text-align: center; vertical-align: middle;' colspan='2'><b>" + item.Proceso + "</b></td><td></td><td style='width: 5px;'></td>";
            }
            html_arbol_objetivos += "</tr><tr style='height: 20px;'>";
            for (int i = 0; i < (cant_tds * 2); i++)
            {
                //Lineas de empalme para las etiquetas de arbol objetivos
                //if (count_tds == 1)
                //{
                //    html_arbol_objetivos += "<td style='border-left: dashed #000 2px;'></td>";
                //    count_tds++;
                //}
                //else if (count_tds == 3)
                //{
                //    html_arbol_objetivos += "<td></td>";
                //    count_tds = 0;
                //}
                //else
                //{
                //    html_arbol_objetivos += "<td ></td>";
                //    count_tds++;
                //}
            }

            html_arbol_objetivos += "</tr><tr>";
            foreach (var item in arbolobjetivos)
            {
                problema = item.Proyecto.Problema;
                //Etiqueta Arbol de Objetivos
                //html_arbol_objetivos += "<td style='border: solid 2px #000; width: 80px; height: 80px; background: #0571AB; color: #fff; text-align: center; vertical-align: middle;' colspan='2'><b>En la siguiente página se diligenciaran las actividades para este objetivo.</b></td><td></td><td style='width: 5px;'></td>";
            }



            html_arbol_objetivos += "</tr></table>";

            return html_arbol_objetivos;
        }

        protected string UpdateHTMLArbolObjetivosReport()
        {

            var arbolobjetivos = from p in new Model.ESMBDDataContext().Causas_Efectos
                                 where p.Proyecto_id == proyecto_id
                                 select p;

            var proyecto = (from p in new Model.ESMBDDataContext().Proyectos
                            where p.Id == proyecto_id
                            select p).Single();

            string html_arbol_objetivos = "<table style='width: 800px;'><tr>";
            string problema = "";
            foreach (var item in arbolobjetivos)
            {
                problema = item.Proyecto.Problema;
                html_arbol_objetivos += "<td style='border: solid 2px #000; width: 80px; max-width: 80px; max-height: 80px;  font-size: 10px; height: 80px; background: #92C414; color: #fff; text-align: center; vertical-align: middle;' colspan='2'><b>" + item.Beneficios + "</b></td><td></td><td style='width: 5px;'></td>";
            }
            html_arbol_objetivos += "</tr><tr style='height: 20px;'>";
            int cant_tds = (arbolobjetivos.Count() * 2);
            int colspan_proyecto = (arbolobjetivos.Count() * 4);
            int count_tds = 0;
            for (int i = 0; i < (cant_tds * 2); i++)
            {
                if (count_tds == 1)
                {
                    html_arbol_objetivos += "<td style='border-left: dashed #000 2px;'></td>";
                    count_tds++;
                }
                else if (count_tds == 3)
                {
                    html_arbol_objetivos += "<td></td>";
                    count_tds = 0;
                }
                else
                {
                    html_arbol_objetivos += "<td ></td>";
                    count_tds++;
                }
            }

            html_arbol_objetivos += "</tr><tr>";

            html_arbol_objetivos += "<td style='height:100px; text-align:center; width: 100%; font-size: 10px; vertical-align: middle; background: #258A0C; border: dashed #000 2px;' colspan='" + colspan_proyecto.ToString() + "'>" + proyecto.Proposito.ToUpper() + "</td>";

            html_arbol_objetivos += "</tr><tr style='height: 20px;'>";

            for (int i = 0; i < (cant_tds * 2); i++)
            {
                if (count_tds == 1)
                {
                    html_arbol_objetivos += "<td style='border-left: dashed #000 2px;'></td>";
                    count_tds++;
                }
                else if (count_tds == 3)
                {
                    html_arbol_objetivos += "<td></td>";
                    count_tds = 0;
                }
                else
                {
                    html_arbol_objetivos += "<td ></td>";
                    count_tds++;
                }
            }

            html_arbol_objetivos += "</tr><tr><td style='height:100px; width: 100%; font-size: 10px; text-align:center; vertical-align: middle; border: dashed #000 2px;' colspan='" + colspan_proyecto.ToString() + "'>" + problema.ToUpper() + "</td></tr><tr style='height: 20px;'>";

            for (int i = 0; i < (cant_tds * 2); i++)
            {
                if (count_tds == 1)
                {
                    html_arbol_objetivos += "<td style='border-left: dashed #000 2px;'></td>";
                    count_tds++;
                }
                else if (count_tds == 3)
                {
                    html_arbol_objetivos += "<td></td>";
                    count_tds = 0;
                }
                else
                {
                    html_arbol_objetivos += "<td ></td>";
                    count_tds++;
                }
            }

            html_arbol_objetivos += "</tr><tr>";
            foreach (var item in arbolobjetivos)
            {
                problema = item.Proyecto.Problema;
                html_arbol_objetivos += "<td style='border: solid 2px #000;  max-width: 80px; max-height: 80px;  font-size: 10px; width: 80px; height: 80px; background: #0571AB; color: #fff; text-align: center; vertical-align: middle;' colspan='2'><b>" + item.Proceso + "</b></td><td></td><td style='width: 5px;'></td>";
            }
            html_arbol_objetivos += "</tr><tr style='height: 20px;'>";


            html_arbol_objetivos += "</tr><tr>";
            foreach (var item in arbolobjetivos)
            {
                problema = item.Proyecto.Problema;
            }



            html_arbol_objetivos += "</tr></table>";

            return html_arbol_objetivos;
        }
    }
}