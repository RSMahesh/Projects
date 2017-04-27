using System;
namespace InfoWebTicketSystem.BRL.Interface
{
    public interface IAccountService
    {
        Entities.Account Authentication(Entities.Account account);
        void Register(Entities.Account account);
       // void GetUser(Guid userId);
    }
}
