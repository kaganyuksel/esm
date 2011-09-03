using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ESM.Objetos
{
    public class CConsolidacion
    {
        ESM.Model.ESMBDDataContext db = new Model.ESMBDDataContext();

        public string ObtenerConslAmbiente(int idmedicion)
        {
            int consolidadoNo = (from con in db.Consolidacion
                                 where con.IdMedicion == idmedicion
                                 select con.IdConsolidacion).Single();

            var conls = from conamb in db.ConslAmbiente
                        join amd in db.Ambientes on conamb.IdAmbiente equals amd.IdAmbiente
                        where conamb.IdConsolidado == consolidadoNo
                        select new { amd.Ambiente, conamb.Valor };


            object[,] colseries = null; /*{ { "Ambiente1", "Ambiente2" }, { 0, 1 } };*/
            int contador = 0;

            colseries = new object[conls.Count(), 2];

            foreach (var item in conls)
            {


                colseries[contador, 0] = item.Ambiente;
                colseries[contador, 1] = contador;

                contador++;
            }

            object[] colgra = new object[colseries.GetLength(0)]; /*{ 0, 1 };*/

            for (int i = 0; i < colseries.GetLength(0); i++)
            {
                colgra[i] = colseries[i, 1];
            }

            object[,] colvalores = new object[colseries.GetLength(0), 2];

            var ambiconso = conls.ToList();

            for (int v = 0; v < colvalores.GetLength(0); v++)
            {
                colvalores[v, 0] = 0;
                colvalores[v, 1] = ambiconso[v].Valor;
            }

            xmlbyamcharts xml = new xmlbyamcharts();

            xml.Series = colseries;
            xml.Graphs = colgra;
            xml.Valores = colvalores;

            string xmldata = xml.ObtenerXml();

            return xmldata;
        }

        public string ObtenerProcesos(int idmedicion)
        {
            int consolidadoNo = (from con in db.Consolidacion
                                 where con.IdMedicion == idmedicion
                                 select con.IdConsolidacion).Single();

            var conls = from conamb in db.ConslProceso
                        join pro in db.Procesos on conamb.IdProceso equals pro.IdProceso
                        where conamb.IdConsolidado == consolidadoNo
                        select new { pro.Proceso, conamb.Valor };


            object[,] colseries = null; /*{ { "Ambiente1", "Ambiente2" }, { 0, 1 } };*/
            int contador = 0;

            colseries = new object[conls.Count(), 2];

            foreach (var item in conls)
            {


                colseries[contador, 0] = item.Proceso;
                colseries[contador, 1] = contador;

                contador++;
            }

            object[] colgra = new object[colseries.GetLength(0)]; /*{ 0, 1 };*/

            for (int i = 0; i < colseries.GetLength(0); i++)
            {
                colgra[i] = colseries[i, 1];
            }

            object[,] colvalores = new object[colseries.GetLength(0), 2];

            var ambiconso = conls.ToList();

            for (int v = 0; v < colvalores.GetLength(0); v++)
            {
                colvalores[v, 0] = 0;
                colvalores[v, 1] = ambiconso[v].Valor;
            }

            xmlbyamcharts xml = new xmlbyamcharts();

            xml.Series = colseries;
            xml.Graphs = colgra;
            xml.Valores = colvalores;

            string xmldata = xml.ObtenerXml();

            return xmldata;
        }

        public string ObtenerComponentes(int idmedicion)
        {
            int consolidadoNo = (from con in db.Consolidacion
                                 where con.IdMedicion == idmedicion
                                 select con.IdConsolidacion).Single();

            var conls = from concomp in db.ConslComponente
                        join com in db.Componentes on concomp.IdComponente equals com.IdComponente
                        where concomp.IdConsolidado == consolidadoNo
                        select new { com.Componente, concomp.Valor };


            object[,] colseries = null; /*{ { "Ambiente1", "Ambiente2" }, { 0, 1 } };*/
            int contador = 0;

            colseries = new object[conls.Count(), 2];

            foreach (var item in conls)
            {


                colseries[contador, 0] = item.Componente;
                colseries[contador, 1] = contador;

                contador++;
            }

            object[] colgra = new object[colseries.GetLength(0)]; /*{ 0, 1 };*/

            for (int i = 0; i < colseries.GetLength(0); i++)
            {
                colgra[i] = colseries[i, 1];
            }

            object[,] colvalores = new object[colseries.GetLength(0), 2];

            var ambiconso = conls.ToList();

            for (int v = 0; v < colvalores.GetLength(0); v++)
            {
                colvalores[v, 0] = 0;
                colvalores[v, 1] = ambiconso[v].Valor;
            }

            xmlbyamcharts xml = new xmlbyamcharts();

            xml.Series = colseries;
            xml.Graphs = colgra;
            xml.Valores = colvalores;

            string xmldata = xml.ObtenerXml();

            return xmldata;
        }

        public string ValidarNodos(string id, string nombre, int medicion)
        {
            bool banderaambiente = false;
            bool banderaproceso = false;
            bool banderacomponente = false;

            var amb = from am in db.Ambientes
                      where am.IdAmbiente == Convert.ToInt32(id)
                      select am;

            var pro = from pr in db.Procesos
                      where pr.IdProceso == Convert.ToInt32(id)
                      select pr;

            var com = from co in db.Componentes
                      where co.IdComponente == Convert.ToInt32(id)
                      select co;

            if (amb.Count() != 0)
            {
                foreach (var item in amb)
                {
                    if (item.Ambiente == nombre)
                        banderaambiente = true;
                    else
                    {
                        foreach (var ipro in pro)
                        {
                            if (ipro.Proceso == nombre)
                                banderaproceso = true;
                            else
                            {
                                foreach (var icom in com)
                                {
                                    if (icom.Componente == nombre)
                                        banderacomponente = true;
                                }
                            }
                        }
                    }
                }
            }

            if (pro.Count() != 0)
            {

                foreach (var ipro in pro)
                {
                    if (ipro.Proceso == nombre)
                        banderaproceso = true;
                    else
                    {
                        foreach (var icom in com)
                        {
                            if (icom.Componente == nombre)
                                banderacomponente = true;
                        }
                    }
                }
            }

            if (banderaambiente)
                return ObtenerProcesos(medicion);
            else if (banderaproceso)
                return ObtenerComponentes(medicion);
            else if (banderacomponente)
                return "";

            return null;
        }

        public bool Exist(int idmedicion)
        {
            try
            {
                int conso = (from con in db.Consolidacion
                             where con.IdMedicion == idmedicion
                             select con).Count();

                if (conso != 0)
                    return true;
                else
                    return false;
            }
            catch (Exception) { return false; }
        }
    }



}