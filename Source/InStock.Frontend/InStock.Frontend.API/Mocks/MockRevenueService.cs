using InStock.Common.InventoryService.Abstraction.Entities;
using InStock.Common.InventoryService.Abstraction.Services;
using InStock.Common.InventoryService.Abstraction.TransferObjects.Revenue.Report;
using InStock.Common.Models;
using InStock.Common.Models.Base;
using Refit;

namespace InStock.Frontend.API.Mocks
{
    public class MockRevenueService : BaseMockService, IRevenueService
    {
        private readonly Random _random = new();

        public Task<Result<RevenueResponse>> GetRevenueReport([Header("Authorization")] string accessToken, [Query] RevenueRequest request, CancellationToken? token = null)
        {
            var reports = new List<RevenueReport>();
            for (var i = 0; i < 50; i++)
            {
                reports.Add(new RevenueReport
                {
                    DateRange = GetRange(),
                    ReportDate = _range.End.AddDays(5),
                    Revenue = RndRev()
                });
            }
            return ValueTask.FromResult(new Result<RevenueResponse>
            {
                StatusCode = 200,
                Data = new RevenueResponse
                {
                    Reports = reports
                }
            }).AsTask();
        }

        private DateRange _range = new DateRange(new DateTime(2023, 5, 1), new DateTime(2023, 5, 30));
        private DateRange GetRange()
        {
            var range = new DateRange(_range.Start, _range.End);
            _range.Start = _range.Start.AddMonths(1);
            _range.End = _range.Start.AddMonths(2).AddDays(-1);
            return range;
        }

        private decimal RndRev()
        {
            return (decimal)(_random.NextDouble() * 500) + 500;
        }
    }
}
