using EduAdmin.Common;
using EduAdmin.Context;
using EduAdmin.Features.Subject;

namespace EduAdmin.Feature.Subject;

public class SubjectRepository(AppDbContext context) : GenericRepository<SubjectEntity>(context) {}