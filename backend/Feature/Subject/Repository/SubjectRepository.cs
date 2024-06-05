using EduAdmin.Common.Model;
using EduAdmin.Context;
using EduAdmin.Features.Subject;
using Microsoft.EntityFrameworkCore;

namespace EduAdmin.Feature.Subject.Repository;

public class SubjectRepository(AppDbContext context) : ISubjectRepository 
{
    public SubjectEntity Create(SubjectEntity record) 
    {
        context.Subjects.Add(record);
        context.SaveChanges();
        return record;
    }

    public bool Update(SubjectEntity source) {
        context.Subjects.Update(source);
        return context.SaveChanges() > 0;
    }

    public IEnumerable<SubjectEntity> FindAll(PageRequest pageRequest) 
    {
         return context.Subjects
            .Include(obj => obj.Class)
            .Include(obj => obj.Teacher)
            .Skip((pageRequest.Page - 1) * pageRequest.Size)
            .Take(pageRequest.Size)
            .ToList();
    }

    public SubjectEntity FindById(int id) 
    {
         return context.Subjects
            .Include(obj => obj.Class)
            .Include(obj => obj.Teacher)
            .SingleOrDefault(obj => obj.Id == id)!;
    }
    
    public bool ExistsById(int id) => FindById(id) != null;

    public bool DeleteById(int id)
    {
        if (ExistsById(id)) 
        {
            context.Subjects.Remove(FindById(id));
            return context.SaveChanges() > 0;
        }
        return false;
    }

    public IEnumerable<SubjectEntity> Search(string term)
    {
        term = term.ToLower().Trim();

        return context.Subjects
            .Include(obj => obj.Class)
            .Include(obj => obj.Teacher)
            .Where(obj => 
                obj.Class!.Name.ToLower().Contains(term) ||
                obj.Teacher!.Name.ToLower().Contains(term) ||
                obj.Name.ToLower().Contains(term) 
            )
            .ToList();
    }

    public IEnumerable<SubjectEntity> FindByTeacherId(int id)
    {
        return context.Subjects
            .Include(obj => obj.Class)
            .Include(obj => obj.Teacher)
            .Where(obj => obj.TeacherId == id).ToList();
    }
}