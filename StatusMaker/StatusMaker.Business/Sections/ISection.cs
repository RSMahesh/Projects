using System;

namespace StatusMaker.Business.Sections
{
    public interface ISection
    {
          string GetItmesAsHtml(DateTime statusDate, string memberName, bool validateAganistJira);

          string TemplatePlaceHolder { get; }
    }
}
