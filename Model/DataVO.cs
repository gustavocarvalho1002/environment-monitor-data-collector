using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollector.Model
{
    public record DataVO
    {
        public UnitsNet.Temperature Temperature { get; set; }
        public UnitsNet.Temperature Temperature2 { get; set; }
        public UnitsNet.RelativeHumidity Humidity { get; set; }
        public UnitsNet.Pressure Pressure { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
