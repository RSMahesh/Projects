using Repository;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace InfoWebTicketSystem.MVC
{
    public static class UnitWorkFactory
    {
        public static IUnitOfWork GetUnitOfWork()
        {
            return new UnitOfWork<TicketContext>();
        }
    }
}