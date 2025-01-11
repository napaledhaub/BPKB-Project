using BPKBAPI.Models;
using BPKBAPI.Service;
using BPKBAPI.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BPKBAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BPKBController : ControllerBase
    {
        private readonly IBPKBService _service;

        public BPKBController(IBPKBService service)
        {
            _service = service;
        }

        [HttpPost("CreateBPKB")]
        public async Task<IActionResult> CreateBPKB([FromBody] BPKB bpkb)
        {
            if (bpkb == null)
            {
                return BadRequest();
            }

            await _service.CreateBPKBAsync(bpkb);
            return CreatedAtAction(nameof(CreateBPKB), new { AgreementNumber = bpkb.AgreementNumber }, bpkb);
        }
    }
}
