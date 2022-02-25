using ChiprovciCarpetsShop.Data;
using ChiprovciCarpetsShop.Data.Models;
using ChiprovciCarpetsShop.Models.Dealers;
using System.Linq;

namespace ChiprovciCarpetsShop.Services.Dealers
{
    public class DealerService : IDealerService
    {
        private readonly ChiprovciCarpetsDbContext data;

        public DealerService(ChiprovciCarpetsDbContext data) 
            => this.data = data;

        public int GetId(string userId)
            => this.data.Dealers
                .Where(d => d.UserId == userId)
                .Select(d => d.Id)
                .FirstOrDefault();

        public int Create(string name,string phoneNumber, string userId)
        {
            var dealerData = new Dealer
            {
                Name =name,
                PhoneNumber = phoneNumber,
                UserId = userId
            };

            this.data.Dealers.Add(dealerData);
            this.data.SaveChanges();

            return dealerData.Id;
        }

        public bool IsDealer(string userId)
           => this.data
                .Dealers
                .Any(d => d.UserId == userId);

        
    }
}
