using Burls.Persistence.Browsers.Data;
using Burls.Persistence.Profiles.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Burls.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BurlsDbContext _context;
        private readonly IServiceProvider _serviceProvider;
        private IDbContextTransaction _currentTransaction;
        private bool _disposed;

        private IBrowserRepository _browserRepository;
        public IBrowserRepository BrowserRepository
        {
            get
            {
                return _browserRepository ??= _serviceProvider.GetService(typeof(IBrowserRepository)) as IBrowserRepository;
            }
        }

        private IProfileRepository _profileRepository;
        public IProfileRepository ProfileRepository
        {
            get
            {
                return _profileRepository ??= _serviceProvider.GetService(typeof(IProfileRepository)) as IProfileRepository;
            }
        }

        public UnitOfWork(BurlsDbContext context, IServiceProvider serviceProvider)
        {
            _context = context;
            _serviceProvider = serviceProvider;
        }

        public Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            _currentTransaction ??= await _context.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted, cancellationToken);
        }

        public async Task RetryOnExceptionAsync(Func<CancellationToken, Task> func, CancellationToken cancellationToken = default)
        {
            await _context.Database.CreateExecutionStrategy().ExecuteAsync(func, cancellationToken);
        }

        public async Task CommitTransactionAsync()
        {
            try
            {
                await SaveChangesAsync();
                _currentTransaction?.Commit();
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }
    }
}
