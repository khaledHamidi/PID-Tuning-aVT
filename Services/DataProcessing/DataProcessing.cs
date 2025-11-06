using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stabilization.Models;
namespace Stabilization.Services.DataProcessing
{
    public interface IDataParser
    {
        bool TryParse(string rawData, out SerialDataPoint dataPoint);
        bool TryParseStatusParameter(string data, out KeyValuePair<string, double> parameter);
    }
}
