using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace Calc.Web.Services
{
    public class CalcService : ICalcService
    {
        private readonly ILogger<ICalcService> logger;

        public CalcService(ILogger<ICalcService> logger)
        {
            this.logger = logger;
        }

        public float Add(float[] summands)
        {
            return summands.Sum();
        }

        public float Multiplicate(float[] multipliers)
        {
            return multipliers.Aggregate((res, x) => res *= x);
        }
    }
}
