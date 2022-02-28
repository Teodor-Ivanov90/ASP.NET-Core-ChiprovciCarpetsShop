using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

using static ChiprovciCarpetsShop.Data.DataConstants.User;

namespace ChiprovciCarpetsShop.Data.Models
{
    public class User : IdentityUser
    {
        [MaxLength(FullNameMaxLength)]
        public string FullName { get; set; }
    }
}
