using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using ESM.Objetos;
using ESM.Model;
using System.Web.Script.Services;
using System.IO;

namespace ESM
{
    public partial class ajaxBancoProyectos : System.Web.UI.Page
    {
        #region Propiedades Publicas y Privadas

        JavaScriptSerializer objJavaScriptSerializer = new JavaScriptSerializer();
        Objetos.Cproyecto _objCproyecto = new Objetos.Cproyecto();
        CFuentes_Financiacion objCFuentes_Financiacion = new CFuentes_Financiacion();
        CMatriz_Actores objCMatriz = new CMatriz_Actores();
        CEfectos objCCausas_Efecto = new CEfectos();
        CSubprocesos objSubprocesos = new CSubprocesos();
        CActividades objCActividades = new CActividades();
        int proyecto_id = 0;

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Request.QueryString["deleteFile"] != null)
            {
                int documento_id = Convert.ToInt32(Request.QueryString["deleteFile"]);
                deleteFile(documento_id);
            }
            else if (Request.QueryString["proyecto_id"] != null && Request.QueryString["proyecto_id"] != "undefined" && Request.QueryString["proyecto_id"] != " " && Request.QueryString["proyecto_id"] != "")
                proyecto_id = Convert.ToInt32(Request.QueryString["proyecto_id"]);

            if (!Page.IsPostBack)
            {
                if (Request.QueryString["file"] != null && Request.QueryString["file"].ToString() == "true")
                    CargarFile();

                if (Request.HttpMethod != "POST")
                {
                    if (Request.QueryString["modulo"] != null && proyecto_id != 0)
                    {
                        if (Request.QueryString["tabla"].ToString() == "f_f" && proyecto_id != 0)
                        {
                            string operacion = Session["operacion"] == null ? "" : Session["operacion"].ToString();
                            if (operacion == "add")
                                AgregarFuentesFinanciacion();
                            else if (operacion == "edit")
                                EditarFuentesFinanciacion();
                        }
                        else if (Request.QueryString["tabla"].ToString() == "i" && proyecto_id != 0)
                        {
                            string operacion = Session["operacion"] == null ? "" : Session["operacion"].ToString();
                            if (operacion == "add")
                                AgregarMatrizActores();
                            else if (operacion == "edit")
                                EditarMatriz();
                        }
                        else if (Request.QueryString["tabla"].ToString() == "c_e" && proyecto_id != 0)
                        {
                            string operacion = Session["operacion"] == null ? "" : Session["operacion"].ToString();
                            if (operacion == "add")
                                AgregarCausasEfectos();
                            else if (operacion == "edit")
                                EditarCausasEfectos();
                        }
                        else if (Request.QueryString["tabla".ToString()] == "subp" && proyecto_id != 0)
                        {
                            string operacion = Session["operacion"] == null ? "" : Session["operacion"].ToString();
                            if (operacion == "add")
                                AgregarSubprocesos();
                            else if (operacion == "edit")
                                EditarSubprocesos();
                        }
                        else if (Request.QueryString["tabla"] == "act" && proyecto_id != 0)
                        {
                            string operacion = Session["operacion"] == null ? "" : Session["operacion"].ToString();
                            if (operacion == "add")
                                AgregarActividad();
                            else if (operacion == "edit")
                                EditarActividades();
                        }
                        else if (Request.QueryString["tabla"] == "ind" && proyecto_id != 0)
                        {
                            string operacion = Session["operacion"] == null ? "" : Session["operacion"].ToString();
                            if (operacion == "add")
                                AgregarIndicadores();
                            else if (operacion == "edit")
                                EditarIndicadores();
                        }
                        else if (Request.QueryString["tabla"] == "ind_val_t" && proyecto_id != 0)
                        {
                            string operacion = Session["operacion"] == null ? "" : Session["operacion"].ToString();
                            if (operacion == "add")
                                AgregarIndicadoresValores();
                            else if (operacion == "edit")
                                EditarIndicadoresValores();
                        }

                        if (Request.QueryString["modulo"].ToString() == "fuentes_financiacion")
                            CargarFuentesFinanciacion();
                        else if (Request.QueryString["modulo"].ToString() == "identificacion")
                            CargarIdentificacion();
                        else if (Request.QueryString["modulo"].ToString() == "causas_efectos")
                            CargarCausasEfectos();
                        else if (Request.QueryString["modulo"].ToString() == "subprocesos")
                            CargarSubprocesos();
                        else if (Request.QueryString["modulo"].ToString() == "actividades")
                            CargarActividades();
                        else if (Request.QueryString["modulo"].ToString() == "indicador")
                            CargarIndicadores();
                        else if (Request.QueryString["modulo"].ToString() == "actividades_indicadores")
                            CargarIndicadoresGroupActividades();
                        else if (Request.QueryString["modulo"].ToString() == "plan_operativo")
                            CargarPlanOperativo();
                        else if (Request.QueryString["modulo"].ToString() == "sub_act_group")
                            CargarGroupSubprocesosActividades();
                        else if (Request.QueryString["modulo"].ToString() == "ind_val")
                            CargarIndicadoresValores();
                    }
                    else if (Request.QueryString["actualizarproyecto"] != null && Convert.ToBoolean(Request.QueryString["actualizarproyecto"]))
                        ActualziarProyecto(proyecto_id);
                    else if (Request.QueryString["actualizararbolproblemas"] != null && Convert.ToBoolean(Request.QueryString["actualizararbolproblemas"]))
                        UpdateHTMLArbolProblemas();
                    else if (Request.QueryString["actualizararbolobjetivos"] != null && Convert.ToBoolean(Request.QueryString["actualizararbolobjetivos"]))
                        UpdateHTMLArbolObjetivos();
                    else if (Request.QueryString["refreshMarcoLogico"] != null && Convert.ToBoolean(Request.QueryString["refreshMarcoLogico"]))
                        generateMarcoLogico();
                }
                else
                {
                    if (Request.Form["tipoentidad"] != null)
                    {
                        Session.Add("f_f_id", Request.Form["id"]);
                        Session.Add("tipo_entidad", Request.Form["tipoentidad"]);
                        Session.Add("entidadd", Request.Form["entidad"]);
                        Session.Add("tiporecurso", Request.Form["tiporecurso"]);
                        Session.Add("operacion", Request.Form["oper"]);
                    }
                    else if (Request.Form["grupos"] != null)
                    {
                        Session.Add("id", Request.Form["id"]);
                        Session.Add("grupos", Request.Form["grupos"]);
                        Session.Add("interes", Request.Form["interes"]);
                        Session.Add("problemarecibido", Request.Form["problemarecibido"]);
                        Session.Add("recursosymandatos", Request.Form["recursosymandatos"]);
                        Session.Add("operacion", Request.Form["oper"]);
                    }
                    else if (Request.Form["causa"] != null || Request.Form["objetivo"] != null)
                    {
                        Session.Add("c_e_id", Request.Form["id"]);
                        if (Request.Form["causaindirecta"] != null)
                        {
                            Session.Add("causa", Request.Form["causa"]);
                            Session.Add("efecto", Request.Form["efecto"]);
                            Session.Add("causaindirecta", Request.Form["causaindirecta"]);
                            Session.Add("efectoindirecto", Request.Form["efectoindirecto"]);
                        }
                        else if (Request.Form["objetivo"] != null)
                        {
                            Session.Add("objetivo", Request.Form["objetivo"]);
                            Session.Add("beneficio", Request.Form["beneficio"]);
                        }
                        Session.Add("operacion", Request.Form["oper"]);
                    }
                    else if (Request.Form["ejecutado"] != null)
                    {
                        Session.Add("ind_val_id", Request.Form["id"]);
                        Session.Add("indicador_id", Request.Form["indicador"]);
                        Session.Add("meta", Request.Form["meta"]);
                        Session.Add("fecha", Request.Form["fecha"]);
                        Session.Add("ejecutado", Request.Form["ejecutado"]);
                        Session.Add("operacion", Request.Form["oper"]);
                    }
                    else if (Request.Form["meta"] != null)
                    {
                        Session.Add("ind_id", Request.Form["id"]);
                        Session.Add("actividad", Request.Form["actividad"]);
                        Session.Add("verbo", Request.Form["verbo"]);
                        Session.Add("meta", Request.Form["meta"]);
                        Session.Add("unidad", Request.Form["unidad"]);
                        Session.Add("ssp", Request.Form["ssp"]);
                        Session.Add("fechainicial", Request.Form["fechainicial"]);
                        Session.Add("fechafinal", Request.Form["fechafinal"]);
                        Session.Add("medios", Request.Form["medios"]);
                        Session.Add("supuestos", Request.Form["supuestos"]);
                        Session.Add("descripcion", Request.Form["descripcion"]);
                        Session.Add("operacion", Request.Form["oper"]);
                        Session.Add("tiporedaccion", Request.Form["tiporedaccion"]);
                    }
                    else if (Request.Form["actividad"] != null)
                    {
                        Session.Add("act_id", Request.Form["id"]);
                        Session.Add("subproceso", Request.Form["subproceso"]);
                        Session.Add("actividad", Request.Form["actividad"]);
                        Session.Add("fecha", Request.Form["fecha"]);
                        Session.Add("presupuesto", Request.Form["presupuesto"]);
                        Session.Add("operacion", Request.Form["oper"]);
                    }
                    else if (Request.Form["subproceso"] != null)
                    {
                        Session.Add("sub_id", Request.Form["id"]);
                        Session.Add("proceso", Request.Form["proceso"]);
                        Session.Add("subproceso", Request.Form["subproceso"]);
                        Session.Add("indicador", Request.Form["indicador"]);
                        Session.Add("medios", Request.Form["medios"]);
                        Session.Add("supuestos", Request.Form["supuestos"]);
                        Session.Add("operacion", Request.Form["oper"]);
                    }

                }
            }
        }

        protected void UpdateHTMLArbolProblemas()
        {
            try
            {
                var arbolproblemas = from p in new Model.ESMBDDataContext().Causas_Efectos
                                     where p.Proyecto_id == proyecto_id
                                     select p;

                var proyecto = (from p in new Model.ESMBDDataContext().Proyectos
                                where p.Id == proyecto_id
                                select p).Single();

                string html_arbol_problemas = "<table><tr>";
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
                html_arbol_problemas += "</tr><tr><td style='height:100px; text-align:center; background: #0f6db3; color: #fff; vertical-align: middle; border: dashed #000 2px;' colspan='" + colspan_proyecto.ToString() + "'><b>" + problema.ToUpper() + "</b></td></tr><tr style='height: 20px;'>";

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

                Response.Write(html_arbol_problemas);
            }
            catch (Exception) { Response.Write("null"); }

        }

        protected void UpdateHTMLArbolObjetivos()
        {
            try
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

                html_arbol_objetivos += "<td style='height:100px; text-align:center; background: #258A0C; color: #fff; border: dashed #000; vertical-align: middle; border: dashed #000 2px;' colspan='" + colspan_proyecto.ToString() + "'><b>" + proyecto.Finalidad.ToUpper() + "</b></td>";

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

                html_arbol_objetivos += "</tr><tr><td style='height:100px; text-align:center; vertical-align: middle; border: dashed #000 2px;' colspan='" + colspan_proyecto.ToString() + "'>" + proyecto.Proposito.ToUpper() + "</td></tr><tr style='height: 20px;'>";

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
                    html_arbol_objetivos += "<td style='border: solid 2px #000; width: 80px; height: 80px; background: #0571AB; color: #fff; text-align: center; vertical-align: middle;' colspan='2'><b>En la siguiente página se diligenciaran las actividades para este objetivo.</b></td><td></td><td style='width: 5px;'></td>";
                }

                html_arbol_objetivos += "</tr></table>";

                Response.Write(html_arbol_objetivos);
            }
            catch (Exception) { Response.Write("null"); }

        }

        protected void ActualziarProyecto(int proyecto_id)
        {
            try
            {
                PropiedadesProyecto objPropiedadesProyecto = new PropiedadesProyecto();

                Model.Proyecto objProyecto_Obtenido = _objCproyecto.GetProyecto(proyecto_id);
                if (objProyecto_Obtenido != null)
                {
                    objPropiedadesProyecto.Nombre = objProyecto_Obtenido.Proyecto1.ToString();
                    objPropiedadesProyecto.Problema = objProyecto_Obtenido.Problema.ToString();
                    objPropiedadesProyecto.Id = objProyecto_Obtenido.Id.ToString();

                    List<PropiedadesProyecto> objList_PropiedadesProyecto = new List<PropiedadesProyecto>() { objPropiedadesProyecto };

                    string json_var = objJavaScriptSerializer.Serialize(objList_PropiedadesProyecto);

                    Response.Write(json_var);
                }
                else
                    Response.Write("alert(\"Opss... Ocurrio un error inesperado.\");");

            }
            catch (Exception)
            {
                Response.Write("alert(\"Opss... Ocurrio un error inesperado.\");");
            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        protected void CargarIndicadoresValores()
        {
            try
            {
                int indicador_id = Convert.ToInt32(Request.QueryString["indicador_id"]);

                IQueryable<Indicadores_Meta> indicadores_metas_col = new Objetos.CActividades().getIndicadoresMetasProyecto(indicador_id);

                string json_to_return = "{\"page\":\"1\",\"total\":1,\"records\":\"1\",\"rows\": [";

                foreach (var item in indicadores_metas_col)
                    json_to_return = json_to_return + "{\"id\":\"" + item.Id.ToString() + "\",\"cell\":[\"" + item.Id + "\",\"" + item.Indicadore.Indicador + "\", \"" + item.Meta + "\", \"" + Convert.ToDateTime(item.Fecha_Meta).ToShortDateString() + "\",\"" + item.Ejecutado + "\", \"/ReporteIndicadores.aspx?id=" + item.Indicadore.Id + "\"]} ,";

                json_to_return = json_to_return.Trim(',');
                json_to_return = json_to_return + "]}";

                Response.Write(json_to_return);
            }
            catch (Exception) { Response.Write("null"); }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        protected void CargarFuentesFinanciacion()
        {
            List<PropiedadesFuentesFinanciacion> objList_Fuentes_Financiacion = new List<PropiedadesFuentesFinanciacion>();
            IQueryable<Fuentes_Financiacion> Fuentes_Financiacion_col = new CFuentes_Financiacion().GetFuentesFinanciacion(proyecto_id);

            string json_to_return = "{\"page\":\"1\",\"total\":1,\"records\":\"1\",\"rows\": [";

            foreach (var item in Fuentes_Financiacion_col)
                json_to_return = json_to_return + "{\"id\":\"" + item.Id.ToString() + "\",\"cell\":[\"" + item.Id + "\",\"" + item.Tipo_Entidad + "\", \"" + item.Entidad + "\", \"" + item.Tipo_Recurso + "\"]} ,";

            json_to_return = json_to_return.Trim(',');
            json_to_return = json_to_return + "]}";

            Response.Write(json_to_return);

        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        protected void CargarIdentificacion()
        {
            IQueryable<Matriz_Actore> Matriz_Actores_col = new CMatriz_Actores().GetForProject(proyecto_id);

            string json_to_return = "{\"page\":\"1\",\"total\":1,\"records\":\"1\",\"rows\": [";

            foreach (var item in Matriz_Actores_col)
            {
                json_to_return = json_to_return + "{\"id\":\"" + item.Id.ToString() + "\",\"cell\":[\"" + item.Id + "\",\"" + item.Grupos + "\", \"" + item.Interes + "\", \"" + item.Problema_Percibido + "\",\"" + item.Recursos_Mandatos + "\"]} ,";
            }
            json_to_return = json_to_return.Trim(',');
            json_to_return = json_to_return + "]}";

            Response.Write(json_to_return);

        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        protected void CargarCausasEfectos()
        {
            IQueryable<Causas_Efecto> causas_efectos_col = new CEfectos().getCausas_Efectos(proyecto_id);

            string json_to_return = "{\"page\":\"1\",\"total\":1,\"records\":\"1\",\"rows\": [";

            foreach (var item in causas_efectos_col)
            {
                json_to_return = json_to_return + "{\"id\":\"" + item.Id.ToString() + "\",\"cell\":[\"" + item.Id + "\",\"" + item.Causa + "\", \"" + item.Efecto + "\", \"" + item.Beneficios + "\",\"" + item.CausaIndirecta + "\",\"" + item.EfectoIndirecto + "\",\"" + item.Proceso + "\"]} ,";
            }
            json_to_return = json_to_return.Trim(',');
            json_to_return = json_to_return + "]}";

            Response.Write(json_to_return);

        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        protected void CargarSubprocesos()
        {
            IQueryable<Subproceso> subprocesos_col = new CSubprocesos().getSubprocesos(proyecto_id);

            string json_to_return = "{\"page\":\"1\",\"total\":1,\"records\":\"1\",\"rows\": [";

            foreach (var item in subprocesos_col)
            {
                json_to_return = json_to_return + "{\"id\":\"" + item.Id.ToString() + "\",\"cell\":[\"" + item.Id + "\",\"" + item.Causas_Efecto.Causa + "\", \"" + item.Subproceso1 + "\", \"" + item.Indicador + "\",\"" + item.Medios + "\",\"" + item.Supuestos + "\"]} ,";
            }
            json_to_return = json_to_return.Trim(',');
            json_to_return = json_to_return + "]}";

            Response.Write(json_to_return);

        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        protected void CargarActividades()
        {
            ///TODO:JCMM: Modificación para buen funcionamiento de interfaz usuario
            /// 23 May 2012 - 02:18 p.m.
            int subproceso_id = Request.QueryString["subproceso_id"] == null || Request.QueryString["subproceso_id"] == "" ? 0 : Convert.ToInt32(Request.QueryString["subproceso_id"]);
            IQueryable<Actividade> actividades_col = new CActividades().getActividades(subproceso_id);

            string json_to_return = "{\"page\":\"1\",\"total\":1,\"records\":\"1\",\"rows\": [";

            foreach (var item in actividades_col)
            {
                json_to_return = json_to_return + "{\"id\":\"" + item.Id.ToString() + "\",\"cell\":[\"" + item.Id + "\",\"" + item.Subproceso.Subproceso1 + "\", \"" + item.Actividad + "\", \"" + Convert.ToDateTime(item.fecha).ToShortDateString() + "\",\"" + item.Presupuesto + "\"]} ,";
            }
            json_to_return = json_to_return.Trim(',');
            json_to_return = json_to_return + "]}";

            Response.Write(json_to_return);

        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        protected void CargarIndicadores()
        {

            ESMBDDataContext db = new Model.ESMBDDataContext();

            int actividad_id = Request.QueryString["actividad_id"] == null || Request.QueryString["actividad_id"] == "" ? 0 : Convert.ToInt32(Request.QueryString["actividad_id"]);

            var indicadores_col = from ind in db.Indicadores
                                  where ind.Actividade.Id == actividad_id
                                  select ind;

            string json_to_return = "{\"page\":\"1\",\"total\":1,\"records\":\"1\",\"rows\": [";

            foreach (var item in indicadores_col)
            {
                string ssp_val = item.SSP.ToString() == "True" ? "Si" : "No";
                json_to_return = json_to_return + "{\"id\":\"" + item.Id.ToString() + "\",\"cell\":[\"" + item.Id + "\",\"" + item.Actividade.Actividad + "\", \"" + item.Verbo.Verbo1 + "\", \"" + item.meta + "\",\"" + item.Unidade.Unidad + "\",\"" + item.descripcion + "\",\"" + ssp_val + "\",\"" + Convert.ToDateTime(item.fecha_indicador_inicial).ToShortDateString() + "\",\"" + Convert.ToDateTime(item.fecha_indicador_final).ToShortDateString() + "\",\"" + item.Indicador + "\",\"" + item.medios + "\",\"" + item.supuestos + "\"]} ,";
            }
            json_to_return = json_to_return.Trim(',');
            json_to_return = json_to_return + "]}";

            Response.Write(json_to_return);
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        protected void CargarIndicadoresGroupActividades()
        {

            ESMBDDataContext db = new Model.ESMBDDataContext();

            var indicadores_col = from ind in db.Indicadores
                                  where ind.Actividade.Subproceso.Causas_Efecto.Proyecto_id == proyecto_id
                                  select ind;

            string json_to_return = "{\"page\":\"1\",\"total\":1,\"records\":\"1\",\"rows\": [";

            foreach (var item in indicadores_col)
            {
                json_to_return = json_to_return + "{\"id\":\"" + item.Id.ToString() + "\",\"cell\":[\"" + item.Id + "\",\"" + item.Actividade.Actividad + "\", \"" + item.Indicador + "\"]} ,";
            }
            json_to_return = json_to_return.Trim(',');
            json_to_return = json_to_return + "]}";

            Response.Write(json_to_return);
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        protected void CargarGroupSubprocesosActividades()
        {
            ESMBDDataContext db = new Model.ESMBDDataContext();

            var actividades_col = from act in db.Actividades
                                  where act.Subproceso.Causas_Efecto.Proyecto_id == proyecto_id
                                  select act;

            string json_to_return = "{\"page\":\"1\",\"total\":1,\"records\":\"1\",\"rows\": [";

            foreach (var item in actividades_col)
            {
                json_to_return = json_to_return + "{\"id\":\"" + item.Id.ToString() + "\",\"cell\":[\"" + item.Id + "\",\"" + item.Subproceso.Subproceso1 + "\", \"" + item.Actividad + "\"]} ,";
            }
            json_to_return = json_to_return.Trim(',');
            json_to_return = json_to_return + "]}";

            Response.Write(json_to_return);
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        protected void CargarPlanOperativo()
        {
            ESMBDDataContext db = new Model.ESMBDDataContext();

            var subproceso_col = from subprocesos in db.Subprocesos
                                 where subprocesos.Causas_Efecto.Proyecto_id == proyecto_id
                                 select subprocesos;

            string json_to_return = "{\"page\":\"1\",\"total\":1,\"records\":\"1\",\"rows\": [";

            foreach (var item in subproceso_col)
            {
                json_to_return = json_to_return + "{\"id\":\"" + item.Id.ToString() + "\",\"cell\":[\"" + item.Id + "\",\"" + item.Causas_Efecto.Causa + "\",\"" + item.Subproceso1 + "\"]} ,";
            }
            json_to_return = json_to_return.Trim(',');
            json_to_return = json_to_return + "]}";

            Response.Write(json_to_return);
        }

        protected void AgregarIndicadoresValores()
        {
            //Session.Add("ind_val_id", Request.Form["id"]);

            int indicador_id = Convert.ToInt32(Session["indicador_id"].ToString());
            int meta = Convert.ToInt32(Session["meta"].ToString());
            DateTime fecha = Convert.ToDateTime(Session["fecha"].ToString());
            int ejecutado = Convert.ToInt32(Session["ejecutado"].ToString());

            if (objCActividades.AddMeta_Valor(indicador_id, fecha, meta, ejecutado))
            {
                Session.Remove("ind_val_id");
                Session.Remove("indicador_id");
                Session.Remove("indicador");
                Session.Remove("meta");
                Session.Remove("fecha");
                Session.Remove("ejecutado");
                Session.Remove("operacion");

            }
        }

        protected void EditarIndicadoresValores()
        {
            int meta_id = Convert.ToInt32(Session["ind_val_id"]);
            int indicador_id = Convert.ToInt32(Session["indicador_id"].ToString());
            int meta = Convert.ToInt32(Session["meta"].ToString());
            DateTime fecha = Convert.ToDateTime(Session["fecha"].ToString());
            int ejecutado = Convert.ToInt32(Session["ejecutado"].ToString());

            if (objCActividades.UpdateMeta(meta_id, indicador_id, ejecutado, fecha))
            {
                Session.Remove("ind_val_id");
                Session.Remove("indicador_id");
                Session.Remove("indicador");
                Session.Remove("meta");
                Session.Remove("fecha");
                Session.Remove("ejecutado");
                Session.Remove("operacion");

            }
        }

        protected void AgregarIndicadores()
        {
            bool metaIsNumeric = true;
            int actividad = Convert.ToInt32(Session["actividad"].ToString());
            int verbo = Convert.ToInt32(Session["verbo"].ToString());

            for (int i = 0; i < Session["meta"].ToString().Length; i++)
            {
                bool IsNumber = char.IsNumber(Session["meta"].ToString(), i);
                if (!IsNumber)
                    metaIsNumeric = false;
            }

            int meta = 0;
            if (metaIsNumeric)
                meta = Convert.ToInt32(Session["meta"].ToString());

            int unidad = Convert.ToInt32(Session["unidad"].ToString());
            string medios = Session["medios"].ToString();
            string supuestos = Session["supuestos"].ToString();
            string descripcion = Session["descripcion"].ToString();
            bool ssp = false;

            if (Session["ssp"].ToString() == "Si")
                ssp = true;

            DateTime fechainicial = Convert.ToDateTime(Session["fechainicial"].ToString());
            DateTime fechafinal = Convert.ToDateTime(Session["fechafinal"].ToString());

            Model.ESMBDDataContext db = new ESMBDDataContext();

            string verbo_text = (from v in db.Verbos
                                 where v.Id == verbo
                                 select v.Verbo1).Single();

            string unidad_text = (from u in db.Unidades
                                  where u.Id == unidad
                                  select u.Unidad).Single();

            string indicador = "";

            if (Session["tiporedaccion"].ToString() == "entre")
            {
                indicador = verbo_text + " " + meta + " " + descripcion + " entre " + fechainicial.ToShortDateString() + " y " + fechafinal.ToShortDateString();
            }
            else if (Session["tiporedaccion"].ToString() == "hasta")
            {
                indicador = "A " + fechainicial.ToShortDateString() + " " + verbo_text + " " + meta + " " + descripcion;
            }

            if (objCActividades.AddIndicador(actividad, indicador, verbo, unidad, fechainicial, fechafinal, meta, ssp, medios, supuestos, descripcion))
            {
                Session.Remove("ind_id");
                Session.Remove("actividad");
                Session.Remove("verbo");
                Session.Remove("meta");
                Session.Remove("unidad");
                Session.Remove("ssp");
                Session.Remove("fechainicial");
                Session.Remove("fechafinal");
                Session.Remove("operacion");
                Session.Remove("medios");
                Session.Remove("supuestos");
                Session.Remove("descripcion");
                Session.Remove("tiporedaccion");
            }
        }

        protected void EditarIndicadores()
        {
            int ind_id = Convert.ToInt32(Session["ind_id"].ToString());
            int verbo = Convert.ToInt32(Session["verbo"].ToString());
            int meta = Convert.ToInt32(Session["meta"].ToString());
            int unidad = Convert.ToInt32(Session["unidad"].ToString());
            DateTime fechainicial = Convert.ToDateTime(Session["fechainicial"].ToString());
            DateTime fechafinal = Convert.ToDateTime(Session["fechafinal"].ToString());
            string medios = Session["medios"].ToString();
            string supuestos = Session["supuestos"].ToString();
            string descripcion = Session["descripcion"].ToString();

            bool ssp = false;

            if (Session["ssp"].ToString() == "Si")
                ssp = true;


            Model.ESMBDDataContext db = new ESMBDDataContext();

            string verbo_text = (from v in db.Verbos
                                 where v.Id == verbo
                                 select v.Verbo1).Single();

            string unidad_text = (from u in db.Unidades
                                  where u.Id == unidad
                                  select u.Unidad).Single();


            string indicador = "";

            if (Session["tiporedaccion"].ToString() == "entre")
            {
                indicador = verbo_text + " " + meta + " " + descripcion + " entre " + fechainicial + " y " + fechafinal;
            }
            else if (Session["tiporedaccion"].ToString() == "hasta")
            {
                indicador = "A " + fechainicial + " " + verbo_text + " " + meta + " " + descripcion;
            }

            if (objCActividades.UpdateIndicador(ind_id, indicador, verbo, unidad, fechainicial, fechafinal, meta, ssp, medios, supuestos, descripcion))
            {
                Session.Remove("ind_id");
                Session.Remove("actividad");
                Session.Remove("verbo");
                Session.Remove("meta");
                Session.Remove("unidad");
                Session.Remove("ssp");
                Session.Remove("fechainicial");
                Session.Remove("fechafinal");
                Session.Remove("operacion");
                Session.Remove("medios");
                Session.Remove("supuestos");
                Session.Remove("descripcion");
                Session.Remove("tiporedaccion");
            }
        }

        protected void AgregarSubprocesos()
        {
            int proceso = Convert.ToInt32(Session["proceso"].ToString());
            string subproceso = Session["subproceso"].ToString();
            string indicador = Session["indicador"].ToString();
            string medios = Session["medios"].ToString();
            string supuestos = Session["supuestos"].ToString();

            if (objSubprocesos.Add(proceso, subproceso, indicador, medios, supuestos))
            {
                Session.Remove("sub_id");
                Session.Remove("proceso");
                Session.Remove("subproceso");
                Session.Remove("indicador");
                Session.Remove("medios");
                Session.Remove("supuestos");
                Session.Remove("operacion");
            }
        }

        protected void AgregarActividad()
        {
            int subproceso = Convert.ToInt32(Session["subproceso"].ToString());
            string actividad = Session["actividad"].ToString();
            string fecha = Session["fecha"].ToString();
            decimal presupuesto_decimal = Session["presupuesto"].ToString() != "" ? Convert.ToDecimal(Session["presupuesto"].ToString()) : 0;
            float presupuesto = Convert.ToInt64(presupuesto_decimal);

            if (objCActividades.Add(subproceso, actividad, presupuesto, fecha))
            {
                Session.Remove("act_id");
                Session.Remove("actividad");
                Session.Remove("subproceso");
                Session.Remove("fecha");
                Session.Remove("presupuesto");
                Session.Remove("operacion");
            }
        }

        protected void AgregarMatrizActores()
        {
            string group = Session["grupos"].ToString();
            string interes = Session["interes"].ToString();
            string problemarecibido = Session["problemarecibido"].ToString();
            string recursosymandatos = Session["recursosymandatos"].ToString();

            if (objCMatriz.AddItem(group, interes, problemarecibido, recursosymandatos, proyecto_id))
            {
                Session.Remove("grupos");
                Session.Remove("interes");
                Session.Remove("problemarecibido");
                Session.Remove("recursosymandatos");
                Session.Remove("operacion");
            }
        }

        protected void AgregarFuentesFinanciacion()
        {
            string tipo_entidad = Session["tipo_entidad"].ToString();
            string entidad = Session["entidadd"].ToString();
            string tipo_recurso = Session["tiporecurso"].ToString();
            if (objCFuentes_Financiacion.AddItem(entidad, tipo_entidad, tipo_recurso, proyecto_id))
            {
                Session.Remove("tipo_entidad");
                Session.Remove("entidadd");
                Session.Remove("tiporecurso");
                Session.Remove("operacion");
            }
        }

        protected void AgregarCausasEfectos()
        {
            try
            {
                string causa = null;
                string efecto = null;
                string causaIndirecta = null;
                string efectoIndirecto = null;
                string objetivo = null;
                string beneficio = null;

                if (Session["causaindirecta"] != null)
                {
                    causa = Session["causa"].ToString();
                    efecto = Session["efecto"].ToString();
                    causaIndirecta = Session["causaindirecta"].ToString();
                    efectoIndirecto = Session["efectoindirecto"].ToString();
                }
                else if (Session["objetivo"] != null)
                {
                    objetivo = Session["objetivo"].ToString();
                    beneficio = Session["beneficio"].ToString();
                }
                if (objCCausas_Efecto.Add(efecto, causa, beneficio, proyecto_id, "#fff", causaIndirecta, efectoIndirecto, objetivo))
                {
                    Session.Remove("causa");
                    Session.Remove("efecto");
                    Session.Remove("beneficio");
                    Session.Remove("operacion");
                    Session.Remove("causaindirecta");
                    Session.Remove("efectoindirecto");
                    Session.Remove("objetivo");

                }
            }
            catch { }
        }

        protected void EditarActividades()
        {
            int actividad_id = Convert.ToInt32(Session["act_id"].ToString());
            string actividad = Session["actividad"].ToString();
            string fecha = Session["fecha"].ToString();
            decimal presupuesto_decimal = Convert.ToDecimal(Session["presupuesto"].ToString());
            float presupuesto = Convert.ToInt64(presupuesto_decimal);

            if (objCActividades.Update(actividad_id, actividad, presupuesto, fecha))
            {
                Session.Remove("act_id");
                Session.Remove("actividad");
                Session.Remove("subproceso");
                Session.Remove("fecha");
                Session.Remove("presupuesto");
                Session.Remove("operacion");
            }
        }

        protected void EditarSubprocesos()
        {
            int sub_id = Convert.ToInt32(Session["sub_id"].ToString());
            string subproceso = Session["subproceso"].ToString();
            string indicador = Session["indicador"].ToString();
            string medios = Session["medios"].ToString();
            string supuestos = Session["supuestos"].ToString();

            if (objSubprocesos.Update(sub_id, subproceso, indicador, medios, supuestos))
            {
                Session.Remove("sub_id");
                Session.Remove("proceso");
                Session.Remove("subproceso");
                Session.Remove("indicador");
                Session.Remove("medios");
                Session.Remove("supuestos");
                Session.Remove("operacion");
            }
        }

        protected void EditarCausasEfectos()
        {
            int c_e_id = Convert.ToInt32(Session["c_e_id"].ToString());
            string causa = null;
            string efecto = null;
            string causaIndirecta = null;
            string efectoIndirecto = null;
            string objetivo = null;
            string beneficio = null;

            if (Session["causaindirecta"] != null)
            {
                causa = Session["causa"].ToString();
                efecto = Session["efecto"].ToString();
                causaIndirecta = Session["causaindirecta"].ToString();
                efectoIndirecto = Session["efectoindirecto"].ToString();
            }
            else if (Session["objetivo"] != null)
            {
                objetivo = Session["objetivo"].ToString();
                beneficio = Session["beneficio"].ToString();
            }

            if (objCCausas_Efecto.Update(c_e_id, causa, efecto, objetivo, beneficio, causaIndirecta, efectoIndirecto, objetivo))
            {
                Session.Remove("c_e_id");
                Session.Remove("causa");
                Session.Remove("efecto");
                Session.Remove("beneficio");
                Session.Remove("operacion");
                Session.Remove("causaindirecta");
                Session.Remove("efectoindirecto");
                Session.Remove("objetivo");
            }
        }

        protected void EditarFuentesFinanciacion()
        {
            int ff_id = Convert.ToInt32(Session["f_f_id"].ToString());
            string tipo_entidad = Session["tipo_entidad"].ToString();
            string entidad = Session["entidadd"].ToString();
            string tipo_recurso = Session["tiporecurso"].ToString();

            if (objCFuentes_Financiacion.UpdateItem(ff_id, entidad, tipo_entidad, tipo_recurso, proyecto_id))
            {
                Session.Remove("f_f_id");
                Session.Remove("tipo_entidad");
                Session.Remove("entidadd");
                Session.Remove("tiporecurso");
                Session.Remove("operacion");
            }
        }

        protected void EditarMatriz()
        {
            int id = Convert.ToInt32(Session["id"].ToString());
            string group = Session["grupos"].ToString();
            string interes = Session["interes"].ToString();
            string problemarecibido = Session["problemarecibido"].ToString();
            string recursosymandatos = Session["recursosymandatos"].ToString();

            if (objCMatriz.UpdateItem(id, group, interes, problemarecibido, recursosymandatos))
            {
                Session.Remove("grupos");
                Session.Remove("interes");
                Session.Remove("problemarecibido");
                Session.Remove("recursosymandatos");
                Session.Remove("operacion");
            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        protected void CargarFile()
        {
            try
            {
                string file_path = Request.QueryString["path"].ToString();

                FileInfo objFileInfo = new FileInfo(file_path);

                string array_json = String.Format("{\"name\": \"{0}\",\"size\":\"{1}\"}", objFileInfo.Name, objFileInfo.Length);

                Response.Write(array_json);
            }
            catch (Exception) { Response.Write("null"); }
        }

        protected void generateMarcoLogico()
        {
            Model.ESMBDDataContext db = new Model.ESMBDDataContext();

            var procesos = from p in db.Causas_Efectos
                           where p.Proyecto_id == proyecto_id
                           select p;

            var proyecto_info = (from py in db.Proyectos
                                 where py.Id == proyecto_id
                                 select py).Single();

            string html = "<table border='1' cellspacing='0' style='border: 1px solid #000;'><caption>Marco Lógico</caption>";
            html += "<tr><td style='border: 1px solid #000;'>" + proyecto_info.Finalidad + "</td><td style='border: 1px solid #000;'></td><td style='border: 1px solid #000;'>INDICADOR</td><td style='border: 1px solid #000;'>MEDIOS DE VERIFICACIÓN</td><td style='border: 1px solid #000;'>SUPUESTOS</td></tr>";
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

                html += "<tr style='background: #" + color_cadena + "'><td style='border: 1px solid #000;'><b>PROCESO:</b></td><td style='border: 1px solid #000;'>" + procesos_item.Proceso + "</td><td style='border: 1px solid #000;'></td><td style='border: 1px solid #000;'></td><td style='border: 1px solid #000;'></td><tr>";
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

            Response.Write(html);
        }

        protected void deleteFile(int file_id)
        {
            try
            {
                var db = new ESM.Model.ESMBDDataContext();

                var proyectoFile = (from dp in db.Documentos_Proyectos
                                    where dp.Id == file_id
                                    select dp).Single();

                proyectoFile.Estado = false;

                db.SubmitChanges();

                Response.Write("Proceso Completado!");
            }
            catch (Exception)
            {
                Response.Write("Proceso sin éxito.");
            }
        }
    }
}