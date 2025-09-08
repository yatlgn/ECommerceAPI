using ECommerceAPI.Application.Interfaces.Repositories;
using ECommerceAPI.Application.Interfaces.UnitOfWorks;
using ECommerceAPI.Domain.Common;
using ECommerceAPI.Persistence.Context;
using ECommerceAPI.Persistence.Repositories;
using System.Threading.Tasks;

namespace ECommerceAPI.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IReadRepository<T> GetReadRepository<T>() where T : class, IEntityBase, new()
        {
            return new ReadRepository<T>(_context);
        }

        public IWriteRepository<T> GetWriteRepository<T>() where T : class, IEntityBase, new()
        {
            return new WriteRepository<T>(_context);
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }
    }
}
