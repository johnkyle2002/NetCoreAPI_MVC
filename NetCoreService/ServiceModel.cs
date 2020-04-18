using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NetCoreInterface;
using NetCoreRepository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreService
{
    public class ServiceModel<TModel> : IServiceModel<TModel> where TModel : class
    {
        private readonly NetCoreDBContext _context;

        public ServiceModel(NetCoreDBContext context)
        {
            _context = context;
        }

        public DbSet<TModel> Entity
        {
            get { return _context.Set<TModel>(); }
        }
         
        public async Task<TModel> Get(int id)
        {
            return await Entity.FindAsync(id);
        }
        public async Task<IEnumerable<TModel>> GetAll()
        {
            return await Entity.ToListAsync();
        }


        public async Task Create(TModel employee)
        {
            Entity.Add(employee);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(TModel employee)
        {
            Entity.Remove(employee);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Exists(Expression<Func<TModel, bool>> whereExpression = null)
        {
            return await Entity.AnyAsync(whereExpression);
        }

        public async Task<bool> Update(TModel employee)
        {
            try
            {
                Entity.Update(employee).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
    }
}
