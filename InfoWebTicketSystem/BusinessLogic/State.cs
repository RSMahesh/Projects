using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ResellerClub.Interface;
using System.Xml.Linq;
using System.Data;
using ResellerClub.DataAccess;

namespace ResellerClub.BusinessLogic
{
    public class State : BaseBRL, IState
    {

        private List<string> GetCountryStateFromBigRock(string countryCode)
        {
            string url = "https://www.bigrock.com/misc/getState.php?countrycode="+countryCode;
            string xml = PostUrl(url);
            return ParseStaeXML(xml);
        }

        private List<string> ParseStaeXML(string xml)
        {
            XElement xelement = XElement.Parse(xml);
            IEnumerable<XElement> states = xelement.Elements();
            List<string> stateList = new List<string>();
            decimal total = 0;
            foreach (var state in states)
            {
                if (state.Element("name").Value.ToUpperInvariant() != "SELECT STATE"
                    && state.Element("name").Value.ToUpperInvariant() != "OTHER"
                    )
                {
                    stateList.Add(state.Element("name").Value);
                }
            }

            return stateList;
        }
        public List<string> GetCountryState(string countryCode)
        {
            List<string> stateList;
            using (var connection = ConnectionFactory.GetConnection())
            {
                var dalState = new ResellerClub.DataAccess.State(ConnectionFactory.GetConnection());
                stateList = dalState.GetCountryState(countryCode);

                if (stateList.Count < 1)
                {
                    stateList = GetCountryStateFromBigRock(countryCode);
                    dalState.InsertCountryState(countryCode, stateList);
                }
            }
           return  stateList;
        }
    }
}
