namespace Business;

public interface ITax  {
    Taxes<decimal> Calculate();
    Taxes<string> CalculateAndFormat();
}