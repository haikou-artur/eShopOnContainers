using Coupon.Domain.Models;

namespace Coupon.Domain.Interfaces.Services
{
    public interface ILoyaltyProgramService
    {
        Task<LoyaltyPoint> GetLoyaltyPoint(string userId);

        Task ApplyLoyaltyProgram(string userId, decimal paymentAmount);

        Task UpdateLoyaltyPoint(LoyaltyPoint loyaltyPoint);
    }
}
