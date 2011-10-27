using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESM.Objetos;
using System.Text;
using System.Web.UI.HtmlControls;
using Excel = Microsoft.Office.Interop.Excel;
using System.Data;
using System.Threading;
using System.IO;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace ESM
{
    public partial class ReportesMen : System.Web.UI.Page
    {
        #region Propiedades Privadas y Publicas

        volatile bool ProcesoFinalizado = false;

        #endregion

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
            lbltotal.Visible = true;
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
            lbltotal.Visible = false;
            var objlist = CReportes.ReportAgendaEE();

            IQueryable agenda_ee = (IQueryable)objlist[0];
            int visitados = (int)objlist[1];
            int porvisitar = (int)objlist[2];

            gvAgendaEE.DataSource = agenda_ee;
            gvAgendaEE.DataBind();

            GridView objgv = new GridView();
            objgv.DataSource = agenda_ee;
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

            //lbltotal.Text = "Total registros: " + total.ToString() + " de " + cantidadee + " = " + result.ToString() + "% -- ";

            decimal porvisitart = cantidadee - visitados;
            decimal porvisitaree = cantidadee - visitados;
            lbltotalees.Text = cantidadee.ToString();
            lblvisitados.Text = visitados.ToString() + " - " + Math.Round((visitados / cantidadee) * 100, 1).ToString();
            lblporvisitar.Text = porvisitart.ToString() + " - " + Math.Round((porvisitaree / cantidadee) * 100, 1).ToString();
            lblporvisitaragendados.Text = porvisitar.ToString() + " - " + Math.Round(porvisitar / porvisitart * 100, 1);

            Visualizacion(false, true, false, false);
        }

        protected void Visualizacion(bool AgendaSE, bool AgendaEE, bool DiligenSE, bool DiliEE)
        {
            gvAgendaSE.Visible = AgendaSE;
            gvAgendaEE.Visible = AgendaEE;
            gvdilise.Visible = DiligenSE;
            gvDiliEE.Visible = DiliEE;
            divse.Visible = DiligenSE;
            pnlgveedili.Visible = DiliEE;
            infoadicionalAgendaEE.Visible = AgendaEE;
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
                System.Web.UI.Page pagina = new System.Web.UI.Page();
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

        protected bool Export(GridView objGridView, string nombre)
        {
            try
            {
                objGridView.Visible = true;
                StringBuilder objsb = new StringBuilder();
                System.IO.StringWriter sw = new System.IO.StringWriter(objsb);
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                System.Web.UI.Page pagina = new System.Web.UI.Page();
                var form = new HtmlForm();
                pagina.EnableEventValidation = false;
                pagina.DesignerInitialize();
                pagina.Controls.Add(form);
                form.Controls.Add(objGridView);
                pagina.RenderControl(htw);
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + nombre + ".xls");
                Response.Charset = "UTF-8";
                Response.ContentEncoding = Encoding.Default;
                Response.Write(objsb.ToString());
                Response.End();
                objGridView.Visible = false;
                return true;
            }
            catch (Exception) { return false; }

        }

        protected void ExportExcel_Click(object sender, EventArgs e)
        {
            if (Session["ExportResumen"] != null)
            {
                bool resumido = Convert.ToBoolean(Session["ExportResumen"]);
                StartExport(resumido);
            }
            else
                StartExport();
        }

        protected void StartExport(bool resumido = false)
        {
            if (gvAgendaEE.Visible)
            {
                var objlist = CReportes.ReportAgendaEE();
                Export((IQueryable)objlist[0], "AgendaEE");
            }
            else if (gvAgendaSE.Visible)
                Export(CReportes.ReportAgendaSE(), "AgendaSE");
            else if (gvdilise.Visible)
            {
                gvdilise.AllowPaging = false;
                ReportDiligenciamientoSE();
                Objetos.GridViewExportUtil.Export("Diligenciamiento SE.xls", gvdilise);

            }
            else if (gvDiliEE.Visible && !resumido)
                Export(gvcopyDiliEE, "Diligenciamiento EE.xls");

            else if (gvDiliEE.Visible && resumido)
            {
                DataTable dt = new DataTable();
                dt = ConvertToDataTable(gvcopyDiliEE);
                string filename = Server.MapPath("/Excel/Diligenciamiento_EE_v01.xlsx");
                ExportDataTable(dt, filename);
                Response.Redirect("/Excel/Diligenciamiento_EE_v01.xlsx");
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
            lbltotal.Visible = false;
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
                gvcopyDiliEE.DataSource = ee;
                gvcopyDiliEE.DataBind();


                for (short j = 0; j < gvDiliEE.Rows.Count; j++)
                {

                    Label lblidie = (Label)gvDiliEE.Rows[j].FindControl("lblidie");
                    Label lblevalest = (Label)gvDiliEE.Rows[j].FindControl("lblevalest");
                    Label lblevalpad = (Label)gvDiliEE.Rows[j].FindControl("lblevalpad");
                    Label lblevalprof = (Label)gvDiliEE.Rows[j].FindControl("lblevalprof");
                    Label lblevaldir = (Label)gvDiliEE.Rows[j].FindControl("lblevaldir");
                    Label lblevaledu = (Label)gvDiliEE.Rows[j].FindControl("lblevaledu");
                    Label lblcantdir = (Label)gvDiliEE.Rows[j].FindControl("lblcantdir");
                    Label lblcantest = (Label)gvDiliEE.Rows[j].FindControl("lblcantest");
                    Label lblcantpad = (Label)gvDiliEE.Rows[j].FindControl("lblcantpad");
                    Label lblcantedu = (Label)gvDiliEE.Rows[j].FindControl("lblcantedu");
                    Label lblcantpro = (Label)gvDiliEE.Rows[j].FindControl("lblcantpro");
                    Label lblpei = (Label)gvDiliEE.Rows[j].FindControl("lblpei");
                    Label lblpmi = (Label)gvDiliEE.Rows[j].FindControl("lblpmi");
                    Label lblmaco = (Label)gvDiliEE.Rows[j].FindControl("lblmaco");
                    Label lblplan = (Label)gvDiliEE.Rows[j].FindControl("lblplan");
                    Label lblproy = (Label)gvDiliEE.Rows[j].FindControl("lblproy");
                    Label lblotros = (Label)gvDiliEE.Rows[j].FindControl("lblotros");
                    Label lblactaeecargada = (Label)gvDiliEE.Rows[j].FindControl("lblactaeecargada");
                    Label lblestadoactaee = (Label)gvDiliEE.Rows[j].FindControl("lblestadoactaee");
                    Label lblcita = (Label)gvDiliEE.Rows[j].FindControl("lblcita");
                    Label lblobservaciones = (Label)gvDiliEE.Rows[j].FindControl("lblobservaciones");

                    Label lblidiec = (Label)gvcopyDiliEE.Rows[j].FindControl("lblidie");
                    Label lblevalestc = (Label)gvcopyDiliEE.Rows[j].FindControl("lblevalest");
                    Label lblevalpadc = (Label)gvcopyDiliEE.Rows[j].FindControl("lblevalpad");
                    Label lblevalprofc = (Label)gvcopyDiliEE.Rows[j].FindControl("lblevalprof");
                    Label lblevaldirc = (Label)gvcopyDiliEE.Rows[j].FindControl("lblevaldir");
                    Label lblevaleduc = (Label)gvcopyDiliEE.Rows[j].FindControl("lblevaledu");
                    Label lblcantdirc = (Label)gvcopyDiliEE.Rows[j].FindControl("lblcantdir");
                    Label lblcantestc = (Label)gvcopyDiliEE.Rows[j].FindControl("lblcantest");
                    Label lblcantpadc = (Label)gvcopyDiliEE.Rows[j].FindControl("lblcantpad");
                    Label lblcanteduc = (Label)gvcopyDiliEE.Rows[j].FindControl("lblcantedu");
                    Label lblcantproc = (Label)gvcopyDiliEE.Rows[j].FindControl("lblcantpro");
                    Label lblpeic = (Label)gvcopyDiliEE.Rows[j].FindControl("lblpei");
                    Label lblpmic = (Label)gvcopyDiliEE.Rows[j].FindControl("lblpmi");
                    Label lblmacoc = (Label)gvcopyDiliEE.Rows[j].FindControl("lblmaco");
                    Label lblplanc = (Label)gvcopyDiliEE.Rows[j].FindControl("lblplan");
                    Label lblproyc = (Label)gvcopyDiliEE.Rows[j].FindControl("lblproy");
                    Label lblotrosc = (Label)gvcopyDiliEE.Rows[j].FindControl("lblotros");
                    Label lblactaeecargadac = (Label)gvcopyDiliEE.Rows[j].FindControl("lblactaeecargada");
                    Label lblestadoactaeec = (Label)gvcopyDiliEE.Rows[j].FindControl("lblestadoactaee");
                    Label lblcitac = (Label)gvcopyDiliEE.Rows[j].FindControl("lblcita");
                    Label lblobservacionesc = (Label)gvcopyDiliEE.Rows[j].FindControl("lblobservaciones");

                    int idmedicion = 0;
                    for (short i = 0; i < 6; i++)
                    {
                        try
                        {
                            #region Seccion Consulta Acta

                            var acee = from aee in db.ActaVisitaEEs
                                       where aee.IdEE == Convert.ToInt32(lblidie.Text)
                                       select new { aee.AsociadosActaVisitaEEs, aee.Medicione.FechaMedicion };

                            #region Seccion Consulta Documentos

                            foreach (var item in acee)
                            {
                                foreach (var asistitem in item.AsociadosActaVisitaEEs)
                                {
                                    switch (asistitem.IdActor)
                                    {
                                        //Estudiante
                                        case 1:
                                            int cantest = (from c in item.AsociadosActaVisitaEEs
                                                           where c.IdActor == 1
                                                           select c).Count();

                                            lblcantest.Text = cantest.ToString();
                                            lblcantestc.Text = cantest.ToString();

                                            break;
                                        //Profesional
                                        case 2:
                                            int cantpro = (from c in item.AsociadosActaVisitaEEs
                                                           where c.IdActor == 2
                                                           select c).Count();

                                            lblcantpro.Text = cantpro.ToString();
                                            lblcantproc.Text = cantpro.ToString();
                                            break;
                                        //Educador
                                        case 3:
                                            int cantedu = (from c in item.AsociadosActaVisitaEEs
                                                           where c.IdActor == 3
                                                           select c).Count();

                                            lblcantedu.Text = cantedu.ToString();
                                            lblcanteduc.Text = cantedu.ToString();
                                            break;
                                        //Padre de Familia
                                        case 4:
                                            int cantpad = (from c in item.AsociadosActaVisitaEEs
                                                           where c.IdActor == 4
                                                           select c).Count();

                                            lblcantpad.Text = cantpad.ToString();
                                            lblcantpadc.Text = cantpad.ToString();
                                            break;
                                        //Directivos
                                        case 6:
                                            int cantdir = (from c in item.AsociadosActaVisitaEEs
                                                           where c.IdActor == 6
                                                           select c).Count();

                                            lblcantdir.Text = cantdir.ToString();
                                            lblcantdirc.Text = cantdir.ToString();
                                            break;
                                    }
                                }
                            }

                            #endregion

                            #endregion

                            #region Seccion Consulta Evaluacion
                            var coleval = (from ev in db.Evaluacions
                                           where ev.IdActor == i + 1 && ev.IdIE == Convert.ToInt32(lblidie.Text)
                                           select new { ev.IdMedicion, ev.EstadoEvaluacion.Estado }).Single();

                            string evalact = coleval.Estado;

                            idmedicion = (int)coleval.IdMedicion;

                            switch (i + 1)
                            {
                                case 1:
                                    lblevalest.Text = evalact;
                                    lblevalestc.Text = evalact;
                                    break;
                                case 2:
                                    lblevalprof.Text = evalact;
                                    lblevalprofc.Text = evalact;
                                    break;
                                case 3:
                                    lblevaledu.Text = evalact;
                                    lblevaleduc.Text = evalact;
                                    break;
                                case 4:
                                    lblevalpad.Text = evalact;
                                    lblevalpadc.Text = evalact;
                                    break;
                                case 6:
                                    lblevaldir.Text = evalact;
                                    lblevaldirc.Text = evalact;
                                    break;

                            }


                            #region Documentos Establecimiento Educativo

                            var docsaee = from deec in db.AsignaDocumentos
                                          where deec.IdMedicion == idmedicion
                                          select deec;

                            foreach (var docsitem in docsaee)
                            {
                                switch (docsitem.IdDocumento)
                                {
                                    //PEI
                                    case 1:
                                        lblpei.Text = "Diligenciado";
                                        lblpeic.Text = "Diligenciado";
                                        break;
                                    //PMI
                                    case 2:
                                        lblpmi.Text = "Diligenciado";
                                        lblpmic.Text = "Diligenciado";
                                        break;
                                    //Manual de Convivencia
                                    case 3:
                                        lblmaco.Text = "Diligenciado";
                                        lblmacoc.Text = "Diligenciado";
                                        break;
                                    //Plan de Estudios
                                    case 4:
                                        lblplan.Text = "Diligenciado";
                                        lblplanc.Text = "Diligenciado";
                                        break;
                                    //DPP
                                    case 9:
                                        lblproy.Text = "Diligenciado";
                                        lblproyc.Text = "Diligenciado";
                                        break;
                                    //Otros
                                    case 10:
                                        lblotros.Text = "Diligenciado";
                                        lblotrosc.Text = "Diligenciado";
                                        break;
                                    //ActaVisitaEE
                                    case 11:
                                        lblactaeecargada.Text = "Diligenciado";
                                        lblactaeecargadac.Text = "Diligenciado";
                                        break;

                                }
                            }

                            #endregion


                            #endregion

                            #region Seccion Consulta Lectura Contexto EE
                            ESM.Model.LecturaContextoEE lcee = null;
                            try
                            {
                                lcee = (from lceec in db.LecturaContextoEEs
                                        where lceec.IdIE == Convert.ToInt32(lblidie.Text)
                                        select lceec).Single();
                            }
                            catch (Exception) { lcee = null; }



                            if (lcee != null)
                            {
                                bool estadolcee = true;

                                if (!(bool)lcee.f11 && !(bool)lcee.f12 && !(bool)lcee.f13 && !(bool)lcee.f14 && !(bool)lcee.f15)
                                    estadolcee = false;

                                if (!(bool)lcee._1_2bRural && !(bool)lcee._1_2bUrbana)
                                    estadolcee = false;

                                if (!(bool)lcee.C_1 && !(bool)lcee.C_2 && !(bool)lcee.C_3 && !(bool)lcee.C_4 && !(bool)lcee.C_5)
                                    estadolcee = false;

                                int totalestrato = Convert.ToInt32(lcee._2_2_E1) + Convert.ToInt32(lcee._2_2_E2) + Convert.ToInt32(lcee._2_2_E3) + Convert.ToInt32(lcee._2_2_E4) + Convert.ToInt32(lcee._2_2_E5) + Convert.ToInt32(lcee._2_2_E6);

                                if (totalestrato < 100)
                                {
                                    if (lblobservaciones.Text == "Ninguna")
                                        lblobservaciones.Text = "La suma total de estratos no puede ser menor a 100%.";
                                    estadolcee = false;
                                }
                                else if (totalestrato > 100)
                                {
                                    estadolcee = false;
                                    if (lblobservaciones.Text == "Ninguna")
                                        lblobservaciones.Text = "La suma total de estratos no puede ser mayor a 100%.";
                                }

                                int totalsisben = Convert.ToInt32(lcee._2_3_S1) + Convert.ToInt32(lcee._2_3_S2) + Convert.ToInt32(lcee._2_3_S3) + Convert.ToInt32(lcee._2_3_S4) + Convert.ToInt32(lcee._2_3_NoSabe) + Convert.ToInt32(lcee._2_3_NoTiene);

                                if (totalsisben < 100)
                                {
                                    if (lblobservaciones.Text == "Ninguna")
                                        lblobservaciones.Text = "La suma total de sisben no puede ser menor a 100%.";
                                    estadolcee = false;
                                }
                                else if (totalsisben > 100)
                                {
                                    if (lblobservaciones.Text == "Ninguna")
                                        lblobservaciones.Text = "La suma total de sisben no puede ser mayor a 100%.";
                                    estadolcee = false;
                                }

                                if ((bool)lcee._2_4_Si)
                                    if (lcee._2_5_1.ToString().ToString().Trim() == "0" && lcee._2_5_2.ToString().ToString().Trim() == "0" && lcee._2_5_3.ToString().ToString().Trim() == "0")
                                        estadolcee = false;

                                if (lcee._3_1.ToString().Trim().Length == 0)
                                    estadolcee = false;
                                if (lcee._3_2.ToString().Trim().Length == 0)
                                    estadolcee = false;
                                if (lcee._3_3.ToString().Trim().Length == 0)
                                    estadolcee = false;

                                if ((bool)lcee._3_4_Si)
                                    if (lcee._3_4_1.ToString().Trim().Length == 0)
                                        estadolcee = false;

                                if ((bool)lcee._3_5_Si)
                                {
                                    if (lcee._3_5_1.ToString().Trim().Length == 0)
                                        estadolcee = false;
                                    if (lcee._3_5_2.ToString().Trim().Length == 0)
                                        estadolcee = false;
                                    if (lcee._3_5_3.ToString().Trim().Length == 0)
                                        estadolcee = false;
                                    if (lcee._3_5_4.ToString().Trim().Length == 0)
                                        estadolcee = false;
                                    if (lcee._3_5_5.ToString().Trim().Length == 0)
                                        estadolcee = false;
                                    if (lcee._3_5_6.ToString().Trim().Length == 0)
                                        estadolcee = false;
                                    if (lcee._3_5_7.ToString().Trim().Length == 0)
                                        estadolcee = false;
                                }

                                if (lcee._3_6.ToString().Trim().Length == 0)
                                    estadolcee = false;
                                if (lcee._3_7.ToString().Trim().Length == 0)
                                    estadolcee = false;


                                if ((bool)lcee._3_8_Si)
                                    if (lcee._3_8_1.ToString().Trim().Length == 0)
                                        estadolcee = false;

                                if ((bool)lcee._3_9_Si)
                                    if (lcee._3_9_1.ToString().Trim().Length == 0)
                                        estadolcee = false;

                                if (!(bool)lcee._4_1_Algunas && (bool)lcee._4_1_Si && (bool)lcee._4_1_No)
                                    estadolcee = false;
                                else
                                    if ((bool)lcee._4_1_Si)
                                        if (lcee._4_2.ToString().Trim().Length == 0)
                                            estadolcee = false;

                                if ((bool)lcee._4_3_Si)
                                    if (lcee._4_3_1.ToString().Trim().Length == 0)
                                        estadolcee = false;

                                if ((bool)lcee._5_1_Si)
                                    if (lcee._5_1_1.ToString().Trim().Length == 0)
                                        estadolcee = false;

                                //Validacion General para acta de establecimiento educativo
                                if (estadolcee)
                                {
                                    lblestadoactaee.Text = "Diligenciado";
                                    lblestadoactaeec.Text = "Diligenciado";

                                }
                                else
                                {
                                    lblestadoactaee.Text = "Parcial";
                                    lblestadoactaeec.Text = "Parcial";
                                }

                            }


                            #endregion

                            #region Fecha Visita
                            ESM.Model.Cita objCita = null;

                            try
                            {
                                objCita = (from c in db.Citas
                                           where c.IdEE == Convert.ToInt32(lblidie.Text)
                                           select c).Take(1).Single();
                            }
                            catch (Exception) { objCita = null; }

                            if (objCita != null)
                            {
                                lblcita.Text = objCita.FechaInicio.ToShortDateString();
                                lblcitac.Text = objCita.FechaInicio.ToShortDateString();
                            }
                            #endregion

                        }
                        catch (Exception) { }
                    }
                }
            }
            catch (Exception)
            {
                ProcesoFinalizado = false;
            }
        }

        protected void lknDiliEE_Click(object sender, EventArgs e)
        {
            lbltotal.Visible = false;
            Session.Remove("ExportResumen");
            ReportEE();
            Visualizacion(false, false, false, true);
        }

        #region ExcelDocs Coment
        //protected void ExcelDoc(GridView objGridView = null)
        //{
        //    var xls = new Excel.Application();
        //    var libro = xls.Workbooks.Open(Server.MapPath("/Excel/Diligenciamiento_EE_v01.xls"));
        //    var hoja = xls.Worksheets[1];

        //    #region Carga de Archivo
        //    for (int i = 0; i < objGridView.Columns.Count; i++)
        //    {
        //        hoja.Cells[1, i + 1] = objGridView.Columns[i].HeaderText;

        //    }


        //    for (int i = 0; i < objGridView.Columns.Count; i++)
        //    {
        //        for (int j = 0; j < objGridView.Rows.Count; j++)
        //        {
        //            if (i < 5)
        //                dr[i] = row.Cells[i].Cells[i].Text.ToString();

        //            switch (i)
        //            {
        //                case 5:
        //                    Label lblcita = (Label)row.Cells[i].FindControl("lblcita");
        //                    dr[i] = lblcita.Text;
        //                    break;
        //                case 6:
        //                    Label lblpei = (Label)row.Cells[i].FindControl("lblpei");
        //                    dr[i] = lblpei.Text;
        //                    break;
        //                case 7:
        //                    Label lblpmi = (Label)row.Cells[i].FindControl("lblpmi");
        //                    dr[i] = lblpmi.Text;
        //                    break;
        //                case 8:
        //                    Label lblmaco = (Label)row.Cells[i].FindControl("lblmaco");
        //                    dr[i] = lblmaco.Text;
        //                    break;
        //                case 9:
        //                    Label lblplan = (Label)row.Cells[i].FindControl("lblplan");
        //                    dr[i] = lblplan.Text;
        //                    break;
        //                case 10:
        //                    Label lblproy = (Label)row.Cells[i].FindControl("lblproy");
        //                    dr[i] = lblproy.Text;
        //                    break;
        //                case 11:
        //                    Label lblotros = (Label)row.Cells[i].FindControl("lblotros");
        //                    dr[i] = lblotros.Text;
        //                    break;
        //                case 12:
        //                    Label lblactaeecargada = (Label)row.Cells[i].FindControl("lblactaeecargada");
        //                    dr[i] = lblactaeecargada.Text;
        //                    break;
        //                case 13:
        //                    Label lblcantdir = (Label)row.Cells[i].FindControl("lblcantdir");
        //                    dr[i] = lblcantdir.Text;
        //                    break;
        //                case 14:
        //                    Label lblcantest = (Label)row.Cells[i].FindControl("lblcantest");
        //                    dr[i] = lblcantest.Text;
        //                    break;
        //                case 15:
        //                    Label lblcantedu = (Label)row.Cells[i].FindControl("lblcantedu");
        //                    dr[i] = lblcantedu.Text;
        //                    break;
        //                case 16:
        //                    Label lblcantpad = (Label)row.Cells[i].FindControl("lblcantpad");
        //                    dr[i] = lblcantpad.Text;
        //                    break;
        //                case 17:
        //                    Label lblcantpro = (Label)row.Cells[i].FindControl("lblcantpro");
        //                    dr[i] = lblcantpro.Text;
        //                    break;
        //                case 18:
        //                    Label lblestadoactaee = (Label)row.Cells[i].FindControl("lblestadoactaee");
        //                    dr[i] = lblestadoactaee.Text;
        //                    break;
        //                case 19:
        //                    Label lblobservaciones = (Label)row.Cells[i].FindControl("lblobservaciones");
        //                    dr[i] = lblobservaciones.Text;
        //                    break;
        //                case 20:
        //                    Label lblevalest = (Label)row.Cells[i].FindControl("lblevalest");
        //                    dr[i] = lblevalest.Text;
        //                    break;
        //                case 21:
        //                    Label lblevalpad = (Label)row.Cells[i].FindControl("lblevalpad");
        //                    dr[i] = lblevalpad.Text;
        //                    break;
        //                case 22:
        //                    Label lblevalprof = (Label)row.Cells[i].FindControl("lblevalprof");
        //                    dr[i] = lblevalprof.Text;
        //                    break;
        //                case 23:
        //                    Label lblevaldir = (Label)row.Cells[i].FindControl("lblevaldir");
        //                    dr[i] = lblevaldir.Text;
        //                    break;
        //                case 24:
        //                    Label lblevaledu = (Label)row.Cells[i].FindControl("lblevaledu");
        //                    dr[i] = lblevaledu.Text;
        //                    break;
        //            }


        //        }
        //    }



        //string filename = "/Excel/Diligenciamiento_EE_" + DateTime.Now.ToString("yyMMdd") + ".xls";
        //string serverpath = Server.MapPath(filename);
        //if (!File.Exists(filename))
        //{
        //    xls.ActiveWorkbook.SaveCopyAs(serverpath);
        //    if (File.Exists(serverpath))
        //        Response.Redirect(filename);
        //}
        //else
        //{
        //    File.Delete(serverpath);
        //    xls.ActiveWorkbook.SaveCopyAs(serverpath);
        //    if (File.Exists(serverpath))
        //        Response.Redirect(filename);
        //}

        //}
        #endregion

        protected void lknDiliRes_Click(object sender, EventArgs e)
        {
            ReportEE();
            Visualizacion(false, false, false, true);
            Session.Add("ExportResumen", true);
        }

        #region Metodo Excel Open XML

        public void ExportDataTable(DataTable table, string exportFile)
        {
            //create the empty spreadsheet template and save the file //using the class generated by the Productivity tool  
            ExcelDocument excelDocument = new ExcelDocument();
            excelDocument.CreatePackage(exportFile);
            //string filename = "";
            //File.Copy(filename, filename, true);
            //populate the data into the spreadsheet  
            using (SpreadsheetDocument spreadsheet = SpreadsheetDocument.Open(exportFile, true))
            {
                WorkbookPart workbook = spreadsheet.WorkbookPart;
                //create a reference to Sheet1  
                WorksheetPart worksheet = workbook.WorksheetParts.Last();
                SheetData data = worksheet.Worksheet.GetFirstChild<SheetData>();

                //add column names to the first row  
                Row header = new Row();
                header.RowIndex = (UInt32)1;

                foreach (DataColumn column in table.Columns)
                {
                    Cell headerCell = createTextCell(
                        table.Columns.IndexOf(column) + 1,
                        1,
                        column.ColumnName);

                    header.AppendChild(headerCell);
                }
                data.AppendChild(header);

                //loop through each data row  
                DataRow contentRow;
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    contentRow = table.Rows[i];
                    data.AppendChild(createContentRow(contentRow, i + 2));
                }
            }
        }
        private Cell createTextCell(int columnIndex, int rowIndex, object cellValue)
        {
            Cell cell = new Cell();

            cell.DataType = CellValues.InlineString;
            cell.CellReference = getColumnName(columnIndex) + rowIndex;

            InlineString inlineString = new InlineString();
            Text t = new Text();

            t.Text = cellValue.ToString();
            inlineString.AppendChild(t);
            cell.AppendChild(inlineString);

            return cell;
        }
        private Row createContentRow(DataRow dataRow, int rowIndex)
        {
            Row row = new Row
            {
                RowIndex = (UInt32)rowIndex
            };

            for (int i = 0; i < dataRow.Table.Columns.Count; i++)
            {
                Cell dataCell = createTextCell(i + 1, rowIndex, dataRow[i]);
                row.AppendChild(dataCell);
            }
            return row;
        }
        private string getColumnName(int columnIndex)
        {
            int dividend = columnIndex;
            string columnName = String.Empty;
            int modifier;

            while (dividend > 0)
            {
                modifier = (dividend - 1) % 26;
                columnName =
                    Convert.ToChar(65 + modifier).ToString() + columnName;
                dividend = (int)((dividend - modifier) / 26);
            }

            return columnName;
        }

        protected DataTable ConvertToDataTable(GridView objgv)
        {
            DataTable dt = new DataTable();

            // add the columns to the datatable            
            if (objgv.HeaderRow != null)
            {

                for (int i = 0; i < objgv.HeaderRow.Cells.Count; i++)
                {
                    dt.Columns.Add(objgv.HeaderRow.Cells[i].Text);
                }
            }

            //  add each of the data rows to the table
            foreach (GridViewRow row in objgv.Rows)
            {
                DataRow dr;
                dr = dt.NewRow();

                for (int i = 0; i < row.Cells.Count; i++)
                {
                    if (i < 5)
                        dr[i] = row.Cells[i].Text.Replace("&nbsp;", "");

                    switch (i)
                    {
                        case 5:
                            Label lblcita = (Label)row.Cells[i].FindControl("lblcita");
                            dr[i] = lblcita.Text;
                            break;
                        case 6:
                            Label lblpei = (Label)row.Cells[i].FindControl("lblpei");
                            dr[i] = lblpei.Text;
                            break;
                        case 7:
                            Label lblpmi = (Label)row.Cells[i].FindControl("lblpmi");
                            dr[i] = lblpmi.Text;
                            break;
                        case 8:
                            Label lblmaco = (Label)row.Cells[i].FindControl("lblmaco");
                            dr[i] = lblmaco.Text;
                            break;
                        case 9:
                            Label lblplan = (Label)row.Cells[i].FindControl("lblplan");
                            dr[i] = lblplan.Text;
                            break;
                        case 10:
                            Label lblproy = (Label)row.Cells[i].FindControl("lblproy");
                            dr[i] = lblproy.Text;
                            break;
                        case 11:
                            Label lblotros = (Label)row.Cells[i].FindControl("lblotros");
                            dr[i] = lblotros.Text;
                            break;
                        case 12:
                            Label lblactaeecargada = (Label)row.Cells[i].FindControl("lblactaeecargada");
                            dr[i] = lblactaeecargada.Text;
                            break;
                        case 13:
                            Label lblcantdir = (Label)row.Cells[i].FindControl("lblcantdir");
                            dr[i] = lblcantdir.Text;
                            break;
                        case 14:
                            Label lblcantest = (Label)row.Cells[i].FindControl("lblcantest");
                            dr[i] = lblcantest.Text;
                            break;
                        case 15:
                            Label lblcantedu = (Label)row.Cells[i].FindControl("lblcantedu");
                            dr[i] = lblcantedu.Text;
                            break;
                        case 16:
                            Label lblcantpad = (Label)row.Cells[i].FindControl("lblcantpad");
                            dr[i] = lblcantpad.Text;
                            break;
                        case 17:
                            Label lblcantpro = (Label)row.Cells[i].FindControl("lblcantpro");
                            dr[i] = lblcantpro.Text;
                            break;
                        case 18:
                            Label lblestadoactaee = (Label)row.Cells[i].FindControl("lblestadoactaee");
                            dr[i] = lblestadoactaee.Text;
                            break;
                        case 19:
                            Label lblobservaciones = (Label)row.Cells[i].FindControl("lblobservaciones");
                            dr[i] = lblobservaciones.Text;
                            break;
                        case 20:
                            Label lblevalest = (Label)row.Cells[i].FindControl("lblevalest");
                            dr[i] = lblevalest.Text;
                            break;
                        case 21:
                            Label lblevalpad = (Label)row.Cells[i].FindControl("lblevalpad");
                            dr[i] = lblevalpad.Text;
                            break;
                        case 22:
                            Label lblevalprof = (Label)row.Cells[i].FindControl("lblevalprof");
                            dr[i] = lblevalprof.Text;
                            break;
                        case 23:
                            Label lblevaldir = (Label)row.Cells[i].FindControl("lblevaldir");
                            dr[i] = lblevaldir.Text;
                            break;
                        case 24:
                            Label lblevaledu = (Label)row.Cells[i].FindControl("lblevaledu");
                            dr[i] = lblevaledu.Text;
                            break;
                    }
                }
                dt.Rows.Add(dr);
            }

            //  add the footer row to the table
            if (objgv.FooterRow != null)
            {
                DataRow dr;
                dr = dt.NewRow();

                for (int i = 0; i < objgv.FooterRow.Cells.Count; i++)
                {
                    dr[i] = objgv.FooterRow.Cells[i].Text.Replace("&nbsp;", "");
                }
                dt.Rows.Add(dr);
            }

            return dt;
        }
        #endregion
    }
}
