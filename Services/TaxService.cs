using Business;
using Microsoft.Extensions.Caching.Memory;

namespace Services;

public class TaxService : ITaxService
{
    private readonly IMemoryCache _memoryCache;

    public TaxService(IMemoryCache memoryCache) {
        _memoryCache = memoryCache;
    }

    public Taxes<string> Calculate(TaxPayer payer)
    {
        if (!_memoryCache.TryGetValue(payer.SSN, out Taxes<string> cacheValue))
        {
            var imaginaria = new Imaginaria(
                payer.GrossIncome.HasValue ? payer.GrossIncome.Value : decimal.Zero,
                payer.CharitySpent.HasValue ? payer.CharitySpent.Value : decimal.Zero
            );

            cacheValue = imaginaria.CalculateAndFormat();

            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromSeconds(3));

            _memoryCache.Set(payer.SSN, cacheValue, cacheEntryOptions);
        }

        return cacheValue;
    }
}
