using System;
using System.Collections.Generic;

namespace JP.Base.Implementations.MVP.Contracts.Views.Base
{
    public interface IBaseModelValidationView
    {
        event EventHandler<IDictionary<int, string>> NotifyUIValidationErrors;
    }
}