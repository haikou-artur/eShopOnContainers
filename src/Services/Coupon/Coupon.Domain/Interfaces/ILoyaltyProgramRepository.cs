using Coupon.Domain.Models;

namespace Coupon.Domain.Interfaces
{
    public interface ILoyaltyProgramRepository
    {
        Task<LoyaltyPoint> GetLoyaltyPoint(string userId);

        Task CreateLoyaltyPoint(LoyaltyPoint loyaltyPoint);

        Task UpdateLoyaltyPoint(LoyaltyPoint loyaltyPoint);
    }
}
