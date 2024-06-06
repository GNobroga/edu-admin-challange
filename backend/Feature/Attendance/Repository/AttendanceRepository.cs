using EduAdmin.Common.Model;
using EduAdmin.Context;
using EduAdmin.Features.Attendance;
using Microsoft.EntityFrameworkCore;

namespace EduAdmin.Feature.Attendance.Repository;

public class AttendanceRepository(AppDbContext context) : IAttendanceRepository {

    public AttendanceEntity Create(AttendanceEntity record) 
    {
        context.Attendances.Add(record);
        context.SaveChanges();
        return record;
    }

    public bool Update(AttendanceEntity source) {
        context.Attendances.Update(source);
        return context.SaveChanges() > 0;
    }

    public IEnumerable<AttendanceEntity> FindAll(PageRequest pageRequest) 
    {
         return context.Attendances
            .Include(obj => obj.Student)
            .Include(obj => obj.Subject)
            .OrderBy(obj => obj.Id)
            .Skip((pageRequest.Page - 1) * pageRequest.Size)
            .Take(pageRequest.Size)
            .ToList();
    }

    public AttendanceEntity FindById(int id) 
    {
        return context.Attendances
            .Include(obj => obj.Student)
            .Include(obj => obj.Subject)
            .FirstOrDefault(obj => obj.Id == id)!;
    }
    
    public bool ExistsById(int id) => FindById(id) != null;

    public bool DeleteById(int id)
    {   
        if (ExistsById(id))
        {
            context.Attendances.Remove(FindById(id));
            return context.SaveChanges() > 0;
        }
        return false;
    }

    public IEnumerable<AttendanceEntity> Search(string term)
    {
        term = term.Trim().ToLower();
       return context.Attendances
            .Include(obj => obj.Student)
            .Include(obj => obj.Subject)
            .Where(obj => 
                obj.Student!.Name.Trim().ToLower().Contains(term) ||
                obj.Student!.Email.Trim().ToLower().Contains(term) ||
                obj.Subject!.Name.Trim().ToLower().Contains(term) ||
                obj.Date.ToString().Contains(term) 
            ).ToList();
    }

    public (int, int) GetCountPresentByStudentId(int id)
    {
        var count =  context.Attendances
            .Include(obj => obj.Student)
            .Where(obj => obj.StudentId == id && obj.Present)
            .Count();

        var total =  context.Attendances
            .Include(obj => obj.Student)
            .Where(obj => obj.StudentId == id)
            .Count();

        return (count, total);
    }
}