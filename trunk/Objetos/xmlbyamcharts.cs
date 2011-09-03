using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace ESM.Objetos
{
    public class xmlbyamcharts
    {

        #region Propiedades Privadas y Publicas

        private object[,] _series;

        public object[,] Series
        {
            get { return _series; }
            set { _series = value; }
        }

        private object[] _graphs;

        public object[] Graphs
        {
            get { return _graphs; }
            set { _graphs = value; }
        }

        private object[,] _valores;

        public object[,] Valores
        {
            get { return _valores; }
            set { _valores = value; }
        }
        #endregion

        public string ObtenerXml()
        {
            return GenerarXmlData();
        }

        protected string GenerarXmlData()
        {

            StringBuilder xmldata = new StringBuilder();
            xmldata.Append("<chart>");
            xmldata.Append(" <series>");
            for (int i = 0; i < _series.GetLength(0); i++)
            {
                xmldata.Append(String.Format("  <value xid='{0}'>{1}</value>", i, _series[i, 1]));
            }
            xmldata.Append(" </series>");
            xmldata.Append(" <graphs>");

            for (int g = 0; g < _graphs.Length; g++)
            {
                int contador = 0;
                xmldata.Append(String.Format(" <graph gid='{0}'>", _graphs[g].ToString()));
                for (int v = 0; v < _valores.GetLength(0); v++)
                {
                    if (Convert.ToInt32(_valores[v, 0]) == g)
                    {
                        xmldata.Append(String.Format(" <value xid='{0}'>{1}</value>", contador, _valores[v, 1].ToString().Replace(',', '.')));

                        contador++;
                    }
                }
                xmldata.Append(" </graph>");

                break;
            }
            xmldata.Append(" </graphs>");
            xmldata.Append(" </chart>");

            return xmldata.ToString();
        }
    }
}