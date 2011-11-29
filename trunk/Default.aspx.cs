using System;
using System.ComponentModel.DataAnnotations;
using System.Web.DynamicData;
using System.Linq;
using ESM.Objetos;
using System.Collections.Generic;
using System.Text;

namespace ESM
{
    public partial class _Default : System.Web.UI.Page
    {
        #region Propiedades Privadas Publicas
        int idusuario = 0;
        #endregion


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["idusuario"] != null)
            {
                idusuario = Convert.ToInt32(Session["idusuario"]);

                CRoles objCRoles = new CRoles();

                string rol = objCRoles.ObtenerRol(idusuario);

                if (rol == "Administrador")
                {
                    AdministracionConfiguracion.Visible = true;
                    AdministracionUsuario.Visible = true;
                    MEN.Visible = true;
                    ModEval.Visible = true;
                    ModMonitoreo.Visible = true;
                }
                else if (rol == "Consultor")
                {
                    var idc = objCRoles.IdConsultor;

                    Session.Add("identiConsultor", idc);
                    citas.HRef = String.Concat("/Citas.aspx?id=", idc);
                    AdministracionConfiguracion.Visible = true;
                    AdministracionUsuario.Visible = true;
                    AdministracionConfiguracion.Visible = false;
                    AdministracionUsuario.Visible = false;
                    MEN.Visible = false;
                    ModEval.Visible = true;
                }
                else if (rol == "MEN")
                {
                    ModEval.Visible = false;
                    AdministracionConfiguracion.Visible = false;
                    AdministracionUsuario.Visible = false;
                    AdministracionConfiguracion.Visible = false;
                    AdministracionUsuario.Visible = false;
                    MEN.Visible = true;
                    ModEval.Visible = true;
                }
            }

            if (Request.IsAuthenticated)
            {

                System.Collections.IList visibleTables = Global.DefaultModel.VisibleTables;
                if (visibleTables.Count == 0)
                {
                    throw new InvalidOperationException("No hay tablas accesibles. Asegúrese de que hay al menos un modelo de datos registrado en Global.asax y de que está habilitada la técnica scaffolding, o bien implemente páginas personalizadas.");
                }
                Menu1.DataSource = visibleTables;
                Menu1.DataBind();

                CActividades objCActividades = new CActividades();

                List<object[,]> objlist = objCActividades.getIndicadoresVencidos(idusuario);
                List<object[,]> objlist_p = objCActividades.getIndicadoresVencidosPorVencer(idusuario);
                StringBuilder objStringBuilder = new StringBuilder();

                #region Por Vencer

                if (objlist_p != null)
                {
                    objStringBuilder.Append("<table id='coleccion_notificaciones_p_vencer' class='notificaciones_p'> ");

                    for (int i = 0; i < objlist_p.Count; i++)
                    {
                        objStringBuilder.Append("<tr> ");
                        objStringBuilder.Append("<th colspan='4' style='border: dashed 1px #dd9e16; color: #353535;'>Indicadores por Vencer</th>");
                        objStringBuilder.Append("</tr> ");
                        objStringBuilder.Append("<tr> ");
                        object[,] indicadores_vencidos = objlist_p[i];
                        objStringBuilder.Append("<th>Problema Central</th> ");
                        objStringBuilder.Append("<th>Actividad</th> ");
                        objStringBuilder.Append("<th>Indicador</th> ");
                        objStringBuilder.Append("<th>Fecha</th> ");
                        objStringBuilder.Append("</tr> ");
                        for (int j = 0; j < indicadores_vencidos.GetLength(0); j++)
                        {
                            objStringBuilder.Append("<tr> ");
                            objStringBuilder.Append("<td> ");
                            objStringBuilder.Append(indicadores_vencidos[j, 0].ToString());
                            objStringBuilder.Append("</td> ");
                            objStringBuilder.Append("<td> ");
                            objStringBuilder.Append(indicadores_vencidos[j, 1].ToString());
                            objStringBuilder.Append("</td> ");
                            objStringBuilder.Append("<td> ");
                            objStringBuilder.Append(indicadores_vencidos[j, 2].ToString());
                            objStringBuilder.Append("</td> ");
                            objStringBuilder.Append("<td> ");
                            objStringBuilder.Append(indicadores_vencidos[j, 3].ToString());
                            objStringBuilder.Append("</td> ");
                            objStringBuilder.Append("</tr>");
                        }

                    }

                    objStringBuilder.Append("</table> ");

                    t_notificaciones.InnerHtml = objStringBuilder.ToString();
                }

                #endregion

                #region Vencidos
                if (objlist != null)
                {
                    objStringBuilder.Append("<br/> <br/> <table id='coleccion_notificaciones' class='notificaciones'> ");

                    for (int i = 0; i < objlist.Count; i++)
                    {
                        objStringBuilder.Append("<tr> ");
                        objStringBuilder.Append("<th colspan='4' style='border: dashed 1px #9f0606; color: #353535;'>Indicadores Vencidos</th>");
                        objStringBuilder.Append("</tr> ");
                        objStringBuilder.Append("<tr> ");
                        object[,] indicadores_vencidos = objlist[i];
                        objStringBuilder.Append("<th>Problema Central</th> ");
                        objStringBuilder.Append("<th>Actividad</th> ");
                        objStringBuilder.Append("<th>Indicador</th> ");
                        objStringBuilder.Append("<th>Fecha</th> ");
                        objStringBuilder.Append("</tr> ");
                        for (int j = 0; j < indicadores_vencidos.GetLength(0); j++)
                        {
                            objStringBuilder.Append("<tr> ");
                            objStringBuilder.Append("<td> ");
                            objStringBuilder.Append(indicadores_vencidos[j, 0].ToString());
                            objStringBuilder.Append("</td> ");
                            objStringBuilder.Append("<td> ");
                            objStringBuilder.Append(indicadores_vencidos[j, 1].ToString());
                            objStringBuilder.Append("</td> ");
                            objStringBuilder.Append("<td> ");
                            objStringBuilder.Append(indicadores_vencidos[j, 2].ToString());
                            objStringBuilder.Append("</td> ");
                            objStringBuilder.Append("<td> ");
                            objStringBuilder.Append(indicadores_vencidos[j, 3].ToString());
                            objStringBuilder.Append("</td> ");
                            objStringBuilder.Append("</tr>");
                        }

                    }

                    objStringBuilder.Append("</table> ");

                    t_notificaciones.InnerHtml = objStringBuilder.ToString();
                }
                #endregion

            }
            else
                Response.Redirect("/Login.aspx");

            ObtenerTema(Menu1);

        }

        protected void ObtenerTema(System.Web.UI.WebControls.GridView objGridView)
        {
            if (objGridView.Rows.Count != 0)
            {
                objGridView.HeaderStyle.CssClass = "trheader";

                int color = 0;
                for (int i = 0; i < objGridView.Rows.Count; i++)
                {
                    if (color == 0)
                    {
                        objGridView.Rows[i].CssClass = "trgris";
                        color = 1;
                    }
                    else if (color == 1)
                    {
                        objGridView.Rows[i].CssClass = "trblanca";
                        color = 0;
                    }
                }
            }

        }

    }
}
