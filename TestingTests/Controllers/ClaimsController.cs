using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestingTests.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestingTests.Controllers
{
    [Route("api/[controller]")]
    public class ClaimsController : Controller
    {
        private readonly IRepositoryClaims claimsRepository;

        public ClaimsController(IRepositoryClaims claimsRepository)
        {
            this.claimsRepository = claimsRepository;
        }

        // GET: api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await claimsRepository.GetAll();

            if(result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid claimId)
        {
            var result = await claimsRepository.Get(claimId);

            if(result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Claim claim)
        {
            if (claim != null)
            {
                await claimsRepository.Add(claim);

                return CreatedAtAction(nameof(Get), new { id = claim.ClaimId }, claim);
            }

            return BadRequest("claim data is invalid");
            
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid claimId, [FromBody] Claim claim)
        {
            if (claim == null)
            {
                return BadRequest("invalid claim data in body");
            }

            if(claimId != claim.ClaimId)
            {
                return BadRequest("claimId mismatch");
            }

            var result = await claimsRepository.Update(claim);

            return Ok(result);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await claimsRepository.Delete(id);

            if(result == null)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
