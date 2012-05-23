using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;
using System.Text;

namespace ESM
{
    public partial class BancoProyectos : System.Web.UI.Page
    {

        #region Propiedades publicas y privadas

        protected Objetos.Cproyecto _objProyecto = new Objetos.Cproyecto();
        int proyecto_id = 0;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Request.IsAuthenticated)
            {

                if (ban_proyecto_id.Value != " ")
                {
                    proyecto_id = Convert.ToInt32(ban_proyecto_id.Value);
                    ban_proyecto_id.Value = ban_proyecto_id.Value;

                    refreshFiles(proyecto_id);
                    CargarMarcoLogico();
                }

                if (!Page.IsPostBack)
                {
                    CargarColeccionProyectos();
                }
            }
            else
                Response.Redirect("Login.aspx");
        }

        protected void btnNuevoProyecto_Click(object sender, EventArgs e)
        {

        }

        protected void btnalmacenarproyecto_Click(object sender, EventArgs e)
        {
            AlmacenarProyetco();
        }

        protected void AlmacenarProyetco()
        {
            try
            {
                string nombre_proyecto = txtnombreproyecto.Value;
                string problema = txtproblema.Value;
                string proposito = txtproposito.Value;
                string finalidad = txtfinalidad.Value;

                if (ban_proyecto_id.Value == " ")
                {
                    int proyecto_id = _objProyecto.Add(problema, nombre_proyecto, proposito, finalidad);
                    ban_proyecto_id.Value = proyecto_id.ToString();
                }
                else
                {
                    int proyecto_id = Convert.ToInt32(ban_proyecto_id.Value);
                    _objProyecto.Update(proyecto_id, nombre_proyecto, problema, proposito, finalidad);
                }

                Response.Redirect("/BancoProyectos.aspx");
            }
            catch (Exception)
            { Response.Write("<script type='text/javascript'>alert('Opss... Ocurrio un error inesperado.');<script/>"); }
        }

        protected void ActualziarProyecto(int proyecto_id)
        {
            try
            {
                Model.Proyecto objProyecto_Obtenido = _objProyecto.GetProyecto(proyecto_id);
                if (objProyecto_Obtenido != null)
                {
                    txtnombreproyecto.Value = objProyecto_Obtenido.Proyecto1.ToString();
                    txtproblema.Value = objProyecto_Obtenido.Problema.ToString();
                    ban_proyecto_id.Value = objProyecto_Obtenido.Id.ToString();
                }
                else
                    Response.Write("alert('Opss... Ocurrio un error inesperado.');");
            }
            catch (Exception)
            {
                Response.Write("alert('Opss... Ocurrio un error inesperado.');");
            }
        }

        protected void CargarColeccionProyectos()
        {
            cmbproyectos.DataSource = _objProyecto.GetProyectos();
            cmbproyectos.DataTextField = "Proyecto1";
            cmbproyectos.DataValueField = "Id";
            cmbproyectos.DataBind();
        }

        protected void btncargarproyecto_Click(object sender, EventArgs e)
        {
            ActualziarProyecto(Convert.ToInt32(cmbproyectos.SelectedValue));
            Response.Write("$('#magazine').turn('next');");
        }

        protected void btncargar_proyecto_Click(object sender, EventArgs e)
        {

        }

        protected void btnalmacenarregistro_Click(object sender, EventArgs e)
        {
            Almacenar_Registro();
        }

        public void Almacenar_Registro()
        {
            try
            {
                Model.Registro_Proyecto proyecto_informacion = (from p in new Model.ESMBDDataContext().Registro_Proyectos
                                                                where p.proyecto_id == Convert.ToInt32(ban_proyecto_id.Value)
                                                                select p).Single();

                Objetos.CRegistro_Proyectos _objCRegistro_Proyectos = new Objetos.CRegistro_Proyectos();

                if (proyecto_informacion == null)
                    _objCRegistro_Proyectos.AddItem(txtcargo.Value, txtdependencia.Value, Convert.ToDateTime(txtfechaelaboracion.Value), txtjustificacion.Value, txtmpp1.Value, txtmpp2.Value, txtmpp3.Value, proyecto_id, txtresponsable.Value);
                else
                    _objCRegistro_Proyectos.UpdateItem(proyecto_informacion.Id, txtcargo.Value, txtdependencia.Value, Convert.ToDateTime(txtfechaelaboracion.Value), txtjustificacion.Value, txtmpp1.Value, txtmpp2.Value, txtmpp3.Value, txtresponsable.Value);

            }
            catch (Exception) { }
        }

        protected void btncargar_Click(object sender, EventArgs e)
        {
            ban_proyecto_id.Value = cmbproyectos.SelectedValue;
            if_c_e.Attributes.Add("src", "/jqgrid_causas_efectos.aspx?proyecto_id=" + ban_proyecto_id.Value);
            if_marco_logico.Attributes.Add("src", "/jqgrid_marco_logico.aspx?proyecto_id=" + ban_proyecto_id.Value);
            if_ejecucion.Attributes.Add("src", "/jqgrid_ejecucion.aspx?proyecto_id=" + ban_proyecto_id.Value);
            if_valores_indicadores.Attributes.Add("src", "jqgrid_valores.aspx?proyecto_id=" + ban_proyecto_id.Value);

            Model.Proyecto proyecto_informacion = (from p in new Model.ESMBDDataContext().Proyectos
                                                   where p.Id == Convert.ToInt32(ban_proyecto_id.Value)
                                                   select p).Single();

            txtnombreproyecto.Value = proyecto_informacion.Proyecto1;
            txtproblema.Value = proyecto_informacion.Problema;
            txtproposito.Value = proyecto_informacion.Proposito;
            txtfinalidad.Value = proyecto_informacion.Finalidad;

            proyecto_id = Convert.ToInt32(ban_proyecto_id.Value);

            CargarMarcoLogico();

            try
            {
                var registro_proyecto = proyecto_informacion.Registro_Proyectos.Single();

                txtdependencia.Value = registro_proyecto.Dependencia;
                txtfechaelaboracion.Value = registro_proyecto.Fecha.ToString();
                txtjustificacion.Value = registro_proyecto.Justificacion;
                txtmpp1.Value = registro_proyecto.Mpp_1;
                txtmpp2.Value = registro_proyecto.Mpp_2;
                txtmpp3.Value = registro_proyecto.Mpp_3;
                txtresponsable.Value = registro_proyecto.responsable;
                txtcargo.Value = registro_proyecto.Cargo;

                Objetos.CEfectos objCCausas_Efecto = new Objetos.CEfectos();

                var coleccion_causas_efectos = objCCausas_Efecto.getCausas_Efectos(Convert.ToInt32(ban_proyecto_id.Value));

                string html = "<li>" + proyecto_informacion.Problema + "<ul>";
                string causas_html = "<li>Causas<ul>";
                string efectos_html = "<li>Efectos<ul>";

                string html_objetivos = "<li>" + proyecto_informacion.Problema + "<ul>";
                string beneficios_html = "<li>Beneficios<ul>";
                string objetivos_html = "<li>Objetivos<ul>";

                foreach (var item in coleccion_causas_efectos.Take(3))
                {
                    causas_html = causas_html + "<li title='" + item.Causa + "'>" + item.Causa + "</li>";
                    efectos_html = efectos_html + "<li title='" + item.Efecto + "' >" + item.Efecto + "</li>";

                    string beneficio = item.Beneficios == null ? "No Asignado" : item.Beneficios;

                    beneficios_html = beneficios_html + "<li>" + beneficio + "</li>";
                    objetivos_html = objetivos_html + "<li>" + item.Causa + "</li>";
                }
                causas_html = causas_html + "</ul></li>";
                efectos_html = efectos_html + "</ul></li>";

                html = html + causas_html + efectos_html + "</ul></li>";

                org.InnerHtml = html;

                beneficios_html = beneficios_html + "</ul></li>";
                objetivos_html = objetivos_html + "</ul></li>";

                html_objetivos = html_objetivos + beneficios_html + objetivos_html + "</ul></li>";

                org_objetivos.InnerHtml = html_objetivos;

                refreshFiles(Convert.ToInt32(ban_proyecto_id.Value));
            }
            catch (Exception)
            {

            }

        }

        protected void UploadFiles()
        {
            try
            {
                string nombre_proyecto = txtnombreproyecto.Value.Substring(0, 15);
                FileUpload objFileUpload = files;
                #region Carga de Documento
                if (objFileUpload.PostedFile.FileName != "")
                {
                    string path = Server.MapPath("~/Files/Proyectos");

                    if (!Directory.Exists(path + "/" + nombre_proyecto))
                    {
                        Directory.CreateDirectory(path + "/" + nombre_proyecto);
                        Directory.CreateDirectory(path + "/" + nombre_proyecto + "/Actas");
                        path = path + "/" + nombre_proyecto + "/Actas";
                    }
                    else
                        path = path + "/" + nombre_proyecto + "/Actas";

                    path = path + "/" + objFileUpload.FileName;

                    int iniciar = objFileUpload.FileName.Length - 3;
                    int finalizar = objFileUpload.FileName.Length;

                    var extencion = objFileUpload.FileName.Substring(iniciar);

                    string strextencion = extencion.ToString();

                    if (strextencion != "pdf" && strextencion != "zip" && strextencion != "rar" && strextencion != "doc" && strextencion != "docx")
                    {
                        Response.Write("<script>alert('Error el archivo no pudo ser cargado, revise el tipo de documento que intenta cargar. Formatos compatibles:pdf,doc,zip,rar.');</script>");
                    }
                    else
                    {
                        bool update = false;
                        if (objFileUpload.PostedFile.ContentLength <= 5242880)
                        {
                            if (File.Exists(path))
                            {
                                update = true;
                                File.Delete(path);
                            }

                            objFileUpload.PostedFile.SaveAs(path);

                            if (!update)
                            {
                                if (new Objetos.Cproyecto().CargarDocumentos(objFileUpload.FileName, objFileUpload.PostedFile.ContentType, objFileUpload.PostedFile.ContentLength.ToString(), proyecto_id, "add", 0))
                                    ban_files.Value = "1";
                                else
                                    File.Delete(path);
                            }
                            else
                                ban_files.Value = "1";
                        }
                        else
                        {
                            Response.Write("<script>alert('Para cargar un archivo se debe tener en cuenta que el tamaño sea menor o igual a 5MB');</script>");
                        }
                    }
                }


                #endregion
            }
            catch (Exception)
            {
                Response.Write("<script>alert('Error al cargar el archivo seleccionado.');</script>");
            }
        }

        protected void btnuploadfile_Click(object sender, EventArgs e)
        {
            UploadFiles();
            refreshFiles(proyecto_id);

        }

        protected void refreshFiles(int Proyecto_id)
        {
            var files_proyect = from d_p in new Model.ESMBDDataContext().Documentos_Proyectos
                                where d_p.proyectoid == Proyecto_id
                                select d_p;

            string html_file = "";

            foreach (var item in files_proyect)
            {
                html_file += "<li><strong><a href='/Files/Proyectos/" + item.Proyecto.Proyecto1 + "/Actas/" + item.Ruta + "'>" + item.Ruta + "</a></strong> ( " + item.Tipo + " ) --  Tamaño: " + (Convert.ToInt32(item.Tamano) / 1024).ToString() + "<b>KB</b></li>";

            }

            list.InnerHtml = "<ul>" + html_file + "</ul>";
        }

        protected void btnExportarProyecto_Click(object sender, EventArgs e)
        {

            string nombre_proyecto = txtnombreproyecto.Value.Substring(0, 15);

            string path = Server.MapPath("~/Files/Proyectos/");

            if (Directory.Exists(path + nombre_proyecto))
            {
                if (!Directory.Exists(path + nombre_proyecto + "/Actas"))
                    Directory.CreateDirectory(path + nombre_proyecto + "/Actas");
                if (!Directory.Exists(path + nombre_proyecto + "/Programacion"))
                    Directory.CreateDirectory(path + nombre_proyecto + "/Programacion");
                if (!Directory.Exists(path + nombre_proyecto + "/M.S.E"))
                    Directory.CreateDirectory(path + nombre_proyecto + "/M.S.E");

                string path_files = path + nombre_proyecto + @"\Programacion\MarcoLogico.xls";
                string path_files_arbol_problemas = path + nombre_proyecto + @"\Programacion\ArbolProblemas.xls";
                string path_files_arbol_objetivos = path + nombre_proyecto + @"\Programacion\ArbolObjetivos.xls";

                string html_marcologico = generateMarcoLogico(proyecto_id);
                string html_arbolproblemas = generateArbolProblemas();
                string html_arbolobjetivos = generateArbolObjetivos();
                if (!File.Exists(path_files))
                {
                    StreamWriter objFile = new StreamWriter(path_files, true, Encoding.UTF8);
                    objFile.Write(html_marcologico);
                    objFile.Flush();
                    objFile.Close();
                }
                else
                {
                    File.Delete(path_files);

                    StreamWriter objFile = new StreamWriter(path_files, true, Encoding.UTF8);
                    objFile.Write(html_marcologico);
                    objFile.Flush();
                    objFile.Close();
                }

                if (!File.Exists(path_files_arbol_problemas))
                {
                    StreamWriter objFile = new StreamWriter(path_files_arbol_problemas, true, Encoding.UTF8);
                    objFile.Write(html_arbolproblemas);
                    objFile.Flush();
                    objFile.Close();
                }
                else
                {
                    File.Delete(path_files_arbol_problemas);

                    StreamWriter objFile = new StreamWriter(path_files_arbol_problemas, true, Encoding.UTF8);
                    objFile.Write(html_arbolproblemas);
                    objFile.Flush();
                    objFile.Close();
                }

                if (!File.Exists(path_files_arbol_objetivos))
                {
                    StreamWriter objFile = new StreamWriter(path_files_arbol_objetivos, true, Encoding.UTF8);
                    objFile.Write(html_arbolobjetivos);
                    objFile.Flush();
                    objFile.Close();
                }
                else
                {
                    File.Delete(path_files_arbol_objetivos);

                    StreamWriter objFile = new StreamWriter(path_files_arbol_objetivos, true, Encoding.UTF8);
                    objFile.Write(html_arbolobjetivos);
                    objFile.Flush();
                    objFile.Close();
                }
            }
            else
            {
                Directory.CreateDirectory(path + "/" + nombre_proyecto);

                if (!Directory.Exists(path + nombre_proyecto + "/Actas"))
                    Directory.CreateDirectory(path + nombre_proyecto + "/Actas");
                if (!Directory.Exists(path + nombre_proyecto + "/Programacion"))
                    Directory.CreateDirectory(path + nombre_proyecto + "/Programacion");
                if (!Directory.Exists(path + nombre_proyecto + "/M.S.E"))
                    Directory.CreateDirectory(path + nombre_proyecto + "/M.S.E");

                string path_files = path + nombre_proyecto + @"\Programacion\MarcoLogico.xls";
                string path_files_arbol_problemas = path + nombre_proyecto + @"\Programacion\ArbolProblemas.xls";
                string path_files_arbol_objetivos = path + nombre_proyecto + @"\Programacion\ArbolObjetivos.xls";

                string html_marcologico = generateMarcoLogico(proyecto_id);
                string html_arbolproblemas = generateArbolProblemas();
                string html_arbolobjetivos = generateArbolObjetivos();
                if (!File.Exists(path_files))
                {
                    StreamWriter objFile = new StreamWriter(path_files, true, Encoding.UTF8);
                    objFile.Write(html_marcologico);
                    objFile.Flush();
                    objFile.Close();
                }
                else
                {
                    File.Delete(path_files);

                    StreamWriter objFile = new StreamWriter(path_files, true, Encoding.UTF8);
                    objFile.Write(html_marcologico);
                    objFile.Flush();
                    objFile.Close();
                }

                if (!File.Exists(path_files_arbol_problemas))
                {
                    StreamWriter objFile = new StreamWriter(path_files_arbol_problemas, true, Encoding.UTF8);
                    objFile.Write(html_arbolproblemas);
                    objFile.Flush();
                    objFile.Close();
                }
                else
                {
                    File.Delete(path_files_arbol_problemas);

                    StreamWriter objFile = new StreamWriter(path_files_arbol_problemas, true, Encoding.UTF8);
                    objFile.Write(html_arbolproblemas);
                    objFile.Flush();
                    objFile.Close();
                }

                if (!File.Exists(path_files_arbol_objetivos))
                {
                    StreamWriter objFile = new StreamWriter(path_files_arbol_objetivos, true, Encoding.UTF8);
                    objFile.Write(html_arbolobjetivos);
                    objFile.Flush();
                    objFile.Close();
                }
                else
                {
                    File.Delete(path_files_arbol_objetivos);

                    StreamWriter objFile = new StreamWriter(path_files_arbol_objetivos, true, Encoding.UTF8);
                    objFile.Write(html_arbolobjetivos);
                    objFile.Flush();
                    objFile.Close();
                }

            }


            if (!File.Exists(path + nombre_proyecto + ".zip"))
            {
                using (var zip = new Ionic.Zip.ZipFile())
                {
                    zip.AddDirectory(path + "/" + nombre_proyecto);
                    zip.Save(path + nombre_proyecto + ".zip");
                }
            }
            else
            {
                File.Delete(path + nombre_proyecto + ".zip");

                using (var zip = new Ionic.Zip.ZipFile())
                {
                    zip.AddDirectory(path + "/" + nombre_proyecto);
                    zip.Save(path + nombre_proyecto + ".zip");
                }
            }


            Response.Redirect("/Files/Proyectos/" + nombre_proyecto + ".zip");
        }

        protected void CargarMarcoLogico()
        {
            content_marcologico.InnerHtml = generateMarcoLogico(proyecto_id);
        }

        protected string generateMarcoLogico(int Proyecto_id)
        {
            Model.ESMBDDataContext db = new Model.ESMBDDataContext();

            var procesos = from p in db.Causas_Efectos
                           where p.Proyecto_id == Proyecto_id
                           select p;

            string html = "<table style='border: 1px solid #000;'><caption>Marco Logico</caption>";
            html = html + "<tr ><td style='border: 1px solid #000;' vertical-align: middle;'>Proceso</td><td style='border: 1px solid #000;'>Subproceso</td><td style='border: 1px solid #000;'>Actividad</td></tr>";
            foreach (var procesos_item in procesos)
            {
                var subprocesos = from sp in db.Subprocesos
                                  where sp.Causas_Efecto.Proyecto_id == proyecto_id && sp.Proceso_id == procesos_item.Id
                                  select sp;

                html = html + "<tr ><td style='border: 1px solid #000;'>" + procesos_item.Causa + "</td><td style='border: 1px solid #000;'></td><td style='border: 1px solid #000;'></td></tr>";

                foreach (var subprocesos_item in subprocesos)
                {
                    var actividades = from a in db.Actividades
                                      where a.Subproceso.Causas_Efecto.Proyecto_id == proyecto_id && a.Subproceso_id == subprocesos_item.Id
                                      select a;

                    html = html + "<tr ><td style='border: 1px solid #000;'>" + procesos_item.Causa + "</td><td style='border: 1px solid #000;'>" + subprocesos_item.Subproceso1 + "</td><td style='border: 1px solid #000;'></td></tr>";

                    foreach (var actividades_item in actividades)
                    {
                        html = html + "<tr ><td style='border: 1px solid #000;'>" + procesos_item.Causa + "</td><td style='border: 1px solid #000;'>" + subprocesos_item.Subproceso1 + "</td><td style='border: 1px solid #000;' >" + actividades_item.Actividad + "</td></tr>";
                    }
                }
            }
            html = html + "</table>";

            return html;
        }

        protected string generateArbolProblemas()
        {
            var arbolproblemas = from p in new Model.ESMBDDataContext().Causas_Efectos
                                 where p.Proyecto_id == proyecto_id
                                 select p;

            string html_arbol_problemas = "<h1>Arbol Problemas</h1><h3>Efectos</h3><table><tr>";
            string problema = "";
            foreach (var item in arbolproblemas)
            {
                problema = item.Proyecto.Problema;
                html_arbol_problemas += "<td style='border: solid 2px #000; width: 100px;'>" + item.Efecto + "</td>";
            }

            html_arbol_problemas += "</tr><tr><td style='height:100px; text-align:center;' colspan='" + arbolproblemas.Count().ToString() + "'>" + problema + "</td></tr><tr>";

            foreach (var item in arbolproblemas)
            {
                problema = item.Proyecto.Problema;
                html_arbol_problemas += "<td style='border: solid 2px #000; width: 100px;'>" + item.Causa + "</td>";
            }

            html_arbol_problemas += "</tr><table><h3>Causas</h3>";

            return html_arbol_problemas;
        }

        protected string generateArbolObjetivos()
        {

            var arbolobjetivos = from p in new Model.ESMBDDataContext().Causas_Efectos
                                 where p.Proyecto_id == proyecto_id
                                 select p;

            string html_arbol_objetivos = "<h1>Arbol Objetivos</h1><h3>Beneficios</h3><table><tr>";
            string problema = "";
            foreach (var item in arbolobjetivos)
            {
                problema = item.Proyecto.Problema;
                html_arbol_objetivos += "<td style='border: solid 2px #000; width: 100px;'>" + item.Beneficios + "</td>";
            }

            html_arbol_objetivos += "</tr><tr><td style='height:100px; text-align:center;' colspan='" + arbolobjetivos.Count().ToString() + "'>" + problema + "</td></tr><tr>";

            foreach (var item in arbolobjetivos)
            {
                problema = item.Proyecto.Problema;
                html_arbol_objetivos += "<td style='border: solid 2px #000 ; width: 100px;'>" + item.Causa + "</td>";
            }

            html_arbol_objetivos += "</tr><table><h3>Objetivos</h3>";

            return html_arbol_objetivos;
        }

    }
}