using System.Linq.Expressions;
using AutoMapper;
using EduAdmin.Context;
using Microsoft.EntityFrameworkCore;

namespace EduAdmin.Common;

public abstract class GenericRepository<TEntity> (AppDbContext context) where TEntity: BaseEntity<int>
{   
    protected AppDbContext Context => context;
    protected DbSet<TEntity> _dataset = context.Set<TEntity>();

    public TEntity Create(TEntity record) 
    {
        _dataset.Add(record);
        Context.SaveChanges();
        return record;
    }

    public bool Update(TEntity source) {
        _dataset.Update(source);
        return context.SaveChanges() > 0;
    }

    public IEnumerable<TEntity> FindAll(PageRequest pageRequest, List<Expression<Func<TEntity, object>>>? navigationProperties = null) 
    {
         return BuildWithIncludes(_dataset, navigationProperties).Skip((pageRequest.Page - 1) * pageRequest.Size).Take(pageRequest.Size).ToList();
    }

    public TEntity? FindById(int id, List<Expression<Func<TEntity, object>>>? navigationProperties = null) => BuildWithIncludes(_dataset,  navigationProperties).FirstOrDefault(entity => entity.Id == id);
    
    public bool ExistsById(int id) => FindById(id) != null;

    public bool DeleteById(int id)
    {
        _dataset.Remove(FindById(id)!);
        return Context.SaveChanges() > 0;
    }

    public static IQueryable<TEntity> BuildWithIncludes(IQueryable<TEntity> query, List<Expression<Func<TEntity, object>>>? navigationProperties)
    {
        if (navigationProperties == null)
        {
            return query;
        }

        foreach (var navigationProperty in navigationProperties)
        {
            query = query.Include(navigationProperty);
        }

        return query;
    }
    
 
}