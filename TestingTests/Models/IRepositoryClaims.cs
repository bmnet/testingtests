using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestingTests.Models
{
    public interface IRepositoryClaims
    {
        Task<IEnumerable<Claim>> GetAll();
        Task<Claim> Get(Guid claimId);
        Task<Claim> Update(Claim claim);
        Task<Claim> Delete(Guid id);
        Task<Claim> Add(Claim claim);
    }
}
