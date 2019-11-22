using Microsoft.Extensions.Logging;
using System;
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
            if (summands.Any(float.IsInfinity))
            {
                throw new ApplicationException("Too big or low number.");
            }

            return summands.Sum();
        }

        public float Multiplicate(float[] multipliers)
        {
            return multipliers.Aggregate((res, x) => res *= x);
        }
    }
}
