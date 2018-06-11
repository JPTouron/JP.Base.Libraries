using System;
using System.Collections.Generic;
using JP.Base.Logic.ModelValidator;
using JP.Base.Implementations.MVP.Contracts.Views.Base;
using JP.Base.MVP;

namespace JP.Base.Implementations.MVP.Views.Base
{
    /// <summary>
    /// base view to group and define basic view functionality for views that display model
    /// information <remarks>it is worthy to note that after creating a control that inherits from
    /// this class, in order for the VS form designer to be able to render it, you might have to restart
    /// VS and open the designer, as it may not work right after control creation</remarks>
    /// </summary>
    public abstract class BaseItemView<TModel, TIView, ErrorEnumerationType> : BaseView, IBaseItemView<TModel, TIView>
        where TIView : IView<TIView>
        where TModel : class
    {
        public BaseItemView()
        {
        }

        public virtual TModel Model
        {
            get;
            set;
        }

        protected abstract string ItemName { get; }

        public event EventHandler NotifyPerformCreateItem;

        public event EventHandler<bool> NotifyPerformUpdateItem;

        public event EventHandler NotifyTerminating;

        public event NotifyDisplayConfirmDeletionMessageHandler NotifyUIDisplayConfirmDeletionMessage;

        public event EventHandler<IDictionary<int, string>> NotifyUIValidationErrors;

        public virtual bool DisplayConfirmDeletionMessage(string message)
        {
            var evt = NotifyUIDisplayConfirmDeletionMessage;
            var returnedVal = false;
            if (evt != null)
                evt("Borrar " + ItemName, message, ref returnedVal);

            return returnedVal;
        }

        public bool PerformCreateItem(TModel model)
        {
            var result = false;

            if (ValidateModel(model))
            {
                var evt = NotifyPerformCreateItem;
                if (evt != null)
                {
                    Model = model;
                    evt(this, null);
                    result = true;
                }
            }

            return result;
        }

        public bool PerformDeleteItem(TModel model)
        {
            return UpdateOrDeleteItem(model, true);
        }

        public bool PerformUpdateItem(TModel model)
        {
            return UpdateOrDeleteItem(model, false);
        }

        public void TerminateView()
        {
            var notify = NotifyTerminating;
            if (notify != null)
                notify(this, null);
        }

        protected abstract BaseValidator<ErrorEnumerationType, TModel> GetValidator();

        protected virtual bool ValidateModel(TModel model)
        {
            var validator = GetValidator();

            validator.Validate(model);

            if (validator.Errors.Count > 0)
            {
                var evt = NotifyUIValidationErrors;
                if (evt != null)
                    evt(this, (IDictionary<int, string>)validator.GetMessagesForErrors());
            }

            return validator.Errors.Count == 0;
        }

        private bool UpdateOrDeleteItem(TModel model, bool forDeletion)
        {
            bool result = false;

            if (forDeletion || ValidateModel(model))
            {
                var evt = NotifyPerformUpdateItem;
                if (evt != null)
                {
                    Model = model;
                    evt(this, forDeletion);
                    result = true;
                }
            }

            return result;
        }
    }
}