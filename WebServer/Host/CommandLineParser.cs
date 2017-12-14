using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServer.Host
{
    class CommandLineParser
    {
        string[] _commadLineParameters;
        public CommandLineParser(string[] commadLineParameters)
        {
            _commadLineParameters = commadLineParameters;
        }

        public string GetParameterValue(string parameterName)
        {
            for (var indx = 0; indx < _commadLineParameters.Length-1; indx++)
            {
                if(_commadLineParameters[indx].Equals(parameterName, StringComparison.OrdinalIgnoreCase ))
                {
                    return _commadLineParameters[indx + 1];
                }
            }
            return string.Empty;
        }
    }
}
