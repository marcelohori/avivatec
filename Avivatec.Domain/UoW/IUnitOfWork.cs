using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Avivatec.Domain.UoW
{
    public interface IUnitOfWork
    {
        Task<bool> CommitAsync();
    }
}
