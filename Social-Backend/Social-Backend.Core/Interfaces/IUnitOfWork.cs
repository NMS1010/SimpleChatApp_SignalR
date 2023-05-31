using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Backend.Core.Interfaces
{
    public interface IUnitOfWork<out TContext> where TContext : DbContext, new()
    {
        TContext Context { get; }

        Task CreateTransaction();

        Task Commit();

        Task Rollback();

        Task Save();
    }
}