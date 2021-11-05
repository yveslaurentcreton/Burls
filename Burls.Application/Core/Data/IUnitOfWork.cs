using Burls.Application.Browsers.Data;
using Burls.Application.Profiles.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Burls.Application.Core.Data
{
    public interface IUnitOfWork : IDisposable
    {
        IBrowserRepository BrowserRepository { get; }
        IProfileRepository ProfileRepository { get; }

        Task BeginTransactionAsync(CancellationToken cancellationToken = default);
        Task CommitTransactionAsync();
        void RollbackTransaction();
        Task RetryOnExceptionAsync(Func<CancellationToken, Task> func, CancellationToken cancellationToken = default);
        Task SaveChangesAsync();
    }
}
