using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;

namespace Map.Core.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task CompleteAsync();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IDbContextTransaction BeginTransaction();

        /// <summary>
        /// 
        /// </summary>
        void CommitTransaction();

        /// <summary>
        /// 
        /// </summary>
        void RollBackTransaction();
    }
}
