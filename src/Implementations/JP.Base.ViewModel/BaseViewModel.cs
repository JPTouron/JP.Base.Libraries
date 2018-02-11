using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JP.Base.ViewModel
{
    public class BaseViewModel<T>
    {
        public T Id { get; set; }

        public byte[] Version { get; set; }

    }
}
