using Business;

namespace Services;
public interface ITaxService
{
    public Taxes<string> Calculate(TaxPayer payer);
}
