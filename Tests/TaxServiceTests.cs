using Xunit;
using Microsoft.Extensions.Caching.Memory;

using Services;
using Business;
using Moq;
using System;

namespace Tests;

public class TaxServiceTests
{
    [Fact]
    public void Should_Return_Net_With_No_Taxes()
    {
        var payer = new TaxPayer { GrossIncome = 900 };
        var imaginaria = new Imaginaria(
                payer.GrossIncome.HasValue ? payer.GrossIncome.Value : decimal.Zero,
                payer.CharitySpent.HasValue ? payer.CharitySpent.Value : decimal.Zero
            );

        var result = imaginaria.Calculate();

        Assert.Equal(result.NetIncome, payer.GrossIncome);
    }

    [Theory]
    [InlineData(3400, 0, 240, 300, 540, 2860)]
    [InlineData(2500, 150, 135, 202.5,  337.5, 2162.5)]
    [InlineData(3600, 360, 224, 300,  524, 3076)]
    public void Should_Return_Net_With_Income_Social_Taxes_And_Charity(
        decimal GrossIncome,
        decimal CharitySpent,
        decimal IncomeTax,
        decimal SocialTax,
        decimal TotalTax,
        decimal NetIncome
    )
    {
        var imaginaria = new Imaginaria(GrossIncome, CharitySpent);

        var result = imaginaria.Calculate();

        Assert.Equal(CharitySpent, result.CharitySpent);
        Assert.Equal(IncomeTax, result.IncomeTax);
        Assert.Equal(SocialTax, result.SocialTax);
        Assert.Equal(TotalTax, result.TotalTax);
        Assert.Equal(NetIncome, result.NetIncome);
    }
}