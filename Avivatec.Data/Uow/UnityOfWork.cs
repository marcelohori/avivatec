using Avivatec.Data.Context;
using Avivatec.Domain.UoW;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Avivatec.Data.Uow
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AvivatecDbContext _context;

        public UnitOfWork(AvivatecDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CommitAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

    }
}
