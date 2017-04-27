using System;
using ResellerClub.Interface.Messages;
using System.Collections.Generic;

namespace ResellerClub.Interface
{
    public interface IState : IBaseInterface 
    {
       List<string> GetCountryState(string countryCode);
    }
}
