using InStock.Common.Abstraction.Services.Logger;
using InStock.Common.Core.Extensions;
using InStock.Common.InventoryService.Abstraction.Entities;
using InStock.Common.InventoryService.Abstraction.Services;
using InStock.Common.InventoryService.Abstraction.TransferObjects.Revenue.Report;
using InStock.Frontend.Abstraction.Managers;
using InStock.Frontend.Core.Extensions;

namespace InStock.Frontend.Core.Managers
{
    public class RevenueManager : IRevenueManager
    {
        private readonly IStorageManager _storageManager;
        private readonly ILogger _logger;
        private readonly IRevenueService _revenueService;

        public RevenueManager(
            ILogger logger,
            IStorageManager storageManager,
            IRevenueService service)
        {
            _logger = logger;
            _revenueService = service;
            _storageManager = storageManager;
        }

        public async Task<IList<RevenueReport>> GetRevenueReportAsync()
        {
            try
            {
                var accessToken = await _storageManager.GetAccessTokenAsync();
                var token = new CancellationTokenSource().Token;
                var request = new RevenueRequest
                {
                    
                };

                var result = await _revenueService.GetRevenueReport(accessToken!, request, token);

                if (result.IsSuccessfulStatusCode() && result.Data != null)
                {
                    return result.Data.Reports;
                }
            }
            catch (Exception e)
            {
                _logger.LogExceptionAsync(e).FireAndForgetSafeAsync();
            }

            return new List<RevenueReport>();
        }
    }
}
