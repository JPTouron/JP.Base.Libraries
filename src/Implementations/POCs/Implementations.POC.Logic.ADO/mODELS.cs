using JP.Base.DAL.EF6.Model;
using JP.Base.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementations.POC.Logic.ADO
{
    public class Client:BaseModelEf<int>

    {
        public string Code { get; set; }
        public string Name { get; set; }


    }
}
