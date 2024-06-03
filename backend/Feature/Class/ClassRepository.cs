using EduAdmin.Common;
using EduAdmin.Context;
using EduAdmin.Features.Class;

namespace EduAdmin.Feature.Class;

public class ClassRepository(AppDbContext context) : GenericRepository<ClassEntity>(context) {}