using EduAdmin.Common.Model;
using EduAdmin.Context;
using EduAdmin.Features.Class;

namespace EduAdmin.Feature.Class.Repository;

public class ClassRepository(AppDbContext context) : IClassRepository
{
    public ClassEntity Create(ClassEntity record) 
    {
        context.Classes.Add(record);
        context.SaveChanges();
        return record;
    }

    public bool Update(ClassEntity source) {
        context.Classes.Update(source);
        return context.SaveChanges() > 0;
    }

    public IEnumerable<ClassEntity> FindAll(PageRequest pageRequest) 
    {   
        Console.WriteLine(pageRequest.Size);
         return context.Classes
            .OrderBy(obj => obj.Id)
            .Skip((pageRequest.Page - 1) * pageRequest.Size)
            .Take(pageRequest.Size)
            .ToList();
    }

    public ClassEntity FindById(int id) 
    {
         return context.Classes
            .SingleOrDefault(obj => obj.Id == id)!;
    }
    
    public bool ExistsById(int id) => FindById(id) != null;

    public bool DeleteById(int id)
    {
        if (ExistsById(id))
        {
            context.Classes.Remove(FindById(id));
            return context.SaveChanges() > 0;
        }
        return false;
    }

    public IEnumerable<ClassEntity> Search(string term)
    {
        return context.Classes.Where(obj => obj.Name.Contains(term));
    }
}