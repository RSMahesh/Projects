using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfoWebTicketSystem.BRL.Interface;

namespace InfoWebTicketSystem.BRL
{
    public class AccountService : BaseBRL, IAccountService
    {
        public void Register(Entities.Account account)
        {
            var unitOfWork = UnitWorkFactory.GetUnitOfWork();
            var accountRepositary = unitOfWork.GetRepository<Entities.Account>();
            accountRepositary.Add(account);
            unitOfWork.SaveChanges();
        }

        public Entities.Account Authentication(Entities.Account account)
        {
            Entities.Account validAcc= null;
            var unitOfWork = UnitWorkFactory.GetUnitOfWork();
            var accountRepositary = unitOfWork.GetRepository<Entities.Account>();
            var accList = accountRepositary.Find(a => a.Email == account.Email && a.Password == account.Password);
            if(accList.Count() > 0)
            validAcc =  accList.ElementAt(0);

            return validAcc;
        }
    }
}
