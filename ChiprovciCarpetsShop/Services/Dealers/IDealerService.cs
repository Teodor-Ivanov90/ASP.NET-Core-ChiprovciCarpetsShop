using ChiprovciCarpetsShop.Models.Dealers;
using Microsoft.AspNetCore.Mvc;

namespace ChiprovciCarpetsShop.Services.Dealers
{
    public interface IDealerService
    {
        bool IsDealer(string userId);

        int Create(string name, string phoneNumber, string userId);

        int GetId(string userId);
    }
}
