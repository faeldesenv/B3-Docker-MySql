using CalculadoraCdb.Api.Entities;
using CalculadoraCdb.Api.Interface;
using CalculadoraCdb.Api.Model;
using CalculadoraCdb.Api.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CalculadoraCdb.Api.Controllers
{
    [Tags("Cálculos do CDB")]
    [ApiController]
    [Route("api/[controller]")]
    public class CdbController : ControllerBase
    {
        private readonly ICalculadoraCdbService _calculadoraCdbService;
        private readonly ICalculoCdbRepository _repository;

        /// <summary>
        /// Initializes a new instance of <see cref="CdbController"/>.
        /// </summary>
        /// <param name="calculadoraCdbService">The CDB calculation service.</param>
        /// <param name="repository">The CDB calculation repository.</param>
        public CdbController(ICalculadoraCdbService calculadoraCdbService, ICalculoCdbRepository repository)
        {
            _calculadoraCdbService = calculadoraCdbService;
            _repository = repository;
        }

        /// <summary>
        /// Calculates gross and net CDB investment values.
        /// </summary>
        /// <param name="request">The calculation request with initial value and period.</param>
        /// <returns>Gross and net values of the investment.</returns>
        [HttpPost("calculate")]
        [ProducesResponseType(typeof(CalculaCdbResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Calculate([FromBody] CalculaCdbRequest request)
        {
            if (request.ValorInvestido <= 0)
            {
                ModelState.AddModelError(nameof(request.ValorInvestido), "O valor investido deve ser positivo.");
                return ValidationProblem();
            }

            if (request.Meses <= 1)
            {
                ModelState.AddModelError(nameof(request.Meses), "O prazo deve ser maior que 1 mês.");
                return ValidationProblem();
            }

            var result = _calculadoraCdbService.Calculate(request.ValorInvestido, request.Meses);

            await _repository.SaveAsync(new CalculoCdb
            {
                ValorInvestido = request.ValorInvestido!.Value,
                Meses = request.Meses!.Value,
                ValorBruto = result.ValorBruto,
                ValorLiquido = result.ValorLiquido,
                DataCalculo = DateTime.UtcNow
            });

            return Ok(result);
        }
    }
}
