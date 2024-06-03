using EduAdmin.Common;
using EduAdmin.Context;
using EduAdmin.Features.Grade;

namespace EduAdmin.Feature.Grade;

public class GradeRepository(AppDbContext context) : GenericRepository<GradeEntity>(context) {}