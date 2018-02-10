using System;
using System.Collections.Generic;
using System.Linq;

namespace JP.Base.Logic.ModelValidator
{
    public abstract class BaseValidator<ErrorEnumerationType, ViewModelType>
    {
        public BaseValidator()
        {
            Errors = new List<ErrorEnumerationType>();
        }

        public IList<ErrorEnumerationType> Errors { get; protected set; }

        public abstract string GetErrorMessage(ErrorEnumerationType error);

        public IDictionary<int, string> GetMessagesForErrors()
        {
            var dic = new Dictionary<int, string>();

            if (Errors.Count > 0)
                Errors.ToList().ForEach(x => { dic.Add(Convert.ToInt32(x), GetErrorMessage(x)); });

            return dic;
        }

        public abstract void Validate(ViewModelType viewModel);
    }
}