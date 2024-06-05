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

}