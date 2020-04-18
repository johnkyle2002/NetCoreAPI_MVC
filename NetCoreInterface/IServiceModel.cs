using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NetCoreInterface
{
    public interface IServiceModel<TModel> where TModel : class
    {
        DbSet<TModel> Entity { get; }

        Task Create(TModel employee);
        Task Delete(TModel employee);
        Task<bool> Exists(Expression<Func<TModel, bool>> whereExpression = null);
        Task<TModel> Get(int id);
        Task<IEnumerable<TModel>> GetAll();
        Task<bool> Update(TModel employee);
    }
}