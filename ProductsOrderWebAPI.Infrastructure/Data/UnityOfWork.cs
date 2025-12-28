using ProductsOrderWebAPI.Domain.Interfaces;

namespace ProductsOrderWebAPI.Infrastructure.Data
{
    public class UnityOfWork(AppDbContext appDbContext) : IUnityOfWork
    {
        private readonly AppDbContext _context = appDbContext;
        public async Task<int> CommitChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
