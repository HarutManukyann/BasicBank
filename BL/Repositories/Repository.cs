using BL.Core;
using BL.Repositories.Interfaceis;
using FCBankBasicHelper.Models;

namespace BL.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class 
    {
        protected readonly FcbankBasicContext context;
        public Repository(FcbankBasicContext context)
        {
            this.context = context;
        }
        public TEntity Get(int id)
        {
            return context.Set<TEntity>().Find(id);
        }
        public IEnumerable<TEntity> GetAll()
        {
            return context.Set<TEntity>().ToList();
        }
        public void Add(TEntity entity)
        {
            using (var scope = TransactionBL.CreateTransactionScope())
            {
                try
                {
                    context.Set<TEntity>().AddAsync(entity);
                    context.SaveChanges();
                    scope.Complete();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        public void AddRange(IEnumerable<TEntity> entities)
        {
            using (var scope = TransactionBL.CreateTransactionScope())
            {
                try
                {
                    context.Set<TEntity>().AddRangeAsync(entities);
                    context.SaveChanges();
                    scope.Complete();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        public void Update(TEntity entity)
        {
            using (var scope = TransactionBL.CreateTransactionScope())
            {
                try
                {
                    context.Set<TEntity>().Update(entity);
                    context.SaveChanges();
                    scope.Complete();
                }
                catch (Exception)
                {
                    throw;
                }
            }
            
        }
        public void Remove(TEntity entity)
        {
            using (var scope = TransactionBL.CreateTransactionScope())
            {
                try
                {
                    context.Set<TEntity>().Remove(entity);
                    context.SaveChanges();
                    scope.Complete();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            using (var scope = TransactionBL.CreateTransactionScope())
            {
                try
                {
                    context.Set<TEntity>().RemoveRange(entities);
                    context.SaveChanges();
                    scope.Complete();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}
