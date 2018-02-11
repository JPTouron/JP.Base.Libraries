using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JP.Base.MVP.Implementation.Contracts.Views.Base;

namespace MVPWithLog4Net
{
    public interface IFormView : IBaseModelView<Model>, IBaseModelValidationView, IBaseView<IFormView>
    {
    }
}
