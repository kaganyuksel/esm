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

            Visualizacion(true, false, false, false, false);
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

            Visualizacion(false, true, false, false, false);
        }

        protected void Visualizacion(bool AgendaSE, bool AgendaEE, bool DiligenSE, bool DiliEE, bool DiliEEres)
        {
            gvAgendaSE.Visible = AgendaSE;
            gvAgendaEE.Visible = AgendaEE;
            gvdilise.Visible = DiligenSE;
            gvDiliEE.Visible = DiliEE;
            GvDiliEEres.Visible = DiliEEres;
            divse.Visible = DiligenSE;
            pnlgveedili.Visible = DiliEE;
            pnlgveedilires.Visible = DiliEEres;
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
                ExcelDoc(gvDiliEE);
                //Objetos.GridViewExportUtil.Export("Diligenciamiento EE.xls", gvDiliEE);

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
            Visualizacion(false, false, true, false, false);
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

        protected void GvDiliEEres_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                this.GvDiliEEres.PageIndex = e.NewPageIndex;
                ReportEEResumido();
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

                                            break;
                                        //Profesional
                                        case 2:
                                            int cantpro = (from c in item.AsociadosActaVisitaEEs
                                                           where c.IdActor == 2
                                                           select c).Count();

                                            lblcantpro.Text = cantpro.ToString();
                                            break;
                                        //Educador
                                        case 3:
                                            int cantedu = (from c in item.AsociadosActaVisitaEEs
                                                           where c.IdActor == 3
                                                           select c).Count();

                                            lblcantedu.Text = cantedu.ToString();
                                            break;
                                        //Padre de Familia
                                        case 4:
                                            int cantpad = (from c in item.AsociadosActaVisitaEEs
                                                           where c.IdActor == 4
                                                           select c).Count();

                                            lblcantpad.Text = cantpad.ToString();
                                            break;
                                        //Directivos
                                        case 6:
                                            int cantdir = (from c in item.AsociadosActaVisitaEEs
                                                           where c.IdActor == 6
                                                           select c).Count();

                                            lblcantdir.Text = cantdir.ToString();
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
                                        break;
                                    //PMI
                                    case 2:
                                        lblpmi.Text = "Diligenciado";
                                        break;
                                    //Manual de Convivencia
                                    case 3:
                                        lblmaco.Text = "Diligenciado";
                                        break;
                                    //Plan de Estudios
                                    case 4:
                                        lblplan.Text = "Diligenciado";
                                        break;
                                    //DPP
                                    case 9:
                                        lblproy.Text = "Diligenciado";
                                        break;
                                    //Otros
                                    case 10:
                                        lblotros.Text = "Diligenciado";
                                        break;
                                    //ActaVisitaEE
                                    case 11:
                                        lblactaeecargada.Text = "Diligenciado";
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
                                    lblestadoactaee.Text = "Diligenciado";
                                else
                                    lblestadoactaee.Text = "Parcial";

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
                            }
                            #endregion

                        }
                        catch (Exception) { }


                    }


                }
            }
            catch (Exception) { }
        }

        protected void ReportEEResumido()
        {
            try
            {
                ESM.Model.ESMBDDataContext db = new Model.ESMBDDataContext();

                var ee = from eec in db.Secretaria_Educacions
                         select new
                         {
                             eec.Nombre,
                             Consultor = eec.Consultore.Nombre,
                             eec.Establecimiento_Educativos
                         };

                GvDiliEEres.DataSource = ee;
                GvDiliEEres.DataBind();
                int Contador = 0;



                foreach (var eeitem in ee)
                {
                    Label lblevalest = (Label)GvDiliEEres.Rows[Contador].FindControl("lblevalest");
                    Label lblevalpad = (Label)GvDiliEEres.Rows[Contador].FindControl("lblevalpad");
                    Label lblevalprof = (Label)GvDiliEEres.Rows[Contador].FindControl("lblevalprof");
                    Label lblevaldir = (Label)GvDiliEEres.Rows[Contador].FindControl("lblevaldir");
                    Label lblevaledu = (Label)GvDiliEEres.Rows[Contador].FindControl("lblevaledu");
                    Label lblpei = (Label)GvDiliEEres.Rows[Contador].FindControl("lblpei");
                    Label lblpmi = (Label)GvDiliEEres.Rows[Contador].FindControl("lblpmi");
                    Label lblmaco = (Label)GvDiliEEres.Rows[Contador].FindControl("lblmaco");
                    Label lblplan = (Label)GvDiliEEres.Rows[Contador].FindControl("lblplan");
                    Label lblproy = (Label)GvDiliEEres.Rows[Contador].FindControl("lblproy");
                    Label lblotros = (Label)GvDiliEEres.Rows[Contador].FindControl("lblotros");
                    Label lblactaeecargada = (Label)GvDiliEEres.Rows[Contador].FindControl("lblactaeecargada");

                    int totalpei = 0;
                    int totalpmi = 0;
                    int totalmaco = 0;
                    int totalproy = 0;
                    int totalplan = 0;
                    int totalotros = 0;
                    int totalactas = 0;

                    foreach (var eesingleitem in eeitem.Establecimiento_Educativos)
                    {
                        int idmedicion = 0;

                        for (short i = 0; i < 6; i++)
                        {
                            try
                            {
                                #region Seccion Consulta Evaluacion
                                var coleval = (from ev in db.Evaluacions
                                               where ev.IdActor == i + 1 && ev.IdIE == eesingleitem.IdIE
                                               select new { ev.IdMedicion, ev.EstadoEvaluacion.Estado }).Single();

                                string evalact = coleval.Estado;

                                idmedicion = (int)coleval.IdMedicion;

                                //switch (i + 1)
                                //{
                                //    case 1:
                                //        lblevalest.Text = evalact;
                                //        break;
                                //    case 2:
                                //        lblevalprof.Text = evalact;
                                //        break;
                                //    case 3:
                                //        lblevaledu.Text = evalact;
                                //        break;
                                //    case 4:
                                //        lblevalpad.Text = evalact;
                                //        break;
                                //    case 6:
                                //        lblevaldir.Text = evalact;
                                //        break;

                                //}
                                #endregion

                            }
                            catch (Exception) { }

                        }

                        #region Documentos Establecimiento Educativo

                        var docsaee = from deec in db.AsignaDocumentos
                                      where deec.IdMedicion == idmedicion
                                      select deec;

                        totalpei = totalpei + (from docsee in docsaee
                                               where docsee.IdDocumento == 1
                                               select docsee).Count();

                        totalpmi = totalpmi + (from docsee in docsaee
                                               where docsee.IdDocumento == 2
                                               select docsee).Count();

                        totalmaco = totalmaco + (from docsee in docsaee
                                                 where docsee.IdDocumento == 3
                                                 select docsee).Count();

                        totalplan = totalplan + (from docsee in docsaee
                                                 where docsee.IdDocumento == 4
                                                 select docsee).Count();

                        totalproy = totalproy + (from docsee in docsaee
                                                 where docsee.IdDocumento == 9
                                                 select docsee).Count();

                        totalotros = totalotros + (from docsee in docsaee
                                                   where docsee.IdDocumento == 10
                                                   select docsee).Count();

                        totalactas = totalactas + (from docsee in docsaee
                                                   where docsee.IdDocumento == 11
                                                   select docsee).Count();


                        #endregion

                        #region Seccion Consulta Lectura Contexto EE
                        ESM.Model.LecturaContextoEE lcee = null;
                        try
                        {
                            lcee = (from lceec in db.LecturaContextoEEs
                                    where lceec.IdIE == eesingleitem.IdIE
                                    select lceec).Single();
                        }
                        catch (Exception) { lcee = null; }



                        if (lcee != null)
                        {

                        }


                        #endregion
                    }

                    lblpei.Text = totalpei.ToString();
                    lblpmi.Text = totalpmi.ToString();
                    lblplan.Text = totalplan.ToString();
                    lblproy.Text = totalproy.ToString();
                    lblmaco.Text = totalmaco.ToString();
                    lblotros.Text = totalotros.ToString();
                    lblactaeecargada.Text = totalactas.ToString();

                    Contador++;
                }

            }
            catch (Exception) { }
        }

        protected void lknDiliEE_Click(object sender, EventArgs e)
        {
            ReportEE();
            Visualizacion(false, false, false, true, false);
        }

        protected void lknDiliEEres_Click(object sender, EventArgs e)
        {

            Visualizacion(false, false, false, false, true);
        }

        protected void ExcelDoc(GridView objGridView = null)
        {
            var xls = new Excel.Application();
            var libro = xls.Workbooks.Open(@"E:\ESM\Excel\Diligenciamiento_EE_v01.xls");
            var hoja = xls.Worksheets[1];

            #region Carga de Archivo
            for (int i = 0; i < objGridView.Columns.Count; i++)
            {
                hoja.Cells[1, i + 1] = objGridView.Columns[i].HeaderText;

            }


            for (int i = 0; i < objGridView.Columns.Count; i++)
            {
                for (int j = 0; j < objGridView.Rows.Count; j++)
                {
                    if (i < 5)
                        hoja.Cells[j + 2, i + 1] = objGridView.Rows[j].Cells[i].Text.ToString();

                    switch (i)
                    {
                        case 5:
                            Label lblcita = (Label)objGridView.Rows[j].FindControl("lblcita");
                            hoja.Cells[j + 2, i + 1] = lblcita.Text;
                            break;
                        case 6:
                            Label lblpei = (Label)objGridView.Rows[j].FindControl("lblpei");
                            hoja.Cells[j + 2, i + 1] = lblpei.Text;
                            break;
                        case 7:
                            Label lblpmi = (Label)objGridView.Rows[j].FindControl("lblpmi");
                            hoja.Cells[j + 2, i + 1] = lblpmi.Text;
                            break;
                        case 8:
                            Label lblmaco = (Label)objGridView.Rows[j].FindControl("lblmaco");
                            hoja.Cells[j + 2, i + 1] = lblmaco.Text;
                            break;
                        case 9:
                            Label lblplan = (Label)objGridView.Rows[j].FindControl("lblplan");
                            hoja.Cells[j + 2, i + 1] = lblplan.Text;
                            break;
                        case 10:
                            Label lblproy = (Label)objGridView.Rows[j].FindControl("lblproy");
                            hoja.Cells[j + 2, i + 1] = lblproy.Text;
                            break;
                        case 11:
                            Label lblotros = (Label)objGridView.Rows[j].FindControl("lblotros");
                            hoja.Cells[j + 2, i + 1] = lblotros.Text;
                            break;
                        case 12:
                            Label lblactaeecargada = (Label)objGridView.Rows[j].FindControl("lblactaeecargada");
                            hoja.Cells[j + 2, i + 1] = lblactaeecargada.Text;
                            break;
                        case 13:
                            Label lblcantdir = (Label)objGridView.Rows[j].FindControl("lblcantdir");
                            hoja.Cells[j + 2, i + 1] = lblcantdir.Text;
                            break;
                        case 14:
                            Label lblcantest = (Label)objGridView.Rows[j].FindControl("lblcantest");
                            hoja.Cells[j + 2, i + 1] = lblcantest.Text;
                            break;
                        case 15:
                            Label lblcantedu = (Label)objGridView.Rows[j].FindControl("lblcantedu");
                            hoja.Cells[j + 2, i + 1] = lblcantedu.Text;
                            break;
                        case 16:
                            Label lblcantpad = (Label)objGridView.Rows[j].FindControl("lblcantpad");
                            hoja.Cells[j + 2, i + 1] = lblcantpad.Text;
                            break;
                        case 17:
                            Label lblcantpro = (Label)objGridView.Rows[j].FindControl("lblcantpro");
                            hoja.Cells[j + 2, i + 1] = lblcantpro.Text;
                            break;
                        case 18:
                            Label lblestadoactaee = (Label)objGridView.Rows[j].FindControl("lblestadoactaee");
                            hoja.Cells[j + 2, i + 1] = lblestadoactaee.Text;
                            break;
                        case 19:
                            Label lblobservaciones = (Label)objGridView.Rows[j].FindControl("lblobservaciones");
                            hoja.Cells[j + 2, i + 1] = lblobservaciones.Text;
                            break;
                        case 20:
                            Label lblevalest = (Label)objGridView.Rows[j].FindControl("lblevalest");
                            hoja.Cells[j + 2, i + 1] = lblevalest.Text;
                            break;
                        case 21:
                            Label lblevalpad = (Label)objGridView.Rows[j].FindControl("lblevalpad");
                            hoja.Cells[j + 2, i + 1] = lblevalpad.Text;
                            break;
                        case 22:
                            Label lblevalprof = (Label)objGridView.Rows[j].FindControl("lblevalprof");
                            hoja.Cells[j + 2, i + 1] = lblevalprof.Text;
                            break;
                        case 23:
                            Label lblevaldir = (Label)objGridView.Rows[j].FindControl("lblevaldir");
                            hoja.Cells[j + 2, i + 1] = lblevaldir.Text;
                            break;
                        case 24:
                            Label lblevaledu = (Label)objGridView.Rows[j].FindControl("lblevaledu");
                            hoja.Cells[j + 2, i + 1] = lblevaledu.Text;
                            break;
                    }


                }
            }

            #endregion

            xls.ActiveWorkbook.SaveCopyAs(@"E:\ESM\Excel\Nuevo"+ DateTime.Now.ToString("yyMMdd") +".xlsx");


        }
    }
}