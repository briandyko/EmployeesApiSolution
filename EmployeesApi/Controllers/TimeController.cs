using EmployeesApi.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeesApi.Controllers
{
    public class TimeController : ControllerBase
    {
        [HttpGet("time")] // GET /time
        public ActionResult GetTheTime ([FromServices] ISystemTime clock)
        {
            //throw new ArgumentOutOfRangeException();
            //clock = new SystemTime(); -- this tightly couples --- rely on the abstraction (interface) -- b/c that can change -- think Jeff's credit card vendor thing
            return Ok($"The time is {clock.GetCurrent().ToLongTimeString()}");
        }

    }
}
