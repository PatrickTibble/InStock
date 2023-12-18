using InStock.Common.InventoryService.Abstraction.Entities;
using InStock.Frontend.Abstraction.Adapters;
using InStock.Frontend.Abstraction.Models;
using System.Drawing;

namespace InStock.Frontend.Core.Adapters
{
    public class RevenueToChartDataSetAdapter : IAdapter<IList<RevenueReport>, ChartDataSet>
    {
        public ChartDataSet Convert(IList<RevenueReport> value)
        {
            return new ChartDataSet
            {
                Axis = new Axes
                {
                    X = new Axis
                    {
                        Names = new List<string>()
                        {
                            "One",
                            "Two",
                            "Three"
                        }
                    },
                    Y = new Axis
                    {
                        Names = new List<string>()
                        {
                            "Uno",
                            "Dos",
                            "Tres"
                        }
                    }
                },
                Points = value.Select(r => new ChartPoint((double)r.Revenue)).ToList()
            };
        }
    }
}
