using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO.DAL.Commands.Contracts
{
    public interface IDbAccess
    {
         bool CreateUser(object user);
    }
}
