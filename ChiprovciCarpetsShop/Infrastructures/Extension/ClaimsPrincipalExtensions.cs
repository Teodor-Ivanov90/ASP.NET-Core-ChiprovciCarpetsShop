using System.Security.Claims;

namespace ChiprovciCarpetsShop.Infrastructures.Extension
{
    public static class ClaimsPrincipalExtensions
    {
        public static string Id(this ClaimsPrincipal user) 
            => user.FindFirst(ClaimTypes.NameIdentifier).Value;
    }
}
