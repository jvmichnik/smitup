using Microsoft.EntityFrameworkCore;
using SmitUp.Domain.Core.Commands;
using SmitUp.Domain.Core.Transaction;
using System.Threading.Tasks;

namespace SmitUp.Infra.Data
{
    public abstract class UnitOfWork<T>: IUnitOfWork where T : DbContext
    {
        private readonly T _context;
        public UnitOfWork(T context)
        {
            _context = context;
        }
        public async Task<CommandResponse> Commit()
        {
            var result = await _context.SaveChangesAsync();
            return result > 0 ? CommandResponse.Ok : CommandResponse.Fail;
        }
    }
}
