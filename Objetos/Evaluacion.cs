using System.Data;
using System.Data.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ESM.Model;
using System.Web.UI.WebControls;
using System;

namespace EvaluationSettings
{
    /// <summary>
    /// Objeto que almacena la informacion y parametrizacion de la evaluacio para ESM
    /// </summary>
    public class CEvaluacion
    {
        #region Propiedades Privadas y Publicas

        private string _error;

        public string Error
        {
            get { return _error; }
        }


        private ESMBDDataContext db = new ESMBDDataContext();

        /// <summary>
        /// Almacena el valor que indica que la evaluacion sera presentada por un estudiante privada
        /// </summary>
        private bool _estudiantes = false;
        /// <summary>
        /// Almacena el valor que indica que la evaluacion sera presentada por un estudiante publica
        /// </summary>
        public bool Estudiantes
        {
            get { return _estudiantes; }
            set { _estudiantes = value; }
        }
        /// <summary>
        /// Almacena el valor que indica que la evaluacion sera presentada por un docente privada
        /// </summary>
        private bool _docentes = false;
        /// <summary>
        /// Almacena el valor que indica que la evaluacion sera presentada por un Docente publica
        /// </summary>
        public bool Docentes
        {
            get { return _docentes; }
            set { _docentes = value; }
        }
        /// <summary>
        /// Almacena el valor que indica que la evaluacion sera presentada por una Secretaria de Educacion privada
        /// </summary>
        private bool _secretariaEducacion = false;
        /// <summary>
        /// Almacena el valor que indica que la evaluacion sera presentada por una Secretaria de Educacion Publica
        /// </summary>
        public bool SecretariaEducacion
        {
            get { return _secretariaEducacion; }
            set { _secretariaEducacion = value; }
        }
        /// <summary>
        /// Almacena el valor que indica que la evaluacion sera presentada por un Padre privada
        /// </summary>
        private bool _padres = false;
        /// <summary>
        /// Almacena el valor que indica que la evaluacion sera presentada por un Padre Publica
        /// </summary>
        public bool Padres
        {
            get { return _padres; }
            set { _padres = value; }
        }
        /// <summary>
        /// Almacena el valor que indica que la evaluacion sera presentada por un Profesional privada
        /// </summary>
        private bool _profesional = false;
        /// <summary>
        /// Almacena el valor que indica que la evaluacion sera presentada por un Profesioanl publica
        /// </summary>
        public bool Profesional
        {
            get { return _profesional; }
            set { _profesional = value; }
        }
        /// <summary>
        /// Almacena el valor que indica que la evaluacion sera presentada por un Directivo privada
        /// </summary>
        private bool _directivos;
        /// <summary>
        /// Almacena el valor que indica que la evaluacion sera presentada por un Directivo publica
        /// </summary>
        public bool Directivos
        {
            get { return _directivos; }
            set { _directivos = value; }
        }

        private int _idEvaluacion;

        public int IdEvaluacion
        {
            get { return _idEvaluacion; }
        }

        #endregion

        /// <summary>
        /// Constructor de la Clase CEvaluacion
        /// </summary>
        public CEvaluacion()
        {

        }

        /// <summary>
        /// Obtiene Evaluacion basado en el metodo ObtenerEvaluacion()
        /// </summary>
        /// <returns>IQueryable con la informacion de la evaluacion segun el actor seleccionado</returns>
        public List<IQueryable<Preguntas>> LoadEvaluation()
        {
            return ObtenerEvaluacion();
        }

        /// <summary>
        /// Obtiene la coleccion de preguntas que se va a mostrar para cada uno de los actores
        /// </summary>
        /// <returns>Coleccion de preguntas para el actor deseado</returns>
        private List<IQueryable<Preguntas>> ObtenerEvaluacion()
        {
            try
            {
                var rAmbientes = (from a in db.Ambientes
                                  select new { a.IdAmbiente }).Distinct();

                //Valida si la evaluacion sera aplicada a Directivos
                if (_directivos)
                {
                    List<IQueryable<Preguntas>> objlist = new List<IQueryable<Preguntas>>();

                    foreach (var item in rAmbientes)
                    {
                        objlist.Add(PreguntasDirectivos(item.IdAmbiente));
                    }

                    return objlist;

                }
                //Valida si la evaluacion sera aplicada a Estudiantes
                if (_estudiantes)
                {
                    List<IQueryable<Preguntas>> objlist = new List<IQueryable<Preguntas>>();

                    foreach (var item in rAmbientes)
                    {
                        objlist.Add(PreguntasEstudiantes(item.IdAmbiente));
                    }

                    return objlist;
                }
                //Valida si la evaluacion sera aplicada a un Profesional de Campo
                else if (_profesional)
                {
                    List<IQueryable<Preguntas>> objlist = new List<IQueryable<Preguntas>>();

                    foreach (var item in rAmbientes)
                    {
                        objlist.Add(PreguntasProfesional(item.IdAmbiente));
                    }

                    return objlist;
                }

                //Valida si la evaluacion sera aplicada a Padres de Familia
                else if (_padres)
                {
                    List<IQueryable<Preguntas>> objlist = new List<IQueryable<Preguntas>>();

                    foreach (var item in rAmbientes)
                    {
                        objlist.Add(PreguntasPadres(item.IdAmbiente));
                    }

                    return objlist;
                }
                //Valida si la evaluacion sera aplicada a Docentes
                else if (_docentes)
                {
                    List<IQueryable<Preguntas>> objlist = new List<IQueryable<Preguntas>>();

                    foreach (var item in rAmbientes)
                    {
                        objlist.Add(PreguntasEducadores(item.IdAmbiente));
                    }
                    return objlist;
                }
                //Valida si la evaluacion sera aplicada a Ninguno
                else
                    return null;


            }
            /*En caso de presentar excepcion retorno una Iqueryable vacia o null*/
            catch (System.Exception) { return null; }
        }

        public IQueryable<Preguntas> PreguntasEducadores(int idambiente)
        {
            try
            {
                var rEvaluacion = from e in db.Preguntas
                                  where e.Componentes.Procesos.Ambientes.IdAmbiente == idambiente && e.Docente == true
                                  select e;

                if (rEvaluacion.Count() != 0)
                    return rEvaluacion;
                else
                    return null;
            }
            catch (Exception) { return null; }

        }

        public IQueryable<Preguntas> PreguntasEstudiantes(int idambiente)
        {
            try
            {
                var rEvaluacion = from e in db.Preguntas
                                  where e.Componentes.Procesos.Ambientes.IdAmbiente == idambiente && e.Estudiante == true
                                  select e;
                if (rEvaluacion.Count() != 0)
                    return rEvaluacion;
                else
                    return null;
            }
            catch (Exception) { return null; }

        }

        public IQueryable<Preguntas> PreguntasPadres(int idambiente)
        {
            try
            {
                var rEvaluacion = from e in db.Preguntas
                                  where e.Componentes.Procesos.Ambientes.IdAmbiente == idambiente && e.Padres == true
                                  select e;
                if (rEvaluacion.Count() != 0)
                    return rEvaluacion;
                else
                    return null;
            }
            catch (Exception) { return null; }

        }

        public IQueryable<Preguntas> PreguntasDirectivos(int idambiente)
        {
            try
            {
                var rEvaluacion = from e in db.Preguntas
                                  where e.Componentes.Procesos.Ambientes.IdAmbiente == idambiente && e.Directivo == true
                                  select e;
                if (rEvaluacion.Count() != 0)
                    return rEvaluacion;
                else
                    return null;
            }
            catch (Exception) { return null; }

        }

        public IQueryable<Preguntas> PreguntasProfesional(int idambiente)
        {
            try
            {
                var rEvaluacion = from e in db.Preguntas
                                  where e.Componentes.Procesos.Ambientes.IdAmbiente == idambiente && e.Profesional == true
                                  select e;
                if (rEvaluacion.Count() != 0)
                    return rEvaluacion;
                else
                    return null;
            }
            catch (Exception) { return null; }

        }

        public bool ValidarActores(int ie, int actor, int idmedicion)
        {
            return ValidarGrupo(ie, actor, idmedicion);
        }
        /// <summary>
        /// Almacena los resultados obtenidos en la evaluacion presentada por un actor
        /// </summary>
        /// <param name="ResultadosByPre"></param>
        /// <param name="idie"></param>
        /// <param name="idactor"></param>
        /// <param name="idusuario"></param>
        /// <returns></returns>
        public bool Almacenar(object[,] ResultadosByPre, int idie, int idmedicion, int idactor, int idusuario, bool estado, int tipo)
        {
            try
            {
                int idevalinsert = RegistrarEvaluacion(idie, idmedicion, idactor, idusuario, estado, tipo);
                if (idevalinsert != 0)
                {
                    InsertarResultados(ResultadosByPre, idevalinsert);
                    db.SubmitChanges();

                    return true;
                }
                else
                    return false;

            }
            catch (System.Exception) { return false; }
        }

        protected int RecuperarIdEvaluacion()
        {
            try
            {
                int idEvaluacion = (from e in db.Evaluacion
                                    select e.IdEvaluacion).Max();

                return idEvaluacion;
            }
            catch (Exception) { return 0; }
        }

        protected int RegistrarEvaluacion(int idie, int idmedicion, int idactor, int idusuario, bool estado, int tipo)
        {
            try
            {
                ESM.Model.ESMBDDataContext db = new ESM.Model.ESMBDDataContext();

                Evaluacion objEvaluacion = null;
                if (tipo == 1)
                {
                    #region Estado SE
                    switch (estado)
                    {
                        case true:
                            objEvaluacion = new Evaluacion
                            {
                                IdActor = idactor,
                                IdSE = idie,
                                Fecha = DateTime.Now,
                                IdUsuario = idusuario,
                                IdMedicion = idmedicion,
                                IdEstado = 2
                            };
                            break;

                        case false:
                            objEvaluacion = new Evaluacion
                            {
                                IdActor = idactor,
                                IdSE = idie,
                                Fecha = DateTime.Now,
                                IdUsuario = idusuario,
                                IdMedicion = idmedicion,
                                IdEstado = 1
                            };
                            break;
                    }
                    #endregion
                }
                else
                {
                    #region Estado IE
                    switch (estado)
                    {
                        case true:
                            objEvaluacion = new Evaluacion
                            {
                                IdActor = idactor,
                                IdIE = idie,
                                Fecha = DateTime.Now,
                                IdUsuario = idusuario,
                                IdMedicion = idmedicion,
                                IdEstado = 2
                            };
                            break;

                        case false:
                            objEvaluacion = new Evaluacion
                            {
                                IdActor = idactor,
                                IdIE = idie,
                                Fecha = DateTime.Now,
                                IdUsuario = idusuario,
                                IdMedicion = idmedicion,
                                IdEstado = 1
                            };
                            break;
                    }
                    #endregion
                }
                db.Evaluacion.InsertOnSubmit(objEvaluacion);
                db.SubmitChanges();

                _idEvaluacion = objEvaluacion.IdEvaluacion;

                return objEvaluacion.IdEvaluacion;
            }
            catch (Exception) { return 0; }
        }

        /// <summary>
        /// Inserta una coleccion de resultados basandose en un arreglo bidimensional 
        /// que requiere como parametro.
        /// </summary>
        /// <param name="ResultadosByPre">Colleccion del tipo object[,] que contiene los resultados obtenidos para la evaluacion</param>
        /// <param name="idEvaluacion">Id de la evaluacion  a la que pertenece la coleccion de resultados</param>
        /// <returns></returns>
        private bool InsertarResultados(object[,] ResultadosByPre, int idEvaluacion)
        {
            try
            {
                /*Se Crea un clico for para recorrer una coleccion bidimencioal que 
                 *almacena la informacion que se obtubo luego de responder la evaluacion*/
                for (int i = 0; i < ResultadosByPre.GetLength(0); i++)
                {
                    Resultados objResultados = null;
                    /*Instancio un nuevo elemento del tipo Resultados*/
                    if (ResultadosByPre[i, 1] != null)
                    {
                        if (ResultadosByPre[i, 2] != null && ResultadosByPre[i, 2].ToString() != "")
                        {
                            objResultados = new Resultados
                            {
                                /*Asigno a los parametros de el mismo objeto los valores de la coleccion*/
                                IdPregunta = (int)ResultadosByPre[i, 0],
                                Valor = (bool)ResultadosByPre[i, 1],
                                Sesiones = Convert.ToInt32(ResultadosByPre[i, 2]),
                                Pendiente = (bool)ResultadosByPre[i, 3]
                            };
                        }
                        else
                        {
                            objResultados = new Resultados
                            {
                                /*Asigno a los parametros de el mismo objeto los valores de la coleccion*/
                                IdPregunta = (int)ResultadosByPre[i, 0],
                                Valor = (bool)ResultadosByPre[i, 1],
                                Pendiente = (bool)ResultadosByPre[i, 3]
                            };
                        }
                    }
                    else
                    {
                        if (ResultadosByPre[i, 2] != null && ResultadosByPre[i, 2].ToString() != "")
                        {
                            objResultados = new Resultados
                            {
                                /*Asigno a los parametros de el mismo objeto los valores de la coleccion*/
                                IdPregunta = (int)ResultadosByPre[i, 0],
                                Sesiones = Convert.ToInt32(ResultadosByPre[i, 2]),
                                Pendiente = (bool)ResultadosByPre[i, 3]
                            };
                        }
                        else
                        {
                            objResultados = new Resultados
                            {
                                /*Asigno a los parametros de el mismo objeto los valores de la coleccion*/
                                IdPregunta = (int)ResultadosByPre[i, 0],
                                Pendiente = (bool)ResultadosByPre[i, 3]
                            };
                        }
                    }


                    /*Realizo el proceso de insercion en el modelo de base de datos
                     *con el objeto actual
                     */
                    db.Resultados.InsertOnSubmit(objResultados);
                    /*Ejecuto el proceso de grabado en la base de datos este proceso se realiza
                     * por resultado para evitar perdida de informacion en un comit general
                     */
                    db.SubmitChanges();
                    /*Obtengo el ultimo id de resultado insertado*/
                    int idResultado = (from r in db.Resultados
                                       select r.IdResultados).Max();

                    /*Instancion un objeto del tipo ResultadosByEvaluacion*/
                    ResultadosByEvaluacion objResultadosByEvaluacion = new ResultadosByEvaluacion
                    {
                        /*Asigno los valores de los parametos del metodo presente
                         * a los parametros del objeto del tipo ResultadosByEvaluacion 
                         * actual
                         */
                        IdEvaluacion = idEvaluacion,
                        IdResultado = idResultado
                    };
                    /*Realizo el proceso de insercion para el objeto ResultadosByEvaluacion*/
                    db.ResultadosByEvaluacion.InsertOnSubmit(objResultadosByEvaluacion);
                    db.SubmitChanges();
                }
                /*retorno un valor verdadero en caso de no presentar excepcion de ningun tipo*/
                return true;
            }
            /*En caso de presentar una excepcion retorno un valor false*/
            catch (Exception) { return false; }
        }

        public bool InsertarResultadosParcial(object[,] Results, int eval)
        {
            return InsertarResultados(Results, eval);
        }

        /// <summary>
        /// Valida que la cantidad de evaluaciones en la tabla Evaluacion sea igual al
        /// numero de actores que existen en la tabla actores.
        /// </summary>
        /// <param name="idie">Identificador de Institucion Educativa por la que realizaremos
        ///  el proceso de busqueda.</param>
        protected bool ValidarGrupo(int idie, int idactor, int idmedicion)
        {
            try
            {

                ///TODO: JCCM: Revisar esta variable
                //Valido la cantidad de evaluaciones que hay para ese id de medicion consultado previamente
                int cantidad_eval = (from eval in db.Evaluacion
                                     where eval.IdIE == idie && eval.IdMedicion == idmedicion
                                     select eval.IdActor).Count();


                /*Obtengo la cantidad de actores que existen en la tabla para comparar con la cantidad de
                 * evaluaciones realizadas en la institucion educativa*/
                int cantidad_actores = (from actor in db.Actores
                                        where actor.IdRama != 1
                                        select actor.IdActor).Count();

                //Creo una variable para almacenar el resultado de la validacion realizada por el presente metodo
                bool result_valid = false;

                //Valido si la cantidad de las dos variables coincide
                switch (cantidad_actores == cantidad_eval)
                {
                    /*En caso de ser el resultado de la validacion true entonces adsigno a la valiable
                     *result_valid el valor true a retorar */
                    //case true: result_valid = true;
                    //    ESM.Model.Mediciones medi = new ESM.Model.Mediciones { FechaMedicion = DateTime.Now };
                    //    db.Mediciones.InsertOnSubmit(medi);
                    //    db.SubmitChanges();

                    ///TODO: JCMM: Consolidar Pendiente
                    ///
                    //var valid = from con in db.Consolidacion
                    //            where con.IdMedicion == idmedicion
                    //            select con;
                    //if (valid.Count() == 0)
                    //    ConsolidarEvaluaciones(Convert.ToInt32(id_ult_medicion));

                    //break;
                    /*En caso de ser el resultado de la validacion false entonces adsigno a la valiable
                    *result_valid el valor false a retorar */
                    case false: result_valid = false;
                        break;

                }

                switch (result_valid)
                {
                    case false:
                        var colactores = from a in db.Actores
                                         where a.Actor != "No Asignado"
                                         select new { a.IdActor };

                        var coleval = (from e in db.Evaluacion
                                       where e.IdIE == idie && e.IdMedicion == idmedicion
                                       select new { e.IdActor }).Distinct();

                        List<int> listactores = new List<int>();
                        List<int> listeval = new List<int>();
                        foreach (var c in colactores)
                        {
                            listactores.Add(c.IdActor);
                        }

                        foreach (var e in coleval)
                        {
                            listeval.Add(e.IdActor);
                        }

                        listactores.Sort();
                        listeval.Sort();

                        int[] faltan = new int[listactores.Count - listeval.Count];

                        if (listactores.Count != listeval.Count)
                        {
                            IEnumerable<int> query = (listactores.Count() > listeval.Count()) ? listactores.Except(listeval) : listeval.Except(listactores);
                            int contador = 0;
                            foreach (var q in query)
                            {
                                faltan[contador] = q;
                                contador++;
                            }
                        }

                        foreach (var item in faltan)
                        {
                            if (idactor == item)
                            {
                                result_valid = true;
                                break;
                            }
                        }
                        break;
                }

                this._error = "En la medicion actual ya existe una evaluacion para el actor seleccionado.";
                /*Retorno el valor de la variable que almacena el resultado de la validacion 
                 * realizada por el metodo presente o actual*/
                return result_valid;

            }
            /*En caso de presentarse algun tipo de Excepcion retorno un valor del tipo false
             */
            catch (LinqDataSourceValidationException) { return false; }
            /*En caso de presentarse algun tipo de Excepcion retorno un valor del tipo false
             */
            catch (Exception) { return false; }


        }

        public List<IQueryable> LoadEvalParcial(int eval, int actor)
        {
            return LoadParcial(eval, actor);
        }

        public Preguntas ObtenerDatosPregunta(int idpregunta)
        {
            try
            {
                var pre = (from p in db.Preguntas
                           where p.IdPregunta == idpregunta
                           select p).Single();

                return pre;
            }
            catch (Exception) { return null; }

        }

        protected List<IQueryable> LoadParcial(int eval, int actor)
        {
            try
            {
                List<IQueryable> objList = null;

                var rAmbientes = (from a in db.Ambientes
                                  select new { a.IdAmbiente }).Distinct();

                IQueryable preguntas = null;

                switch (actor)
                {
                    case 1:
                        objList = new List<IQueryable>();

                        foreach (var item in rAmbientes)
                        {
                            objList.Add(ParcialPreguntasEstudiantes(item.IdAmbiente));
                        }
                        break;
                    case 2:
                        objList = new List<IQueryable>();

                        foreach (var item in rAmbientes)
                        {
                            objList.Add(ParcialPreguntasProfesionales(item.IdAmbiente));
                        }
                        break;
                    case 3:
                        objList = new List<IQueryable>();

                        foreach (var item in rAmbientes)
                        {
                            objList.Add(ParcialPreguntasDocentes(item.IdAmbiente));
                        }
                        break;
                    case 4:
                        objList = new List<IQueryable>();

                        foreach (var item in rAmbientes)
                        {
                            objList.Add(ParcialPreguntasPadres(item.IdAmbiente));
                        }
                        break;
                    case 6:
                        objList = new List<IQueryable>();

                        foreach (var item in rAmbientes)
                        {
                            objList.Add(ParcialPreguntasDirectivos(item.IdAmbiente));
                        }
                        break;
                }

                return objList;


            }
            catch (Exception) { return null; }

        }

        public IQueryable ParcialPreguntasEstudiantes(int idambiente)
        {
            try
            {
                var preguntas = from p in db.Preguntas
                                where p.Componentes.Procesos.Ambientes.IdAmbiente == idambiente && p.Estudiante == true
                                select new
                                {
                                    p.IdOrden,
                                    p.IdPregunta,
                                    No_Pregunta = p.IdPregunta,
                                    Pregunta = p.Pregunta,
                                    p.Etiqueta
                                };

                return preguntas;
            }
            catch (Exception) { return null; }

        }

        public IQueryable ParcialPreguntasProfesionales(int idambiente)
        {
            try
            {
                var preguntas = from p in db.Preguntas
                                where p.Componentes.Procesos.Ambientes.IdAmbiente == idambiente && p.Profesional == true
                                select new
                                {
                                    p.IdOrden,
                                    p.IdPregunta,
                                    No_Pregunta = p.IdPregunta,
                                    Pregunta = p.Pregunta,
                                    p.Etiqueta
                                };

                return preguntas;
            }
            catch (Exception) { return null; }

        }

        public IQueryable ParcialPreguntasPadres(int idambiente)
        {
            try
            {
                var preguntas = from p in db.Preguntas
                                where p.Componentes.Procesos.Ambientes.IdAmbiente == idambiente && p.Padres == true
                                select new
                                {
                                    p.IdOrden,
                                    p.IdPregunta,
                                    No_Pregunta = p.IdPregunta,
                                    Pregunta = p.Pregunta,
                                    p.Etiqueta
                                };

                return preguntas;
            }
            catch (Exception) { return null; }

        }

        public IQueryable ParcialPreguntasDocentes(int idambiente)
        {
            try
            {
                var preguntas = from p in db.Preguntas
                                where p.Componentes.Procesos.Ambientes.IdAmbiente == idambiente && p.Docente == true
                                select new
                                {
                                    p.IdOrden,
                                    p.IdPregunta,
                                    No_Pregunta = p.IdPregunta,
                                    Pregunta = p.Pregunta,
                                    p.Etiqueta
                                };

                return preguntas;
            }
            catch (Exception) { return null; }

        }

        public IQueryable ParcialPreguntasDirectivos(int idambiente)
        {
            try
            {
                var preguntas = from p in db.Preguntas
                                where p.Componentes.Procesos.Ambientes.IdAmbiente == idambiente && p.Directivo == true
                                select new
                                {
                                    p.IdOrden,
                                    p.IdPregunta,
                                    No_Pregunta = p.IdPregunta,
                                    Pregunta = p.Pregunta,
                                    p.Etiqueta
                                };

                return preguntas;
            }
            catch (Exception) { return null; }

        }

        public IQueryable ObtenerTopEvaluacion(int top, int idmedicion, int idie)
        {
            try
            {
                var evalbymedi = (from e in db.Evaluacion
                                  where e.IdMedicion == idmedicion && e.IdIE == idie
                                  select new { No_Evaluacion = e.IdEvaluacion, No_Actor = e.IdActor, Actor = e.Actores.Actor, Fecha = e.Fecha, Estado = e.EstadoEvaluacion.Estado, Medicion = e.Mediciones.IdMedicion }).Take(top);

                return evalbymedi;
            }
            catch (LinqDataSourceValidationException) { return null; }
            catch (Exception) { return null; }
        }
        public void Consolidar(int idmedicion)
        {
            ConsolidarEvaluaciones(idmedicion);
        }
        protected void ConsolidarEvaluaciones(int idmedicion)
        {
            try
            {
                int idconsolidado = ConsolidarPreguntas(idmedicion);

                var results_cat = from cat in db.Categorias
                                  select cat;
                string[] MC = null;
                string[] APRO = null;
                string[] PERTI = null;
                string[] EXIS = null;

                foreach (var item in results_cat)
                {
                    switch (item.IdCategoria)
                    {
                        case 1:
                            MC = item.Validacion.ToString().Split(',');
                            break;
                        case 2:
                            APRO = item.Validacion.ToString().Split(',');
                            break;
                        case 3:
                            PERTI = item.Validacion.ToString().Split(',');
                            break;
                        case 4:
                            EXIS = item.Validacion.ToString().Split(',');
                            break;
                        case 7:

                            break;
                    }

                }

                var comp = from comps in db.Componentes
                           select comps;

                #region Consolidacion Componentes
                foreach (var item in comp)
                {
                    var consldPre = from c in db.ConslPregunta
                                    join pre in db.Preguntas on c.IdPregunta equals pre.IdPregunta
                                    where pre.IdComponente == item.IdComponente
                                    select c.Valor;

                    var valores_validar = consldPre.ToList();

                    int coincidenciasMC = 0;
                    int coincidenciasAPRO = 0;
                    int coincidenciasPERT = 0;
                    int coincidenciasEXIS = 0;
                    if (valores_validar != null)
                    {
                        for (int i = 0; i < valores_validar.Count; i++)
                        {
                            if (i > MC.Length - 1)
                                break;
                            else
                            {
                                bool valor_requerido = false;
                                if (MC[i] == "1")
                                    valor_requerido = true;
                                if (valores_validar[i] == valor_requerido)
                                    coincidenciasMC++;
                            }

                        }

                        for (int i = 0; i < valores_validar.Count; i++)
                        {
                            if (i > APRO.Length - 1)
                                break;
                            else
                            {
                                bool valor_requerido = false;
                                if (APRO[i] == "1")
                                    valor_requerido = true;
                                if (valores_validar[i] == valor_requerido)
                                    coincidenciasAPRO++;
                            }

                        }

                        for (int i = 0; i < valores_validar.Count; i++)
                        {
                            if (i > PERTI.Length - 1)
                                break;
                            else
                            {
                                bool valor_requerido = false;
                                if (PERTI[i] == "1")
                                    valor_requerido = true;
                                if (valores_validar[i] == valor_requerido)
                                    coincidenciasPERT++;
                            }

                        }

                        for (int i = 0; i < valores_validar.Count; i++)
                        {
                            if (i > EXIS.Length - 1)
                                break;
                            else
                            {
                                bool valor_requerido = false;
                                if (EXIS[i] == "1")
                                    valor_requerido = true;
                                if (valores_validar[i] == valor_requerido)
                                    coincidenciasEXIS++;
                            }

                        }


                        List<int> Coincidencias_Categorias = new List<int>();
                        Coincidencias_Categorias.Add(coincidenciasMC);
                        Coincidencias_Categorias.Add(coincidenciasAPRO);
                        Coincidencias_Categorias.Add(coincidenciasPERT);
                        Coincidencias_Categorias.Add(coincidenciasEXIS);

                        Coincidencias_Categorias.Sort();

                        #region MC
                        if (Coincidencias_Categorias[3] == coincidenciasMC)
                        {
                            decimal valor_MC = (from cate in db.Categorias
                                                where cate.IdCategoria == 1
                                                select cate.Valor).Single();

                            ConslComponente objConslComponente = new ConslComponente
                            {
                                IdComponente = item.IdComponente,
                                Valor = valor_MC,
                                IdConsolidado = idconsolidado
                            };
                            db.ConslComponente.InsertOnSubmit(objConslComponente);
                            db.SubmitChanges();
                        }

                        #endregion

                        #region APRO
                        else if (Coincidencias_Categorias[3] == coincidenciasAPRO)
                        {
                            decimal valor_APRO = (from cate in db.Categorias
                                                  where cate.IdCategoria == 2
                                                  select cate.Valor).Single();

                            ConslComponente objConslComponente = new ConslComponente
                            {
                                IdComponente = item.IdComponente,
                                Valor = valor_APRO,
                                IdConsolidado = idconsolidado
                            };
                            db.ConslComponente.InsertOnSubmit(objConslComponente);
                            db.SubmitChanges();
                        }

                        #endregion
                        #region PERT
                        else if (Coincidencias_Categorias[3] == coincidenciasPERT)
                        {
                            decimal valor_PERT = (from cate in db.Categorias
                                                  where cate.IdCategoria == 3
                                                  select cate.Valor).Single();

                            ConslComponente objConslComponente = new ConslComponente
                            {
                                IdComponente = item.IdComponente,
                                Valor = valor_PERT,
                                IdConsolidado = idconsolidado
                            };
                            db.ConslComponente.InsertOnSubmit(objConslComponente);
                            db.SubmitChanges();
                        }
                        #endregion
                        #region EXIS
                        else if (Coincidencias_Categorias[3] == coincidenciasEXIS)
                        {
                            decimal valor_EXIS = (from cate in db.Categorias
                                                  where cate.IdCategoria == 4
                                                  select cate.Valor).Single();

                            ConslComponente objConslComponente = new ConslComponente
                            {
                                IdComponente = item.IdComponente,
                                Valor = valor_EXIS,
                                IdConsolidado = idconsolidado
                            };
                            db.ConslComponente.InsertOnSubmit(objConslComponente);
                            db.SubmitChanges();
                        }
                        #endregion
                        #region PERT
                        else if (Coincidencias_Categorias[3] == 0)
                        {

                            ConslComponente objConslComponente = new ConslComponente
                            {
                                IdComponente = item.IdComponente,
                                Valor = 0,
                                IdConsolidado = idconsolidado
                            };
                            db.ConslComponente.InsertOnSubmit(objConslComponente);
                            db.SubmitChanges();
                        }
                        #endregion
                    }
                #endregion

                }

                #region Consolidacion por Proceso

                var colprocesos = from pro in db.Procesos
                                  select pro;

                foreach (var item in colprocesos)
                {
                    var ccompo = from ccom in db.ConslComponente
                                 join com in db.Componentes on ccom.IdComponente equals com.IdComponente
                                 where ccom.IdConsolidado == idconsolidado && com.IdProceso == item.IdProceso
                                 select new { ccom.Valor };

                    decimal totalcomp = 0;
                    foreach (var valcomp in ccompo)
                    {
                        totalcomp = totalcomp + Convert.ToDecimal(valcomp.Valor);
                    }

                    decimal valorproceso = totalcomp / Convert.ToInt32(ccompo.Count());

                    ConslProceso objConslProceso = new ConslProceso
                    {
                        IdConsolidado = idconsolidado,
                        IdProceso = item.IdProceso,
                        Valor = valorproceso
                    };

                    db.ConslProceso.InsertOnSubmit(objConslProceso);
                    db.SubmitChanges();
                }
                #endregion


                #region Consolidacion por Ambiente
                var colAmbientes = from amb in db.Ambientes
                                   select amb;

                foreach (var item in colAmbientes)
                {
                    var cslproc = from cproc in db.ConslProceso
                                  join proc in db.Procesos on cproc.IdProceso equals proc.IdProceso
                                  where cproc.IdConsolidado == idconsolidado && proc.IdAmbiente == item.IdAmbiente
                                  select new { cproc.Valor };

                    decimal totalpro = 0;
                    foreach (var valcomp in cslproc)
                    {
                        totalpro = totalpro + Convert.ToDecimal(valcomp.Valor);
                    }


                    decimal valorAmbiente = totalpro / Convert.ToInt32(cslproc.Count());

                    ConslAmbiente objConslAmbiente = new ConslAmbiente
                    {
                        IdConsolidado = idconsolidado,
                        IdAmbiente = item.IdAmbiente,
                        Valor = valorAmbiente
                    };

                    db.ConslAmbiente.InsertOnSubmit(objConslAmbiente);
                    db.SubmitChanges();
                }
                #endregion
            }
            catch (Exception)
            {

                ///error
            }

        }

        protected int ConsolidarPreguntas(int idmedicion)
        {
            try
            {
                int contador = 0;
                int idconsolidado = 0;
                var colcomp = from c in db.Componentes
                              select new { c.IdComponente };

                foreach (var comp in colcomp)
                {

                    var colpre = from p in db.Preguntas
                                 where p.IdComponente == comp.IdComponente
                                 select new { p.IdPregunta, p.IdPrivilegiado };

                    var colpreguntas = colpre.ToList();

                    if (colpreguntas.Count == 0)
                    {
                        return idconsolidado;
                    }



                    int cant_actores = (from a in db.Actores
                                        where a.IdRama != 1
                                        select a.IdActor).Count();
                    int idie = 0;

                    #region Por Pregunta

                    foreach (var pre in colpre)
                    {
                        object[,] actores_results = new object[cant_actores, 2];

                        for (int i = 0; i < cant_actores - 1; i++)
                        {
                            bool valor = false;
                            bool noaplica = false;

                            var resbyevalcount = (from rbe in db.ResultadosByEvaluacion
                                                  join r in db.Resultados on rbe.IdResultado equals r.IdResultados
                                                  join evalu in db.Evaluacion on rbe.IdEvaluacion equals evalu.IdEvaluacion
                                                  where r.IdPregunta == pre.IdPregunta && evalu.IdActor == i + 1
                                                  && evalu.IdEstado == 1 && evalu.IdMedicion == idmedicion
                                                  select new { r.IdPregunta, r.Valor, r.Pendiente, evalu.IdMedicion, evalu.IdIE }).Count();

                            if (resbyevalcount != 0)
                            {

                                var resbyeval = (from rbe in db.ResultadosByEvaluacion
                                                 join r in db.Resultados on rbe.IdResultado equals r.IdResultados
                                                 join evalu in db.Evaluacion on rbe.IdEvaluacion equals evalu.IdEvaluacion
                                                 where r.IdPregunta == pre.IdPregunta && evalu.IdActor == i + 1
                                                 && evalu.IdEstado == 1 && evalu.IdMedicion == idmedicion
                                                 select new { r.IdPregunta, r.Valor, r.Pendiente, evalu.IdMedicion, evalu.IdIE }).Single();

                                idie = Convert.ToInt32(resbyeval.IdIE);

                                if (resbyeval.Valor == true)
                                    valor = true;
                                else if (resbyeval.Valor == false)
                                    valor = false;
                                else if (resbyeval.Valor != true || resbyeval.Valor != false)
                                    noaplica = true;

                                actores_results[i, 0] = i + 1;
                                if (valor)
                                    actores_results[i, 1] = true;
                                if (!valor)
                                {
                                    if (!noaplica && !valor)
                                        actores_results[i, 1] = false;
                                    else
                                        actores_results[i, 1] = null;

                                }

                            }
                            int cant_si = 0;
                            int cant_no = 0;
                            int cant_noaplica = 0;
                            int res_privilegiado = 0;
                            for (int u = 0; u < actores_results.GetLength(0); u++)
                            {
                                if (pre.IdPrivilegiado == u)
                                {
                                    if (Convert.ToBoolean(actores_results[u, 1]) == true)
                                    {
                                        cant_si++;
                                        res_privilegiado = 1;
                                    }
                                    else if (Convert.ToBoolean(actores_results[u, 1]) == false)
                                    {
                                        cant_no++;
                                        res_privilegiado = 0;
                                    }
                                    else if (actores_results[u, 1] == null)
                                    {
                                        cant_noaplica++;
                                        res_privilegiado = 2;
                                    }
                                }
                                else
                                {
                                    if (Convert.ToBoolean(actores_results[u, 1]) == true)
                                    {
                                        cant_si++;
                                    }
                                    else if (Convert.ToBoolean(actores_results[u, 1]) == false)
                                    {
                                        cant_no++;
                                    }
                                    else if (actores_results[u, 1] == null)
                                        cant_noaplica++;
                                }
                            }
                            if (contador == 0)
                            {
                                Consolidacion objConsolidacion = new Consolidacion
                                {
                                    IdIE = idie,
                                    IdMedicion = idmedicion
                                };


                                db.Consolidacion.InsertOnSubmit(objConsolidacion);
                                db.SubmitChanges();
                                contador++;
                            }

                            var ult_idcons = (from cons in db.Consolidacion
                                              select cons.IdConsolidacion).Max();

                            idconsolidado = ult_idcons;

                            if (cant_si > cant_no)
                            {
                                ConslPregunta objConslPregunta = new ConslPregunta
                                {
                                    IdConsolidado = ult_idcons,
                                    IdPregunta = pre.IdPregunta,
                                    Valor = true
                                };
                                db.ConslPregunta.InsertOnSubmit(objConslPregunta);
                                db.SubmitChanges();
                            }
                            if (cant_si < cant_no)
                            {
                                ConslPregunta objConslPregunta = new ConslPregunta
                                {
                                    IdConsolidado = ult_idcons,
                                    IdPregunta = pre.IdPregunta,
                                    Valor = false
                                };
                                db.ConslPregunta.InsertOnSubmit(objConslPregunta);
                                db.SubmitChanges();
                            }
                            if (cant_si == cant_no)
                            {
                                if (res_privilegiado == 1)
                                {
                                    ConslPregunta objConslPregunta = new ConslPregunta
                                    {
                                        IdConsolidado = ult_idcons,
                                        IdPregunta = pre.IdPregunta,
                                        Valor = true
                                    };
                                    db.ConslPregunta.InsertOnSubmit(objConslPregunta);
                                    db.SubmitChanges();
                                }
                                else if (res_privilegiado == 0)
                                {
                                    ConslPregunta objConslPregunta = new ConslPregunta
                                    {
                                        IdConsolidado = ult_idcons,
                                        IdPregunta = pre.IdPregunta,
                                        Valor = false
                                    };
                                    db.ConslPregunta.InsertOnSubmit(objConslPregunta);
                                    db.SubmitChanges();


                                }
                                else
                                {

                                    ConslPregunta objConslPregunta = new ConslPregunta
                                    {
                                        IdConsolidado = ult_idcons,
                                        IdPregunta = pre.IdPregunta,
                                        NoAplica = true
                                    };
                                    db.ConslPregunta.InsertOnSubmit(objConslPregunta);
                                    db.SubmitChanges();

                                }

                            }
                            else
                                break;


                        }
                    }
                    #endregion

                }

                return idconsolidado;
            }
            catch (LinqDataSourceValidationException)
            {
                return 0;
            }
            catch (Exception)
            {

                return 0;
            }
        }

        public AyudaByPregunta ObtenerAyudaPregunta(int idpregunta)
        {
            try
            {
                var abp = (from a in db.AyudaByPregunta
                           where a.IdPregunta == idpregunta
                           select a).Single();

                return abp;
            }
            catch (Exception) { return null; }
        }

        public IQueryable MedicionesIE(int IdIe)
        {
            try
            {
                var medi = (from m in db.Evaluacion
                            where m.IdIE == IdIe
                            select new
                            {
                                Identificador = m.IdMedicion,
                                Evaluaciones = (from eva in db.Evaluacion where eva.IdIE == IdIe && eva.IdMedicion == m.IdMedicion select eva).Count(),
                                Fecha = m.Mediciones.FechaMedicion
                            }).Distinct();

                if (medi.Count() != 0)
                {
                    return medi;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception) { return null; }
        }

        public int CrearMedion()
        {
            try
            {
                Mediciones objMediciones = new Mediciones
                {
                    FechaMedicion = DateTime.Now
                };

                db.Mediciones.InsertOnSubmit(objMediciones);
                db.SubmitChanges();

                return objMediciones.IdMedicion;

            }
            catch (Exception) { return 0; }
        }

        public bool ActualizarEvaluacion(object[,] VResultados, int ideval, bool estado)
        {
            try
            {
                var results = from e in db.ResultadosByEvaluacion
                              where e.IdEvaluacion == ideval
                              select e;

                int contvector = 0;
                for (int i = 0; i < VResultados.GetLength(0); i++)
                {
                    int contador = 0;

                    foreach (var item in results)
                    {
                        int idpregunta = Convert.ToInt32(VResultados[contvector, 0]);

                        if (item.Resultados.IdPregunta == idpregunta)
                        {
                            if (VResultados[contvector, 2] != null && VResultados[contvector, 2].ToString() != "")
                            {
                                if (VResultados[contvector, 1] != null)
                                {
                                    item.Resultados.Valor = (bool)VResultados[contvector, 1];
                                    item.Resultados.Sesiones = Convert.ToInt32(VResultados[contvector, 2]);
                                    item.Resultados.Pendiente = (bool)VResultados[contvector, 3];

                                    db.SubmitChanges();
                                }
                            }
                            else
                            {
                                if (VResultados[contvector, 1] != null)
                                {
                                    item.Resultados.Valor = (bool)VResultados[contvector, 1];
                                    item.Resultados.Pendiente = (bool)VResultados[contvector, 3];

                                    db.SubmitChanges();
                                }
                            }
                            contador++;
                            break;
                        }

                    }
                    if (contador == 0)
                    {

                        Resultados objResultados = null;
                        if (VResultados[contvector, 2] != null && VResultados[contvector, 2].ToString() != "")
                        {
                            if (VResultados[contvector, 1] != null)
                            {
                                objResultados = new Resultados
                                {
                                    IdPregunta = Convert.ToInt32(VResultados[contvector, 0]),
                                    Valor = Convert.ToBoolean(VResultados[contvector, 1]),
                                    Sesiones = Convert.ToInt32(VResultados[contvector, 2]),
                                    Pendiente = Convert.ToBoolean(VResultados[contvector, 3])
                                };
                            }
                            else
                            {
                                objResultados = new Resultados
                                {
                                    IdPregunta = Convert.ToInt32(VResultados[contvector, 0])
                                };
                            }
                        }
                        else
                        {
                            if (VResultados[contvector, 1] != null)
                            {
                                objResultados = new Resultados
                                {
                                    IdPregunta = Convert.ToInt32(VResultados[contvector, 0]),
                                    Valor = Convert.ToBoolean(VResultados[contvector, 1]),
                                    Pendiente = Convert.ToBoolean(VResultados[contvector, 3])
                                };
                            }
                            else
                            {
                                objResultados = new Resultados
                                {
                                    IdPregunta = Convert.ToInt32(VResultados[contvector, 0])
                                };
                            }
                        }
                        db.Resultados.InsertOnSubmit(objResultados);
                        db.SubmitChanges();

                        ResultadosByEvaluacion objResultadosByEvaluacion = new ResultadosByEvaluacion
                        {
                            IdEvaluacion = ideval,
                            IdResultado = objResultados.IdResultados
                        };
                        db.ResultadosByEvaluacion.InsertOnSubmit(objResultadosByEvaluacion);
                        db.SubmitChanges();

                    }
                    contvector++;
                }
                var eval = (from ev in db.Evaluacion
                            where ev.IdEvaluacion == ideval
                            select ev).Single();
                if (estado)
                    eval.IdEstado = 1;
                else
                    eval.IdEstado = 2;

                db.SubmitChanges();
                return true;
            }
            catch (Exception) { return false; }
        }
    }

}


