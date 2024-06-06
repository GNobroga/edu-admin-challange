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
            .ThenInclude(subject => subject!.Teacher)
            .OrderBy(obj => obj.Id)
            .Skip((pageRequest.Page - 1) * pageRequest.Size)
            .Take(pageRequest.Size)
            .ToList();
    }

    public GradeEntity FindById(int id) 
    {
         return context.Grades
            .Include(obj => obj.Student)
            .Include(obj => obj.Subject)
            .ThenInclude(subject => subject!.Teacher)
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

    public IEnumerable<GradeEntity> FindByStudentId(int id) => 
        context.Grades
            .Include(grade => grade.Student)
            .Include(grade => grade.Subject)
            .ThenInclude(subject => subject!.Teacher)
            .Where(grade => grade.StudentId == id).ToList();

    public IEnumerable<GradeEntity> Search(string term)
    {
        term = term.ToLower().Trim();

        return context.Grades
            .Include(obj => obj.Student)
            .Include(obj => obj.Subject)
            .ThenInclude(subject => subject!.Teacher)
            .Where(obj =>
                 obj.Student!.Name.ToLower().Contains(term) ||
                 obj.Student!.Email.ToLower().Contains(term) ||
                 obj.Subject!.Teacher!.Email.ToLower().Contains(term) ||
                 obj.Subject!.Teacher!.Name.ToLower().Contains(term) ||
                 obj.Subject!.Name.ToLower().Contains(term) ||
                 obj.Value!.ToString().ToLower().Contains(term)
            );  
    }
}