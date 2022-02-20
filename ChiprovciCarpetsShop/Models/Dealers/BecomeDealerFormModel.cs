using System.ComponentModel.DataAnnotations;

using static ChiprovciCarpetsShop.Data.DataConstants.Dealer;

namespace ChiprovciCarpetsShop.Models.Dealers
{
    public class BecomeDealerFormModel
    {
        [Required]
        [StringLength(NameMaxLength,MinimumLength = NameMinLength)]
        public string Name { get; set; }

        [RegularExpression(PhoneRegex)]
        public string PhoneNumber { get; set; }
    }
}
