using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public interface IBaseRepository<T>
    {
        Task<List<T>> FindAll();
        Task<T> FindSingleByCondition(Expression<Func<T, bool>> expression);
        Task<bool> FindAnyByCondition(Expression<Func<T, bool>> expression);
        Task<List<T>> FindListByCondition(Expression<Func<T, bool>> expression);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }

    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected SolutionDatabaseContext RepositoryContext { get; set; }
        protected IMapper Mapper { get; set; }

        public BaseRepository(SolutionDatabaseContext repositoryContext, IMapper mapper)
        {
            this.RepositoryContext = repositoryContext;
            this.Mapper = mapper;
        }
        public virtual async Task<List<T>> FindAll()
        {
            return await this.RepositoryContext.Set<T>().AsNoTracking().ToListAsync();
        }

        public virtual async Task<T> FindSingleByCondition(Expression<Func<T, bool>> expression)
        {
            return await this.RepositoryContext.Set<T>().Where(expression).AsNoTracking().FirstOrDefaultAsync();
        }

        public virtual async Task<bool> FindAnyByCondition(Expression<Func<T, bool>> expression)
        {
            return await this.RepositoryContext.Set<T>().Where(expression).AnyAsync();
        }

        public virtual async Task<List<T>> FindListByCondition(Expression<Func<T, bool>> expression)
        {
            return await this.RepositoryContext.Set<T>().Where(expression).AsNoTracking().ToListAsync();
        }

        public void Create(T entity)
        {
            this.RepositoryContext.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            this.RepositoryContext.Set<T>().Update(entity);
        }

        public virtual void Delete(T entity)
        {
            this.RepositoryContext.Set<T>().Remove(entity);
        }
    }
}
