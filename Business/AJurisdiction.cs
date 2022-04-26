namespace Business;

public abstract class AJurisdiction {
    protected abstract decimal IncomeTax { get; }
    protected abstract string CurrencyCode { get; }
    protected abstract decimal CharitySpentTax { get; }
    protected abstract char CurrencySymbol { get; }
    protected abstract decimal Threshold { get; }
    protected abstract decimal SocialContributionsTax { get; }
}