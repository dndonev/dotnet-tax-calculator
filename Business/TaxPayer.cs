using System.ComponentModel.DataAnnotations;

using Validations;

namespace Business;

public class TaxPayer
{
    [Required]
    [FullNameAttribute(ErrorMessage = "Fullname must contain at leats first and last name")]
    public string? FullName { get; set; }

    [Required]
    [RegularExpression("^\\d{4,10}$", ErrorMessage = "The number must be between 5 to 10 digits")]
    public long? SSN { get; set; }

    [Required]
    public decimal? GrossIncome { get; set; }

    public decimal? CharitySpent { get; set; }
}
