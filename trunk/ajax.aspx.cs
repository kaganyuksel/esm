using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESM.Objetos;
using EvaluationSettings;
using System.Text;

namespace ESM
{
    public partial class ajax : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Response.Clear();
                #region Ajax

                if (Request.QueryString["serial"] != null && Request.QueryString["serial"] == "ok")
                {
                    bool estado_evaluacion = Convert.ToBoolean(Request.QueryString["estado"]);
                    int actor_id = Convert.ToInt32(Request.QueryString["actor_id"]);

                    CEvaluacion objCEvaluacion = new CEvaluacion();

                    #region Seleccion de Actor
                    switch (actor_id)
                    {
                        //Estudiante
                        case 1:
                            objCEvaluacion.Estudiantes = true;
                            break;
                        //Profesional
                        case 2:
                            objCEvaluacion.Profesional = true;
                            break;
                        //Educador
                        case 3:
                            objCEvaluacion.Docentes = true;
                            break;
                        //Padre de Familia
                        case 4:
                            objCEvaluacion.Padres = true;
                            break;
                        //Directivos
                        case 6:
                            objCEvaluacion.Directivos = true;
                            break;
                    }
                    #endregion

                    List<IQueryable<ESM.Model.Pregunta>> cpreguntas = new List<IQueryable<Model.Pregunta>>();
                    cpreguntas = objCEvaluacion.LoadEvaluation();

                    int total_preguntas = 0;

                    for (int p = 0; p < cpreguntas.Count; p++)
                    {
                        if (cpreguntas[p] != null)
                            total_preguntas = total_preguntas + cpreguntas[p].Count();
                    }

                    object[,] CollectionResultados = new object[total_preguntas, 4];

                    int ubicacion_pregunta = 0;

                    for (int i = 0; i < cpreguntas.Count; i++)
                    {
                        if (cpreguntas[i] != null)
                        {
                            #region Asignar Valores

                            foreach (var item in cpreguntas[i])
                            {
                                CollectionResultados[ubicacion_pregunta, 0] = item.IdPregunta;
                                if (Request.QueryString[item.IdPregunta.ToString()] != null)
                                {
                                    bool valor = Convert.ToBoolean(Request.QueryString[item.IdPregunta.ToString()]);

                                    CollectionResultados[ubicacion_pregunta, 1] = valor;

                                    if ((bool)item.Sesiones)
                                    {
                                        if (Request.QueryString["txtsesion" + item.IdPregunta] != null && Request.QueryString["txtsesion" + item.IdPregunta] != "")
                                        {
                                            int valor_sesion = Convert.ToInt32(Request.QueryString["txtsesion" + item.IdPregunta]);
                                            CollectionResultados[ubicacion_pregunta, 2] = valor_sesion;
                                        }
                                        else
                                            CollectionResultados[ubicacion_pregunta, 2] = null;

                                    }

                                    var aybypregunta = from ap in item.AyudaByPreguntas
                                                       where ap.Lectura == true && ap.Participacion == true && ap.IdPregunta == item.IdPregunta
                                                       select ap;

                                    if (aybypregunta.Count() != 0)
                                    {
                                        if (Request.QueryString["chxPendiente" + item.IdPregunta] != null)
                                        {
                                            CollectionResultados[ubicacion_pregunta, 3] = true;
                                        }
                                        else
                                            CollectionResultados[ubicacion_pregunta, 3] = false;
                                    }
                                    else
                                        CollectionResultados[ubicacion_pregunta, 3] = false;
                                }
                                else
                                {
                                    CollectionResultados[ubicacion_pregunta, 1] = null;
                                    CollectionResultados[ubicacion_pregunta, 2] = null;
                                    CollectionResultados[ubicacion_pregunta, 3] = false;
                                }

                                ubicacion_pregunta++;

                            }

                            #endregion
                        }
                    }
                    int idie = Convert.ToInt32(Session["idie"]);
                    int idmedicion = Convert.ToInt32(Session["idmedicion"]);
                    int idusuario = Convert.ToInt32(Session["idusuario"]);

                    bool almaceno = false;

                    if (Session["ideval"] == null)
                    {
                        almaceno = objCEvaluacion.Almacenar(CollectionResultados, idie, idmedicion, actor_id, idusuario, estado_evaluacion, 2);
                        Session.Add("ideval", objCEvaluacion.IdEvaluacion);
                    }
                    else
                    {
                        int ideval = Convert.ToInt32(Session["ideval"]);
                        almaceno = objCEvaluacion.ActualizarEvaluacion(CollectionResultados, ideval, estado_evaluacion);
                    }
                    if (almaceno && estado_evaluacion)
                        Response.Write("true,parcial");
                    else if (almaceno && !estado_evaluacion)
                        Response.Write("true,terminado");
                    else if (!almaceno)
                        Response.Write("false");
                }

                #endregion

                if (Request.QueryString["estrategias"] != null && Convert.ToBoolean(Request.QueryString["estrategias"]))
                    AlmacenarEstrategia();
                if (Request.QueryString["estrategia_up"] != null && Convert.ToBoolean(Request.QueryString["estrategia_up"]))
                    ActualizarEstrategia();
                else if (Request.QueryString["procesos"] != null && Convert.ToBoolean(Request.QueryString["procesos"]))
                    AlmacenarProceso();
                else if (Request.QueryString["subprocesos"] != null && Convert.ToBoolean(Request.QueryString["subprocesos"]))
                    AlmacenarSubProceso();
                else if (Request.QueryString["subprocesos_up"] != null && Convert.ToBoolean(Request.QueryString["subprocesos_up"]))
                    ActualizarSubproceso();
                else if (Request.QueryString["actividades"] != null && Convert.ToBoolean(Request.QueryString["actividades"]))
                    AlmacenarActividad();
                else if (Request.QueryString["actividadesu"] != null && Convert.ToBoolean(Request.QueryString["actividadesu"]))
                    ActualizarActividad();

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void ActualizarActividad()
        {
            int actividadid = Convert.ToInt32(Request.QueryString["idactividad"]);
            string actividad = Request.QueryString["actividad"].ToString();
            float presupuesto = Convert.ToInt64(Request.QueryString["presupuesto"]);

            CActividades objCActividades = new CActividades();

            objCActividades.Update(actividadid, actividad, presupuesto);

        }

        private void ActualizarSubproceso()
        {
            try
            {

                int subprocesoid = Convert.ToInt32(Request.QueryString["idsubproceso"]);
                string subproceso = Request.QueryString["subproceso"].ToString();

                CSubprocesos objCSubprocesos = new CSubprocesos();

                objCSubprocesos.Update(subprocesoid, subproceso);

                Response.Write("ok");
            }
            catch (Exception) { Response.Write("Transacción Falló"); }


        }

        private void ActualizarEstrategia()
        {
            try
            {

                int idestrategia = Convert.ToInt32(Request.QueryString["idestrategia"]);
                string estrategia = Request.QueryString["estrategia"].ToString();

                CResultados_proyecto objCResultados_proyecto = new CResultados_proyecto();

                objCResultados_proyecto.Update(idestrategia, estrategia);

                Response.Write("ok");
            }
            catch (Exception) { Response.Write("Transacción Falló"); }


        }



        protected void AlmacenarProceso()
        {
            try
            {
                int idResultado = Convert.ToInt32(Request.QueryString["idproceso"]);
                string causa = Request.QueryString["causa"].ToString();
                string resultado = Request.QueryString["proceso"].ToString();

                CEfectos objCEfectos = new CEfectos();

                objCEfectos.Update(idResultado, causa, resultado);
                Response.Write("ok");
            }
            catch (Exception) { Response.Write("Transacción Falló"); }

        }

        protected void AlmacenarSubProceso()
        {
            try
            {
                int idProceso = Convert.ToInt32(Request.QueryString["idproceso"]);
                string subproceso = Request.QueryString["subproceso"].ToString();

                CSubprocesos objCSubprocesos = new CSubprocesos();

                objCSubprocesos.Add(idProceso, subproceso);

                IQueryable<Model.Subproceso> objSubprocesos = objCSubprocesos.LoadSubprocesos(idProceso);
                StringBuilder objsb = new StringBuilder();
                int enumeracion_subprocesos = 1;
                objsb.Append("<table style='width:100%;'> ");
                foreach (var item_subroceso in objSubprocesos)
                {

                    objsb.Append("<tr>");
                    objsb.Append("<td> <label style='color:#000;'> Subproceso No. " + enumeracion_subprocesos.ToString() + "<label>");
                    objsb.Append(" </td> ");
                    objsb.Append("</tr> ");
                    objsb.Append("<tr>");
                    string parametros = "'" + item_subroceso.Id.ToString() + "','txt_area_subproceso_up_" + item_subroceso.Id + "'";
                    objsb.Append("<td> <textarea id='txt_area_subproceso_up_" + item_subroceso.Id + "' placeholder='Texto correspondiente a Subproceso'>" + item_subroceso.Subproceso1 + "</textarea>");
                    objsb.Append(" <input type='button' value='Actualizar Subproceso' onclick=\"ActualizarSubProceso(" + parametros + ");\" /> ");
                    objsb.Append("</td> ");
                    objsb.Append("</tr> ");


                    enumeracion_subprocesos++;
                }
                objsb.Append("</table><br/>");

                Response.Write(objsb.ToString());
            }
            catch (Exception) { Response.Write("Transacción Falló"); }

        }

        protected void AlmacenarEstrategia()
        {
            try
            {
                int idsubproceso = Convert.ToInt32(Request.QueryString["subproceso_id"]);
                string estrategia = Request.QueryString["estrategia"].ToString();

                CResultados_proyecto objCResultados = new CResultados_proyecto();

                objCResultados.Add(idsubproceso, estrategia);

                IQueryable<Model.Resultados_Proyecto> objEstrategias = new CResultados_proyecto().LoadResultados(idsubproceso);

                StringBuilder objsb = new StringBuilder();

                int enumeracion_Estrategias = 1;
                objsb.Append("<table style='width:100%;'> ");
                foreach (var item_estrategias in objEstrategias)
                {

                    objsb.Append("<tr>");
                    objsb.Append("<td> <label style='color:#000;'> Estrategia No. " + enumeracion_Estrategias.ToString() + "<label>");
                    objsb.Append(" </td> ");
                    objsb.Append("</tr> ");
                    objsb.Append("<tr>");
                    string parametros = "'" + item_estrategias.Id.ToString() + "','txt_area_estrategias_up_" + item_estrategias.Id + "'";
                    objsb.Append("<td> <textarea id='txt_area_estrategias_up_" + item_estrategias.Id + "' placeholder='Texto correspondiente a estrategia'>" + item_estrategias.Resultado + "</textarea>");
                    objsb.Append(" <input type='button' value='Actualizar Estrategia' onclick=\"ActualizarEstrategia(" + parametros + ");\" /> ");
                    objsb.Append(" <a title=\"Detalles para Resultado No." + enumeracion_Estrategias.ToString() + "\" class='pretty' href=\"" + Request.Url.Scheme + "://" + Request.Url.Authority + "/DetallesMarcoLogico.aspx?idResultado=" + item_estrategias.Id + "&iframe=true&amp;width=100%&amp;height=100%\"\"><img alt='Detalles' src='/Icons/details.png' width='24px' /></a>");
                    objsb.Append(" <a title=\"Cronograma Resultado No." + enumeracion_Estrategias.ToString() + "\" class='pretty' href=\"" + Request.Url.Scheme + "://" + Request.Url.Authority + "/DiagramaGant.aspx?idResultado=" + item_estrategias.Id + "&iframe=true&amp;width=100%&amp;height=100%\"\"><img alt='Cronograma' src='/Icons/Calender.png' width='24px' /></a>");
                    objsb.Append("</td> ");
                    objsb.Append("</tr> ");

                    enumeracion_Estrategias++;
                }
                objsb.Append("</table><br/>");

                Response.Write(objsb.ToString());
            }
            catch (Exception) { Response.Write("Transacción Falló"); }

        }

        protected void AlmacenarActividad()
        {
            try
            {

                int idResultado = Convert.ToInt32(Request.QueryString["idresultado"]);
                string actividad = Request.QueryString["actividad"].ToString();
                float presupuesto = Convert.ToInt64(Request.QueryString["presupuesto"]);

                CActividades objCActividades = new CActividades();

                objCActividades.Add(idResultado, actividad, presupuesto);

                IQueryable<Model.Actividade> objActividades = new CActividades().getActividades(idResultado);

                StringBuilder objsb_actividades = new StringBuilder();

                int enumeracion_Actividades = 1;
                objsb_actividades.Append("<table style='width:100%;'> ");
                foreach (var item_actividades in objActividades)
                {
                    objsb_actividades.Append("<tr>");
                    objsb_actividades.Append("<td> <label style='color:#000;'> Actividad No. " + enumeracion_Actividades.ToString() + "<label>");
                    objsb_actividades.Append(" </td> ");
                    objsb_actividades.Append("</tr> ");
                    objsb_actividades.Append("<tr>");
                    string parametros = "'" + item_actividades.Id.ToString() + "','txt_area_actividades_up_" + item_actividades.Id + "','txt_actividad_presupuesto_up" + idResultado + "'";
                    objsb_actividades.Append("<td> <textarea id='txt_area_actividades_up_" + item_actividades.Id + "' placeholder='Texto correspondiente a actividades'>" + item_actividades.Actividad + "</textarea><br/>Presupuesto<br/><input type='text' placeholder='Campo exclusivamente numerico' class='numerico' style='width:80%;' value=" + item_actividades.Presupuesto + "  id='txt_actividad_presupuesto_up" + idResultado + "'> <br/>");
                    objsb_actividades.Append(" <input type='button' value='Actualizar Actividad' onclick=\"ActualizarActividad(" + parametros + ");\" /> ");
                    objsb_actividades.Append(" <a title=\"Detalles para Actividad No." + enumeracion_Actividades.ToString() + "\" class='pretty' href=\"" + Request.Url.Scheme + "://" + Request.Url.Authority + "/DetallesMarcoLogico.aspx?idActividad=" + item_actividades.Id + "&iframe=true&amp;width=100%&amp;height=100%\"\"><img alt='Detalles' src='/Icons/details.png' width='24px' /></a>");
                    objsb_actividades.Append(" <a title=\"Cronograma Actividad No." + enumeracion_Actividades.ToString() + "\" class='pretty' href=\"" + Request.Url.Scheme + "://" + Request.Url.Authority + "/DiagramaGant.aspx?idActividad=" + item_actividades.Id + "&iframe=true&amp;width=100%&amp;height=100%\"\"><img alt='Cronograma' src='/Icons/Calender.png' width='24px' /></a>");
                    objsb_actividades.Append("</td> ");
                    objsb_actividades.Append("</tr> ");

                    enumeracion_Actividades++;
                }

                objsb_actividades.Append("</table>");

                Response.Write(objsb_actividades.ToString());

            }
            catch (Exception) { }

        }
    }
}