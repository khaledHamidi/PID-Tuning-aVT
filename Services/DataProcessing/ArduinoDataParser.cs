using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Stabilization.Models;
// Services/DataProcessing/ArduinoDataParser.cs
namespace Stabilization.Services.DataProcessing
{
    public class ArduinoDataParser : IDataParser
    {
        private long _sequenceNumber = 0;




        private long _lastSequenceNumber = -1;

        private void CheckDataLoss(long currentMillis)
        {
            if (_lastSequenceNumber != -1 && currentMillis > _lastSequenceNumber + 1)
            {
                int lostPackets = (int)(currentMillis - _lastSequenceNumber - 1);
             //   Debug.WriteLine($"[LOSS] Lost {lostPackets} packets | {_lastSequenceNumber} → {currentMillis}");
            }
            _lastSequenceNumber = currentMillis;
        }

        public bool TryParse(string rawData, out SerialDataPoint dataPoint)
        {
            dataPoint = default;

            try
            {
                rawData = rawData.Trim('\r', '\n', ' ', '\t');

                if (string.IsNullOrEmpty(rawData) || rawData.Length < 3)
                    return false;

                // Skip informational messages
                if (rawData.StartsWith(">>") || rawData.StartsWith("STATUS"))
                    return false;

                var parts = rawData.Split(':');
                if (parts.Length != 3)
                {
                    parts = rawData.Split(',');
                    if (parts.Length != 3)
                        return false;
                }

                if (long.TryParse(parts[0].Trim(), out long millis) &&
                    double.TryParse(parts[1].Trim(), NumberStyles.Float,
                                  CultureInfo.InvariantCulture, out double angle) &&
                    int.TryParse(parts[2].Trim(), NumberStyles.Integer,
                                CultureInfo.InvariantCulture, out int output))

                {
                    CheckDataLoss(millis);

                    dataPoint = new SerialDataPoint(millis, angle, output,
                                                   Interlocked.Increment(ref _sequenceNumber));
                    return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        public bool TryParseStatusParameter(string data, out KeyValuePair<string, double> parameter)
        {
            parameter = default;

            var parts = data.Split(':');
            if (parts.Length == 2)
            {
                string paramName = parts[0].Trim();
                if (double.TryParse(parts[1].Trim(), NumberStyles.Float,
                                  CultureInfo.InvariantCulture, out double value))
                {
                    parameter = new KeyValuePair<string, double>(paramName, value);
                    return true;
                }
            }

            return false;
        }
    }
}
