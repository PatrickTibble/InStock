using System;

namespace InStock.Frontend.API.Mocks
{
    internal abstract class BaseMockService
    {
        private readonly Random _random = new Random(DateTime.UtcNow.Millisecond);

        /// <summary>
        /// Simulates a network delay between min-max
        /// </summary>
        /// <param name="token"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        protected Task Delay(CancellationToken token, int min = 100, int max = 250)
        {
            var millisecondDelay = _random.Next(min, max);
            var timespanDelay = TimeSpan.FromMilliseconds(millisecondDelay);
            return Task.Delay(timespanDelay, token);
        }
    }
}