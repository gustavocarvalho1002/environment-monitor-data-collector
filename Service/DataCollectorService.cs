using DataCollector.Model;
using Iot.Device.SenseHat;
using System.Device.I2c;

namespace DataCollector.Service
{
    public class DataCollectorService: IDisposable
    {

        private readonly SenseHat _senseHat;
        private readonly I2cBus _i2cBus;

        public DataCollectorService()
        {
            _i2cBus = I2cBus.Create(1);
            _senseHat = new SenseHat(_i2cBus);
        }

        public DataVO GetData()
        {
            var data = new DataVO();

            try
            {
                data.Temperature = _senseHat.Temperature;
                data.Temperature2 = _senseHat.Temperature2;
                data.Humidity = _senseHat.Humidity;
                data.Pressure = _senseHat.Pressure;
                data.Timestamp = DateTime.Now;
            }
            catch (Exception ex)
            {
                ConsoleLogService.LogError($"Error retrieving sensor data: {ex.Message}");
            }

            return data;
        }

        public void Dispose()
        {
            _senseHat.Dispose();
            _i2cBus.Dispose();
        }
    }
}
