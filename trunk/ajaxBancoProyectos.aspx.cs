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
            if (Request.QueryString["proyecto_id"] != null && Request.QueryString["proyecto_id"] != "undefined" && Request.QueryString["proyecto_id"] != " " && Request.QueryString["proyecto_id"] != "")
                proyecto_id = Convert.ToInt32(Request.QueryString["proyecto_id"]);

            if (!Page.IsPostBack)
            {
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
                        else if (Request.QueryString["tabla".ToString()] == "act" && proyecto_id != 0)
                        {
                            string operacion = Session["operacion"] == null ? "" : Session["operacion"].ToString();
                            if (operacion == "add")
                                AgregarActividad();
                            else if (operacion == "edit")
                                EditarActividades();
                        }
                        else if (Request.QueryString["tabla".ToString()] == "ind" && proyecto_id != 0)
                        {
                            string operacion = Session["operacion"] == null ? "" : Session["operacion"].ToString();
                            if (operacion == "add")
                                AgregarIndicadores();
                            else if (operacion == "edit")
                                EditarIndicadores();
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
                    }
                    else if (Request.QueryString["actualizarproyecto"] != null && Convert.ToBoolean(Request.QueryString["actualizarproyecto"]))
                        ActualziarProyecto(proyecto_id);
                    else if (Request.QueryString["actualizararbolproblemas"] != null && Convert.ToBoolean(Request.QueryString["actualizararbolproblemas"]))
                        UpdateHTMLArbolProblemas();
                    else if (Request.QueryString["actualizararbolobjetivos"] != null && Convert.ToBoolean(Request.QueryString["actualizararbolobjetivos"]))
                        UpdateHTMLArbolObjetivos();

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
                    else if (Request.Form["causa"] != null)
                    {
                        Session.Add("c_e_id", Request.Form["id"]);
                        Session.Add("causa", Request.Form["causa"]);
                        Session.Add("efecto", Request.Form["efecto"]);
                        Session.Add("beneficio", Request.Form["beneficio"]);
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
                var features_project = _objCproyecto.GetProyecto(proyecto_id);
                var coleccion_causas_efectos = objCCausas_Efecto.getCausas_Efectos(proyecto_id);

                string html = "<li>" + features_project.Problema.Substring(0,5) + "<ul>";
                string causas_html = "<li>Causas<ul>";
                string efectos_html = "<li>Efectos<ul>";

                foreach (var item in coleccion_causas_efectos)
                {
                    causas_html = causas_html + "<li>" + item.Causa.Substring(0,6) + "</li>";
                    efectos_html = efectos_html + "<li>" + item.Efecto.Substring(0,6) + "</li>";
                }
                causas_html = causas_html + "</ul></li>";
                efectos_html = efectos_html + "</ul></li>";

                html = html + causas_html + efectos_html + "</ul></li>";

                Response.Write(html);
            }
            catch (Exception) { Response.Write("null"); }

        }

        protected void UpdateHTMLArbolObjetivos()
        {
            try
            {
                var features_project = _objCproyecto.GetProyecto(proyecto_id);
                var coleccion_causas_efectos = objCCausas_Efecto.getCausas_Efectos(proyecto_id);

                string html = "<li>" + features_project.Problema.Substring(0,5) + "<ul>";
                string beneficios_html = "<li>Beneficios<ul>";
                string objetivos_html = "<li>Objetivos<ul>";

                foreach (var item in coleccion_causas_efectos.Take(3))
                {
                    string beneficio = item.Beneficios == null ? "No Asignado" : item.Beneficios.Substring(0, 6);
                    beneficios_html = beneficios_html + "<li>" + beneficio + "</li>";
                    objetivos_html = objetivos_html + "<li>" + item.Causa.Substring(0,6) + "</li>";
                }
                beneficios_html = beneficios_html + "</ul></li>";
                objetivos_html = objetivos_html + "</ul></li>";

                html = html + beneficios_html + objetivos_html + "</ul></li>";

                Response.Write(html);
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
                json_to_return = json_to_return + "{\"id\":\"" + item.Id.ToString() + "\",\"cell\":[\"" + item.Id + "\",\"" + item.Causa + "\", \"" + item.Efecto + "\", \"" + item.Beneficios + "\"]} ,";
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
            IQueryable<Actividade> actividades_col = new CActividades().getActividadesProyecto(proyecto_id);

            string json_to_return = "{\"page\":\"1\",\"total\":1,\"records\":\"1\",\"rows\": [";

            foreach (var item in actividades_col)
            {
                json_to_return = json_to_return + "{\"id\":\"" + item.Id.ToString() + "\",\"cell\":[\"" + item.Id + "\",\"" + item.Subproceso.Subproceso1 + "\", \"" + item.Actividad + "\", \"" + item.fecha + "\",\"" + item.Presupuesto + "\"]} ,";
            }
            json_to_return = json_to_return.Trim(',');
            json_to_return = json_to_return + "]}";

            Response.Write(json_to_return);

        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        protected void CargarIndicadores()
        {

            ESMBDDataContext db = new Model.ESMBDDataContext();

            var indicadores_col = from ind in db.Indicadores
                                  where ind.Actividade.Subproceso.Causas_Efecto.Proyecto_id == proyecto_id
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

        protected void AgregarIndicadores()
        {
            int actividad = Convert.ToInt32(Session["actividad"].ToString());
            int verbo = Convert.ToInt32(Session["verbo"].ToString());
            int meta = Convert.ToInt32(Session["meta"].ToString());
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

            var verbo_text = from v in db.Verbos
                             where v.Id == verbo
                             select v.Verbo1;

            var unidad_text = from u in db.Unidades
                              where u.Id == unidad
                              select u.Unidad;

            string indicador = "";

            if (Session["tiporedaccion"].ToString() == "entre")
            {
                indicador = verbo_text + " " + meta + " " + descripcion + " entre " + fechainicial + " y " + fechafinal;
            }
            else if (Session["tiporedaccion"].ToString() == "hasta")
            {
                indicador = "A " + fechainicial + " " + verbo_text + " " + meta + " " + descripcion;
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
            float presupuesto = Convert.ToInt32(Session["presupuesto"].ToString());

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
            string causa = Session["causa"].ToString();
            string efecto = Session["efecto"].ToString();
            string beneficio = Session["beneficio"].ToString();

            if (objCCausas_Efecto.Add(efecto, causa, beneficio, proyecto_id, "fff"))
            {
                Session.Remove("causa");
                Session.Remove("efecto");
                Session.Remove("beneficio");
                Session.Remove("operacion");
            }
        }

        protected void EditarActividades()
        {
            int actividad_id = Convert.ToInt32(Session["act_id"].ToString());
            string actividad = Session["actividad"].ToString();
            string fecha = Session["fecha"].ToString();
            float presupuesto = Convert.ToInt32(Session["presupuesto"].ToString());

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
            string causa = Session["causa"].ToString();
            string efecto = Session["efecto"].ToString();
            string beneficio = Session["beneficio"].ToString();

            if (objCCausas_Efecto.Update(c_e_id, causa, efecto, beneficio))
            {
                Session.Remove("c_e_id");
                Session.Remove("causa");
                Session.Remove("efecto");
                Session.Remove("beneficio");
                Session.Remove("operacion");
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

    }
}