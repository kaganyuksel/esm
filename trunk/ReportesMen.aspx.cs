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

            Visualizacion(true, false);
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

            Visualizacion(false, true);
        }

        protected void Visualizacion(bool AgendaSE, bool AgendaEE)
        {
            gvAgendaSE.Visible = AgendaSE;
            gvAgendaEE.Visible = AgendaEE;
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
        }

        protected void ReportDiligenciamientoSE()
        {
            try
            {

                string estadoacta = null;
                bool actaarriba = false;

                ESM.Model.ESMBDDataContext db = new Model.ESMBDDataContext();

                var col = from s in db.Secretaria_Educacions
                          join ase in db.ActaVisitaSEs on s.IdSecretaria equals ase.IdSE
                          join asigse in db.AsignaDocumentos on ase.IdMedicion equals asigse.IdMedicion
                          join asistse in db.AsociadoActaVisitaSEs on ase.IdActaVisita equals asistse.IdActaVisita
                          join cita in db.Citas on s.IdSecretaria equals cita.IdSE
                          join lcse in db.LecturaContextoSEs on s.IdSecretaria equals lcse.IdSecretaria
                          select new
                          {
                              s.IdSecretaria,
                              s.Nombre,
                              Consultor = s.Consultore.Nombre,
                              cita.FechaInicio,
                              ase,
                              FechaDiligenciamiento = ase.Medicione.FechaMedicion,
                              ase.AsociadoActaVisitaSEs,
                              CantAsocioados = ase.AsociadoActaVisitaSEs.Count,
                              asigse,
                              CantDocumentos = asigse.Documento.AsignaDocumentos.Count,
                              asistse,
                              lcse
                          };

                gvdilise.DataSource = col;
                gvdilise.DataBind();

                for (int i = 0; i < gvdilise.Rows.Count; i++)
                {
                    Label lblidse = (Label)gvdilise.Rows[i].FindControl("lblidse");
                    Label lblActaDocumento = (Label)gvdilise.Rows[i].FindControl("lblActaDocumento");

                    var docs = from d in col
                               where d.IdSecretaria == Convert.ToInt32(lblidse.Text)
                               select d.asigse;

                    foreach (var item in docs)
                    {
                        if (item.IdDocumento == 14)
                        {
                            lblActaDocumento.Text = "Si";
                            break;
                        }
                        else
                            lblActaDocumento.Text = "No";
                    }
                }

            }
            catch (Exception) { }
        }

        protected void lknDiliSE_Click(object sender, EventArgs e)
        {
            ReportDiligenciamientoSE();
        }
    }
}