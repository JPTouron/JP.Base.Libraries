using System;
using System.Collections.Generic;
using System.Windows.Forms;
using JP.Base.Common.Forms;
using JP.Base.Forms.GenericCRUD.Forms.Interfaces;
using JP.Base.Forms.GenericCRUD.EventHandlers;
using JP.Base.Forms.GenericCRUD.Properties;

namespace JP.Base.Forms.GenericCRUD.Forms
{
    /// <summary>
    /// v1.1.1 - JP - 13-04-09
    /// Formulario basico que reune las propiedades basicas para utlizarse como ABM de cualquier entidad que hereda de WIN FORMS
    /// para usarlo debe heredarse de este FORM
    /// </summary>
    /// <remarks></remarks>
    public partial class CRUDForm : Form, ICRUDForm
    {
        #region Fields

        private bool displayProgressBar = true;
        private bool enableQueryFunctionality = true;
        private bool enableSearchFunctionality = true;
        private bool enhanceDisplayArea = true;
        private bool formStateAffectsContainedControls = true;
        private ABMCommonRoutines moABMRoutines;

        private ABMCommonRoutines.enModes modoActual;
        private Timer timerPBarSimulation;

        #endregion Fields

        public bool UseMarqueeProgressBar { get; set; }

        #region IABMForm Members

        public bool DisplayProgressBar
        {
            get
            {
                return displayProgressBar;
            }
            set
            {
                displayProgressBar = value;
                PBar.Visible = value;
            }
        }

        public bool EnableQueryFunctionality
        {
            get
            {
                return enableQueryFunctionality;
            }
            set
            {
                enableQueryFunctionality = value;
                ChangeQueryButtonVisibility(value);
            }
        }

        public bool EnableSearchFunctionality
        {
            get
            {
                return enableSearchFunctionality;
            }
            set
            {
                enableSearchFunctionality = value;
                ChangeSearchButtonVisibility(value);
            }
        }

        public bool EnhanceDisplayArea
        {
            get
            {
                return enhanceDisplayArea;
            }
            set
            {
                enhanceDisplayArea = value;
            }
        }

        public bool FormStateAffectsContainedControls
        {
            get
            {
                return formStateAffectsContainedControls;
            }
            set
            {
                formStateAffectsContainedControls = value;
            }
        }

        public ABMCommonRoutines.enModes ModoActual
        {
            get { return modoActual; }
            private set
            {
                modoActual = value;
            }
        }

        public event CancellingHandler Cancelling;

        public event ChangeItemsListStateHandler ChangeItemsListState;

        public event DeletingItemHandler DeletingItem;

        public event EnterEditingModeHandler EnterEditingMode;

        public event EnterNewModeHandler EnterNewMode;

        public event EnterNormalModeHandler EnterNormalMode;

        public event EnterQueryModeHandler EnterQueryMode;

        public event EnterSearchModeHandler EnterSearchMode;

        public event SavingItemHandler SavingItem;

        public event NodeSelectedHandler SelectedNode;

        public void InitializeProgressBar(int minimum, int maximum)
        {
            PBar.Minimum = minimum;
            PBar.Maximum = maximum;
            PBar.Value = minimum;
        }

        public void ResetProgressBar()
        {
            PBar.Value = PBar.Minimum;
        }

        public void SimulateLoadingProgressBar(int minimum, int maximum)
        {
            InitializeProgressBar(minimum, maximum);
            StartProgressBarLoadingSimulation();
        }

        public void StopProgressBarSimulation()
        {
            if (!UseMarqueeProgressBar)
            {
                PBar.Value = PBar.Maximum;
                timerPBarSimulation.Stop();
                timerPBarSimulation.Tick -= new EventHandler(timerPBarSimulation_Tick);
                timerPBarSimulation.Dispose();
            }
            else
            {
                PBar.Style = ProgressBarStyle.Continuous;
                PBar.MarqueeAnimationSpeed = 0;
            }
        }

        public void UpdateProgressBar(int value)
        {
            if ((value >= PBar.Maximum))
            {
                PBar.Value = PBar.Maximum;
            }
            else if ((value <= PBar.Minimum))
            {
                PBar.Value = PBar.Minimum;
            }
            else
            {
                PBar.Value = value;
            }
        }

        #endregion IABMForm Members

        #region Constructor

        public CRUDForm() : this(true)
        {
        }

        public CRUDForm(bool formStateAffectsContainedControls)
        {
            this.formStateAffectsContainedControls = formStateAffectsContainedControls;

            InitializeComponent();
            FormClosed += ABMForm_FormClosed;
            Load += ABMForm_Load;

            ChangeQueryButtonVisibility(enableQueryFunctionality);
            ChangeSearchButtonVisibility(enableSearchFunctionality);
            PBar.Visible = displayProgressBar;
            UseMarqueeProgressBar = false;
        }

        #endregion Constructor

        #region Form Event Handlers

        protected virtual void ABMForm_Load(object sender, System.EventArgs e)
        {
            moABMRoutines = new ABMCommonRoutines();
            modoActual = ABMCommonRoutines.enModes.Normal;

            var _with1 = moABMRoutines;
            _with1.currentForm = this;
            _with1.cmdSearchName = CMDBuscar.Name;
            _with1.cmdCancelName = CMDCancelar.Name;
            _with1.cmdQueryName = CMDConsultar.Name;
            _with1.cmdEditName = CMDEditar.Name;
            _with1.cmdNewName = CMDNuevo.Name;
            _with1.cmdDeleteName = CMDEliminar.Name;

            UpdateControls(modoActual);
        }

        private void ABMForm_FormClosed(System.Object sender, System.Windows.Forms.FormClosedEventArgs e)
        {
            moABMRoutines = null;
        }

        #endregion Form Event Handlers

        #region CMDs

        protected virtual void CMDBuscar_Click(object sender, System.EventArgs e)
        {
            modoActual = ABMCommonRoutines.enModes.Search;

            UpdateControls(modoActual);
            ChangeCurrentMode(modoActual);
        }

        protected virtual void CMDCancelar_Click(object sender, System.EventArgs e)
        {
            FireEvent(Cancelling, new object[] { modoActual });
            modoActual = ABMCommonRoutines.enModes.Normal;

            UpdateControls(modoActual);
            ChangeCurrentMode(modoActual);
        }

        protected virtual void CMDConsultar_Click(System.Object sender, System.EventArgs e)
        {
            modoActual = ABMCommonRoutines.enModes.Query;
            UpdateControls(modoActual);
            ChangeCurrentMode(modoActual);
        }

        protected virtual void CMDEditar_Click(System.Object sender, System.EventArgs e)
        {
            bool actionCancelled = false;

            if (modoActual != ABMCommonRoutines.enModes.Edition)
            {
                modoActual = ABMCommonRoutines.enModes.Edition;
            }
            else
            {
                var paramList = new object[] { false, actionCancelled };
                FireEvent(SavingItem, paramList);
                actionCancelled = (bool)paramList[1];

                if (!actionCancelled)
                    modoActual = ABMCommonRoutines.enModes.Normal;
            }

            if (!actionCancelled)
            {
                UpdateControls(modoActual);
                ChangeCurrentMode(modoActual);
            }
        }

        protected virtual void CMDEliminar_Click(object sender, System.EventArgs e)
        {
            FireEvent(DeletingItem, null);
            modoActual = ABMCommonRoutines.enModes.Normal;

            UpdateControls(modoActual);
            ChangeCurrentMode(modoActual);
        }

        protected void CMDNuevo_Click(object sender, System.EventArgs e)
        {
            bool actionCancelled = false;

            if (modoActual != ABMCommonRoutines.enModes.New)
            {
                modoActual = ABMCommonRoutines.enModes.New;
            }
            else
            {
                var paramList = new object[] { true, actionCancelled };
                FireEvent(SavingItem, paramList);
                actionCancelled = (bool)paramList[1];

                if (!actionCancelled)
                    modoActual = ABMCommonRoutines.enModes.Normal;
            }

            if (!actionCancelled)
            {
                UpdateControls(modoActual);
                ChangeCurrentMode(modoActual);
            }
        }

        #endregion CMDs

        #region TreeView Methods

        private void TViewCategorias_AfterSelect(System.Object sender, System.Windows.Forms.TreeViewEventArgs e)
        {
            if (e.Node.Nodes.Count > 0)
            {
                this.Text = "Seleccione una categoria";
                GrpDatosCargados.Text = "Datos cargados";
                GrpCargaDatos.Text = "Cargar datos";
            }
            else
            {
                this.Text = "Administrador de " + e.Node.Text;
                GrpDatosCargados.Text = e.Node.Text + " cargados";
                GrpCargaDatos.Text = "Cargar datos de " + e.Node.Text;
            }

            FireEvent(SelectedNode, new object[] { sender, e });
        }

        private void TViewCategorias_NodeMouseDoubleClick(object sender, System.Windows.Forms.TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Nodes.Count == 0)
            {
                HorizontalSplitter.ToggleState();
            }
        }

        #endregion TreeView Methods

        #region Form Controls Behavior

        protected virtual void UpdateControls(ABMCommonRoutines.enModes pModo)
        {
            switch (pModo)
            {
                case ABMCommonRoutines.enModes.Search:
                    SetSearchMode();

                    break;

                case ABMCommonRoutines.enModes.Query:
                    SetQueryMode();

                    break;

                case ABMCommonRoutines.enModes.Edition:
                    SetEditMode();
                    CMDEditar.Text = "&Guardar";
                    CMDEditar.Image = Resources.font_awesome_4_7_0_save_16_0_000000_none;

                    break;

                case ABMCommonRoutines.enModes.Normal:
                    SetNormalMode();
                    CMDNuevo.Text = "&Nuevo";
                    CMDNuevo.Image = Resources.font_awesome_4_7_0_plus_16_0_000000_none1;
                    CMDEditar.Text = "&Editar";
                    CMDEditar.Image = Resources.material_icons_3_0_1_mode_edit_16_0_000000_none;
                    PBar.Value = PBar.Minimum;

                    break;

                case ABMCommonRoutines.enModes.New:
                    SetNewMode();
                    CMDNuevo.Text = "&Guardar";
                    CMDNuevo.Image = Resources.font_awesome_4_7_0_save_16_0_000000_none;

                    break;
            }

            bool enableDisplayControls = (pModo == ABMCommonRoutines.enModes.Normal | pModo == ABMCommonRoutines.enModes.Query | pModo == ABMCommonRoutines.enModes.Search);

            TViewCategorias.Enabled = enableDisplayControls;

            if (ChangeItemsListState != null)
                ChangeItemsListState(enableDisplayControls);

            SPCDatosCargados.Panel2.Enabled = enableDisplayControls;

            if (enhanceDisplayArea)
                UpdateSplitters(pModo);
        }

        private void EnableABMButton(ABMCommonRoutines.enModes pModo, ButtonBase oCtl)
        {
            string ctlName = oCtl.Name.ToLower();

            if (ctlName == CMDNuevo.Name.ToLower())
            {
                oCtl.Enabled = (pModo == ABMCommonRoutines.enModes.Normal | pModo == ABMCommonRoutines.enModes.New | pModo == ABMCommonRoutines.enModes.Search);
            }
            else if (ctlName == CMDCancelar.Name.ToLower())
            {
                oCtl.Enabled = (pModo == ABMCommonRoutines.enModes.Edition | pModo == ABMCommonRoutines.enModes.New | pModo == ABMCommonRoutines.enModes.Query | pModo == ABMCommonRoutines.enModes.Search);
            }
            else if (ctlName == CMDConsultar.Name.ToLower())
            {
                oCtl.Enabled = (pModo == ABMCommonRoutines.enModes.Normal | pModo == ABMCommonRoutines.enModes.Search);
            }
            else if (ctlName == CMDEditar.Name.ToLower())
            {
                oCtl.Enabled = (pModo == ABMCommonRoutines.enModes.Normal | pModo == ABMCommonRoutines.enModes.Edition | pModo == ABMCommonRoutines.enModes.Search);
            }
            else if (ctlName == CMDBuscar.Name.ToLower())
            {
                oCtl.Enabled = (pModo == ABMCommonRoutines.enModes.Normal | pModo == ABMCommonRoutines.enModes.Query | pModo == ABMCommonRoutines.enModes.Search);
            }
            else if (ctlName == CMDEliminar.Name.ToLower())
            {
                oCtl.Enabled = (pModo == ABMCommonRoutines.enModes.Normal | pModo == ABMCommonRoutines.enModes.Query | pModo == ABMCommonRoutines.enModes.Search);
            }
        }

        private void EnableABMButtons(ABMCommonRoutines.enModes pModo)
        {
            List<ButtonBase> oList = null;
            ButtonBase oCtl = null;

            moABMRoutines.CreateControlList<ButtonBase>(PNLToolBar, ref oList);

            foreach (ButtonBase oCtl_loopVariable in oList)
            {
                oCtl = oCtl_loopVariable;
                EnableABMButton(pModo, oCtl);
            }
        }

        private void SetEditMode()
        {
            EnableABMButtons(ABMCommonRoutines.enModes.Edition);
            if (formStateAffectsContainedControls)
                moABMRoutines.LockTextBoxes<TextBoxBase>(false);
        }

        private void SetNewMode()
        {
            EnableABMButtons(ABMCommonRoutines.enModes.New);

            if (formStateAffectsContainedControls)
            {
                moABMRoutines.ClearTextBoxes();
                moABMRoutines.LockTextBoxes<TextBoxBase>(false);
            }
        }

        private void SetNormalMode()
        {
            EnableABMButtons(ABMCommonRoutines.enModes.Normal);
            if (formStateAffectsContainedControls)
                moABMRoutines.LockTextBoxes<TextBoxBase>(true);
        }

        private void SetQueryMode()
        {
            EnableABMButtons(ABMCommonRoutines.enModes.Query);
            if (formStateAffectsContainedControls)
                moABMRoutines.LockTextBoxes<TextBoxBase>(true);
        }

        private void SetSearchMode()
        {
            EnableABMButtons(ABMCommonRoutines.enModes.Search);
            if (formStateAffectsContainedControls)
                moABMRoutines.LockTextBoxes<TextBoxBase>(true);
        }

        private void UpdateSplitters(ABMCommonRoutines.enModes mode)
        {
            bool updateVerticalSplitter = false;
            bool updateHorizontalSplitter = false;

            switch (mode)
            {
                case ABMCommonRoutines.enModes.Normal:
                    updateHorizontalSplitter = !HorizontalSplitter.ControlToHide.Visible;
                    updateVerticalSplitter = !VerticalSplitter.ControlToHide.Visible;
                    break;

                case ABMCommonRoutines.enModes.New:
                case ABMCommonRoutines.enModes.Edition:
                    updateHorizontalSplitter = HorizontalSplitter.ControlToHide.Visible;
                    break;

                case ABMCommonRoutines.enModes.Query:
                case ABMCommonRoutines.enModes.Search:
                    updateVerticalSplitter = VerticalSplitter.ControlToHide.Visible;
                    updateHorizontalSplitter = HorizontalSplitter.ControlToHide.Visible;
                    break;
            }

            if (updateVerticalSplitter)
                VerticalSplitter.ToggleState();

            if (updateHorizontalSplitter)
                HorizontalSplitter.ToggleState();
        }

        #endregion Form Controls Behavior

        #region Collapsible Controls Special Properties

        #region Collapsable Splitter 1

        protected bool Splitter1UseAnimations
        {
            get { return HorizontalSplitter.UseAnimations; }
            set { HorizontalSplitter.UseAnimations = value; }
        }

        protected void Splitter1_Toggle_Status()
        {
            HorizontalSplitter.ToggleState();
        }

        #endregion Collapsable Splitter 1

        #region Collapsable Splitter 2

        protected bool Splitter2UseAnimations
        {
            get { return VerticalSplitter.UseAnimations; }
            set { VerticalSplitter.UseAnimations = value; }
        }

        protected void Splitter2_Toggle_Status()
        {
            VerticalSplitter.ToggleState();
        }

        #endregion Collapsable Splitter 2

        #endregion Collapsible Controls Special Properties

        #region Private Methods

        private void ChangeCurrentMode(ABMCommonRoutines.enModes newMode)
        {
            Delegate eventHandler;
            object[] paramsList = null;

            switch (newMode)
            {
                case ABMCommonRoutines.enModes.Normal:
                    eventHandler = EnterNormalMode;
                    break;

                case ABMCommonRoutines.enModes.Edition:
                    eventHandler = EnterEditingMode;
                    break;

                case ABMCommonRoutines.enModes.New:
                    eventHandler = EnterNewMode;
                    break;

                case ABMCommonRoutines.enModes.Query:
                    eventHandler = EnterQueryMode;
                    break;

                case ABMCommonRoutines.enModes.Search:
                    eventHandler = EnterSearchMode;
                    break;

                default:
                    eventHandler = EnterNormalMode;
                    break;
            }

            FireEvent(eventHandler, paramsList);
        }

        private void ChangeQueryButtonVisibility(bool show)
        {
            CMDConsultar.Visible = show;
        }

        private void ChangeSearchButtonVisibility(bool show)
        {
            CMDBuscar.Visible = show;
        }

        private void FireEvent(Delegate evnt, object[] paramsList)
        {
            if (evnt != null)
                evnt.DynamicInvoke(paramsList);
        }

        private void StartProgressBarLoadingSimulation()
        {
            if (!UseMarqueeProgressBar)
            {
                timerPBarSimulation = new Timer();
                timerPBarSimulation.Interval = 1000;
                timerPBarSimulation.Tick += new EventHandler(timerPBarSimulation_Tick);
                timerPBarSimulation.Start();
            }
            else
            {
                PBar.Style = ProgressBarStyle.Marquee;
                PBar.MarqueeAnimationSpeed = 30;
            }
        }

        private void timerPBarSimulation_Tick(object sender, EventArgs e)
        {
            if (PBar.Value * PBar.Maximum / 100 > 10)
                UpdateProgressBar(PBar.Value + 1);
        }

        #endregion Private Methods
    }
}