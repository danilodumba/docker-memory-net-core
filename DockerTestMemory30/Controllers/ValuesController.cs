using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DockerTestMemory30.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        const int MEGA = 1048576;
        // GET api/values
        [HttpGet("byte")]
        public async Task<ActionResult> GetByte()
        {
            using (Process currentProc = Process.GetCurrentProcess())
            {
                decimal memoryUsed = currentProc.PrivateMemorySize64;
                decimal workSetting64 = currentProc.WorkingSet64;
                decimal memoryGC = GC.GetTotalMemory(false);

                var result = new {
                    memoryUsed,
                    workSetting64,
                    memoryGC
                };

                return Ok(await Task.FromResult(result));
            }
        }

        [HttpGet("mega")]
        public async Task<IActionResult> GetMega()
        {
            Process currentProc = Process.GetCurrentProcess();
            decimal memoryUsed = currentProc.PrivateMemorySize64 / MEGA;
            decimal workSetting64 = currentProc.WorkingSet64 / MEGA;
            decimal memoryGC = GC.GetTotalMemory(false) / MEGA;

            currentProc.Dispose();

            var result = new {
                memoryUsed,
                workSetting64,
                memoryGC
            };

            return Ok(await Task.FromResult(result));
            
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await Task.FromResult("Api funcionando"));
        }
    }
}
