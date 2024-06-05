using EduAdmin.Common.Model;
using EduAdmin.Context;
using EduAdmin.Features.Grade;
using Microsoft.EntityFrameworkCore;

namespace EduAdmin.Feature.Grade.Repository;

public class GradeRepository(AppDbContext context) : IGradeRepository 
{
    public GradeEntity Create(GradeEntity record) 
    {
        context.Grades.Add(record);
        context.SaveChanges();
        return record;
    }

    public bool Update(GradeEntity source) {
        context.Grades.Update(source);
        return context.SaveChanges() > 0;
    }

    public IEnumerable<GradeEntity> FindAll(PageRequest pageRequest) 
    {
         return context.Grades
            .Include(obj => obj.Student)
            .Include(obj => obj.Subject)
            .Skip((pageRequest.Page - 1) * pageRequest.Size)
            .Take(pageRequest.Size)
            .ToList();
    }

    public GradeEntity FindById(int id) 
    {
         return context.Grades
            .Include(obj => obj.Student)
            .Include(obj => obj.Subject)
            .SingleOrDefault(obj => obj.Id == id)!;
    }
    
    public bool ExistsById(int id) => FindById(id) != null;

    public bool DeleteById(int id)
    {
        if (ExistsById(id)) 
        {
            context.Grades.Remove(FindById(id));
            return context.SaveChanges() > 0;
        }
        return false;
    }

    public IEnumerable<GradeEntity> FindByStudentId(int id) => context.Grades.Include(grade => grade.Student).Include(grade => grade.Subject).Where(grade => grade.StudentId == id).ToList();
    
}