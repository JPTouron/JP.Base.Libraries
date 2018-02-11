using System;
using System.Collections.Generic;

namespace JP.Base.MVP.Implementation.Contracts.Views.Base
{
    public interface IBaseModelValidationView
    {
        event EventHandler<IDictionary<int, string>> NotifyUIValidationErrors;
    }
}