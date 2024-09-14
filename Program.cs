using DataCollector.DataAccess;
using DataCollector.Service;

namespace DataCollector
{
    class Program
    {
        private const int DATA_COLLECTING_INTERVAL = 5000;
        static void Main()
        {
            using var dataRepository = new DataRepository();
            using var dataCollectorService = new DataCollectorService();

            ConsoleLogService.LogSuccess("Data Collector Service started.");

            while (true)
            {
                var data = dataCollectorService.GetData();
                dataRepository.InsertData(data);
                Thread.Sleep(DATA_COLLECTING_INTERVAL);
            }
        }
    }
}