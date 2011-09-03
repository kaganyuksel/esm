using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ESM
{
    public partial class LecturaContexto : System.Web.UI.Page
    {
        #region Objetos, Propiedades Publicas y Privadas

        LecturasContexto.LecturaContextoSECRE _objLecturaContextoSECRE = new LecturasContexto.LecturaContextoSECRE();

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void CargarSE()
        {

        }
        protected void Almacenar()
        {
            _objLecturaContextoSECRE.__2_1 = txt21.Text;
            if (rbtn22Si.Checked)
                _objLecturaContextoSECRE.__2_2 = true;

            else if (rbtn22No.Checked == true)
                _objLecturaContextoSECRE.__2_2 = false;

            _objLecturaContextoSECRE.__2_2_1 = txt221.Text;
            _objLecturaContextoSECRE.__2_2_2 = txt222.Text;

            #region 223

            if (cblist223.Items[0].Selected)
                _objLecturaContextoSECRE.__2_2_3EE = true;
            else
                _objLecturaContextoSECRE.__2_2_3EE = false;
            if (cblist223.Items[1].Selected)
                _objLecturaContextoSECRE.__2_2_3EST = true;
            else
                _objLecturaContextoSECRE.__2_2_3EST = false;

            if (cblist223.Items[2].Selected)
                _objLecturaContextoSECRE.__2_2_3EDU = true;
            else
                _objLecturaContextoSECRE.__2_2_3EDU = false;

            if (cblist223.Items[3].Selected)
                _objLecturaContextoSECRE.__2_2_3DIR = true;
            else
                _objLecturaContextoSECRE.__2_2_3DIR = false;

            if (cblist223.Items[4].Selected)
                _objLecturaContextoSECRE.__2_2_3PAD = true;
            else
                _objLecturaContextoSECRE.__2_2_3PAD = false;

            #endregion

            _objLecturaContextoSECRE.__2_2_3Otro = txt223Orto.Text;

            _objLecturaContextoSECRE.__2_2_4 = txt224.Text;
            _objLecturaContextoSECRE.__2_2_5 = txt225.Text;

            if (rbtn23Si.Checked)
                _objLecturaContextoSECRE.__2_3 = true;
            else if (rbtn23No.Checked)
                _objLecturaContextoSECRE.__2_3 = false;

            _objLecturaContextoSECRE.__2_3_1 = txt231.Text;
            _objLecturaContextoSECRE.__3_1 = txt31.Text;
            _objLecturaContextoSECRE.__3_2 = txt32.Text;
            _objLecturaContextoSECRE.__3_3 = txt33.Text;
            _objLecturaContextoSECRE.__3_4 = txt34.Text;
            _objLecturaContextoSECRE.__3_5 = txt35.Text;
            _objLecturaContextoSECRE.__3_6 = txt36.Text;

            if (rbtn41Si.Checked)
                _objLecturaContextoSECRE.__4_1 = true;
            else if (rbtn41No.Checked)
                _objLecturaContextoSECRE.__4_1 = false;

            _objLecturaContextoSECRE.__4_1_1 = txt411.Text;

            #region 5.1 Departamental/Municipal
            if (cblist51DedMun.Items[0].Selected)
                _objLecturaContextoSECRE.__5_1_RADDEP = true;
            else
                _objLecturaContextoSECRE.__5_1_RADDEP = false;

            if (cblist51DedMun.Items[1].Selected)
                _objLecturaContextoSECRE.__5_1_PRENDEP = true;
            else
                _objLecturaContextoSECRE.__5_1_PRENDEP = false;

            if (cblist51DedMun.Items[2].Selected)
                _objLecturaContextoSECRE.__5_1_TELDEP = true;
            else
                _objLecturaContextoSECRE.__5_1_TELDEP = false;

            if (cblist51DedMun.Items[0].Selected)
                _objLecturaContextoSECRE.__5_1_INTDEP = true;
            else
                _objLecturaContextoSECRE.__5_1_INTDEP = false;
            #endregion

            #region Local

            if (cblist51DedMun.Items[0].Selected)
                _objLecturaContextoSECRE.__5_1_RADMUN = true;
            else
                _objLecturaContextoSECRE.__5_1_RADMUN = false;

            if (cblist51DedMun.Items[1].Selected)
                _objLecturaContextoSECRE.__5_1_PRENMUN = true;
            else
                _objLecturaContextoSECRE.__5_1_PRENMUN = false;

            if (cblist51DedMun.Items[2].Selected)
                _objLecturaContextoSECRE.__5_1_TELMUN = true;
            else
                _objLecturaContextoSECRE.__5_1_TELMUN = false;

            if (cblist51DedMun.Items[0].Selected)
                _objLecturaContextoSECRE.__5_1_INTMUN = true;
            else
                _objLecturaContextoSECRE.__5_1_INTMUN = false;

            #endregion

            _objLecturaContextoSECRE.__5_2 = txt52.Text;
            if (rbtn118Si.Checked)
                _objLecturaContextoSECRE.__1_18 = true;
            else
                _objLecturaContextoSECRE.__1_18 = false;

            if (rbtn118No.Checked)
                _objLecturaContextoSECRE.__1_18 = true;
            else
                _objLecturaContextoSECRE.__1_18 = false;

            _objLecturaContextoSECRE.__1_19 = txt119.Text;
            _objLecturaContextoSECRE.Observaciones = txtObservaciones.Text;

            int idsecretaria = 0;
            int idmedicion = 0;

            _objLecturaContextoSECRE.Almacenar(idsecretaria, idmedicion);

        }
    }
}