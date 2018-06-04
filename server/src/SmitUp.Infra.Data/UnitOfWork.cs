using Microsoft.EntityFrameworkCore;
using SmitUp.Domain.Core.Commands;
using SmitUp.Domain.Core.Transaction;
using SmitUp.Infra.Data.Context;
using System.Threading.Tasks;

namespace SmitUp.Infra.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SmitUpContext _context;
        public UnitOfWork(SmitUpContext context)
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
