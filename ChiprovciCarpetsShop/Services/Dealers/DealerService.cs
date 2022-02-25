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

        public void SaveInDb(BecomeDealerFormModel dealer, string userId)
        {
            var dealerData = new Dealer
            {
                Name = dealer.Name,
                PhoneNumber = dealer.PhoneNumber,
                UserId = userId
            };

            this.data.Dealers.Add(dealerData);
            this.data.SaveChanges();
        }

        public bool UserIsAlreadyDealer(string userId)
           => this.data
                .Dealers
                .Any(d => d.UserId == userId);
    }
}
