using log4net;
using System;
using JP.Base.MVP.Implementation.Presenters.Base;

namespace MVPWithLog4Net
{
    public class FormPresenter : BaseModelPresenter<IFormPresenter, IFormView, Model>, IFormPresenter
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public FormPresenter(IFormView view) : base(view)
        {
            ConnectView(view, true);
        }

        public void CreateNewItem()
        {
            //saved! :-P
        }

        public void UpdateItem(bool deleteItem)
        {
            try
            {
                throw new Exception("dont wanna update shit");
            }
            catch (Exception ex)
            {
                HandleError(ex, true);
            }
        }

        protected override IFormPresenter GetPresenterEndpoint()
        {
            return this;
        }

        protected override void LogException(Exception ex)
        {
            log.Error($"An exception has occurred", ex);
        }

        protected override void RefreshView(IFormView viewInstance)
        {
            viewInstance.Model = Model;
        }
    }
}