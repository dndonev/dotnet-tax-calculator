using System.Globalization;

namespace Business;

public abstract class ACalculations : AJurisdiction, ITax {
    
    private decimal _income;
    private bool _isTaxable;
    private decimal _taxableIncome;
    private decimal _charitySpent;
    protected byte _trailingZeroes;
    protected ACalculations(decimal income, decimal charitySpent) {
        this._income = income;
        this._isTaxable = income > 1000;
        this._charitySpent = charitySpent;
        this._taxableIncome = TaxableIncomeCalculation();
    }

    public virtual Func<decimal> IncomeTaxCalculation  {
        get {
            return () => this._taxableIncome * IncomeTax;
        }
    }   
    public virtual Func<decimal> CharitySpentCalculation {
        get {
            return () => 
                Math.Max(Math.Min(CharitySpentTax * this._income, this._charitySpent), decimal.Zero);
        }
    }
    public virtual Func<decimal> TaxableIncomeCalculation {
        get {
            return () =>
                Math.Max((this._income - CharitySpentCalculation()) - Threshold, 0);
        }
    }
    public virtual Func<decimal> SocialContributionsTaxCalculation {
        get {
            return () => 
                Math.Min(this._taxableIncome * SocialContributionsTax, SocialContributionsTax * 2000m);
        }
    }

    private string FormatTax(decimal value) {

        NumberFormatInfo LocalFormat = (NumberFormatInfo)NumberFormatInfo.CurrentInfo.Clone();
           // Replace the default currency symbol with the local currency symbol.
        LocalFormat.CurrencySymbol = CurrencySymbol.ToString();

        return value.ToString("C", LocalFormat);
    }
    public virtual Taxes<decimal> Calculate()
    {
        var charitySpent = this.CharitySpentCalculation();
        var incomeTax = this.IncomeTaxCalculation();
        var socialTax = this.SocialContributionsTaxCalculation();
        var totalTax = incomeTax + socialTax;

        return new Taxes<decimal> {
            NetIncome = this._income - totalTax,
            IncomeTax = incomeTax,
            GrossIncome = this._income,
            CharitySpent = charitySpent,
            SocialTax = socialTax,
            TotalTax = totalTax
        };
    }

    public virtual Taxes<string> CalculateAndFormat()
    {
        var result = Calculate();

        return new Taxes<string> {
            NetIncome = FormatTax(result.NetIncome),
            IncomeTax = FormatTax(result.IncomeTax),
            GrossIncome = FormatTax(result.GrossIncome),
            CharitySpent = FormatTax(result.CharitySpent),
            SocialTax = FormatTax(result.SocialTax),
            TotalTax = FormatTax(result.TotalTax)
        };
    }
}