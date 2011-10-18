using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESM.Objetos;
using System.Text;
using System.Web.UI.HtmlControls;

namespace ESM
{
    public partial class ReportesMen : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.IsAuthenticated)
            {
                CRoles objCRoles = new CRoles();
                int idusuario = Convert.ToInt32(Session["idusuario"]);
                string rol = objCRoles.ObtenerRol(idusuario);
                if (rol == "Administrador" || rol == "MEN")
                {

                }
                else
                {
                    Response.Write("<script>alert('Acceso Denegado!');</script>");
                    Response.Redirect("Login.aspx");
                }

            }
            else
                Response.Redirect("/Login.aspx");
        }

        protected void lknAgendaSE_Click(object sender, EventArgs e)
        {
            gvAgendaSE.DataSource = CReportes.ReportAgendaSE(); ;
            gvAgendaSE.DataBind();

            GridView objgv = new GridView();
            objgv.DataSource = CReportes.ReportAgendaSE();
            objgv.DataBind();

            int total = 0;
            for (int i = 0; i < objgv.Rows.Count; i++)
            {
                total++;
            }

            decimal cantidadse = (from ses in new ESM.Model.ESMBDDataContext().Secretaria_Educacions
                                  select ses).Count();

            decimal result = Math.Round((total / cantidadse) * 100, 1);

            lbltotal.Text = "Total registros: " + total.ToString() + " de " + cantidadse + " = " + result.ToString() + "% -- ";

            Visualizacion(true, false, false, false);
        }

        protected void lknAgendaEE_Click(object sender, EventArgs e)
        {
            gvAgendaEE.DataSource = CReportes.ReportAgendaEE();
            gvAgendaEE.DataBind();

            GridView objgv = new GridView();
            objgv.DataSource = CReportes.ReportAgendaEE();
            objgv.DataBind();

            decimal total = 0;
            for (int i = 0; i < objgv.Rows.Count; i++)
            {
                total++;
            }
            decimal cantidadee = (from ees in new ESM.Model.ESMBDDataContext().Establecimiento_Educativos
                                  where ees.Estado == true
                                  select ees).Count();

            decimal result = Math.Round((total / cantidadee) * 100, 1);

            lbltotal.Text = "Total registros: " + total.ToString() + " de " + cantidadee + " = " + result.ToString() + "% -- ";

            Visualizacion(false, true, false, false);
        }

        protected void Visualizacion(bool AgendaSE, bool AgendaEE, bool DiligenSE, bool DiliEE)
        {
            gvAgendaSE.Visible = AgendaSE;
            gvAgendaEE.Visible = AgendaEE;
            gvdilise.Visible = DiligenSE;
            gvDiliEE.Visible = DiliEE;
        }

        protected void gvAgendaSE_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                this.gvAgendaSE.PageIndex = e.NewPageIndex;
                gvAgendaSE.DataSource = CReportes.ReportAgendaSE();
                gvAgendaSE.DataBind();
            }
            catch (Exception) { }

        }

        protected void gvAgendaEE_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                this.gvAgendaEE.PageIndex = e.NewPageIndex;
                gvAgendaEE.DataSource = CReportes.ReportAgendaEE();
                gvAgendaEE.DataBind();
            }
            catch (Exception) { }

        }

        protected bool Export(IQueryable datasource, string nombre)
        {
            try
            {
                StringBuilder objsb = new StringBuilder();
                System.IO.StringWriter sw = new System.IO.StringWriter(objsb);
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                Page pagina = new Page();
                var form = new HtmlForm();
                GridView objgv = new GridView();
                objgv.EnableViewState = false;
                objgv.AllowPaging = false;
                objgv.DataSource = datasource;
                objgv.DataBind();
                pagina.EnableEventValidation = false;
                pagina.DesignerInitialize();
                pagina.Controls.Add(form);
                form.Controls.Add(objgv);
                pagina.RenderControl(htw);
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + nombre + ".xls");
                Response.Charset = "UTF-8";
                Response.ContentEncoding = Encoding.Default;
                Response.Write(objsb.ToString());
                Response.End();

                return true;
            }
            catch (Exception) { return false; }

        }

        protected void Unnamed1_Click(object sender, EventArgs e)
        {
            if (gvAgendaEE.Visible)
                Export(CReportes.ReportAgendaEE(), "AgendaEE");
            else if (gvAgendaSE.Visible)
                Export(CReportes.ReportAgendaSE(), "AgendaSE");
            else if (gvdilise.Visible)
            {
                gvdilise.AllowPaging = false;
                ReportDiligenciamientoSE();
                Objetos.GridViewExportUtil.Export("Diligenciamiento SE.xls", gvdilise);

            }
            else if (gvDiliEE.Visible)
            {
                gvDiliEE.AllowPaging = false;
                ReportEE();
                Objetos.GridViewExportUtil.Export("Diligenciamiento EE.xls", gvDiliEE);

            }
        }

        protected void ReportDiligenciamientoSE()
        {
            try
            {
                IQueryable<ESM.Model.AsociadoActaVisitaSE> asistse = null;
                IQueryable<ESM.Model.AsignaDocumento> asigse = null;
                ESM.Model.ESMBDDataContext db = new Model.ESMBDDataContext();

                var ses = from s in db.Secretaria_Educacions
                          select new { s, s.IdSecretaria, s.Nombre, Consultor = s.Consultore.Nombre };

                gvdilise.DataSource = ses;
                gvdilise.DataBind();

                for (int i = 0; i < gvdilise.Rows.Count; i++)
                {
                    Label lblidse = (Label)gvdilise.Rows[i].FindControl("lblidse");
                    Label lblActaDocumento = (Label)gvdilise.Rows[i].FindControl("lblActaDocumento");
                    Label lblCantAsocioados = (Label)gvdilise.Rows[i].FindControl("lblCantAsocioados");
                    Label lblfechadili = (Label)gvdilise.Rows[i].FindControl("lblfechadili");
                    Label lblactaestado = (Label)gvdilise.Rows[i].FindControl("lblactaestado");
                    Label lbllcestado = (Label)gvdilise.Rows[i].FindControl("lbllcestado");

                    int idse = Convert.ToInt32(lblidse.Text);

                    #region Verificación de Parametros

                    ESM.Model.ActaVisitaSE ase = new ESM.Model.ActaVisitaSE();

                    try
                    {
                        ase = (from asec in db.ActaVisitaSEs
                               where asec.IdSE == idse
                               select asec).Single();
                    }
                    catch (Exception) { ase = null; }

                    if (ase != null)
                    {

                        lblfechadili.Text = ase.Medicione.FechaMedicion.ToShortDateString();

                        asigse = from asigsec in db.AsignaDocumentos
                                 where asigsec.IdMedicion == ase.IdMedicion
                                 select asigsec;

                        foreach (var itemasigse in asigse)
                        {
                            if (itemasigse.IdDocumento == 14)
                            {
                                lblActaDocumento.Text = "Si";
                                break;
                            }

                        }

                        int cantasis = (from aso in db.AsociadoActaVisitaSEs
                                        where aso.IdActaVisita == ase.IdActaVisita
                                        select aso).Count();

                        lblCantAsocioados.Text = cantasis.ToString();
                    }




                    #endregion

                    #region Validacion Lectura Contexto SE

                    ESM.Model.LecturaContextoSE objLecturaContextoSE = new Model.LecturaContextoSE();

                    try
                    {
                        objLecturaContextoSE = (from lcse in db.LecturaContextoSEs
                                                where lcse.IdSecretaria == idse
                                                select lcse).Single();
                    }
                    catch (Exception) { objLecturaContextoSE = null; }

                    bool estadolcse = true;

                    if (objLecturaContextoSE != null)
                    {
                        if (objLecturaContextoSE._2_1_.ToString().Trim().Length == 0)
                            estadolcse = false;

                        if ((bool)objLecturaContextoSE._2_2_)
                            if (objLecturaContextoSE._2_2_1_.ToString().Trim().Length == 0)
                                estadolcse = false;

                        if (objLecturaContextoSE._2_2_2_.ToString().Trim().Length == 0)
                            estadolcse = false;

                        if ((bool)objLecturaContextoSE._2_2_3_EE_)
                        {
                            if (objLecturaContextoSE._2_2_3_EE_Cant == 0)
                                estadolcse = false;
                        }
                        if ((bool)objLecturaContextoSE._2_2_3_EST_)
                        {
                            if (objLecturaContextoSE._2_2_3_EST_Cant == 0)
                                estadolcse = false;
                        }
                        if ((bool)objLecturaContextoSE._2_2_3_EDU)
                        {
                            if (objLecturaContextoSE._2_2_3_EDU_Cant == 0)
                                estadolcse = false;
                        }
                        if ((bool)objLecturaContextoSE._2_2_3_PAD_)
                        {
                            if (objLecturaContextoSE._2_2_3_PAD_Cant == 0)
                                estadolcse = false;
                        }
                        if (objLecturaContextoSE._2_2_3_OTR_1.ToString().Trim().Length != 0)
                        {
                            if (objLecturaContextoSE._2_2_3_OTR_1_Cant == 0)
                                estadolcse = false;
                        }
                        if (objLecturaContextoSE._2_2_3_OTR_2.ToString().Trim().Length != 0)
                        {
                            if (objLecturaContextoSE._2_2_3_OTR_2_Cant == 0)
                                estadolcse = false;
                        }

                        if (objLecturaContextoSE._2_2_3_OTR_3_.ToString().Trim().Length != 0)
                        {
                            if (objLecturaContextoSE._2_2_3_OTR_3_Cant == 0)
                                estadolcse = false;
                        }
                        if (objLecturaContextoSE._2_2_3_OTR_4_.ToString().Trim().Length != 0)
                        {
                            if (objLecturaContextoSE._2_2_3_OTR_4_Cant == 0)
                                estadolcse = false;
                        }
                        if (objLecturaContextoSE._2_2_3_OTR_5_.ToString().Trim().Length != 0)
                        {
                            if (objLecturaContextoSE._2_2_3_OTR_5_Cant == 0)
                                estadolcse = false;
                        }

                        if (objLecturaContextoSE._2_2_4_.ToString().Trim().Length == 0)
                            estadolcse = false;
                        if (objLecturaContextoSE._2_2_5_.ToString().Trim().Length == 0)
                            estadolcse = false;

                        if ((bool)objLecturaContextoSE._2_3_)
                            if (objLecturaContextoSE._2_3_1_.ToString().Trim().Length == 0)
                                estadolcse = false;

                        if (objLecturaContextoSE._3_1_.ToString().Trim().Length == 0)
                            estadolcse = false;
                        if (objLecturaContextoSE._3_2_.ToString().Trim().Length == 0)
                            estadolcse = false;
                        if (objLecturaContextoSE._3_3_.ToString().Trim().Length == 0)
                            estadolcse = false;
                        if (objLecturaContextoSE._3_4_.ToString().Trim().Length == 0)
                            estadolcse = false;
                        if (objLecturaContextoSE._3_5_.ToString().Trim().Length == 0)
                            estadolcse = false;
                        if (objLecturaContextoSE._3_6_.ToString().Trim().Length == 0)
                            estadolcse = false;

                        if ((bool)objLecturaContextoSE._4_1_)
                            if (objLecturaContextoSE._4_1_1_.ToString().Trim().Length == 0)
                                estadolcse = false;

                        if ((bool)objLecturaContextoSE._1_1_8_)
                            if (objLecturaContextoSE._1_1_9_.ToString().Trim().Length == 0)
                                estadolcse = false;

                        if (estadolcse)
                            lbllcestado.Text = "Diligenciada";
                        else if (!estadolcse)
                            lbllcestado.Text = "Parcial";
                    }

                    #endregion



                    if (lblfechadili.Text != "No Asignada" && Convert.ToInt32(lblCantAsocioados.Text) > 0 && lblActaDocumento.Text == "Si")
                        lblactaestado.Text = "Diligenciada";
                    else if (lblfechadili.Text != "No Asignada" && Convert.ToInt32(lblCantAsocioados.Text) >= 0)
                        lblactaestado.Text = "Parcial";
                    else
                        lblactaestado.Text = "Sin Diligenciar";

                }

            }
            catch (Exception) { }
        }

        protected void lknDiliSE_Click(object sender, EventArgs e)
        {
            ReportDiligenciamientoSE();
            Visualizacion(false, false, true, false);
            lbltotal.Visible = false;
        }

        protected void gvdilise_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                this.gvdilise.PageIndex = e.NewPageIndex;
                ReportDiligenciamientoSE();
            }
            catch (Exception) { }


        }

        protected void gvDiliEE_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                this.gvDiliEE.PageIndex = e.NewPageIndex;
                ReportEE();
            }
            catch (Exception) { }


        }

        protected void ReportEE()
        {
            try
            {
                ESM.Model.ESMBDDataContext db = new Model.ESMBDDataContext();

                var ee = from eec in db.Establecimiento_Educativos
                         select new
                         {
                             eec.IdIE,
                             eec.Secretaria_Educacion.Nombre,
                             Consultor = eec.Secretaria_Educacion.Consultore.Nombre,
                             eec.CodigoDane,
                             EENombre = eec.Nombre,
                             eec.Municipio

                         };

                gvDiliEE.DataSource = ee;
                gvDiliEE.DataBind();

                for (short j = 0; j < gvDiliEE.Rows.Count; j++)
                {

                    Label lblidie = (Label)gvDiliEE.Rows[j].FindControl("lblidie");
                    Label lblevalest = (Label)gvDiliEE.Rows[j].FindControl("lblevalest");
                    Label lblevalpad = (Label)gvDiliEE.Rows[j].FindControl("lblevalpad");
                    Label lblevalprof = (Label)gvDiliEE.Rows[j].FindControl("lblevalprof");
                    Label lblevaldir = (Label)gvDiliEE.Rows[j].FindControl("lblevaldir");
                    Label lblevaledu = (Label)gvDiliEE.Rows[j].FindControl("lblevaledu");

                    for (short i = 0; i < 6; i++)
                    {
                        try
                        {
                            string evalact = (from ev in db.Evaluacions
                                              where ev.IdActor == i + 1 && ev.IdIE == Convert.ToInt32(lblidie.Text)
                                              select ev.EstadoEvaluacion.Estado).Single();

                            switch (i + 1)
                            {
                                case 1:
                                    lblevalest.Text = evalact;
                                    break;
                                case 2:
                                    lblevalprof.Text = evalact;
                                    break;
                                case 3:
                                    lblevaledu.Text = evalact;
                                    break;
                                case 4:
                                    lblevalpad.Text = evalact;
                                    break;
                                case 6:
                                    lblevaldir.Text = evalact;
                                    break;

                            }
                        }
                        catch (Exception exx) { }


                    }
                }
            }
            catch (Exception ex) { }
        }

        protected void lknDiliEE_Click(object sender, EventArgs e)
        {
            ReportEE();
            Visualizacion(false, false, false, true);
        }
    }
}