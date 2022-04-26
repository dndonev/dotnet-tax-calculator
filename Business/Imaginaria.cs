namespace Business;

public class Imaginaria : ACalculations
{
    public Imaginaria(decimal income, decimal charitySpent) : base(income, charitySpent)
    {
        SocialContributionsTaxCalculation();
    }

    protected override decimal IncomeTax => 0.10m;
    protected override decimal SocialContributionsTax =>  0.15m;
    protected override decimal CharitySpentTax =>  0.10m;
    protected override decimal Threshold =>  1000m;
    protected override string CurrencyCode => "IDR";
    protected override char CurrencySymbol => '¥';

    public override Func<decimal> SocialContributionsTaxCalculation {
        get {
            return () => decimal.Zero;
        }
    }
}
