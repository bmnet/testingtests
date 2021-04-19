using System;
namespace TestingTests.Models
{
    public class Claim
    {
        public Claim()
        {
            this.ClaimId = Guid.NewGuid();
            this.CreatedAt = DateTime.Now;
        }

        public Guid ClaimId { get; init; }
        public string Description { get; set; }
        public double Payout { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
