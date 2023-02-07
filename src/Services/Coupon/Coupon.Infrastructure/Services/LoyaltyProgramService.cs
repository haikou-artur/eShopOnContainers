using Coupon.Domain.Interfaces;
using Coupon.Domain.Interfaces.Services;
using Coupon.Domain.Models;

namespace Coupon.Infrastructure.Services
{
    public class LoyaltyProgramService : ILoyaltyProgramService
    {
        private const decimal MoneyBackRatio = 0.03M;
        private readonly ILoyaltyProgramRepository _repository;

        public LoyaltyProgramService(ILoyaltyProgramRepository repository)
        {
            _repository = repository;
        }

        private decimal CalculatePoints(decimal paymentTotal)
        {
            return Math.Round(paymentTotal * MoneyBackRatio, 2);
        }

        public async Task ApplyLoyaltyProgram(string userId, decimal paymentAmount)
        {
            var calculatedPoints = CalculatePoints(paymentAmount);
            var points = await GetLoyaltyPoint(userId);
            
            if(points == null)
            {
                await CreateLoyaltyPoint(new LoyaltyPoint
                {
                    UserId = userId,
                    Balance = calculatedPoints
                });
            }
            else
            {
                points.Balance += calculatedPoints;
                await UpdateLoyaltyPoint(points);
            }
        }

        public async Task<LoyaltyPoint> GetLoyaltyPoint(string userId)
        {
            return await _repository.GetLoyaltyPoint(userId);
        }

        private async Task CreateLoyaltyPoint(LoyaltyPoint loyaltyPoint)
        {
            await _repository.CreateLoyaltyPoint(loyaltyPoint);
        }

        public async Task UpdateLoyaltyPoint(LoyaltyPoint loyaltyPoint)
        {
            await _repository.UpdateLoyaltyPoint(loyaltyPoint);
        }


    }
}
