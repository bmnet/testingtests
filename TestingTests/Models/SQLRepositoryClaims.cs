using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TestingTests.Models
{
    public class SQLRepositoryClaims : IRepositoryClaims
    {
        private readonly AppDbContext context;

        public SQLRepositoryClaims(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<Claim> Add(Claim claim)
        {
            var result = await context.Claims.AddAsync(claim);
            await context.SaveChangesAsync();

            return result.Entity;
        }

        public async Task<Claim> Delete(Guid id)
        {
            var result = await context.Claims.FirstOrDefaultAsync(c => c.ClaimId == id);

            if(result != null)
            {
                context.Claims.Remove(result);
                await context.SaveChangesAsync();

                return result;
            }

            return null;
        }

        public async Task<Claim> Get(Guid claimId)
        {
            return await context.Claims.FirstOrDefaultAsync(c => c.ClaimId == claimId);
        }

        public async Task<IEnumerable<Claim>> GetAll()
        {
            return await context.Claims.ToListAsync();
        }

        public async Task<Claim> Update(Claim claim)
        {
            var result = await context.Claims.FirstOrDefaultAsync(c => c.ClaimId == claim.ClaimId);

            if (result != null)
            {
                result.Description = claim.Description;
                result.Payout = claim.Payout;
                result.UpdatedAt = DateTime.Now;

                await context.SaveChangesAsync();

                return result;
            }

            return null;
        }
    }
}
