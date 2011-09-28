using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace ESM
{
    public partial class ModuloDocumentosSE : System.Web.UI.Page
    {
        #region Propiedades Publicas y Privadas

        EvaluationSettings.CDocumentos objCDocumentos = new EvaluationSettings.CDocumentos();
        int idmedicion = 0;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["idmedicion"] != null)
            {
                idmedicion = Convert.ToInt32(Request.QueryString["idmedicion"].ToString());
            }
            else
                Response.Redirect("");

            if (!Page.IsPostBack)
            {
                for (int i = 1; i < 11; i++)
                {
                    bool exist = false;
                    exist = objCDocumentos.ExisteDocumento(i, idmedicion);
                    IQueryable<ESM.Model.AsignaDocumentos> docs = objCDocumentos.Show(idmedicion);

                    foreach (var item in docs)
                    {
                        if (i == 12)
                        {
                            if (exist && i == item.IdDocumento)
                            {
                                lknPAM.Visible = true;
                                lknPAM.HRef = "/Files/" + item.Ruta;
                                lknPAM.InnerHtml = item.Ruta.ToString();
                                btnUpPAM.Text = "Reemplazar";
                            }
                        }
                        else if (i == 13)
                        {
                            if (exist && i == item.IdDocumento)
                            {
                                lknConvenios.Visible = true;
                                lknConvenios.HRef = "/Files/" + item.Ruta;
                                lknConvenios.InnerHtml = item.Ruta.ToString();
                                btnUpConvenios.Text = "Reemplazar";
                            }
                        }
                        else if (i == 14)
                        {
                            if (exist && i == item.IdDocumento)
                            {
                                lknActaVisitaSE.Visible = true;
                                lknActaVisitaSE.HRef = "/Files/" + item.Ruta;
                                lknActaVisitaSE.InnerHtml = item.Ruta.ToString();
                                btnUpActaVisitaSE.Text = "Reemplazar";
                            }
                        }

                    }


                }

            }
        }

        protected void CargarArchivos(FileUpload objFileUpload, int iddocumento)
        {
            try
            {
                if (idmedicion != 0)
                {
                    if (!objCDocumentos.ExisteDocumento(iddocumento, idmedicion))
                    {
                        #region Carga de Documento
                        if (objFileUpload.PostedFile.FileName != "")
                        {
                            string path = String.Format("{0}{1}", Server.MapPath("~/Files/"), objFileUpload.FileName);
                            int iniciar = objFileUpload.FileName.Length - 3;
                            int finalizar = objFileUpload.FileName.Length;

                            var extencion = objFileUpload.FileName.Substring(iniciar);

                            string strextencion = extencion.ToString();

                            if (strextencion != "pdf" && strextencion != "zip" && strextencion != "rar" && strextencion != "doc")
                            {
                                Response.Write("<script>alert('Error el archivo no pudo ser cargado, revise el tipo de documento que intenta cargar. \n\nFormatos compatibles:pdf,doc,zip,rar.');</script>");
                                lblMensaje.Text = "Error el archivo no pudo ser cargado, revise el tipo de documento que intenta cargar. \n\nFormatos compatibles:pdf,doc,zip,rar.";
                            }
                            else
                            {
                                if (objFileUpload.PostedFile.ContentLength <= 2097152)
                                {
                                    if (File.Exists(path))
                                        File.Delete(path);

                                    objFileUpload.PostedFile.SaveAs(path);

                                    if (objCDocumentos.CargarDocumento(idmedicion, iddocumento, objFileUpload.FileName))
                                    {
                                        Response.Write("<script>Alert('El proceso de carga para el archivo finalizo correctamente.');</script>");
                                        lblMensaje.Text = "El proceso de carga para el archivo finalizo correctamente.";

                                    }
                                    else
                                        File.Delete(path);
                                }
                                else
                                {
                                    Response.Write("<script>Alert('Para cargar un archivo se debe tener en cuenta que el tamaño sea menor o igual a 2MB');</script>");
                                    lblMensaje.Text = "Para cargar un archivo se debe tener en cuenta que el tamaño sea menor o igual a 2MB";
                                }
                            }
                        }
                        lblMensaje.Visible = true;

                        switch (iddocumento)
                        {
                            case 12:
                                lknPAM.Visible = true;
                                lknPAM.HRef = "/Files/" + objFileUpload.FileName;
                                btnUpPAM.Text = "Reemplazar";
                                lknPAM.InnerHtml = objFileUpload.FileName;
                                break;
                            case 13:
                                lknConvenios.Visible = true;
                                lknConvenios.HRef = "/Files/" + objFileUpload.FileName;
                                btnUpConvenios.Text = "Reemplazar";
                                break;
                            case 14:
                                lknActaVisitaSE.Visible = true;
                                lknActaVisitaSE.HRef = "/Files/" + objFileUpload.FileName;
                                lknActaVisitaSE.InnerHtml = objFileUpload.FileName;
                                btnUpActaVisitaSE.Text = "Reemplazar";
                                break;
                        }

                        #endregion
                    }
                    else
                    {
                        if (objCDocumentos.EliminarArchivo(idmedicion, iddocumento))
                        {
                            #region Carga de Documento
                            if (objFileUpload.PostedFile.FileName != "")
                            {
                                string path = String.Format("{0}{1}", Server.MapPath("~/Files/"), objFileUpload.FileName);
                                int iniciar = objFileUpload.FileName.Length - 3;
                                int finalizar = objFileUpload.FileName.Length;

                                var extencion = objFileUpload.FileName.Substring(iniciar);

                                string strextencion = extencion.ToString();

                                if (strextencion != "pdf" && strextencion != "zip" && strextencion != "rar" && strextencion != "doc")
                                {
                                    Response.Write("<script>alert('Error el archivo no pudo ser cargado, revise el tipo de documento que intenta cargar. \n\nFormatos compatibles:pdf,doc,zip,rar.');</script>");
                                    lblMensaje.Text = "Error el archivo no pudo ser cargado, revise el tipo de documento que intenta cargar. \n\nFormatos compatibles:pdf,doc,zip,rar.";
                                }
                                else
                                {
                                    if (objFileUpload.PostedFile.ContentLength <= 2097152)
                                    {
                                        if (File.Exists(path))
                                            File.Delete(path);

                                        objFileUpload.PostedFile.SaveAs(path);

                                        if (objCDocumentos.CargarDocumento(idmedicion, iddocumento, objFileUpload.FileName))
                                        {
                                            Response.Write("<script>Alert('El proceso de carga para el archivo finalizó correctamente.');</script>");
                                            lblMensaje.Text = "El proceso de carga para el archivo finalizó correctamente.";
                                        }
                                        else
                                            File.Delete(path);
                                    }
                                    else
                                    {
                                        Response.Write("<script>Alert('Para cargar un archivo se debe tener en cuenta que el tamaño sea menor o igual a 2MB');</script>");
                                        lblMensaje.Text = "Para cargar un archivo se debe tener en cuenta que el tamaño sea menor o igual a 2MB";
                                    }
                                }
                            }
                            lblMensaje.Visible = true;

                            switch (iddocumento)
                            {
                                case 12:
                                    lknPAM.Visible = true;
                                    lknPAM.HRef = "/Files/" + objFileUpload.FileName;
                                    btnUpPAM.Text = "Reemplazar";
                                    lknPAM.InnerHtml = objFileUpload.FileName;
                                    break;
                                case 13:
                                    lknConvenios.Visible = true;
                                    lknConvenios.HRef = "/Files/" + objFileUpload.FileName;
                                    btnUpConvenios.Text = "Reemplazar";
                                    break;
                                case 14:
                                    lknActaVisitaSE.Visible = true;
                                    lknActaVisitaSE.HRef = "/Files/" + objFileUpload.FileName;
                                    lknActaVisitaSE.InnerHtml = objFileUpload.FileName;
                                    btnUpActaVisitaSE.Text = "Reemplazar";
                                    break;
                            }

                            #endregion
                        }
                        else
                        {
                            lblMensaje.Text = "No se puede reemplazar este documento.";
                        }
                    }
                }
                else
                {
                    lblMensaje.Text = "La medicion no pudo ser identificada o no existe.";
                }
            }
            catch (Exception)
            {
                Response.Write("<script>alert('Error al cargar el archivo seleccionado.');</script>");
                lblMensaje.Text = "Error al cargar el archivo seleccionado.";

            }
        }


        protected void btnUpPAM_Click(object sender, EventArgs e)
        {
            CargarArchivos(FPAM, 12);
        }

        protected void btnUpConvenios_Click(object sender, EventArgs e)
        {
            CargarArchivos(FConvenios, 13);
        }

        protected void btnUpActaVisitaSE_Click(object sender, EventArgs e)
        {
            CargarArchivos(FActaVisitaSE, 14);
        }

        protected void ReemplazarControles()
        {
            ESM.Model.ESMBDDataContext db = new Model.ESMBDDataContext();

            var a = from asig in db.AsignaDocumentos
                    where asig.IdMedicion == idmedicion
                    select asig;

            foreach (var item in a)
            {
                if (item.IdDocumento == 1)
                {
                    lknPAM.Visible = true;
                    btnUpPAM.Text = "Reemplazar";
                    FPAM.Visible = false;
                }
                if (item.IdDocumento == 2)
                {
                    lknConvenios.Visible = true;
                    btnUpConvenios.Text = "Reemplazar";
                    FConvenios.Visible = false;
                }
                if (item.IdDocumento == 3)
                {
                    lknActaVisitaSE.Visible = true;
                    btnUpActaVisitaSE.Text = "Reemplazar";
                    FActaVisitaSE.Visible = false;
                }
            }
        }

    }
}