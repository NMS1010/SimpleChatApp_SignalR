using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Social_Backend.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Backend.Infrastructure.Data
{
    public class UnitOfWork<TContext> : IUnitOfWork<TContext>, IDisposable where TContext : DbContext, new()
    {
        private bool _disposed;
        private string _errorMessage = string.Empty;
        private IDbContextTransaction _objTran;
        public TContext Context { get; }

        public UnitOfWork()
        {
            Context = new TContext();
        }

        public async Task Commit()
        {
            await _objTran.CommitAsync();
        }

        public async Task CreateTransaction()
        {
            _objTran = await Context.Database.BeginTransactionAsync();
        }

        public void Dispose()
        {
            Dispose();
            GC.SuppressFinalize(this);
            _disposed = true;
        }

        public async Task Rollback()
        {
            await _objTran.RollbackAsync();
            await _objTran.DisposeAsync();
        }

        public async Task Save()
        {
            try
            {
                await Context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception("Error while executing this operation");
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
                if (disposing)
                    Context.Dispose();
            _disposed = true;
        }
    }
}