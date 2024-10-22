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
                       "*/1 * * * *"); // Cron expression for every 4 minutes
            Console.WriteLine("Application has started");
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public void PingDatabase()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    // Open connection (this is effectively the "ping")
                    connection.Open();

                    // If the connection is opened successfully, the server is reachable
                    Console.WriteLine("SQL Server is reachable.");

                    // Closing the connection
                    connection.Close();
                }
            }
            catch (SqlException ex)
            {
                // Handle the exception if the connection fails
                Console.WriteLine("Unable to reach SQL Server.");
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}
