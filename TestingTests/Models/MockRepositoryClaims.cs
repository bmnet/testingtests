using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestingTests.Models
{
    public class MockRepositoryClaims : IRepositoryClaims
    {
        private List<Claim> claimsList;

        public MockRepositoryClaims()
        {
            claimsList = new List<Claim>
            {
                new Claim
                {
                    Description = "1st mock claim",
                    Payout = 1000
                },
                new Claim
                {
                    Description = "2st mock claim",
                    Payout = 2000.99
                },
                new Claim
                {
                    Description = "3st mock claim",
                    Payout = 900.50
                }
            };
        }

        public async Task<Claim> Add(Claim claim)
        {
            claimsList.Add(claim);

            await Task.Delay(100);
            return claim;
        }

        public async Task<Claim> Delete(Guid id)
        {
            var result = claimsList.FirstOrDefault(c => c.ClaimId == id);

            if(result == null)
            {
                return null;
            }

            claimsList.Remove(result);

            await Task.Delay(100);
            return result;
        }

        public async Task<Claim> Get(Guid claimId)
        {
            var result = claimsList.FirstOrDefault(c => c.ClaimId == claimId);

            if (result == null)
            {
                return null;
            }

            await Task.Delay(100);
            return result;
        }

        public async Task<IEnumerable<Claim>> GetAll()
        {
            await Task.Delay(100);
            return claimsList.ToList();
        }

        public async Task<Claim> Update(Claim claim)
        {
            var result = claimsList.FirstOrDefault(c => c.ClaimId == claim.ClaimId);

            result.Description = claim.Description;
            result.Payout = claim.Payout;
            result.UpdatedAt = DateTime.Now;

            await Task.Delay(100);
            return result;
        }
    }
}
