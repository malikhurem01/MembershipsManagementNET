using Hangfire;
using Microsoft.Data.SqlClient;

namespace MembershipsManagement.HostedServices
{
    public class HangfireStartup : IHostedService
    {
        private readonly IRecurringJobManager _jobManager;
        private readonly IConfiguration _configuration;
        public HangfireStartup(IRecurringJobManager jobManager, IConfiguration configuration)
        {
            _jobManager = jobManager;
            _configuration = configuration;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _jobManager.AddOrUpdate(
                       "recurring-job-every-4-minutes-to-ping-database",
                       () => PingDatabase(),
                       "*/4 * * * *"); // Cron expression for every 4 minutes
            Console.WriteLine("Application has started");
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public void PingDatabase()
        {
            /*using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                Console.WriteLine("Hangfire: SQL Server is reachable.");
                connection.Close();
            }*/
            Console.WriteLine("hangfire ping function disabled");
        }
    }
}
