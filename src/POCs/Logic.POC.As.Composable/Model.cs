using JP.Base.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.POC.As.Composable
{
    public class Model:BaseModel<int>
    {

        public string Name { get; set; }
        public int Age{ get; set; }

        public Position Position { get; set; }

    }

    public class Position:BaseModel<int>
    {
        public string Description { get; set; }
    }
}
