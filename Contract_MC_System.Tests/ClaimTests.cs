using System.Linq;
using System.Security.Claims;
using Contract_MC_System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xunit;

namespace Contract_MC_System.Tests
{
    public class ClaimTests
    {
        [Fact]
        public void CanSubmitClaimSuccessfully()
        {
            var db = new TestDbContext("ClaimTest");

            var newClaim = new Claim
            {
                ClaimId = "CL001",
                HoursWorked = 10,
                HourlyRate = 200,
                Total = 2000,
                Status = "Pending Verification"
            };

            db.Claims.Add(newClaim);
            db.SaveChanges();

            var claim = db.Claims.FirstOrDefault(c => c.ClaimId == "CL001");
            Assert.NotNull(claim);
            Assert.Equal(2000, claim.Total);
        }

        [Fact]
        public void CoordinatorCanApproveClaim()
        {
            var db = new TestDbContext("ApproveTest");

            var claim = new Claim
            {
                ClaimId = "CL002",
                HoursWorked = 8,
                HourlyRate = 150,
                Total = 1200,
                Status = "Pending Verification"
            };
            db.Claims.Add(claim);
            db.SaveChanges();

            // Coordinator approves
            claim.Status = "Approved by Coordinator";
            db.SaveChanges();

            var approvedClaim = db.Claims.FirstOrDefault(c => c.ClaimId == "CL002");
            Assert.Equal("Approved by Coordinator", approvedClaim.Status);
        }

        [Fact]
        public void ManagerCanRejectClaim()
        {
            var db = new TestDbContext("RejectTest");

            var claim = new Claim
            {
                ClaimId = "CL003",
                HoursWorked = 6,
                HourlyRate = 100,
                Total = 600,
                Status = "Pending Verification"
            };
            db.Claims.Add(claim);
            db.SaveChanges();

            // Manager rejects
            claim.Status = "Rejected by Manager";
            db.SaveChanges();

            var rejectedClaim = db.Claims.FirstOrDefault(c => c.ClaimId == "CL003");
            Assert.Equal("Rejected by Manager", rejectedClaim.Status);
        }
    }
}
