using SmitUp.Domain.Core.Commands;
using System.Threading.Tasks;

namespace SmitUp.Domain.Core.Transaction
{
    public interface IUnitOfWork
    {
        Task<CommandResponse> Commit();
    }
}
