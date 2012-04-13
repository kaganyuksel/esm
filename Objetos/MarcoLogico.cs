using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ESM.Model;
using System.Collections;

namespace ESM.Objetos
{


    public class MarcoLogico
    {
        public MarcoLogico()
        {

        }

    }

    public class CEfectos
    {
        #region Propiedades Publicas y Privadas

        protected ESM.Model.ESMBDDataContext _db = new Model.ESMBDDataContext();

        #endregion

        public CEfectos()
        {

        }

        public bool Add(string efecto, string causa, int idproyecto, string color)
        {
            try
            {
                Causas_Efecto objCausas_Efecto = new Causas_Efecto
                {

                    Efecto = efecto,
                    Causa = causa,
                    Color = color,
                    Proyecto_id = idproyecto

                };

                _db.Causas_Efectos.InsertOnSubmit(objCausas_Efecto);
                _db.SubmitChanges();
                return true;
            }
            catch (Exception) { return false; }
        }

        public bool Update(int id, string causa = null, string proceso = null, string indicador = null)
        {
            try
            {
                var efecto = (from e in _db.Causas_Efectos
                              where e.Id == id
                              select e).Single();

                if (causa != null)
                    efecto.Causa = causa;

                if (proceso != null)
                    efecto.Proceso = proceso;

                if (indicador != null)
                    efecto.Indicador_Resultado = indicador;

                _db.SubmitChanges();

                return true;
            }
            catch (Exception) { return false; }

        }

        public IQueryable<Causas_Efecto> getCount(int idproyecto)
        {
            try
            {
                var resultados = from ce in _db.Causas_Efectos
                                 where ce.Proyecto_id == idproyecto
                                 select ce;

                return resultados;
            }
            catch (Exception) { return null; }

        }

        public bool AddMedios(int resultadoid, int medioid)
        {
            try
            {
                Resultados_Medio objResultados_Medio = new Resultados_Medio
                {
                    Medios_de_verificacion_id = medioid,
                    Resultado_id = resultadoid
                };

                _db.Resultados_Medios.InsertOnSubmit(objResultados_Medio);
                _db.SubmitChanges();

                return true;
            }
            catch (Exception) { return false; }

        }

        public bool RemoveMedios(int resultadoid)
        {
            try
            {
                var medios = from r_m in _db.Resultados_Medios
                             where r_m.Resultado_id == resultadoid
                             select r_m;

                foreach (var item in medios)
                {
                    _db.Resultados_Medios.DeleteOnSubmit(item);
                }

                return true;
            }
            catch (Exception) { return false; }

        }

        public bool RemoveSupuestos(int resultadoid)
        {
            try
            {
                var supuestos = from r_s in _db.Resultados_Supuestos
                                where r_s.Resultado_id == resultadoid
                                select r_s;

                foreach (var item in supuestos)
                {
                    _db.Resultados_Supuestos.DeleteOnSubmit(item);
                }

                return true;
            }
            catch (Exception) { return false; }

        }

        public bool AddSupuestos(int resultadoid, int supuestoid)
        {
            try
            {
                Resultados_Supuesto objResultados_Supuesto = new Resultados_Supuesto
                {
                    Supuesto_id = supuestoid,
                    Resultado_id = resultadoid
                };

                _db.Resultados_Supuestos.InsertOnSubmit(objResultados_Supuesto);
                _db.SubmitChanges();

                return true;
            }
            catch (Exception) { return false; }

        }

        public string getEfectos(int proyecto_id)
        {
            string finalidad_proyecto = "";
            try
            {
                try
                {
                    finalidad_proyecto = (from p in _db.Proyectos
                                          where p.Id == proyecto_id
                                          select p.Finalidad).Single();
                }
                catch (Exception) { finalidad_proyecto = null; }



                if (finalidad_proyecto == null || finalidad_proyecto.Trim().Length == 0)
                {

                    string finalidad = "";
                    var efectos = from e in _db.Causas_Efectos
                                  where e.Proyecto_id == proyecto_id
                                  select e;

                    foreach (var item in efectos)
                    {
                        finalidad = finalidad + item.Efecto + ",";
                    }

                    finalidad.Trim(',');

                    return finalidad;
                }
                else
                {
                    return finalidad_proyecto;
                }
            }
            catch (Exception) { return null; }

        }
    }

    public class CMedios
    {
        #region Propiedades Privadas y Publicas

        protected ESMBDDataContext _db = new ESMBDDataContext();

        #endregion

        public int getMedioid(string Medio)
        {
            try
            {
                var medio = (from m in _db.Medios_de_verificacions
                             where m.Medio_de_verificacion.Contains(Medio)
                             select m.Id).Single();

                return medio;
            }
            catch (Exception) { return 0; }

        }

        public bool AddMedios(string medio)
        {
            try
            {
                Medios_de_verificacion objMedios = new Medios_de_verificacion
                {
                    Medio_de_verificacion = medio
                };

                _db.Medios_de_verificacions.InsertOnSubmit(objMedios);
                _db.SubmitChanges();

                return true;
            }
            catch (Exception) { return false; }

        }

        public IQueryable<Medios_de_verificacion> getMediosProyecto(int proyecto_id)
        {
            try
            {
                var m_p = from mp in _db.Proyectos_Medios
                          where mp.Proyecto_id == proyecto_id
                          select mp.Medios_de_verificacion;

                return m_p;
            }
            catch (Exception) { return null; }

        }

        public IQueryable<Medios_de_verificacion> getMediosResultado(int resultado_id)
        {
            try
            {
                var m_r = from mr in _db.Resultados_Medios
                          where mr.Resultado_id == resultado_id
                          select mr.Medios_de_verificacion;

                return m_r;

            }
            catch (Exception) { return null; }

        }

        public IQueryable<Medios_de_verificacion> getMediosActividad(int actividad_id)
        {
            try
            {
                var m_a = from ma in _db.Actividades_Medios
                          where ma.Actividad_id == actividad_id
                          select ma.Medios_de_verificacion;

                return m_a;
            }
            catch (Exception) { return null; }

        }

    }

    public class Csupuestos
    {
        #region Propiedades Privadas y Publicas

        protected ESMBDDataContext _db = new ESMBDDataContext();

        #endregion

        public int getSupuesto_id(string supuesto)
        {
            try
            {
                var supuestoid = (from s in _db.Supuestos
                                  where s.supuesto1.Contains(supuesto)
                                  select s.Id).Single();

                return supuestoid;
            }
            catch (Exception) { return 0; }

        }

        public bool AddSupuesto(string supuesto)
        {
            try
            {
                Supuesto objsupuesto = new Supuesto
                {
                    supuesto1 = supuesto
                };

                _db.Supuestos.InsertOnSubmit(objsupuesto);
                _db.SubmitChanges();

                return true;
            }
            catch (Exception) { return false; }

        }

        public IQueryable<Supuesto> getSupuestosProyecto(int proyecto_id)
        {
            try
            {
                var s_p = from sp in _db.Proyectos_Supuestos
                          where sp.Proyecto_supuesto_id == proyecto_id
                          select sp.Supuesto;

                return s_p;
            }
            catch (Exception) { return null; }

        }

        public IQueryable<Supuesto> getSupuestosResultado(int resultado_id)
        {
            try
            {
                var s_r = from sr in _db.Resultados_Supuestos
                          where sr.Resultado_id == resultado_id
                          select sr.Supuesto;

                return s_r;

            }
            catch (Exception) { return null; }

        }

        public IQueryable<Supuesto> getSupuestosActividad(int actividad_id)
        {
            try
            {
                var s_a = from sa in _db.Actividades_Supuestos
                          where sa.Actividad_id == actividad_id
                          select sa.Supuesto;

                return s_a;
            }
            catch (Exception) { return null; }

        }
    }

    public class Cresponsables
    {
        #region Propiedades Privadas y Publicas

        protected ESMBDDataContext _db = new ESMBDDataContext();

        #endregion

        public int getResponsable_id(string responsable)
        {
            try
            {
                var responsableid = (from s in _db.Usuarios
                                     where s.Nombre.Contains(responsable)
                                     select s.IdUsuario).Single();

                return responsableid;
            }
            catch (Exception) { return 0; }

        }

        public bool AddResponsable(string responsable)
        {
            try
            {
                Responsable objresponsable = new Responsable
                {
                    Responsable1 = responsable
                };

                _db.Responsables.InsertOnSubmit(objresponsable);
                _db.SubmitChanges();

                return true;
            }
            catch (Exception) { return false; }

        }

        public IQueryable<Usuario> getResponsablesActividad(int actividad_id)
        {
            try
            {
                var s_a = from sa in _db.Actividades_Responsables
                          where sa.Actividad_id == actividad_id
                          select sa.Usuario;

                return s_a;
            }
            catch (Exception) { return null; }

        }
    }

    public class Cproyecto
    {
        #region Propiedades Publicas y Privadas

        protected ESMBDDataContext _db = new ESMBDDataContext();

        #endregion

        public IQueryable<Proyecto> GetProyectos()
        {
            try
            {
                var proyectos_coleccion = from p in _db.Proyectos
                                          select p;
                return proyectos_coleccion;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Proyecto GetProyecto(int proyecto_id)
        {
            try
            {
                var proyecto_consulta = (from p in _db.Proyectos
                                         where p.Id == proyecto_id
                                         select p).Single();

                return proyecto_consulta;
            }
            catch (Exception) { return null; }

        }

        public string getname(int proyecto_id)
        {
            try
            {
                string name = (from p in _db.Proyectos
                               where p.Id == proyecto_id
                               select p.Proyecto1).Single();
                return name;
            }
            catch (Exception) { return null; }

        }

        public int Add(string problema, string nombre_proyecto)
        {
            try
            {
                Proyecto objProyecto = new Proyecto
                {
                    Proyecto1 = nombre_proyecto,
                    Problema = problema
                };

                _db.Proyectos.InsertOnSubmit(objProyecto);
                _db.SubmitChanges();

                return objProyecto.Id;
            }
            catch (Exception) { return 0; }

        }

        public bool Update(int idproyecto, string indicador = null, string proposito = null, string finalidad = null)
        {
            try
            {
                var proyecto = (from p in _db.Proyectos
                                where p.Id == idproyecto
                                select p).Single();

                if (indicador != null)
                    proyecto.Indicador = indicador;

                if (proposito != null)
                    proyecto.Proposito = proposito;

                if (finalidad != null)
                    proyecto.Finalidad = finalidad;

                _db.SubmitChanges();

                return true;
            }
            catch (Exception) { return false; }

        }

        public bool AddMedios(int proyectoid, int medioid)
        {
            try
            {
                Proyectos_Medio objProyectos_Medio = new Proyectos_Medio
                {
                    Medios_de_verificacion_id = medioid,
                    Proyecto_id = proyectoid
                };

                _db.Proyectos_Medios.InsertOnSubmit(objProyectos_Medio);
                _db.SubmitChanges();

                return true;
            }
            catch (Exception) { return false; }

        }

        public bool RemoveMedios(int proyectoid)
        {
            try
            {
                var medios = from p_m in _db.Proyectos_Medios
                             where p_m.Proyecto_id == proyectoid
                             select p_m;

                foreach (var item in medios)
                {
                    _db.Proyectos_Medios.DeleteOnSubmit(item);
                }
                _db.SubmitChanges();
                return true;
            }
            catch (Exception) { return false; }

        }

        public bool RemoveSupuestos(int proyectoid)
        {
            try
            {
                var supuestos = from p_s in _db.Proyectos_Supuestos
                                where p_s.Proyecto_supuesto_id == proyectoid
                                select p_s;

                foreach (var item in supuestos)
                {
                    _db.Proyectos_Supuestos.DeleteOnSubmit(item);
                }
                _db.SubmitChanges();
                return true;
            }
            catch (Exception) { return false; }

        }

        public bool AddSupuestos(int proyectoid, int supuestoid)
        {
            try
            {
                Proyectos_Supuesto objProyectos_Supuestos = new Proyectos_Supuesto
                {
                    Supuesto_id = supuestoid,
                    Proyecto_supuesto_id = proyectoid
                };

                _db.Proyectos_Supuestos.InsertOnSubmit(objProyectos_Supuestos);
                _db.SubmitChanges();

                return true;
            }
            catch (Exception) { return false; }

        }

        public static string CargarProposito(int idproyecto)
        {
            try
            {
                string proposito = (from p in new ESMBDDataContext().Proyectos
                                    where p.Id == idproyecto
                                    select p.Proposito).Single();

                return proposito;
            }
            catch (Exception) { return null; }

        }
    }

    public class CActividades
    {
        #region Propiedades Publicas y Privadas

        protected ESMBDDataContext _db = new ESMBDDataContext();

        #endregion

        public IQueryable getMed_Indicador(int indicador_id)
        {
            try
            {
                var mediciones_indicador = from m in _db.Indicadores_Metas
                                           where m.Indicador_id == indicador_id
                                           select new { m.Id, Fecha = m.Fecha_Meta, Meta = m.Meta, Ejecutado = m.Ejecutado == null ? 0 : m.Ejecutado };


                return mediciones_indicador;
            }
            catch (Exception) { return null; }

        }

        public string getActividad_name(int Actividad_id)
        {
            try
            {
                var mediciones_presupuesto = (from m in _db.Actividades
                                              where m.Id == Actividad_id
                                              select m.Actividad).Single();


                return mediciones_presupuesto;
            }
            catch (Exception) { return null; }

        }

        public IQueryable getMed_presupuesto(int Actividad_id)
        {
            try
            {
                var mediciones_presupuesto = from m in _db.actividades_presupuestos
                                             where m.actividad_id == Actividad_id
                                             select new { m.Id, Fecha = m.fecha, Meta = m.meta, Ejecutado = m.ejecutado == null ? 0 : m.ejecutado };


                return mediciones_presupuesto;
            }
            catch (Exception) { return null; }

        }

        public bool AddMeta_Valor(int indicador_id, DateTime fecha, int meta)
        {
            try
            {
                Indicadores_Meta objMeta = new Indicadores_Meta
                {
                    Indicador_id = indicador_id,
                    Fecha_Meta = fecha,
                    Meta = meta
                };



                _db.Indicadores_Metas.InsertOnSubmit(objMeta);
                //_db.Indicadores_Valors.InsertOnSubmit(objValor);

                _db.SubmitChanges();

                return true;
            }
            catch (Exception) { return false; }

        }

        public bool AddMeta_presupuesto(int Actividad_id, DateTime Fecha, decimal Meta)
        {
            try
            {
                actividades_presupuesto objMeta = new actividades_presupuesto
                {
                    actividad_id = Actividad_id,
                    fecha = Fecha,
                    meta = Meta
                };

                _db.actividades_presupuestos.InsertOnSubmit(objMeta);

                _db.SubmitChanges();

                return true;
            }
            catch (Exception) { return false; }

        }

        public bool UpdateMeta_presupuesto(object idpresupuesto, int Actividad_id, Decimal valor, DateTime fecha)
        {
            try
            {

                var metas_valores = from m in _db.actividades_presupuestos
                                    where m.Id == Convert.ToInt32(idpresupuesto)
                                    select m;

                if (metas_valores.Count() == 0)
                {
                    actividades_presupuesto objValor = new actividades_presupuesto
                    {
                        actividad_id = Actividad_id,
                        fecha = fecha,
                        ejecutado = valor,
                    };

                    _db.actividades_presupuestos.InsertOnSubmit(objValor);
                }
                else
                {

                    actividades_presupuesto _objvalor = (from m in _db.actividades_presupuestos
                                                         where m.Id == Convert.ToInt32(idpresupuesto)
                                                         select m).Single();

                    _objvalor.ejecutado = valor;

                }

                _db.SubmitChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public bool UpdateMeta(object idmeta, int indicador_id, int valor, DateTime fecha)
        {
            try
            {

                var metas_valores = from m in _db.Indicadores_Metas
                                    where m.Id == Convert.ToInt32(idmeta)
                                    select m;

                if (metas_valores.Count() == 0)
                {
                    Indicadores_Meta objValor = new Indicadores_Meta
                    {
                        Indicador_id = indicador_id,
                        Fecha_Meta = fecha,
                        Ejecutado = valor,
                    };

                    _db.Indicadores_Metas.InsertOnSubmit(objValor);
                }
                else
                {

                    Indicadores_Meta _objvalor = (from m in _db.Indicadores_Metas
                                                  where m.Id == Convert.ToInt32(idmeta)
                                                  select m).Single();

                    _objvalor.Ejecutado = valor;

                }

                _db.SubmitChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<object[,]> getIndicadoresVencidos(int usuario_id)
        {
            try
            {
                List<object[,]> objList = new List<object[,]>();

                var actividades_usuario = (from u in _db.Actividades_Responsables
                                           where u.Usuario_id == usuario_id
                                           select u).Distinct();

                foreach (var item in actividades_usuario)
                {
                    var indicadores_vencidos = from ind in _db.Indicadores
                                               where ind.Actividad_id == item.Actividad_id && ind.fecha_indicador_final < DateTime.Now
                                               select ind;

                    object[,] caracteristicas_indicador = new object[indicadores_vencidos.Count(), 4];
                    int indicador_actual = 0;

                    foreach (var item_indicadores in indicadores_vencidos)
                    {
                        caracteristicas_indicador[indicador_actual, 0] = item_indicadores.Actividade.Resultados_Proyecto.Subproceso.Causas_Efecto.Proyecto.Problema;
                        caracteristicas_indicador[indicador_actual, 1] = item_indicadores.Actividade.Actividad;
                        caracteristicas_indicador[indicador_actual, 2] = item_indicadores.Indicador;
                        caracteristicas_indicador[indicador_actual, 3] = item_indicadores.Fecha_Creacion;

                        indicador_actual++;
                    }

                    objList.Add(caracteristicas_indicador);
                }

                return objList;
            }
            catch (Exception) { return null; }

        }

        public List<object[,]> getIndicadoresVencidosPorVencer(int usuario_id)
        {
            try
            {
                List<object[,]> objList = new List<object[,]>();

                var actividades_usuario = (from u in _db.Actividades_Responsables
                                           where u.Usuario_id == usuario_id
                                           select u).Distinct();

                foreach (var item in actividades_usuario)
                {
                    var indicadores_vencidos = from ind in _db.Indicadores
                                               where ind.Actividad_id == item.Actividad_id && ind.fecha_indicador_final >= DateTime.Now && ind.fecha_indicador_final <= DateTime.Now.AddDays(5)
                                               select ind;

                    object[,] caracteristicas_indicador = new object[indicadores_vencidos.Count(), 4];
                    int indicador_actual = 0;

                    foreach (var item_indicadores in indicadores_vencidos)
                    {
                        caracteristicas_indicador[indicador_actual, 0] = item_indicadores.Actividade.Resultados_Proyecto.Subproceso.Causas_Efecto.Proyecto.Problema;
                        caracteristicas_indicador[indicador_actual, 1] = item_indicadores.Actividade.Actividad;
                        caracteristicas_indicador[indicador_actual, 2] = item_indicadores.Indicador;
                        caracteristicas_indicador[indicador_actual, 3] = item_indicadores.Fecha_Creacion;

                        indicador_actual++;
                    }

                    objList.Add(caracteristicas_indicador);
                }

                return objList;
            }
            catch (Exception) { return null; }

        }


        public int Add(int idresultado, string actividad, float presupuesto, string Fecha = "")
        {
            try
            {
                Actividade objActividade = new Actividade
                {
                    Resultado_id = idresultado,
                    Actividad = actividad,
                    Presupuesto = presupuesto,
                    fecha = Fecha == "" ? DateTime.Now : Convert.ToDateTime(Fecha)
                };

                _db.Actividades.InsertOnSubmit(objActividade);
                _db.SubmitChanges();

                return objActividade.Id;
            }
            catch (Exception) { return 0; }

        }

        public bool Update(int actividadid, string actividad = null, float presupuesto = 0, string fecha = "")
        {
            try
            {
                var actividad_consulta = (from a in _db.Actividades
                                          where a.Id == actividadid
                                          select a).Single();

                if (actividad != null)
                    actividad_consulta.Actividad = actividad;

                if (presupuesto != 0)
                    actividad_consulta.Presupuesto = presupuesto;

                if (fecha != null && fecha != "")
                    actividad_consulta.fecha = Convert.ToDateTime(fecha);


                _db.SubmitChanges();

                return true;
            }
            catch (Exception) { return false; }

        }

        public bool AddIndicador(int idactividad, string indicador, int verboid, int unidadid, DateTime fecha_inicial, DateTime fecha_final, int meta_numero, bool esSSP)
        {
            try
            {
                Indicadore objIndicadore = new Indicadore
                {
                    Actividad_id = idactividad,
                    Indicador = indicador,
                    fecha_indicador_inicial = fecha_inicial,
                    fecha_indicador_final = fecha_final,
                    Fecha_Creacion = DateTime.Now.Date,
                    meta = meta_numero,
                    verbo_id = verboid,
                    unidad_id = unidadid,
                    SSP = esSSP
                };

                _db.Indicadores.InsertOnSubmit(objIndicadore);
                _db.SubmitChanges();

                return true;
            }
            catch (Exception) { return false; }

        }

        public bool UpdateIndicador(int indicadorid, string indicador, int verboid, int unidadid, DateTime fecha_inicial, DateTime fecha_final, int meta, bool esSSP)
        {
            try
            {

                var indicador_actividad = (from i in _db.Indicadores
                                           where i.Id == indicadorid
                                           select i).Single();

                indicador_actividad.Indicador = indicador;
                indicador_actividad.fecha_indicador_inicial = fecha_inicial;
                indicador_actividad.fecha_indicador_final = fecha_final;
                indicador_actividad.Fecha_Creacion = DateTime.Now.Date.AddHours(3);
                indicador_actividad.verbo_id = verboid;
                indicador_actividad.unidad_id = unidadid;
                indicador_actividad.SSP = esSSP;

                _db.SubmitChanges();

                return true;
            }
            catch (Exception) { return false; }

        }

        public bool AddMedios(int actividadid, int medioid)
        {
            try
            {
                Actividades_Medio objActividades_Medio = new Actividades_Medio
                {
                    Medios_de_verificacion_id = medioid,
                    Actividad_id = actividadid
                };

                _db.Actividades_Medios.InsertOnSubmit(objActividades_Medio);
                _db.SubmitChanges();

                return true;
            }
            catch (Exception) { return false; }

        }

        public bool RemoveMedios(int actividadid)
        {
            try
            {
                var medios = from a_m in _db.Actividades_Medios
                             where a_m.Actividad_id == actividadid
                             select a_m;

                foreach (var item in medios)
                {
                    _db.Actividades_Medios.DeleteOnSubmit(item);
                }
                _db.SubmitChanges();
                return true;
            }
            catch (Exception) { return false; }

        }

        public bool RemoveSupuestos(int resultadoid)
        {
            try
            {
                var supuestos = from a_s in _db.Actividades_Supuestos
                                where a_s.Actividad_id == resultadoid
                                select a_s;

                foreach (var item in supuestos)
                {
                    _db.Actividades_Supuestos.DeleteOnSubmit(item);
                }

                _db.SubmitChanges();

                return true;
            }
            catch (Exception) { return false; }

        }

        public bool RemoveResponsables(int resultadoid)
        {
            try
            {
                var responsables = from a_s in _db.Actividades_Responsables
                                   where a_s.Actividad_id == resultadoid
                                   select a_s;

                foreach (var item in responsables)
                {
                    _db.Actividades_Responsables.DeleteOnSubmit(item);
                }

                _db.SubmitChanges();

                return true;
            }
            catch (Exception) { return false; }

        }

        public bool AddSupuestos(int actividadid, int supuestoid)
        {
            try
            {
                Actividades_Supuesto objActividades_Supuesto = new Actividades_Supuesto
                {
                    Supuesto_id = supuestoid,
                    Actividad_id = actividadid
                };

                _db.Actividades_Supuestos.InsertOnSubmit(objActividades_Supuesto);
                _db.SubmitChanges();

                return true;
            }
            catch (Exception) { return false; }

        }

        public bool AddResponsables(int actividadid, int responsableid)
        {
            try
            {
                Actividades_Responsable objActividades_Responsable = new Actividades_Responsable
                {
                    Usuario_id = responsableid,
                    Actividad_id = actividadid
                };

                _db.Actividades_Responsables.InsertOnSubmit(objActividades_Responsable);
                _db.SubmitChanges();

                return true;
            }
            catch (Exception) { return false; }

        }

        public IQueryable<Indicadore> getIndicadores(int actividad_id)
        {
            try
            {
                var indicadores = from ind in _db.Indicadores
                                  where ind.Actividad_id == actividad_id
                                  select ind;

                return indicadores;
            }
            catch (Exception) { return null; }

        }

        public IQueryable<Actividade> getActividades(int resultado_id)
        {
            try
            {
                var actividades_consulta = from a in new ESM.Model.ESMBDDataContext().Actividades
                                           where a.Resultado_id == resultado_id
                                           select a;

                return actividades_consulta;
            }
            catch (Exception) { return null; }

        }
    }

    public class CSubprocesos
    {
        #region Propiedades Publicas y Privadas

        protected ESMBDDataContext _db = new ESMBDDataContext();

        #endregion

        public int Add(int idproceso, string subproceso, string indicador = null)
        {
            try
            {
                Subproceso objSubproceso = new Subproceso
                {
                    Proceso_id = idproceso,
                    Subproceso1 = subproceso,
                    Indicador = indicador
                };

                _db.Subprocesos.InsertOnSubmit(objSubproceso);
                _db.SubmitChanges();

                return objSubproceso.Id;
            }
            catch (Exception) { return 0; }

        }

        public bool Update(int subprocesoid, string subproceso = null, string indicador = null)
        {
            try
            {
                var subproceso_consulta = (from a in _db.Subprocesos
                                           where a.Id == subprocesoid
                                           select a).Single();

                if (subproceso != null)
                    subproceso_consulta.Subproceso1 = subproceso;

                if (indicador != null)
                    subproceso_consulta.Indicador = indicador;


                _db.SubmitChanges();

                return true;
            }
            catch (Exception) { return false; }

        }

        public bool AddMedios(int subprocesoid, int medioid)
        {
            try
            {
                Subprocesos_Medios_Verificacion objSubprocesos_Medios_Verificacion = new Subprocesos_Medios_Verificacion
                {
                    Medio_Verificacion_id = medioid,
                    Subproceso_id = subprocesoid
                };

                _db.Subprocesos_Medios_Verificacions.InsertOnSubmit(objSubprocesos_Medios_Verificacion);
                _db.SubmitChanges();

                return true;
            }
            catch (Exception) { return false; }

        }

        public bool RemoveMedios(int subprocesoid)
        {
            try
            {
                var medios = from a_m in _db.Subprocesos_Medios_Verificacions
                             where a_m.Subproceso_id == subprocesoid
                             select a_m;

                foreach (var item in medios)
                {
                    _db.Subprocesos_Medios_Verificacions.DeleteOnSubmit(item);
                }
                _db.SubmitChanges();
                return true;
            }
            catch (Exception) { return false; }

        }

        public bool RemoveSupuestos(int subprocesoid)
        {
            try
            {
                var supuestos = from a_s in _db.Subprocesos_Medios_Verificacions
                                where a_s.Subproceso_id == subprocesoid
                                select a_s;

                foreach (var item in supuestos)
                {
                    _db.Subprocesos_Medios_Verificacions.DeleteOnSubmit(item);
                }

                _db.SubmitChanges();

                return true;
            }
            catch (Exception) { return false; }

        }

        public bool AddSupuestos(int subprocesoid, int supuestoid)
        {
            try
            {
                Subprocesos_Supuesto objSubprocesos_Supuestos = new Subprocesos_Supuesto
                {
                    Supuestos_id = supuestoid,
                    Subproceso_id = subprocesoid
                };

                _db.Subprocesos_Supuestos.InsertOnSubmit(objSubprocesos_Supuestos);
                _db.SubmitChanges();

                return true;
            }
            catch (Exception) { return false; }

        }

        public IQueryable<Subproceso> LoadSubprocesos(int proceso_id)
        {
            try
            {
                var subprocesos = from s in _db.Subprocesos
                                  where s.Proceso_id == proceso_id
                                  select s;

                return subprocesos;
            }
            catch (Exception) { return null; }

        }

        public IQueryable<Causas_Efecto> getProcesos(int proyecto_id)
        {
            try
            {
                var procesos = (from p in _db.Causas_Efectos
                                where p.Proyecto_id == proyecto_id
                                select p).Distinct();

                return procesos;
            }
            catch (Exception) { return null; }

        }
    }


    public class CResultados_proyecto
    {
        #region Propiedades Publicas y Privadas

        protected ESMBDDataContext _db = new ESMBDDataContext();

        #endregion

        public int Add(int idsubproceso, string resultado, string indicador = null)
        {
            try
            {
                Resultados_Proyecto objResultados_Proyecto = new Resultados_Proyecto
                {
                    Subproceso_id = idsubproceso,
                    Resultado = resultado,
                    Indicador = indicador
                };

                _db.Resultados_Proyectos.InsertOnSubmit(objResultados_Proyecto);
                _db.SubmitChanges();

                return objResultados_Proyecto.Id;
            }
            catch (Exception) { return 0; }

        }

        public bool Update(int resultado_id, string resultado = null, string indicador = null)
        {
            try
            {
                var resultado_consulta = (from a in _db.Resultados_Proyectos
                                          where a.Id == resultado_id
                                          select a).Single();

                if (resultado != null)
                    resultado_consulta.Resultado = resultado;

                if (indicador != null)
                    resultado_consulta.Indicador = indicador;

                _db.SubmitChanges();

                return true;
            }
            catch (Exception) { return false; }

        }

        public bool AddMedios(int resultado_id, int medioid)
        {
            try
            {
                Resultados_Medio objResultados_Medio = new Resultados_Medio
                {
                    Medios_de_verificacion_id = medioid,
                    Resultado_id = resultado_id
                };

                _db.Resultados_Medios.InsertOnSubmit(objResultados_Medio);
                _db.SubmitChanges();

                return true;
            }
            catch (Exception) { return false; }

        }

        public bool RemoveMedios(int resultadoid)
        {
            try
            {
                var medios = from a_m in _db.Resultados_Medios
                             where a_m.Resultado_id == resultadoid
                             select a_m;

                foreach (var item in medios)
                {
                    _db.Resultados_Medios.DeleteOnSubmit(item);
                }
                _db.SubmitChanges();
                return true;
            }
            catch (Exception) { return false; }

        }

        public bool RemoveSupuestos(int resultado_id)
        {
            try
            {
                var supuestos = from a_s in _db.Resultados_Supuestos
                                where a_s.Resultado_id == resultado_id
                                select a_s;

                foreach (var item in supuestos)
                {
                    _db.Resultados_Supuestos.DeleteOnSubmit(item);
                }

                _db.SubmitChanges();

                return true;
            }
            catch (Exception) { return false; }

        }

        public bool AddSupuestos(int resultado_id, int supuestoid)
        {
            try
            {
                Resultados_Supuesto objResultados_Supuesto = new Resultados_Supuesto
                {
                    Supuesto_id = supuestoid,
                    Resultado_id = resultado_id
                };

                _db.Resultados_Supuestos.InsertOnSubmit(objResultados_Supuesto);
                _db.SubmitChanges();

                return true;
            }
            catch (Exception) { return false; }

        }

        public IQueryable<Resultados_Proyecto> LoadResultados(int subproceso_id)
        {
            try
            {
                var resultados = from s in _db.Resultados_Proyectos
                                 where s.Subproceso_id == subproceso_id
                                 select s;

                return resultados;
            }
            catch (Exception) { return null; }

        }

        public IQueryable<Subproceso> getSubprocesos(int proceso_id)
        {
            try
            {
                var subprocesos = (from p in _db.Subprocesos
                                   where p.Proceso_id == proceso_id
                                   select p).Distinct();

                return subprocesos;
            }
            catch (Exception) { return null; }

        }
    }

    public class CMatriz_Actores
    {

        #region Propiedades Publicas y Privadas

        protected ESMBDDataContext _db = new ESMBDDataContext();

        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        public CMatriz_Actores()
        {

        }

        public bool AddItem(string grupos, string interes, string problema_percibido, string recursos_mandatos, int proyecto_id)
        {
            try
            {
                Matriz_Actore objMatriz_Actores = new Matriz_Actore()
                {
                    Grupos = grupos,
                    Interes = interes,
                    Problema_Percibido = problema_percibido,
                    Recursos_Mandatos = recursos_mandatos,
                    proyecto_id = proyecto_id
                };

                _db.Matriz_Actores.InsertOnSubmit(objMatriz_Actores);
                _db.SubmitChanges();

                return true;
            }
            catch (Exception) { return false; }
        }

        public bool UpdateItem(int matriz_id, string grupos, string interes, string problema_percibido, string recursos_mandatos)
        {
            try
            {
                var element_return = (from m_a in _db.Matriz_Actores
                                      where m_a.Id == matriz_id
                                      select m_a).Single();
                element_return.Grupos = grupos;
                element_return.Interes = interes;
                element_return.Problema_Percibido = problema_percibido;
                element_return.Recursos_Mandatos = recursos_mandatos;

                _db.SubmitChanges();

                return true;
            }
            catch (Exception) { return false; }
        }



    }

    public class CFuentes_Financiacion
    {

        #region Propiedades Publicas y Privadas

        protected ESMBDDataContext _db = new ESMBDDataContext();

        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        public CFuentes_Financiacion()
        {

        }

        public bool AddItem(string entidad, string tipo_entidad, string tipo_recurso, int proyecto_id)
        {
            try
            {
                Fuentes_Financiacion objFuentes_Financiacion = new Fuentes_Financiacion()
                {
                    Entidad = entidad,
                    Tipo_Entidad = tipo_entidad,
                    Tipo_Recurso = tipo_recurso,
                    proyecto_id = proyecto_id
                };

                _db.Fuentes_Financiacions.InsertOnSubmit(objFuentes_Financiacion);
                _db.SubmitChanges();

                return true;
            }
            catch (Exception) { return false; }
        }

        public bool UpdateItem(int fuentes_id, string entidad, string tipo_entidad, string tipo_recurso, int proyecto_id)
        {
            try
            {
                var element_return = (from f_f in _db.Fuentes_Financiacions
                                      where f_f.Id == fuentes_id
                                      select f_f).Single();

                element_return.Entidad = entidad;
                element_return.Tipo_Recurso = tipo_entidad;
                element_return.Tipo_Recurso = tipo_recurso;

                _db.SubmitChanges();

                return true;
            }
            catch (Exception) { return false; }
        }



    }

    public class CRegistro_Proyectos
    {

        #region Propiedades Publicas y Privadas

        protected ESMBDDataContext _db = new ESMBDDataContext();

        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        public CRegistro_Proyectos()
        {

        }

        public bool AddItem(string cargo, string dependencia, DateTime fecha, string justificacion, string mpp_1, string mpp_2, string mpp_3, int proyecto_id)
        {
            try
            {
                Registro_Proyecto objRegistro_Proyectos = new Registro_Proyecto()
                {
                    //Registro de Proyecto
                    Cargo = cargo,
                    Dependencia = dependencia,
                    Fecha = fecha,
                    //Justificación
                    Justificacion = justificacion,
                    //Marco de Política Publica
                    Mpp_1 = mpp_1,
                    Mpp_2 = mpp_2,
                    Mpp_3 = mpp_3,

                    proyecto_id = proyecto_id
                };

                _db.Registro_Proyectos.InsertOnSubmit(objRegistro_Proyectos);
                _db.SubmitChanges();

                return true;
            }
            catch (Exception) { return false; }
        }

        public bool UpdateItem(int registro_id, string cargo, string dependencia, DateTime fecha, string justificacion, string mpp_1, string mpp_2, string mpp_3)
        {
            try
            {
                var element_return = (from r_p in _db.Registro_Proyectos
                                      where r_p.Id == registro_id
                                      select r_p).Single();

                //Registro de Proyecto
                element_return.Cargo = cargo;
                element_return.Dependencia = dependencia;
                element_return.Fecha = fecha;
                //Justificación
                element_return.Justificacion = justificacion;
                //Marco Política Publica
                element_return.Mpp_1 = mpp_1;
                element_return.Mpp_2 = mpp_2;
                element_return.Mpp_3 = mpp_3;

                _db.SubmitChanges();

                return true;
            }
            catch (Exception) { return false; }
        }



    }
}