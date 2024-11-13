using Microsoft.AspNetCore.SignalR;

using Repositorio;

namespace ArrendaVisit.SingnaIR
{
    public class TimeWorker : BackgroundService
    {
        private readonly IHubContext<ReportHub> _reportHub;
        private readonly IServiceProvider _serviceProvider;
        public TimeWorker(IHubContext<ReportHub> reportHub, IServiceProvider serviceProvider)
        {
            _reportHub = reportHub;
            _serviceProvider = serviceProvider;
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _serviceProvider.CreateScope();
                var reportDataService = scope.ServiceProvider.GetRequiredService<IServicioCliente>();


                var reportData = await reportDataService.GetAllConsultaServicioCliente();
                var methodName = "TransferReportData";


                await _reportHub.Clients.All.SendAsync(
                    methodName,
                    reportData,
                    stoppingToken);


                await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);
            }
        }
    }
}
