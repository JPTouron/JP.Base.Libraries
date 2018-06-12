using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using JP.Base.Common.Forms;
using JP.Base.Implementations.MVP.Contracts.Views.Base;
using JP.Base.Implementations.MVP.Support.Factories;
using JP.Base.Implementations.MVP.Winforms.Implementation.Messages;
using JP.Base.Implementations.MVP.Winforms.Contracts;

namespace JP.Base.Implementations.MVP.Winforms.Implementation
{
    /// <summary>
    /// as an absract class now we are forced to inherit from a control in-between if we wanna use the form designer at some point
    ///
    /// <para>this class enables the creation of a simple user control which already has sufficient logic implemented
    /// to support a view within an mvp architecture</para>
    /// <para>this control is designed to handle a model type and be backed by a view class that would repreent that part on an mvp
    /// architecture model</para>
    /// <para>in order to enable visual control edition based on forms designer, create a user control
    /// and make it inherit from this class (we call this a slice control), then create another user control the actual one you are going to use, and
    /// make it inherit from the slice control</para>
    /// </summary>
    /// <typeparam name="TModel">represents the type of the model this control will handle</typeparam>
    /// <typeparam name="TIView">represents the type of the view that will handle this user control</typeparam>
    public abstract partial class BaseItemControl<TModel, TIView> : UserControl, IMVPItemControl
        where TIView : IBaseItemView<TModel, TIView>
        where TModel : class
    {
        private Form parentForm;

        public BaseItemControl()
        {
            InitializeComponent();

            if (ViewProvider.ViewFactory != null)
                CreateAndBindView();
        }

        protected IBaseItemView<TModel, TIView> View { get; set; }

        public event NotifyUpdatedItemHandler NotifyUpdatedItem;

        public event ViewOperationalStatusChangedHandler ViewOperationalStatusChanged;

        /// <summary>
        /// when overriden this method performs clearing actions over the form you are using, by default it does nothing
        /// <para>
        /// this method will be called on <see cref="PrepareNewItemCreation"/> method invocation
        /// </para>
        /// </summary>
        public virtual void ClearForm()
        {
            // by default dont do anything
        }

        public bool PerformCreateItem()
        {
            var model = BindViewToModel();
            var result = View.PerformCreateItem(model);
            DoNotifyUpdatedItem(result);
            return result;
        }

        public bool PerformDeleteItem()
        {
            var model = BindViewToModel();
            var result = View.PerformDeleteItem(model);
            DoNotifyUpdatedItem(result);
            return result;
        }

        public bool PerformUpdateItem()
        {
            var model = BindViewToModel();
            var result = View.PerformUpdateItem(model);
            DoNotifyUpdatedItem(result);
            return result;
        }

        public virtual void PrepareItemEdition()
        {
            RefreshViewData();
            EnableControls(true);
        }

        public virtual void PrepareNewItemCreation()
        {
            ClearForm();
            EnableControls(true);
        }

        public void RefreshViewData()
        {
            RefreshViewDataAdditionalActionsToPerform();
            BindModelToView(View.Model);
        }

        public void SetBusyState()
        {
            Cursor.Current = Cursors.WaitCursor;
        }

        public void SetModel(object model)
        {
            View.Model = (TModel)model;
        }

        public void SetNormalState()
        {
            EnableControls(false);
            RefreshViewData();
            Cursor.Current = Cursors.Default;
        }

        /// <summary>
        /// <para>this method runs last when <see cref="EnableControls(bool)"/> is invoked and all controls have switched state, by default it does nothing</para>
        /// </summary>
        protected virtual void AfterEnablingControls()
        {
            // does nothing by default
        }

        /// <summary>
        /// <para>this method runs first when <see cref="EnableControls(bool)"/> is invoked and controls have NOT switched state</para>
        /// </summary>
        /// <param name="cancelStateSwitch">allows to cancels the state switch of controls</param>
        protected virtual void BeforeEnablingControls(out bool cancelStateSwitch)
        {
            cancelStateSwitch = false;
        }

        /// <summary>
        /// new method to set the binding to the model
        /// <para>this method is invoked when calling <see cref="RefreshViewData"/> and runs after <see cref="RefreshViewDataAdditionalActionsToPerform"/>  </para>
        /// </summary>
        /// <param name="model"></param>
        protected abstract void BindModelToView(TModel model);

        protected virtual void BindToViewEvents()
        {
            View.NotifyUIDisplayErrorMessage += OnViewNotifyDisplayErrorMessage;
            View.NotifyUIDisplayConfirmDeletionMessage += OnViewNotifyDisplayConfirmDeletionMessage;
            View.NotifyUIValidationErrors += OnViewNotifyValidationErrors;

            View.NotifyUISetBusyState += OnNotifySetBusyState;
            View.NotifyUISetNormalState += OnNotifySetNormalState;
        }

        /// <summary>
        /// <para>this method is invoked on <see cref="PerformCreateItem"/> <see cref="PerformDeleteItem"/> <see cref="PerformUpdateItem"/> methods</para>
        /// <para>it defines how to bind the view to a model, passing view's controls values into a model instance and returning it</para>
        /// </summary>
        /// <returns><see cref="TModel"/> as the model type to be returned</returns>
        protected abstract TModel BindViewToModel();

        protected void EnableControls(bool enable)
        {
            var abmRoutines = new ABMCommonRoutines();
            abmRoutines.currentForm = this;

            bool switchStateWasCancelled;
            BeforeEnablingControls(out switchStateWasCancelled);

            if (!switchStateWasCancelled)
            {
                var excludedControls = new List<Type>();
                excludedControls.Add(typeof(Label));
                excludedControls.Add(typeof(Panel));
                abmRoutines.EnableAllControls(enable, this, excludeTypes: excludedControls);
            }

            AfterEnablingControls();
        }

        /// <summary>
        /// <para>this method is called when validation errors are found on the view</para>
        /// <para>if you DO NOT override this method then you will have to override <see cref="SetError(int, string)"/> method in order for
        /// the errors to reach your implementation of this class</para>
        /// </summary>
        /// <param name="e">a dictionary with the errors that were found</param>
        protected virtual void HandleValidationErrors(IDictionary<int, string> e)
        {
            e.ToList().ForEach(item => SetError(item.Key, item.Value));
        }

        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);

            if (parentForm != null)
            {
                parentForm.Closing -= OnParentFormClosing;
            }
            parentForm = FindForm();

            if (parentForm != null)
                parentForm.Closing += OnParentFormClosing;
        }

        protected virtual void OnParentFormClosing(object sender, CancelEventArgs e)
        {
            UnBindViewEvents();
            View.TerminateView();

            parentForm.Closing -= OnParentFormClosing;
            parentForm = null;
        }

        /// <summary>
        /// <para>when overriden this method allows to perform additional actions when refreshind the view data</para>
        /// <para>this method runs first when <see cref="RefreshViewData"/> is called </para>
        /// </summary>
        protected virtual void RefreshViewDataAdditionalActionsToPerform()
        {
            // default action is to do nothin really...
        }

        /// <summary>
        /// <para>this method is invoked by <see cref="HandleValidationErrors(IDictionary{int, string})"/> when errors are found in the view</para>
        /// <para>it will be called consecutively sending each error to your code if you overriden it</para>
        /// <para>you should ONLY override this method if <see cref="HandleValidationErrors(IDictionary{int, string})"/>was NOT overriden</para>
        /// <para>by default this method does nothing</para>
        /// </summary>
        /// <param name="errorInt">the error code/identifier</param>
        /// <param name="errorMessage">the error message</param>
        protected virtual void SetError(int errorInt, string errorMessage)
        {
            throw new NotImplementedException("SetError method was not overriden. Please override either SetError or HandleValidationErrors");
        }

        protected virtual void UnBindViewEvents()
        {
            View.NotifyUIDisplayErrorMessage -= OnViewNotifyDisplayErrorMessage;
            View.NotifyUIDisplayConfirmDeletionMessage -= OnViewNotifyDisplayConfirmDeletionMessage;
            View.NotifyUIValidationErrors -= OnViewNotifyValidationErrors;

            View.NotifyUISetBusyState -= OnNotifySetBusyState;
            View.NotifyUISetNormalState -= OnNotifySetNormalState;
        }

        private void CreateAndBindView()
        {
            View = ViewProvider.ViewFactory.CreateItemView<TModel, TIView>();

            BindToViewEvents();
        }

        private void DoNotifyUpdatedItem(bool result)
        {
            var evt = NotifyUpdatedItem;
            if (result && evt != null)
                NotifyUpdatedItem(true);
        }

        private void OnNotifySetBusyState(object sender, EventArgs e)
        {
            SetBusyState();
        }

        private void OnNotifySetNormalState(object sender, EventArgs e)
        {
            SetNormalState();
        }

        private void OnViewNotifyDisplayConfirmDeletionMessage(string title, string message, ref bool confirmDeletion)
        {
            var renderer = RendererProvider.RendererFactory.CreateRenderer();
            confirmDeletion = renderer.DisplayConfirmDeleteMessage(message, title);
        }

        private void OnViewNotifyDisplayErrorMessage(string message, bool shouldDisableView)
        {
            var renderer = RendererProvider.RendererFactory.CreateRenderer();
            this.Enabled = shouldDisableView;
            ViewOperationalStatusChanged(this, !shouldDisableView);
            renderer.ShowErrorMessage(message);
        }

        private void OnViewNotifyValidationErrors(object sender, IDictionary<int, string> e)
        {
            HandleValidationErrors(e);
        }
    }
}