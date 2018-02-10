using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace JP.Base.Common.Forms
{
    public class ABMCommonRoutines
    {
        public string cmdCancelName = "";
        public string cmdDeleteName = "";
        public string cmdEditName = "";
        public string cmdNewName = "";
        public string cmdQueryName = "";
        public string cmdSearchName = "";
        public ContainerControl currentForm = null;

        public enum enModes
        {
            Normal = 0,
            Edition,
            New,
            Query,
            Search
        }

        /// <summary>
        /// Verifica que todos los campos TXTs tengan algo ingresado
        /// </summary>
        /// <param name="pTextBoxes"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool CheckTextInputLength(List<TextBox> pTextBoxes)
        {
            //v1.0- 07-10-07
            //ESTA FUNCION DEVUELVE TRUE SI TODOS LOS TXTS DE LA MATRIZ DEL PARAM
            //TIENEN LEN > 0

            bool ok = false;
            Control oCtl = null;
            List<TextBox> oList = null;

            CreateControlList<TextBox>(currentForm, ref oList);

            ok = true;
            foreach (Control oCtl_loopVariable in oList)
            {
                oCtl = oCtl_loopVariable;
                if (oCtl.Text.Trim().Length == 0)
                    ok = false;
            }

            return ok;
        }

        /// <summary>
        /// Remueve la comilla simple y las comas de los TXTs del Formulario
        /// </summary>
        /// <remarks>Suele emplearse para enviar consultas SQL</remarks>
        public void ClearSimpleQuotes()
        {
            List<TextBox> oList = null;
            Control oCtl = null;

            CreateControlList<TextBox>(currentForm, ref oList);

            foreach (Control oCtl_loopVariable in oList)
            {
                oCtl = oCtl_loopVariable;
                oCtl.Text = oCtl.Text.Replace("'", "");
                oCtl.Text = oCtl.Text.Replace(",", ";");
            }
        }

        /// <summary>
        /// Borra el contenido de todos los TextBoxes del formulari
        /// </summary>
        public void ClearTextBoxes()
        {
            //v1.0 - 07-10-07
            //ESTA SUB BORRA EL CONTENIDO DE TODOS LOS TXTS
            //DE LA MATRIZ DEL PARAM

            List<TextBoxBase> oList = null;
            TextBoxBase oTXT = null;

            CreateControlList<TextBoxBase>(currentForm, ref oList);

            foreach (TextBoxBase oTXT_loopVariable in oList)
            {
                oTXT = oTXT_loopVariable;
                oTXT.Text = string.Empty;
            }
        }

        /// <summary>
        /// Esta rutina emplea recursividad para listar los controles del tipo pedido
        /// indicandole el control inicial sobre el que debe buscar, grlmente, el mismo FORM
        /// </summary>
        /// <typeparam name="T">Tipo de control a buscar</typeparam>
        /// <param name="pBaseCtl">El control en el que se realziara la busqueda, inicialmente, seria el FORM,
        /// pero conforme se ejecute la rutina este valor cambia para buscar dentro de cada control del
        /// control INICIAL indicado al llamar a la rutina</param>
        /// <param name="pLista">Lista contenedora de los controles buscados</param>
        /// <remarks>v 2.0</remarks>
        public void CreateControlList<T>(Control pBaseCtl, ref List<T> pLista) where T : Control
        {
            Control oCtl = null;

            if (pLista == null)
                pLista = new List<T>();

            foreach (Control oCtl_loopVariable in pBaseCtl.Controls)
            {
                oCtl = oCtl_loopVariable;
                //v2.0 - por fuera del if, listo TODOS los controles q haya en un contenedor
                if (oCtl is T)
                {
                    pLista.Add((T)oCtl);
                }

                //SI CONTROL CONTIENE CONTROLES, LISTARLOS TAMBIEN

                if (oCtl.Controls.Count > 0)
                {
                    pBaseCtl = oCtl;

                    CreateControlList<T>(pBaseCtl, ref pLista);
                }
            }
        }

        public void EnableAllControls(bool pEstado, Control parentControl, bool lockTextBoxes = true, List<Type> excludeTypes = null, bool includeParent = true)
        {
            Control oAnyCtl = null;

            var oAllCtlList = parentControl.Controls.Cast<Control>().ToList();

            if (includeParent)
                parentControl.Enabled = pEstado;

            foreach (Control oAnyCtl_loopVariable in oAllCtlList)
            {
                oAnyCtl = oAnyCtl_loopVariable;

                var process = excludeTypes != null ? !excludeTypes.Cast<Type>().Any(x => oAnyCtl.GetType().IsSubclassOf(x)) : true;

                if (process)
                {
                    if (lockTextBoxes && oAnyCtl is TextBoxBase)
                    {
                        ((TextBoxBase)oAnyCtl).ReadOnly = !pEstado;
                    }
                    else
                        oAnyCtl.Enabled = pEstado;
                }

                if (oAnyCtl.HasChildren)
                {
                    EnableAllControls(pEstado, oAnyCtl, lockTextBoxes, excludeTypes);
                }
            }
        }

        /// <summary>
        /// Activa/Desactiva una lista de controles segun el parametro pEstado
        /// </summary>
        /// <typeparam name="T">Tipo de control a listar</typeparam>
        /// <param name="pCtlList">Lista de controles a modificar</param>
        /// <param name="pEstado">Estado del control, TRUE/FALSE</param>
        /// <remarks></remarks>
        public void EnableControls<T>(List<T> pCtlList, bool pEstado) where T : Control
        {
            Control oCtl = null;

            foreach (Control oCtl_loopVariable in pCtlList)
            {
                oCtl = oCtl_loopVariable;
                oCtl.Enabled = pEstado;
            }
        }

        public void EnableControls<T>(Control baseCtl, bool enabled) where T : Control
        {
            T ctl = default(T);
            List<T> oList = null;

            CreateControlList<T>(baseCtl, ref oList);

            foreach (T ctlItem in oList)
            {
                ctl = ctlItem;
                ctl.Enabled = enabled;
            }
        }

        public bool IsTextBoxEmpty(TextBoxBase Ctl)
        {
            return string.IsNullOrEmpty(Ctl.Text.Trim());
        }

        /// <summary>
        /// v1.0 - 07-10-07
        /// Establece los controles que hereden de TextBoxBase de un formulario como ReadOnly (Locked)
        /// </summary>
        /// <param name="pLocked">Locked: TRUE/FALSE</param>
        /// <remarks></remarks>
        public void LockTextBoxes<T>(bool pLocked) where T : TextBoxBase
        {
            T oTXT = default(T);
            List<T> oList = null;

            CreateControlList<T>(currentForm, ref oList);

            foreach (T oTXT_loopVariable in oList)
            {
                oTXT = oTXT_loopVariable;
                oTXT.ReadOnly = pLocked;
            }
        }

        public void LockTextBoxes(bool pLocked)
        {
            TextBoxBase oTXT = default(TextBoxBase);
            List<TextBoxBase> oList = null;

            CreateControlList<TextBoxBase>(currentForm, ref oList);

            foreach (TextBoxBase oTXT_loopVariable in oList)
            {
                oTXT = oTXT_loopVariable;
                oTXT.ReadOnly = pLocked;
            }
        }

        /// <summary>
        /// FALTA IMPLEMENTACION
        /// </summary>
        /// <param name="LView"></param>
        /// <param name="ColumnHeader"></param>
        /// <remarks></remarks>
        public void LViewColumnOrder(ListView LView, ColumnHeader ColumnHeader)
        {
            throw new NotImplementedException("falta implementacion!!");
        }

        /// <summary>
        /// Selecciona el primer radiobutton de los RadioButtons del formulario
        /// </summary>
        /// <remarks></remarks>
        public void ResetRadioButtons()
        {
            //v1.0 - 07-10-07
            //SELECCIONA LA PRIMER OPCION DE UNA MATROZ DE CONTROLES

            List<RadioButton> oList = null;

            CreateControlList<RadioButton>(currentForm, ref oList);

            oList.Sort();

            oList[0].Checked = true;
        }
    }
}