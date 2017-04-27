using System;

namespace StatusMaker.Business
{
    public interface IStatusGenerator
    {
        string GenerateStatusForSingleDay(DateTime statusDate, string forMember, bool validateAganistJira);
    }
}