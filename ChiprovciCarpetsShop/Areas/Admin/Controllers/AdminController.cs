using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using static ChiprovciCarpetsShop.Areas.Admin.AdminConstants;

namespace ChiprovciCarpetsShop.Areas.Admin.Controllers
{
    [Area(AreaName)]
    [Authorize(Roles = AdministratorRoleName)]
    public abstract class AdminController : Controller
    {
    }
}
