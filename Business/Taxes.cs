namespace Business;
public class Taxes<T>
{
    public T? CharitySpent { get; set; }
    public T? GrossIncome { get; set; }
    public T? IncomeTax { get; set; }
    public T? SocialTax { get; set; }
    public T? NetIncome { get; set; }
    public T? TotalTax { get; set; }
}
