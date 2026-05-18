using CalculadoraCdb.Api.Data;
using CalculadoraCdb.Api.Entities;

namespace CalculadoraCdb.Api.Repository
{
    public class CalculoCdbRepository : ICalculoCdbRepository
    {
        private readonly AppDbContext _context;

        public CalculoCdbRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task SaveAsync(CalculoCdb calculo)
        {
            _context.CalculosCdb.Add(calculo);
            await _context.SaveChangesAsync();
        }
    }
}
