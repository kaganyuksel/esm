﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESM.Model;

namespace ESM.Preguntas
{
    public partial class AyudaPreguntas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.IsAuthenticated)
            {
                if (!Page.IsPostBack)
                {

                    #region Cargar Treeview
                    var trvambi = from am in new ESM.Model.ESMBDDataContext().Ambientes
                                  select am;
                    int contador = 0;
                    foreach (var item in trvambi)
                    {

                        tvayuda.Nodes[0].ChildNodes.Add(new TreeNode(item.Ambiente, item.IdAmbiente.ToString()));

                        var proces = from pro in new ESM.Model.ESMBDDataContext().Procesos
                                     where pro.IdAmbiente == item.IdAmbiente
                                     select pro;

                        foreach (var proc in proces)
                        {
                            tvayuda.Nodes[0].ChildNodes[contador].ChildNodes.Add(new TreeNode(proc.Proceso, proc.IdProceso.ToString()));
                            tvayuda.Nodes[0].ChildNodes[contador].Expanded = false;
                            var com = from comp in new ESM.Model.ESMBDDataContext().Componentes
                                      where comp.IdProceso == proc.IdProceso
                                      select comp;

                            foreach (var compo in com)
                            {
                                if (tvayuda.Nodes[0].ChildNodes[0].ChildNodes.Count > contador)
                                    tvayuda.Nodes[0].ChildNodes[0].ChildNodes[contador].ChildNodes.Add(new TreeNode(compo.Componente, compo.IdComponente.ToString()));

                            }


                        }

                        contador++;

                    }

                    #endregion

                }
                else
                {
                    for (int i = 0; i < tvayuda.Nodes[0].ChildNodes.Count; i++)
                    {
                        tvayuda.Nodes[0].ChildNodes[i].Expanded = false;
                    }

                }
            }
            else
                Response.Redirect("Login.aspx");
        }

        protected void tvayuda_SelectedNodeChanged(object sender, EventArgs e)
        {
            try
            {
                var com = from comp in new ESM.Model.ESMBDDataContext().Componentes
                          where comp.IdComponente == Convert.ToInt32(tvayuda.SelectedNode.Value)
                          select comp;
                foreach (var item in com)
                {


                    if (item.Componente == tvayuda.SelectedNode.Text)
                    {
                        var pregun = from pre in new ESM.Model.ESMBDDataContext().Preguntas
                                     where pre.IdComponente == Convert.ToInt32(tvayuda.SelectedNode.Value)
                                     select pre;
                        if (pregun.Count() != 0)
                        {
                            gvPreguntas.DataSource = pregun;
                            gvPreguntas.DataBind();
                            lblcomponente.Text = item.Componente;
                            ObtenerTema(gvPreguntas);

                            for (int i = 0; i < gvPreguntas.Rows.Count; i++)
                            {
                                var aybpre = from ayuda in new ESM.Model.ESMBDDataContext().AyudaByPregunta
                                             select ayuda;

                                foreach (var abp in aybpre)
                                {
                                    GridViewRow objRow = gvPreguntas.Rows[i];

                                    Label idpregunta = (Label)objRow.FindControl("lblIdPregunta");
                                    TextBox txtPregunta = (TextBox)objRow.FindControl("txtPregunta");
                                    CheckBoxList clist = (CheckBoxList)objRow.FindControl("listDocuments");
                                    TextBox txtDescripcion = (TextBox)objRow.FindControl("txtDescPre");
                                    if (abp.IdPregunta == Convert.ToInt32(idpregunta.Text))
                                    {
                                        txtDescripcion.Text = abp.Descripcion;

                                        {
                                            var q = clist;

                                            if ((bool)abp.PEI)
                                                q.Items[0].Selected = true;
                                            if ((bool)abp.PMI)
                                                q.Items[1].Selected = true;
                                            if ((bool)abp.Manual_de_Convivencia)
                                                q.Items[2].Selected = true;
                                            if ((bool)abp.Planes_de_Estudio)
                                                q.Items[3].Selected = true;
                                        }
                                    }
                                }


                                btndescPreguntas.Visible = true;
                                titulo1.Visible = true;

                            }

                        }
                        else
                        {
                            Response.Write("<script type='text/javascript'>alert('No se encontraron resultados relacionados al componente seleccionado.');</script>");
                        }
                    }
                }
            }
            catch (Exception) { Response.Write("<script type='text/javascript'>alert('No se encontraron resultados relacionados al componente seleccionado.');</script>"); }
        }


        //protected void CreateMessageAlert(string strMessage)
        //{
        //    Guid guidKey = Guid.NewGuid();
        //    this.Page pg = HttpContext.Current.CurrentHandler;
        //    string strScript = "alert('" + strMessage + "');";
        //    this.ClientScript.RegisterStartupScript(pg.GetType(), guidKey.ToString(), strScript, true);
        //}
        protected void ObtenerTema(GridView objGridView)
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

        protected void btndescPreguntas_Click(object sender, EventArgs e)
        {
            try
            {


                for (int i = 0; i < gvPreguntas.Rows.Count; i++)
                {
                    bool pei = false;
                    bool pmi = false;
                    bool manual = false;
                    bool planes = false;

                    GridViewRow objRow = gvPreguntas.Rows[i];

                    Label idpregunta = (Label)objRow.FindControl("lblIdPregunta");
                    TextBox txtPregunta = (TextBox)objRow.FindControl("txtPregunta");
                    CheckBoxList clist = (CheckBoxList)objRow.FindControl("listDocuments");
                    TextBox txtDescripcion = (TextBox)objRow.FindControl("txtDescPre");


                    if (clist.Items[0].Selected)
                        pei = true;
                    if (clist.Items[1].Selected)
                        pmi = true;
                    if (clist.Items[2].Selected)
                        manual = true;
                    if (clist.Items[3].Selected)
                        planes = true;






                    ESM.Model.ESMBDDataContext db = new Model.ESMBDDataContext();

                    var caybp = (from ayu in db.AyudaByPregunta
                                 where ayu.IdPregunta == Convert.ToInt32(idpregunta.Text)
                                 select ayu).Count();
                    if (caybp == 0)
                    {

                        AyudaByPregunta objAyudaPreguntas = new AyudaByPregunta
                        {
                            IdPregunta = Convert.ToInt32(idpregunta.Text),
                            Descripcion = txtDescripcion.Text,
                            PEI = pei,
                            PMI = pmi,
                            Manual_de_Convivencia = manual,
                            Planes_de_Estudio = planes

                        };
                        db.AyudaByPregunta.InsertOnSubmit(objAyudaPreguntas);
                        db.SubmitChanges();


                    }

                    else
                    {
                        var aybp = (from ayu in db.AyudaByPregunta
                                    where ayu.IdPregunta == Convert.ToInt32(idpregunta.Text)
                                    select ayu).Single();

                        aybp.Descripcion = txtDescripcion.Text;
                        aybp.PEI = pei;
                        aybp.PMI = pmi;
                        aybp.Manual_de_Convivencia = manual;
                        aybp.Planes_de_Estudio = planes;

                        db.SubmitChanges();

                        var pre = (from pregun in db.Preguntas
                                   where pregun.IdPregunta == Convert.ToInt32(idpregunta.Text)
                                   select pregun).Single();

                        pre.Pregunta = txtPregunta.Text;

                        db.SubmitChanges();
                    }


                }
                Response.Write("<script>alert('La Actualizacion se realizo correctamente.');</script>");
                //Alert.CreateMessageAlertInUpdatePanel(UpdatePanel1, "La actualización se realizo correctamente");
                //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "scriptok", "alert('Actualizado Correctamente')", true);
            }
            catch (Exception)
            {
                //Alert.CreateMessageAlertInUpdatePanel(UpdatePanel1, "Se produjo un error inesperado.");
            }
        }

        protected void gvPreguntas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                switch (e.Row.RowType)
                {
                    case DataControlRowType.DataRow:
                        Label lblidpregunta = (Label)e.Row.FindControl("lblIdPregunta");
                        TextBox txtPregunta = (TextBox)e.Row.FindControl("txtPregunta");
                        txtPregunta.Enabled = false;
                        txtPregunta.CssClass = String.Format("txtmiltilinepregunta text_{0}", lblidpregunta.Text);

                        break;
                }
            }
            catch (Exception)
            {

            }
        }
    }
}