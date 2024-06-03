namespace EduAdmin.Common;

public abstract class BaseEntity<ID> 
{
    public ID? Id { get; set; }
}