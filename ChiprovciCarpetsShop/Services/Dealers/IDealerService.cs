using ChiprovciCarpetsShop.Models.Dealers;
using Microsoft.AspNetCore.Mvc;

namespace ChiprovciCarpetsShop.Services.Dealers
{
    public interface IDealerService
    {
        bool UserIsAlreadyDealer(string userId);

        void SaveInDb(BecomeDealerFormModel dealer, string userId);

        int GetId(string userId);
    }
}
