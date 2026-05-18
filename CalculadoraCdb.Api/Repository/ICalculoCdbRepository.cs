using CalculadoraCdb.Api.Entities;

namespace CalculadoraCdb.Api.Repository
{
    public interface ICalculoCdbRepository
    {
        Task SaveAsync(CalculoCdb calculo);
    }
}
