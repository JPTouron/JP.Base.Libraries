using LRM.Casiraghi.DAL.Commands.Contracts.Base;
using LRM.Casiraghi.DAL.EF6.Model;
using System.Collections.Generic;

namespace LRM.Casiraghi.DAL.Commands.Contracts
{
    public interface IGetAllUsersCommand : ICommand
    {
        IList<User> ReturnedUsers { get; }
    }
}