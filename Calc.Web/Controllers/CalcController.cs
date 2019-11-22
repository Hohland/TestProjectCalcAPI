using Calc.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace Calc.Web.Controllers
{    
    [ApiController]
    [Route("calc")]    
    public class CalcController : ControllerBase
    {
        private readonly ILogger<CalcController> _logger;
        private readonly ICalcService calcService;

        public CalcController(ILogger<CalcController> logger, ICalcService calcService)
        {
            _logger = logger;
            this.calcService = calcService;
        }

        [Authorize(Policy = "Add")]
        [HttpGet("add")]
        public float Add([FromQuery]float[] summands)
        {
            _logger.LogInformation("Recieved add request");
            return calcService.Add(summands);
        }

        [Authorize(Policy = "Multiplicate")]
        [HttpGet("multiplicate")]
        public float Multiplicate([FromQuery]float[] multipliers)
        {
            _logger.LogInformation("Recieved multiplicate request");
            return calcService.Multiplicate(multipliers);
        }

        [AllowAnonymous]
        [HttpGet("deduct")]
        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 3600)]
        public float Deduct([FromQuery]float minuend, [FromQuery]float deductible)
        {
            _logger.LogInformation("Recieved deduct request");
            return calcService.Add(new[] { minuend, deductible * -1 });
        }

        [AllowAnonymous]
        [HttpGet("divide")]
        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 3600)]
        public float Divide([FromQuery]float divident, [FromQuery]float devider)
        {
            _logger.LogInformation("Recieved divide request");

            if (devider == 0) {
                throw new ApplicationException("Devider shouldn't be equal 0");
            }

            return calcService.Multiplicate(new[] { divident, 1 / devider });
        }
    }
}
