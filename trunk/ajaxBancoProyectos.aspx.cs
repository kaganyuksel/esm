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

                        if (Request.QueryString["modulo"].ToString() == "fuentes_financiacion")
                            CargarFuentesFinanciacion();
                        else if (Request.QueryString["modulo"].ToString() == "identificacion")
                            CargarIdentificacion();

                    }

                    if (Request.QueryString["actualizarproyecto"] != null && Convert.ToBoolean(Request.QueryString["actualizarproyecto"]))
                    {
                        proyecto_id = Convert.ToInt32(Request.QueryString["proyecto_id"]);
                        ActualziarProyecto(proyecto_id);
                    }
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
                }
            }
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