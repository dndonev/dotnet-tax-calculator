using Microsoft.AspNetCore.Mvc;

using Services;
using Business;

namespace tasky.Controllers;

[ApiController]
[Route("[controller]")]
public class Calculator : ControllerBase
{
    private readonly ILogger<Calculator> _logger;
    private readonly ITaxService _service;
    public Calculator(
        ILogger<Calculator> logger,
        ITaxService service)
    {
        _service = service;
        _logger = logger;
    }

    [HttpPost]
    [Route("[action]")]
    public Taxes<string> Calculate([FromBody] TaxPayer payer) => _service.Calculate(payer);
}
