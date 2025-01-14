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

        [HttpPost]
        public async Task<IActionResult> CreateBPKB([FromBody] BPKB bpkb)
        {
            if (bpkb == null)
            {
                return BadRequest();
            }

            await _service.CreateBPKBAsync(bpkb);
            return CreatedAtAction(nameof(CreateBPKB), new { AgreementNumber = bpkb.AgreementNumber }, bpkb);
        }

        [HttpGet("{agreementNumber}")]
        public async Task<ActionResult<BPKB>> GetBPKB(string agreementNumber)
        {
            var bpkb = await _service.GetBPKBByAgreementNumberAsync(agreementNumber);

            if (bpkb == null)
            {
                return NotFound();
            }

            return Ok(bpkb);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBPKB(BPKB bpkb)
        {
            await _service.UpdateBPKBAsync(bpkb);
            return NoContent();
        }

        [HttpDelete("{agreementNumber}")]
        public async Task<IActionResult> DeleteBPKB(string agreementNumber)
        {
            await _service.DeleteBPKBAsync(agreementNumber);
            return NoContent();
        }

    }
}
